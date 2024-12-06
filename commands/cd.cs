using System;
using System.IO;
using PublicVars;

namespace commands.cd {
    public class Cd {
        public static void Execute(string[] args) {
            if (args.Length == 0) {
                Console.WriteLine("No directory provided.");
                return;
            }

            string directoryPath = args[0];

            try {
                if (directoryPath == ".") {
                    Console.WriteLine("Already in the current directory.");
                    return;
                }

                if (directoryPath == "..") {
                    string parentDirectory = Directory.GetParent(Environment.CurrentDirectory)?.FullName;
                    if (parentDirectory != null) {
                        Environment.CurrentDirectory = parentDirectory;
                        PublicVariables.currentDirectory = parentDirectory;
                    } else {
                        Console.WriteLine("Already at the root directory.");
                    }
                    return;
                }

                if (Path.IsPathRooted(directoryPath)) {
                    Environment.CurrentDirectory = directoryPath;
                    PublicVariables.currentDirectory = directoryPath;
                    Console.WriteLine($"Changed directory to: {directoryPath}");
                    return;
                }

                string newDirectory = Path.Combine(Environment.CurrentDirectory, directoryPath);

                if (Directory.Exists(newDirectory)) {
                    Environment.CurrentDirectory = newDirectory;
                    PublicVariables.currentDirectory = newDirectory;
                    Console.WriteLine($"Changed directory to: {newDirectory}");
                } else {
                    Console.WriteLine($"Error: Directory '{newDirectory}' does not exist.");
                }
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
