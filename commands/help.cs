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
            { "dir", @"dir [-h] [-s] [-fs] [-fst <unit>] -d <directory>
  Lists the contents of the specified directory, or the current directory if none is specified.

  Options:
    -h             Show hidden files.
    -s             Show system files.
    -fs            Display file sizes.
    -fst <unit>    Specify the unit for file sizes (B, KB, MB, GB). Requires -fs.
    -d <directory> Specify the directory to list. Required to avoid ambiguity with options.

  Usage Examples:
    dir                      List the contents of the current directory.
    dir -d ""C:\Projects""     List the contents of 'C:\Projects'.
    dir -fs -fst KB -d ""C:\\Projects""   List contents of 'C:\Projects' and show file sizes in KB.
    dir -h -s -d ""C:\HiddenFolder""     List hidden and system files in 'C:\HiddenFolder'." },
            { "pwd", "pwd\n  Displays the current directory path.\n  Usage: pwd" },
            { "exit", "exit\n  Exits the application.\n  Usage: exit" },
            { "cd", "cd [directory]\n  Changes the current directory to the specified directory.\n  Usage: cd [directory]" },
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
  dir                      List the contents of the current directory or specified directory
  pwd                      Show the current directory path
  exit                     Exit the application
  cd [directory]           Change the directory
  rm [file]                Remove the specified file or directory

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
