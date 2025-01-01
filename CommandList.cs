using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CommandListClass
{
    public class CommandList
    {
        public Dictionary<string, Action<string[]>> commandMap;

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
            commandMap.Add("cd", commands.cd.Cd.Execute);
            commandMap.Add("rm", commands.rm.Rm.Execute);
            commandMap.Add("touch", commands.touch.Touch.Execute);
            commandMap.Add("cls", CoreUtils.CommandPromptHandler.CommandPromptTools.ClearConsole);
            commandMap.Add("clear", CoreUtils.CommandPromptHandler.CommandPromptTools.ClearConsole);
            commandMap.Add("beep", CoreUtils.CommandPromptHandler.CommandPromptTools.Beep);
            commandMap.Add("shrimple", commands.ShrimpleCommand.Shrimple.Execute);
            commandMap.Add("constitle", commands.WinTitle.ConsTitle.Execute);
            commandMap.Add("pkill", commands.pkill.Pkill.Execute);
            commandMap.Add("crypt", commands.cryptography.Cryptography.Execute);
            commandMap.Add("cryptography", commands.cryptography.Cryptography.Execute);
            commandMap.Add("info", commands.info.Info.Execute);
            commandMap.Add("assemblyview", commands.assemblyView.AssemblyView.Execute);
            commandMap.Add("asmview", commands.assemblyView.AssemblyView.Execute);
            commandMap.Add("credits", commands.credits.Credits.Execute);
            commandMap.Add("csexec", commands.csExec.CsExec.Execute);
        }

        
        public static void AddCommand(string commandName, Action<string[]> commandAction)
        {
            if (!string.IsNullOrEmpty(commandName) && commandAction != null)
            {
                var commandList = new CommandList();
                commandList.commandMap[commandName.ToLower()] = commandAction;
            }
            else
            {
                Console.WriteLine("Invalid command or action.");
            }
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
