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
            if (path.Contains("*")) {
                RemoveFilesByPattern(path);
            }
            else {
                RemoveFile(path);
            }
        }
        private static void RemoveFilesByPattern(string pattern) {
            string directory = Path.GetDirectoryName(pattern);
            string filePattern = Path.GetFileName(pattern);
            if (string.IsNullOrEmpty(directory)) {
                directory = Directory.GetCurrentDirectory();
            }
            string[] files = Directory.GetFiles(directory, filePattern);

            if (files.Length == 0) {
                Console.WriteLine($"No files matching '{filePattern}' were found.");
                return;
            }
            foreach (var file in files) {
                try {
                    File.Delete(file);
                    Console.WriteLine($"Deleted file: {file}");
                }
                catch (Exception ex) {
                    Console.WriteLine($"Error deleting file {file}: {ex.Message}");
                }
            }
        }
        private static void RemoveFile(string filePath) {
            if (!File.Exists(filePath)) {
                Console.WriteLine($"Error: The file '{filePath}' does not exist.");
                return;
            }

            try {
                File.Delete(filePath);
                Console.WriteLine($"Deleted file: {filePath}");
            }
            catch (Exception ex) {
                Console.WriteLine($"Error deleting file {filePath}: {ex.Message}");
            }
        }
    }
}
