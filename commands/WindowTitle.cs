namespace commands.WinTitle {
    public class ConsTitle {
        public static void Execute(string[] args) {
            if (args.Length == 0) {
                Console.WriteLine("Usage: ConsTitle [Console Title]");
            } else {

                string combinedTitle = string.Join(" ", args);
                Console.Title = combinedTitle;
            }
        }
    }
}
