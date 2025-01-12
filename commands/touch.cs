using System;
using System.IO;

namespace commands.touch {
    public class Touch {
        public static void Execute(string[] args) {
            if (args.Length == 0) {
                Console.WriteLine("Usage: touch [filename]");
                return;
            }

            if (args.Length == 1) {
                string filePath = args[0];


                filePath = filePath.Trim('"');

                if (!File.Exists(filePath)) {
                    try {
                        File.Create(filePath).Dispose();
                        Console.WriteLine("Created file: " + filePath);
                    } catch (Exception ex) {
                        Console.WriteLine($"Error creating file: {ex.Message}");
                    }
                } else {
                    try {
                        File.SetLastWriteTime(filePath, DateTime.Now);
                        Console.WriteLine($"Modified timestamp of {filePath} updated to {DateTime.Now}");
                    } catch (Exception ex) {
                        Console.WriteLine($"Error updating file: {ex.Message}");
                    }
                }
            }
        }
    }
}
