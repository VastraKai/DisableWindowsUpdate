using System;
using System.Diagnostics;


public class Program
{
    static void Main(string[] args)
    {
        new Program().RealMain(args);
    }
    void RealMain(string[] args)
    {
        try
        {
            if (args.Length == 0)
            {
                Console.WriteLine("---------- Windows Update Utility");
                Console.WriteLine("1) Enable Windows update.");
                Console.WriteLine("2) Disable Windows update.");
                Console.Write("Select an action: ");
            repeat:
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.KeyChar.ToString().ToLower() == "1")
                {
                    Console.Write("1\n");
                    WUP.Enable();
                }
                else if (key.KeyChar.ToString().ToLower() == "2")
                {
                    Console.Write("2\n");
                    WUP.Disable();
                }
                else goto repeat;
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey(true);
            }
            else if (args[0].ToLower().StartsWith("-e"))
            {
                WUP.Enable();
            }
            else if (args[0].ToLower().StartsWith("-d"))
            {
                WUP.Disable();
            }
            else
            {
                Console.WriteLine("Invalid argument.");
                Console.WriteLine("Usage: DisableWindowsUpdate.exe [-e|-d]");
                // Wait for ten seconds if the variable cmdcmdline contains cmd /c
                if (Environment.CommandLine.ToLower().Contains("cmd /c"))
                {
                    Thread.Sleep(10000 );
                }
            }
        } catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}