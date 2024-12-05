using System.IO;

namespace PublicVars
{
    public class PublicVariables
    {

        public static string currentDirectory;
        public static string currentDirectoryName;
        public static string userHomeFolder;

        static PublicVariables()
        {
            userHomeFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            currentDirectory = userHomeFolder;
            currentDirectoryName = Path.GetDirectoryName(currentDirectory);
        }
    }
}
