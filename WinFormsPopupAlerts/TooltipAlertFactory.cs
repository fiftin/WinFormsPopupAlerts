using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using WinFormsPopupAlerts.Properties;

namespace WinFormsPopupAlerts
{
    internal class resfinder { }
    [ToolboxBitmap(@"e:\src\winformspopupalerts\WinFormsPopupAlerts\TooltipAlertFactory.bmp")]
    [ToolboxItem(true)]
    public class TooltipAlertFactory : PopupAlertFactory
    {
        public TooltipAlertFactory()
        {
            string[] sa = this.GetType().Assembly.GetManifestResourceNames();
            foreach (string s in sa)
                System.Diagnostics.Trace.WriteLine(s);
        }

        public TooltipAlertFactory(System.ComponentModel.IContainer container)
            : base(container)
        {
            string[] sa = this.GetType().Assembly.GetManifestResourceNames();
            foreach (string s in sa)
                System.Diagnostics.Trace.WriteLine(s);
        }

        protected override PopupAlert CreateAlertImpl(object arg, PopupAlertAlignment align)
        {
            TooltipAlert alert = new TooltipAlert((TooltipAlertArg)arg, align);
            switch (AlertStyle)
            {
                case TooltipAlertStyle.System:
                    alert.Renderer = new SystemTooltipAlertRenderer();
                    break;
                case TooltipAlertStyle.Custom:
                    alert.Renderer = CustomRenderer;
                    break;
            }
            return alert;
        }

        private TooltipAlertStyle alertStyle;
        private TooltipAlertRenderer customRenderer;

        [DefaultValue(null)]
        public TooltipAlertRenderer CustomRenderer
        {
            get { return customRenderer; }
            set { customRenderer = value; }
        }

        [DefaultValue(TooltipAlertStyle.System)]
        public TooltipAlertStyle AlertStyle
        {
            get { return alertStyle; }
            set { alertStyle = value; }
        }
    }
}
