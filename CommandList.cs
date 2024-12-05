using System;
using System.Collections.Generic;

namespace CommandListClass
{
    public class CommandList
    {
        private Dictionary<string, Action<string[]>> commandMap;

        public CommandList()
        {
            commandMap = new Dictionary<string, Action<string[]>>();
            RegisterCommands();
        }
        private void RegisterCommands()
        {
            commandMap.Add("help", commands.help.Help.Execute);
            commandMap.Add("echo", commands.echo.Echo.Execute);
            commandMap.Add("dir", commands.dir.Dir.Execute);
            commandMap.Add("ls", commands.dir.Dir.Execute);
            commandMap.Add("pwd", commands.pwd.Pwd.Execute);
            commandMap.Add("exit", commands.exit.Exit.Execute);

        }
        public void ExecuteCommand(string input)
        {
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0 && commandMap.ContainsKey(parts[0].ToLower()))
            {
                commandMap[parts[0].ToLower()](parts.Length > 1 ? parts[1..] : new string[] { });
            }
            else
            {
                Console.WriteLine($"Unknown command: {parts[0]}");
            }
        }
    }
}
