using Newtonsoft.Json;
using System;
using System.IO;

namespace ConfigMain {
    public class Config {

        public static string FileExist = null;
        public static string ExitMessage = null;
        public static string StartMessage = null;

        public static void MainC() {
            if (CoreUtils.IOHandler.IO.FileExists("ShrimpleConfig.json")) {
                Console.WriteLine("Config file exists. Reading config...");
                ReadConfig();
            } 
            else {
                ResetConfig();
                Console.WriteLine("Config created with default JSON.");
            }
        }

        public static void ReadConfig() {
            string filePath = "ShrimpleConfig.json";
            if (CoreUtils.IOHandler.IO.FileExists(filePath)) {
                string jsonContent = File.ReadAllText(filePath);
                try {
                    var configData = JsonConvert.DeserializeObject<ConfigData>(jsonContent);

                    ExitMessage = configData.ExitMessage;
                    StartMessage = configData.StartMessage;

                    Console.WriteLine("Config loaded successfully.");
                } 
                catch (Exception ex) {
                    Console.WriteLine("Failed to read config: " + ex.Message);
                }
            } else {
                Console.WriteLine("Config file not found.");
            }
        }
        public static void ResetConfig() {
            var configData = new ConfigData {
                ExitMessage = "",
                StartMessage = ""
            };

            string jsonContent = JsonConvert.SerializeObject(configData, Formatting.Indented);
            CoreUtils.IOHandler.IO.CreateFile("ShrimpleConfig.json", jsonContent);

            ExitMessage = configData.ExitMessage;
            StartMessage = configData.StartMessage;

            Console.WriteLine("Config has been reset to default values.");
        }
        private class ConfigData {
            public string ExitMessage { get; set; }
            public string StartMessage { get; set; }
        }
    }
}
