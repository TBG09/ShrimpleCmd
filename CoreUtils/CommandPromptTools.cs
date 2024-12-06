using System.Diagnostics;
using PublicVars;

namespace CoreUtils.CommandPromptHandler {
    public class CommandPromptTools {
        public static void ClearConsole(string[] args) {
            Console.Clear();
        }

        public static void Beep(string[] args) {
            if (args.Length >= 2) {
                if (int.TryParse(args[0], out int frequency) && int.TryParse(args[1], out int duration)) {
                    if (frequency >= 37 && frequency <= 32767) {
                        if (PublicVariables.osType.Contains("win")) {
                            Console.Beep(frequency, duration);
                        } else {
                            try {
                                RunBeepCommand(frequency, duration);
                            } catch {
                                Console.WriteLine("Beep command is not installed. Please install beep to use this functionality on Linux.");
                            }
                        }
                    } else {
                        Console.WriteLine("Invalid frequency. Valid range is 37 to 32767.");
                    }
                } else {
                    Console.WriteLine("Invalid arguments. Please provide valid integers for frequency and duration.");
                }
            } else {
                Console.WriteLine("Usage: Beep <frequency> <duration in milliseconds>");
            }
        }
         public static void Beep2(int frequency, int duration) {

         if (frequency >= 37 && frequency <= 32767) {
            Console.Beep(frequency, duration);
         } else {
            Console.WriteLine("Invalid frequency. Valid range is 37 to 32767.");
         }
        }
    

        private static void RunBeepCommand(int frequency, int duration) {
            var process = new ProcessStartInfo {
                FileName = "beep",
                Arguments = $"-f {frequency} -l {duration}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var beepProcess = Process.Start(process);
            if (beepProcess == null || beepProcess.HasExited) {
                throw new InvalidOperationException("Failed to run beep. It may not be installed.");
            }

            beepProcess.WaitForExit();
        }

        public static void ExecuteFile(string[] args) {
            if (args.Length == 0) {
                Console.WriteLine("Error: No command specified.");
                return;
            }

            string command = args[0];
            string arguments = args.Length > 1 ? string.Join(" ", args.Skip(1)) : string.Empty;

            var processStartInfo = new ProcessStartInfo {
                FileName = command,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try {
                var process = Process.Start(processStartInfo);
                if (process != null) {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (!string.IsNullOrEmpty(output)) {
                        Console.WriteLine(output);
                    }

                    if (!string.IsNullOrEmpty(error)) {
                        Console.WriteLine("Error: " + error);
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine($"Error executing command: {ex.Message}");
            }
        }
    }
}
