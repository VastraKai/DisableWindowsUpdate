using System.Runtime.InteropServices;

namespace WebServerCS
{
    // Extended from ConsoleBase
    public static partial class Console
    {
        internal class WindowsConsoleConfig
        {
            const int STD_OUTPUT_HANDLE = -11;
            const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 4;

            [DllImport("kernel32.dll", SetLastError = true)]
            static extern IntPtr GetStdHandle(int nStdHandle);

            [DllImport("kernel32.dll")]
            static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

            [DllImport("kernel32.dll")]
            static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

            public void SetupConsole()
            {
                IntPtr handle = GetStdHandle(STD_OUTPUT_HANDLE);
                GetConsoleMode(handle, out uint mode);
                mode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
                SetConsoleMode(handle, mode);
            }
        }
        /// <summary>
        /// Use this to setup the console for escape codes
        /// </summary>
        public static void SetupConsole() // Use to setup the console and enable escape codes
        {
            if (Environment.OSVersion.Platform.ToString().ToLower().Contains("win"))
            {
                WindowsConsoleConfig ConConfig = new();
                ConConfig.SetupConsole();
            }
        }
        public static void WriteEach(string? str, int delay = 50)
        {
            if (str != null)
            {
                foreach (char c in str)
                {
                    Console.Write(c);
                    Thread.Sleep(delay);
                }
            }
        }
        public static void WriteLineEach(string? str, int delay = 50)
        {
            if (str != null)
            {
                foreach (char c in str)
                {
                    Console.Write(c);
                    Thread.Sleep(delay);
                }
            }
            Console.Write("\n");
        }

        // Hide the console window
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        public static void HideConsole()
        {
            IntPtr handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
        }
        public static void ShowConsole()
        {
            IntPtr handle = GetConsoleWindow();
            ShowWindow(handle, SW_SHOW);
        }
        // could be used by other things too
        public static string ESC = "\x001B"; // Escape character
        /// <summary>
        /// escColor: Gets the escape code to set a foreground color, or reset the foreground color.
        /// </summary>
        /// <param name="r">The value to be used for Red</param>
        /// <param name="g">The value to be used for Green</param>
        /// <param name="b">The value to be used for Blue</param>
        /// <returns>Foreground color escape code as string</returns>
        public static string EscColor(float r, float g, float b) // returns a foreground color escape code 
        {                                                                            // (changes the foreground color based on red, green, and blue values)
            int red = (int)Math.Round((float)(255 * r));
            int green = (int)Math.Round((float)(255 * g));
            int blue = (int)Math.Round((float)(255 * b));
            return $"{ESC}[38;2;{red};{green};{blue}m";
        }
        public static string Cl(float r, float g, float b) => EscColor(r, g, b);
        public static string R = "\x001B[0m";
    }
}
