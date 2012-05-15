using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;

namespace WinFormsPopupAlerts
{
    public class SystemPopupAlert : TooltipAlert
    {
        public SystemPopupAlert()
            :base()
        {
        }
         
        public SystemPopupAlert(object[] args)
            : base(args)
        {
        }


    }
}

