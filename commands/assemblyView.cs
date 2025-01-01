using System;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace commands.assemblyView {
    public class AssemblyView {
        public static void Execute(string[] args) {
            if (args.Length == 0 || string.IsNullOrEmpty(args[0])) {
                Console.WriteLine("Please provide a valid file path.");
                return;
            }

            string filePath = args[0].Trim('"');  

            if (!File.Exists(filePath)) {
                Console.WriteLine("File does not exist.");
                return;
            }

            try {
                
                Assembly assembly = Assembly.LoadFrom(filePath);
                
                
                PrintAssemblyInfo(assembly);
                
                
                ListClassesAndMethods(assembly);

                
                ListResources(assembly);
            }
            catch (BadImageFormatException) {
                Console.WriteLine("The file is not a valid .NET executable or library.");
            }
            catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void PrintAssemblyInfo(Assembly assembly) {
            Console.WriteLine($"Assembly: {assembly.GetName().Name}");
            Console.WriteLine($"Version: {assembly.GetName().Version}");
            
            
            var frameworkVersion = assembly.GetCustomAttribute<System.Runtime.Versioning.TargetFrameworkAttribute>();
            if (frameworkVersion != null) {
                Console.WriteLine($"Target Framework: {frameworkVersion.FrameworkName}");
            }
            else {
                Console.WriteLine("Target Framework: Not found");
            }
        }

        private static void ListClassesAndMethods(Assembly assembly) {
            Console.WriteLine("\nClasses and Methods:");

            
            Type[] types = assembly.GetTypes();

            foreach (var type in types) {
                Console.WriteLine($"Class: {type.FullName}");

                
                MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                
                foreach (var method in methods) {
                    string accessibility = method.IsPublic ? "Public" : method.IsPrivate ? "Private" : "Protected";
                    string staticMethod = method.IsStatic ? "Static" : "Instance";
                    string returnType = method.ReturnType.Name;

                    Console.WriteLine($"\tMethod: {method.Name}");
                    Console.WriteLine($"\t\tAccess: {accessibility}");
                    Console.WriteLine($"\t\tStatic: {staticMethod}");
                    Console.WriteLine($"\t\tReturn Type: {returnType}");
                }
            }
        }

        private static void ListResources(Assembly assembly) {
            Console.WriteLine("\nResources found:");

            
            var resources = assembly.GetManifestResourceNames();

            if (resources.Length == 0) {
                Console.WriteLine("\tNo resources found.");
            }

            foreach (var resource in resources) {
                Console.WriteLine($"\tResource: {resource}");

                
                if (resource.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                    resource.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                    resource.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)) {
                    Console.WriteLine("\t\tType: Image (PNG/BMP/JPG)");
                }
                else if (resource.EndsWith(".resx", StringComparison.OrdinalIgnoreCase)) {
                    Console.WriteLine("\t\tType: .resx (Resource File)");
                    ListResxContent(assembly, resource);  
                }
                else if (resource.EndsWith(".rc", StringComparison.OrdinalIgnoreCase)) {
                    Console.WriteLine("\t\tType: .rc (Resource Script)");
                    ListRcContent(assembly, resource);  
                }
                else if (resource.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)) {
                    Console.WriteLine("\t\tType: XML (Resource File)");
                }
                else {
                    Console.WriteLine("\t\tType: Other");
                }
            }
        }

        private static void ListResxContent(Assembly assembly, string resource) {
            try {
                using (var stream = assembly.GetManifestResourceStream(resource)) {
                    if (stream != null) {
                        
                        using (var reader = System.Xml.XmlReader.Create(stream)) {
                            while (reader.Read()) {
                                
                                if (reader.IsStartElement() && reader.Name == "data") {
                                    string resourceName = reader["name"];
                                    Console.WriteLine($"\t\t\tResource Name: {resourceName}");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"\t\t\tError reading .resx content: {ex.Message}");
            }
        }


        private static void ListRcContent(Assembly assembly, string resource) {
            
            Console.WriteLine("\t\t\t(Unable to parse .rc files directly. This may require external tools or manual inspection.)");
        }
    }
}
