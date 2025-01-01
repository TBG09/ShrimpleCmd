using System;
using System.IO;
using System.Reflection;
using System.Linq;

namespace commands.csExec {
    public class CsExec {
        public static void Execute(string[] args) {
            try {
                if (args.Length == 0) {
                    Console.WriteLine("No arguments provided.");
                    return;
                }

                
                if (args.Contains("-d")) {
                    int dllIndex = Array.IndexOf(args, "-d") + 1;
                    int namespaceIndex = Array.IndexOf(args, "-n") + 1;
                    int classIndex = Array.IndexOf(args, "-c") + 1;
                    int methodIndex = Array.IndexOf(args, "-m") + 1;
                    int argsIndex = Array.IndexOf(args, "-a") + 1;

                    string dllPath = args[dllIndex];
                    string namespaceName = args[namespaceIndex];
                    string className = args[classIndex];
                    string methodName = args[methodIndex];
                    string methodArguments = args.Length > argsIndex ? args[argsIndex] : "";

                    ExecuteMethodFromDll(dllPath, namespaceName, className, methodName, methodArguments);
                } else {
                    Console.WriteLine("Invalid arguments. Use -d for DLL path.");
                }
            } catch (Exception ex) {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
        }

        private static void ExecuteMethodFromDll(string dllPath, string namespaceName, string className, string methodName, string methodArguments) {
            try {
                
                if (!File.Exists(dllPath)) {
                    Console.WriteLine($"DLL not found at: {dllPath}");
                    return;
                }

                
                var assembly = Assembly.LoadFrom(dllPath);

                
                var type = assembly.GetType($"{namespaceName}.{className}");

                if (type == null) {
                    Console.WriteLine($"Class {className} not found in namespace {namespaceName}.");
                    return;
                }

                
                var method = type.GetMethod(methodName);

                if (method == null) {
                    Console.WriteLine($"Method {methodName} not found in class {className}.");
                    return;
                }

                
                var arguments = string.IsNullOrEmpty(methodArguments) ? null : methodArguments.Split(',').Select(arg => arg.Trim()).ToArray();

                
                method.Invoke(null, arguments);

            } catch (Exception ex) {
                Console.WriteLine($"Error executing method from DLL: {ex.Message}");
            }
        }
    }
}