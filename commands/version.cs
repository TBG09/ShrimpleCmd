namespace commands.Version {
    public class version {
        public static void Execute(string[] args) {
            Console.WriteLine(PublicVars.PublicVariables.VersionNum);
            Console.WriteLine("Version ID: " + PublicVars.PublicVariables.VersionID);
        }
    }
}
