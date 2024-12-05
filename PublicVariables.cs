using System.IO;

namespace PublicVars
{
    public class PublicVariables
    {

        public static string currentDirectory;
        public static string currentDirectoryName;

        static PublicVariables()
        {
 
            currentDirectory = Directory.GetCurrentDirectory();
            currentDirectoryName = Path.GetDirectoryName(currentDirectory);
        }
    }
}
