using System;

namespace commands.exit
{
    public static class Exit
    {
        public static void Execute(string[] args)
        {
            modloader.Modloader.UnloadMods();
            Environment.Exit(0);
        }
    }
}
