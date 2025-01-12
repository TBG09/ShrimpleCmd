using System;
using ConfigMain;

namespace commands.exit
{
    public static class Exit
    {
        public static void Execute(string[] args)
        {
            modloader.Modloader.UnloadMods();
            Config.ReadConfig();
            Console.WriteLine(Config.ExitMessage);
            Environment.Exit(0);
        }
    }
}
