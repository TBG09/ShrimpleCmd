using System;
using System.IO;

namespace CoreUtils.IOHandler {
    public class IO {
        public static string ReadFile(string filePath) {
            if (File.Exists(filePath)) {
                return File.ReadAllText(filePath);
            }
            throw new FileNotFoundException($"The file at '{filePath}' does not exist.");
        }

        public static string GetBaseName(string filePath) {
            if (File.Exists(filePath)) {
                return Path.GetFileNameWithoutExtension(filePath);
            }
            throw new FileNotFoundException($"The file at '{filePath}' does not exist.");
        }

        public static string GetFullName(string filePath) {
            if (File.Exists(filePath)) {
                return Path.GetFileName(filePath);
            }
            throw new FileNotFoundException($"The file at '{filePath}' does not exist.");
        }

        public static void CreateFile(string filePath, string content = "") {
            File.WriteAllText(filePath, content);
        }
        public static void WriteToFile(string filePath, string content) {
            File.WriteAllText(filePath, content);
        }


        public static void AppendToFile(string filePath, string content) {
            File.AppendAllText(filePath, content);
        }

        public static bool FileExists(string filePath) {
            return File.Exists(filePath);
        }

        public static void DeleteFile(string filePath) {
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            } else {
                throw new FileNotFoundException($"The file at '{filePath}' does not exist.");
            }
        }

        public static string GetDirectory(string filePath) {
            if (File.Exists(filePath)) {
                return Path.GetDirectoryName(filePath);
            }
            throw new FileNotFoundException($"The file at '{filePath}' does not exist.");
        }

        public static string GetExtension(string filePath) {
            if (File.Exists(filePath)) {
                return Path.GetExtension(filePath);
            }
            throw new FileNotFoundException($"The file at '{filePath}' does not exist.");
        }
    }
}
