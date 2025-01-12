using System;
using System.IO;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

namespace commands.cryptography
{
    public static class Cryptography
    {
        private static readonly string[] SupportedAlgorithms = new string[]
        {
            "MD5 (hash)",
            "SHA1 (hash)",
            "SHA256 (hash)",
            "SHA512 (hash)",
            "HMACSHA256 (hash)",
            "HMACSHA512 (hash)",
            "SHA384 (hash)",
            "SHA512_256 (hash)"
        };

        public static void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Invalid arguments.");
                return;
            }

            if (args[0].ToLower() == "list")
            {
                ListSupportedAlgorithms();
                return;
            }

            if (args.Length < 2)
            {
                Console.WriteLine("Invalid arguments.");
                return;
            }

            string operation = args[0].ToLower();

            if (operation == "hash")
            {
                if (args.Length < 3)
                {
                    Console.WriteLine("Missing hashing algorithm or file path.");
                    return;
                }

                string algorithm = args[1].ToLower();
                string filePath = args[2].Trim('"');

                if (File.Exists(filePath))
                {
                    HashFile(filePath, algorithm);
                }
                else
                {
                    Console.WriteLine("File not found.");
                }
            }
            else
            {
                Console.WriteLine("Unknown operation.");
            }
        }

        private static void ListSupportedAlgorithms()
        {
            Console.WriteLine("Supported Algorithms:");
            foreach (var algo in SupportedAlgorithms)
            {
                Console.WriteLine(algo);
            }
        }

        private static void HashFile(string filePath, string algorithm)
        {
            HashAlgorithm hashAlgorithm = algorithm switch
            {
                "md5" => MD5.Create(),
                "sha1" => SHA1.Create(),
                "sha256" => SHA256.Create(),
                "sha512" => SHA512.Create(),
                "hmacsha256" => new HMACSHA256(),
                "hmacsha512" => new HMACSHA512(),
                "sha384" => SHA384.Create(),
                "sha512_256" => SHA512.Create(),
                _ => null,
            };

            if (hashAlgorithm == null)
            {
                Console.WriteLine("Invalid hashing algorithm.");
                return;
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                byte[] hashBytes = hashAlgorithm.ComputeHash(fs);
                Console.WriteLine(BitConverter.ToString(hashBytes).Replace("-", "").ToLower());
            }
        }
    }
}
