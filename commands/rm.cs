using System;
using System.IO;
using System.Linq;

namespace commands.rm {
    public class Rm {

        // Method to execute the rm command
        public static void Execute(string[] args) {
            if (args.Length == 0) {
                Console.WriteLine("Error: No file or directory specified.");
                return;
            }

            string path = args[0];

            // If the path is a wildcard like *.zip, we process it accordingly
            if (path.Contains("*")) {
                RemoveFilesByPattern(path);
            }
            else {
                // Handle absolute or relative file path
                RemoveFile(path);
            }
        }

        // Method to remove files by wildcard pattern (like *.zip)
        private static void RemoveFilesByPattern(string pattern) {
            // Get the directory and file extension pattern
            string directory = Path.GetDirectoryName(pattern);
            string filePattern = Path.GetFileName(pattern);

            // If the pattern does not include a directory, we use the current directory
            if (string.IsNullOrEmpty(directory)) {
                directory = Directory.GetCurrentDirectory();
            }

            // Get files matching the pattern
            string[] files = Directory.GetFiles(directory, filePattern);

            if (files.Length == 0) {
                Console.WriteLine($"No files matching '{filePattern}' were found.");
                return;
            }

            // Delete the matching files
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

        // Method to remove a specific file
        private static void RemoveFile(string filePath) {
            // Check if the file exists
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
