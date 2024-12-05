using System;
using PublicVars;

namespace commands.pwd
{
    public static class Pwd
    {
        public static void Execute(string[] args)
        {
            Console.WriteLine(PublicVariables.currentDirectory);
        }
    }
}
