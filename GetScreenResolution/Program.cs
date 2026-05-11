using System;
using System.Runtime.InteropServices;


namespace GetScreenResolution
{
    internal class Program
    {
        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        static void Main(string[] args)
        {
              int screenWidth = GetSystemMetrics(0); // SM_CXSCREEN
              int screenHeight = GetSystemMetrics(1); // SM_CYSCREEN

              Console.WriteLine($"Screen resolution Width: {screenWidth} \nHeight: {screenHeight}");
        }
    }
}
