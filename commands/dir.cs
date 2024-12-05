using System;
using System.IO;
using PublicVars;

namespace commands.dir {
    public class Dir {
        PublicVariables PubVars = new PublicVariables();

        public static void Execute(string[] args) {
            string[] DirContent = Directory.GetFileSystemEntries(Directory.GetCurrentDirectory());
            Console.WriteLine("Objects in directory " + PublicVariables.currentDirectoryName + ":");

            foreach (string entry in DirContent) {
                string entryName = Path.GetFileName(entry);
                if (Directory.Exists(entry)) {
                    Console.WriteLine("[Directory] " + entryName);
                } else {
                    Console.WriteLine(entryName);
                }
            }
        }
    }
}
