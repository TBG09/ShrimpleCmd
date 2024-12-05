using System;
using System.IO;

namespace PublicVars
{
    public class PublicVariables
    {
        public static string VersionNum = "1.2.4";
        public static string osType = Environment.OSVersion.Platform.ToString().ToLower();
        public static string javaVer = Environment.Version.ToString();
        public static string deviceArch = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
        public static string osVersion = Environment.OSVersion.VersionString;
        public static bool runningOnTermux = IsRunningOnTermux();
        public static string SeperatorOSType = SeperatorType();
        public static string totalMemoryGB = (Environment.WorkingSet / (1024L * 1024L * 1024L)).ToString();
        public static string totalMemoryMB = (Environment.WorkingSet / (1024L * 1024L)).ToString();
        public static string totalMemoryKB = (Environment.WorkingSet / 1024L).ToString();
        public static string currentDirectory = Directory.GetCurrentDirectory();
        public static string LinuxDistro = GetLinuxDistro();
        public static string Username = Environment.UserName;
        public static string HomeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static string OSRootDir = RootType();

        public static string currentDirectoryName;

        static PublicVariables()
        {
            currentDirectoryName = Path.GetDirectoryName(currentDirectory);
        }

        private static bool IsRunningOnTermux()
        {
            string termuxPath = "/data/data/com.termux/files/home/.termux";
            return Directory.Exists(termuxPath);
        }


        private static string RootType()
        {
            if (osType.Contains("win"))
            {
                return "C:";
            }
            else
            {
                return "/";
            }
        }

        private static string SeperatorType()
        {
            if (osType.Contains("win"))
            {
                return "\\";
            }
            else
            {
                return "/";
            }
        }

        private static string GetLinuxDistro()
        {
            string[] filesToCheck = { "/etc/os-release", "/etc/lsb-release", "/etc/debian_version", "/etc/redhat-release" };

            foreach (string filePath in filesToCheck)
            {
                try
                {
                    foreach (var line in File.ReadLines(filePath))
                    {
                        if (line.StartsWith("NAME="))
                        {
                            return line.Split('=')[1].Replace("\"", "").Trim();
                        }
                    }
                }
                catch (IOException)
                {
                }
            }

            return "Unknown Linux Distro"; // Default value if no known distro was found
        }
    }
}
