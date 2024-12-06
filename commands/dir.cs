using System;
using System.IO;
using System.Linq;
using PublicVars;

namespace commands.dir
{
    public class Dir
    {
        public static void Execute(string[] args)
        {
            string directoryPath = PublicVariables.currentDirectory;
            bool showHidden = false;
            bool showSystem = false;
            bool showFileSize = false;
            string fileSizeUnit = null;
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "-h":
                        showHidden = true;
                        break;
                    case "-s":
                        showSystem = true;
                        break;
                    case "-fs":
                        showFileSize = true;
                        break;
                    case "-fst":
                        if (i + 1 < args.Length)
                        {
                            fileSizeUnit = args[i + 1].ToUpper();
                            i++;  // Skip next argument since it's the unit
                        }
                        break;
                    case "-d":
                        if (i + 1 < args.Length)
                        {
                            directoryPath = args[i + 1];
                            i++;  // Skip next argument since it's the path
                        }
                        break;
                    default:
                        Console.WriteLine($"Unknown argument: {args[i]}");
                        return;
                }
            }
            if (!Path.IsPathRooted(directoryPath))
                directoryPath = Path.Combine(PublicVariables.currentDirectory, directoryPath);

            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Error: Directory '{directoryPath}' does not exist.");
                return;
            }
            var dirContent = Directory.GetFileSystemEntries(directoryPath)
                .OrderBy(entry => (File.GetAttributes(entry) & FileAttributes.Directory) == 0) // Sort directories before files
                .ToArray();

            Console.WriteLine($"Objects in directory {directoryPath}:");

            foreach (string entry in dirContent)
            {
                try
                {
                    FileAttributes attributes = File.GetAttributes(entry);
                    string entryName = Path.GetFileName(entry);
                    if (!showHidden && (attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                        continue;
                    if (!showSystem && (attributes & FileAttributes.System) == FileAttributes.System)
                        continue;
                    if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        Console.WriteLine("[Directory] " + entryName);
                    }
                    else
                    {
                        string sizeInfo = "";
                        if (showFileSize)
                        {
                            long fileSize = new FileInfo(entry).Length;
                            sizeInfo = FormatFileSize(fileSize, fileSizeUnit);
                        }

                        Console.WriteLine(entryName + sizeInfo);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine($"[Access Denied] {Path.GetFileName(entry)}");
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"[Not Found] {Path.GetFileName(entry)}");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"[Error] {Path.GetFileName(entry)}: {ex.Message}");
                }
            }
        }
        static string FormatFileSize(long bytes, string unit)
        {
            if (string.IsNullOrEmpty(unit)) return $" ({bytes} B)";

            double size = bytes;
            switch (unit)
            {
                case "KB":
                    size = bytes / 1024.0;
                    break;
                case "MB":
                    size = bytes / (1024.0 * 1024);
                    break;
                case "GB":
                    size = bytes / (1024.0 * 1024 * 1024);
                    break;
                default:
                    return $" ({bytes} B)"; // Default to bytes if unit is unknown
            }
            return $" ({size:F2} {unit})";
        }
    }
}
