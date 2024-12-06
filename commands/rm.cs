using System;
using System.IO;
using System.Linq;

namespace commands.rm {
    public class Rm {
        public static void Execute(string[] args) {
            if (args.Length == 0) {
                Console.WriteLine("Error: No file or directory specified.");
                return;
            }

            string path = args[0];
            bool recursive = args.Contains("-r");
            bool force = args.Contains("-f");
            bool directory = args.Contains("-d");

            if (path.Contains("*")) {
                RemoveFilesByPattern(path, recursive, force, directory);
            }
            else {
                Remove(path, recursive, force, directory);
            }
        }

        private static void RemoveFilesByPattern(string pattern, bool recursive, bool force, bool directory) {
            string directoryPath = Path.GetDirectoryName(pattern);
            string filePattern = Path.GetFileName(pattern);
            if (string.IsNullOrEmpty(directoryPath)) {
                directoryPath = Directory.GetCurrentDirectory();
            }
            string[] files = Directory.GetFiles(directoryPath, filePattern);

            if (files.Length == 0) {
                Console.WriteLine($"No files matching '{filePattern}' were found.");
                return;
            }
            foreach (var file in files) {
                try {
                    if (force) {
                        File.SetAttributes(file, FileAttributes.Normal);
                    }
                    File.Delete(file);
                    Console.WriteLine($"Deleted file: {file}");
                }
                catch (Exception ex) {
                    Console.WriteLine($"Error deleting file {file}: {ex.Message}");
                }
            }
        }

        private static void Remove(string path, bool recursive, bool force, bool directory) {
            if (directory) {
                if (Directory.Exists(path)) {
                    try {
                        if (recursive) {
                            foreach (var subDir in Directory.GetDirectories(path)) {
                                Remove(subDir, recursive, force, directory);
                            }
                            foreach (var file in Directory.GetFiles(path)) {
                                RemoveFile(file, force);
                            }
                        }
                        Directory.Delete(path);
                        Console.WriteLine($"Deleted directory: {path}");
                    }
                    catch (Exception ex) {
                        Console.WriteLine($"Error deleting directory {path}: {ex.Message}");
                    }
                } else {
                    Console.WriteLine($"Error: The directory '{path}' does not exist.");
                }
            } else {
                RemoveFile(path, force);
            }
        }

        private static void RemoveFile(string filePath, bool force) {
            if (!File.Exists(filePath)) {
                Console.WriteLine($"Error: The file '{filePath}' does not exist.");
                return;
            }

            try {
                if (force) {
                    File.SetAttributes(filePath, FileAttributes.Normal);
                }
                File.Delete(filePath);
                Console.WriteLine($"Deleted file: {filePath}");
            }
            catch (Exception ex) {
                Console.WriteLine($"Error deleting file {filePath}: {ex.Message}");
            }
        }
    }
}
