using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsPopupAlerts
{
    /// <summary>
    /// Contains values that specify the position on screen at which alert windows are displayed.
    /// </summary>
    public enum AlertAlignment
    {
        /// <summary>
        /// An alert window appears at the bottom left corner of the screen.
        /// </summary>
        BottomLeft,
        /// <summary>
        /// An alert window appears at the bottom right corner of the screen.
        /// </summary>
        BottomRight,
        /// <summary>
        /// An alert window appears at the top left corner of the screen.
        /// </summary>
        TopLeft,
        /// <summary>
        /// An alert window appears at the top right corner of the screen.
        /// </summary>
        TopRight,
    }
}
