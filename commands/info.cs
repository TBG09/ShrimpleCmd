using System.Runtime.InteropServices;

namespace commands.info {
    public class Info {
        public static void Execute(String[] args) {
            Console.WriteLine(@"
Known Bugs:
    Unexpected Crashes as a result of an unhandled exception.");
        }
    }
}