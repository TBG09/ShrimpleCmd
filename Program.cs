using System;
using System.Linq;
using System.Reflection;
using CommandListClass;
using PublicVars;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
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

                commandList.ExecuteCommand(userInput);

                if (userInput.ToLower() == "exit")
                {
                    break;
                }
            }
        }

        static void ShowStartupText()
        {
            Console.WriteLine("*********************************");
            Console.WriteLine("Welcome to Shrimple Cmd");
            Console.WriteLine("Type 'help' for a list of commands");
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
    }
}
