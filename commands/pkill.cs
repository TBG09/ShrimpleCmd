using System;
using System.Diagnostics;

namespace commands.pkill {
    public static class Pkill {
        public static void Execute(string[] args) {
            if (args.Length > 0) {
                if (args[0] == "-p" && args.Length > 1) {
                    if (int.TryParse(args[1], out int pid)) {
                        try {
                            var process = Process.GetProcessById(pid);
                            process.Kill();
                            Console.WriteLine($"Killed process: {process.ProcessName} (ID: {process.Id})");
                        } catch (Exception ex) {
                            Console.WriteLine($"Error while killing process with PID {pid}: {ex.Message}");
                        }
                    } else {
                        Console.WriteLine("Invalid PID.");
                    }
                } else {
                    string inputName = args[0].ToLowerInvariant();
                    if (inputName.EndsWith(".exe")) {
                        inputName = inputName.Substring(0, inputName.Length - 4);
                    }

                    try {
                        bool found = false;
                        foreach (var process in Process.GetProcesses()) {
                            string processName = process.ProcessName.ToLowerInvariant();
                            if (processName == inputName) {
                                process.Kill();
                                found = true;
                                Console.WriteLine($"Killed process: {process.ProcessName} (ID: {process.Id})");
                            }
                        }

                        if (!found) {
                            Console.WriteLine($"No processes found matching: {args[0]}");
                        }
                    } catch (Exception ex) {
                        Console.WriteLine($"Error while killing processes: {ex.Message}");
                    }
                }
            } else {
                Console.WriteLine("Expected process name or -p flag with PID, instead got nothing.");
            }
        }
    }
}
