using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace WinFormsPopupAlerts
{
    /// <summary>
    /// Imports native API functions for works with Windows Themes on low level.
    /// </summary>
    public static class SafeNativeMethods
    {

        /// <summary>
        /// Opens the theme data for a window and its associated class.
        /// </summary>
        /// <param name="hwnd">Handle of the window for which theme data is required.</param>
        /// <param name="pszClassList">Pointer to a string that contains a semicolon-separated list of classes.</param>
        /// <returns>OpenThemeData tries to match each class, one at a time, to a class data section in the active theme. If a match is found, an associated HTHEME handle is returned. If no match is found NULL is returned.</returns>
        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        private extern static IntPtr OpenThemeData(HandleRef hwnd, [MarshalAs(UnmanagedType.LPWStr)] string pszClassList);

        /// <summary>
        /// Retrieves whether a visual style has defined parameters for the specified part and state.
        /// </summary>
        /// <param name="hTheme">Handle to a window's specified theme data. Use OpenThemeData to create an HTHEME.</param>
        /// <param name="iPartId">Value of type int that specifies the part. </param>
        /// <param name="iStateId">Currently unused. The value should be 0.</param>
        /// <returns>
        /// Returns one of the following values:
        /// true - the theme has defined parameters for the specified iPartId and iStateId,
        /// false - he theme does not have defined parameters for the specified iPartId and iStateId.
        /// </returns>
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
            return OpenThemeData(new HandleRef((object)null, IntPtr.Zero), className);
        }

    }
}
