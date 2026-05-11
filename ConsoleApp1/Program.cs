using System;
using System.Runtime.InteropServices;

class Program
{
    // Import function from Windows API
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SystemParametersInfo(
        int uAction, int uParam, string lpvParam, int fuWinIni);

    static void Main()
    {
        string path = @"C:\Downloads\Y.jpg";

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDCHANGE = 0x02;

        bool result = SystemParametersInfo(
            SPI_SETDESKWALLPAPER,
            0,
            path,
            SPIF_UPDATEINIFILE | SPIF_SENDCHANGE
        );

        if (result)
            Console.WriteLine("Wallpaper changed successfully!");
        else
            Console.WriteLine("Failed to change wallpaper.");
    }
}