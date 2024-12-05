using System;
using System.Collections.Generic;

namespace commands.help
{
    public static class Help
    {

        private static readonly Dictionary<string, string> commandHelpMessages = new Dictionary<string, string>
        {
            { "help", "help\n  Displays the list of commands or detailed information about a specific command.\n  Usage: help [command]" },
            { "echo", "echo [message]\n  Echoes the provided message.\n  Usage: echo Hello, World!" },
            { "dir", "dir \n  Lists the contents of the current directory. Can also be used as ls.\n  Usage: dir" },
            { "pwd", "pwd\n  Displays the current directory path.\n  Usage: pwd" },
            { "exit", "exit\n  Exits the application.\n  Usage: exit" },
            { "cd", "cd\n Changes directory to the provided directory.\n Usage: cd [directory]" },
            { "rm", "rm [file]\n  Removes the specified file or directory. Can also use wildcards like *.zip.\n  Usage: rm filename or rm *.zip" }
        };
        public static void Execute(string[] args)
        {
            if (args.Length == 0)
            {

                Console.WriteLine(@"
Available commands:

  help                     Show the list of commands
  echo [message]           Echo the provided message
  dir                      List the contents of the current directory
  pwd                      Show the current directory path
  exit                     Exit the application
  cd                       Change the directory
  rm                       Remove the specified file or directory

Use 'help [command]' for detailed information on a specific command.
");
            }
            else
            {
                string command = args[0].ToLower();
                if (commandHelpMessages.ContainsKey(command))
                {
                    Console.WriteLine(commandHelpMessages[command]);
                }
                else
                {
                    Console.WriteLine($"No detailed help available for '{command}'.");
                }
            }
        }
    }
}
