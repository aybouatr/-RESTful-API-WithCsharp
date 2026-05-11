using System;
using System.Runtime.InteropServices;

namespace MessageBoxWithOS
{
    internal class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);

        static void Main(string[] args)
        {
            MessageBox(IntPtr.Zero, "Hello, World!", "Message Box with OS", 0);
        }
    }
}
