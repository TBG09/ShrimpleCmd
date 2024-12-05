using System;

namespace commands.echo
{
    public static class Echo
    {
        public static void Execute(string[] args)
        {
            if (args.Length > 0)
            {
                string message = string.Join(" ", args);
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("No message provided to echo.");
            }
        }
    }
}
