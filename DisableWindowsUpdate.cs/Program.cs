using System;
using System.Diagnostics;


public class Program
{
    public Logging l = new Logging();
    public WUP wUP = new WUP();
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
                    wUP.Enable();
                }
                else if (key.KeyChar.ToString().ToLower() == "2")
                {
                    Console.Write("2\n");
                    wUP.Disable();
                }
                else goto repeat;
            }
            else if (args[0].ToLower().Contains("--enable"))
            {
                wUP.Enable();
            }
            else if (args[0].ToLower().Contains("--disable"))
            {
                wUP.Disable();
            }
        } catch(Exception ex)
        {
            l.logWrite("" + ex, 10, "INFO");
        }
    }
}