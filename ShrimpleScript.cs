using System;
using System.IO;
using CommandListClass;

namespace ShrimpleLangauge {
    public class ShrimpleLang {
        public static void ExecuteCommandsFromFileCuzYes(string filePath) {
            if (File.Exists(filePath)) {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines) {
                    string command = line.Trim();
                    if (!string.IsNullOrEmpty(command)) {
                        CommandList commandList = new CommandList();
                        commandList.ExecuteCommand(command);
                    }
                }
            } else {
                Console.WriteLine("File does not exist at the specified path.");
            }
        }

        public static void Main(string filePath) {
            ExecuteCommandsFromFileCuzYes(filePath);
        }
    }

}
