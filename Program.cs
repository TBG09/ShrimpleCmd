using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CommandListClass;
using PublicVars;
using System.Diagnostics;
using System.Reflection;
using ConfigHandler;

namespace ConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            ConfigLoader configLoader = new ConfigLoader();
            ShowStartupText();
            CommandList commandList = new CommandList();
            

            while (true)
            {
                string userInput = GetUserInput();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    continue;
                }

                userInput = ReplaceInternalVariables(userInput);

                var expandedCommands = ExpandRangeSyntax(userInput);

                foreach (var command in expandedCommands)
                {
                    if (command.StartsWith("#") && command.EndsWith("#") && !command.Contains("\""))
                    {
                        ExecuteFile(command.Substring(1, command.Length - 2));
                    }
                    else
                    {
                        commandList.ExecuteCommand(command);
                    }
                }

                if (userInput.ToLower() == "exit")
                {
                    break;
                }
            }
        }

        static void ShowStartupText()
        {
            Console.WriteLine("*********************************");
            Console.WriteLine("Welcome to Shrimple Cmd " + PublicVariables.VersionNum);
            Console.WriteLine("Type 'help' for a list of commands");
            Console.WriteLine("Type 'Shrimple' for a small documentation on some features.");
            Console.WriteLine("Beware: Still in development, expect bugs and incomplete things!");
            Console.WriteLine("*********************************");
            Console.WriteLine();
        }

        static string GetUserInput()
        {
            Console.Write(PublicVariables.currentDirectory + "> ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                return GetUserInput();
            }

            return input;
        }
        static string ReplaceInternalVariables(string userInput)
        {
            int startIndex = 0;
            while ((startIndex = userInput.IndexOf("$$", startIndex)) != -1)
            {
                int endIndex = userInput.IndexOf("$$", startIndex + 2);
                if (endIndex == -1)
                {
                    break; 
                }
                string variable = userInput.Substring(startIndex + 2, endIndex - startIndex - 2);
                var field = typeof(PublicVariables).GetField(variable, BindingFlags.Public | BindingFlags.Static);
                var property = typeof(PublicVariables).GetProperty(variable, BindingFlags.Public | BindingFlags.Static);

                if (field != null)
                {
                    userInput = userInput.Replace("$$" + variable + "$$", field.GetValue(null)?.ToString() ?? "");
                }
                else if (property != null)
                {
                    userInput = userInput.Replace("$$" + variable + "$$", property.GetValue(null)?.ToString() ?? "");
                }
                else
                {
                    Console.WriteLine($"Internal Variable '{variable}' does not exist.");
                }

                startIndex = endIndex + 2;
            }

            return userInput;
        }

        static string[] ExpandRangeSyntax(string userInput)
        {
            string pattern = @"\{(\d+)\.\.(\d+)\}";
            Match match = Regex.Match(userInput, pattern);

            if (!match.Success)
            {
                return new[] { userInput };
            }

            int start = int.Parse(match.Groups[1].Value);
            int end = int.Parse(match.Groups[2].Value);
            string prefix = userInput.Substring(0, match.Index);
            string suffix = userInput.Substring(match.Index + match.Length);

            return Enumerable.Range(start, end - start + 1)
                             .Select(i => prefix + i + suffix)
                             .ToArray();
        }

static void ExecuteFile(string command)
{
    try
    {
        var process = new ProcessStartInfo
        {
            FileName = "cmd",
            Arguments = $"/c {command}",
            RedirectStandardOutput = true,  // Redirect the standard output
            RedirectStandardError = true,   // Redirect the error stream as well
            UseShellExecute = false,
            CreateNoWindow = true
        };

        var processExecution = Process.Start(process);

        if (processExecution != null)
        {
            string output = processExecution.StandardOutput.ReadToEnd();
            string error = processExecution.StandardError.ReadToEnd();


            if (!string.IsNullOrEmpty(output))
            {
                Console.ForegroundColor = ConsoleColor.Cyan; 
                Console.WriteLine(output);
                Console.ResetColor(); 
            }
            if (!string.IsNullOrEmpty(error))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error);
                Console.ResetColor(); 
            }

            processExecution.WaitForExit(); 
        }
        else
        {
            Console.WriteLine("Failed to execute command.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error executing command: {ex.Message}");
    }
}

    }
}
