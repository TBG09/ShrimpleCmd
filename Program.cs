using System;
using System.Collections.Generic;
using CommandListClass;
using System.IO;
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
            Console.WriteLine("*********************************");
            Console.WriteLine();
        }

        static string GetUserInput()
        {
            Console.Write(PublicVariables.currentDirectory + "> ");
            string input = Console.ReadLine();

           if (string.IsNullOrEmpty(input))
            {

            return GetUserInput(); 
            }

            return input;
        }

    }
}
