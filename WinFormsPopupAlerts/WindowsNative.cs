using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace WinFormsPopupAlerts
{
    public static class WindowsNative
    {
        public const int SPI_GETWORKAREA = 0x0030;


        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("Kernel32.dll", SetLastError = true)]
        public extern static int GetLastError();

        [DllImport("User32.dll", SetLastError = true)]
        public extern static bool SystemParametersInfo(uint uiAction, uint uiParam, ref Rectangle pvParam, uint fWinIni);

        public static Rectangle GetWorkArea()
        {
            Rectangle rect = new Rectangle();
            if (!SystemParametersInfo(SPI_GETWORKAREA, 0, ref rect, 0))
            {
                throw new Win32Exception(GetLastError());
            }
            return rect;
        }
    }
}
