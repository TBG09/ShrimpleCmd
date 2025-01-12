using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;

namespace modloader {
    public static class Modloader {
        private static readonly List<Thread> ModThreads = new List<Thread>();
        private static readonly List<dynamic> LoadedMods = new List<dynamic>();

        public static void LoadMods() {
            string modsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "mods");

            if (!Directory.Exists(modsPath)) {
                return;
            }

            var dllFiles = Directory.GetFiles(modsPath, "*.dll");
            if (dllFiles.Length == 0) {
                return;
            }

            foreach (var dllPath in dllFiles) {
                try {
                    Console.WriteLine($"Loading mod: {dllPath}");
                    Assembly modAssembly = Assembly.LoadFrom(dllPath);

                    string firstNamespace = modAssembly.GetTypes()[0].Namespace;
                    Type modType = modAssembly.GetType(firstNamespace + ".ModEntryPoint");

                    if (modType == null) {
                        Console.WriteLine($"[ERROR] Mod does not contain a 'ModEntryPoint' class in namespace {firstNamespace}: {dllPath}");
                        continue;
                    }

                    var initMethod = modType.GetMethod("Init");
                    var mainEntryMethod = modType.GetMethod("MainEntry");
                    var exitMethod = modType.GetMethod("Exit");

                    if (initMethod == null || mainEntryMethod == null || exitMethod == null) {
                        Console.WriteLine($"[ERROR] Mod missing one of the required methods (Init, MainEntry, Exit): {dllPath}");
                        continue;
                    }

                    var modInstance = Activator.CreateInstance(modType);
                    LoadedMods.Add(modInstance);

                    initMethod.Invoke(modInstance, null);
                } catch (Exception ex) {
                    Console.WriteLine($"[ERROR] Failed to load mod from {dllPath}: {ex.Message}");
                    Console.WriteLine(ex.StackTrace);
                    continue;
                }
            }

            foreach (var mod in LoadedMods) {
                var thread = new Thread(() => {
                    try {
                        var modType = mod.GetType();
                        var mainEntryMethod = modType.GetMethod("MainEntry");

                        mainEntryMethod.Invoke(mod, null);
                    } catch (Exception ex) {
                        Console.WriteLine(ex.StackTrace);
                    }
                });

                ModThreads.Add(thread);
                thread.Start();
            }
        }

        public static void UnloadMods() {
            foreach (var mod in LoadedMods) {
                try {
                    var modType = mod.GetType();
                    var exitMethod = modType.GetMethod("Exit");
                    exitMethod.Invoke(mod, null);
                } catch (Exception ex) {
                    Console.WriteLine($"[ERROR] Error in Exit method for mod: {mod.GetType().Name}. {ex.Message}");
                    Console.WriteLine(ex.StackTrace);
                }
            }

            foreach (var thread in ModThreads) {
                if (thread.IsAlive) {
                    try {
                        thread.Join(500);
                    } catch (ThreadStateException ex) {
                        Console.WriteLine($"[ERROR] Failed to join thread: {ex.Message}");
                    }
                }
            }
        }
    }
}
