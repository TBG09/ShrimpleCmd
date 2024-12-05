using System;

namespace commands.exit
{
    public static class Exit
    {
        public static void Execute(string[] args)
        {
            Environment.Exit(0);
        }
    }
}
