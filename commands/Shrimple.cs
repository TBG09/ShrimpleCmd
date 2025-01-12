namespace commands.ShrimpleCommand {
    public class Shrimple {
        public static void Execute(string[] args) {
            if (args.Length > 0 && args[0].Equals("help", StringComparison.OrdinalIgnoreCase) || args.Length == 0) {
                Console.WriteLine(@"Shrimple Command Documentation:
The $$ $$ syntax in Shrimple CMD is used for variable referencing. When you enclose a variable name with $$, it is replaced with the corresponding value from internal variables.

Usage:

    Format:
    $$variableName$$

Example:
    If osType is defined as Windows:

    echo $$osType$$

    Outputs:
    echo Windows

# # is used to execute os commands, for example taskmgr if your on windows, or maybe nano hello.txt on linux.


Usage:

    Format:
    #command#

Example:
    #taskmgr# on windows will run taskmgr.

");
            } 
        }
    }
}
