using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace WinFormsPopupAlerts
{
    public static class SafeNativeMethods
    {
        public const int SPI_GETWORKAREA = 0x0030;

        /*
        [DllImport("Gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        public static Region GetRoundRectRegion(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse)
        {
            return System.Drawing.Region.FromHrgn(CreateRoundRectRgn(nLeftRect,
                nTopRect, nRightRect, nBottomRect, nWidthEllipse, nHeightEllipse));
        }
         */



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


        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        private extern static IntPtr OpenThemeData(HandleRef hwnd, [MarshalAs(UnmanagedType.LPWStr)] string pszClassList);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public extern static int CloseThemeData(HandleRef hTheme);


        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        private extern static bool IsThemePartDefined(HandleRef hTheme, int iPartId, int iStateId);

        public static bool IsThemePartDefined(string className, int part, int state)
        {
            IntPtr hTheme = GetThemeHandle(className);
            if (hTheme == IntPtr.Zero)
                return false;
            bool ret = IsThemePartDefined(new HandleRef((object)null, hTheme), part, 0);
            return ret;
        }

        private static IntPtr GetThemeHandle(string className)
        {
            return SafeNativeMethods.OpenThemeData(new HandleRef((object)null, IntPtr.Zero), className);
        }

        public static bool IsThemeDefined(string className)
        {
            try
            {
                IntPtr hTheme = GetThemeHandle(className);
                return hTheme != IntPtr.Zero;
            }
            catch (Exception)
            {
                return false;
            }
        }



        private const int SW_SHOWNOACTIVATE = 4;
        private const int HWND_TOPMOST = -1;
        private const uint SWP_NOACTIVATE = 0x0010;

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        static extern bool SetWindowPos(
             int hWnd,           // window handle
             int hWndInsertAfter,    // placement-order handle
             int X,          // horizontal position
             int Y,          // vertical position
             int cx,         // width
             int cy,         // height
             uint uFlags);       // window positioning flags

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void ShowInactiveTopmost(System.Windows.Forms.Form frm)
        {
            ShowWindow(frm.Handle, SW_SHOWNOACTIVATE);
            SetWindowPos(frm.Handle.ToInt32(), HWND_TOPMOST,
            frm.Left, frm.Top, frm.Width, frm.Height,
            SWP_NOACTIVATE);
        }

    }
}
