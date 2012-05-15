using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace WinFormsPopupAlerts
{
    public abstract class TooltipAlertRenderer : Component
    {
        private Size maxSize;
        private Size minSize;
        private Padding padding;

        public Padding Padding
        {
            get { return padding; }
            set { padding = value; }
        }

        public Size MinSize
        {
            get { return minSize; }
            set { minSize = value; }
        }

        public Size MaxSize
        {
            get { return maxSize; }
            set { maxSize = value; }
        }

        public TooltipAlertRenderer()
        {
            InitializeComponent();
        }

        public TooltipAlertRenderer(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }


        private void InitializeComponent()
        {
        }

        public abstract void Draw(Graphics dc, string title, string text, TooltipAlertIcon icon = TooltipAlertIcon.None, Icon customIcon = null);

        public abstract Rectangle GetBodyRect(Graphics dc, string title, string text, TooltipAlertIcon icon = TooltipAlertIcon.None, Icon customIcon = null);

        public abstract Rectangle GetTitleRect(Graphics dc, string title, string text, TooltipAlertIcon icon = TooltipAlertIcon.None, Icon customIcon = null);

        public Rectangle GetRect(Graphics dc, string title, string text, TooltipAlertIcon icon = TooltipAlertIcon.None, System.Drawing.Icon customIcon = null)
        {
            Rectangle titleRect = GetTitleRect(dc, title, text, icon, customIcon);
            Rectangle bodyRect = GetBodyRect(dc, title, text, icon, customIcon);
            return GetRect(dc, titleRect, bodyRect, icon, customIcon);
        }

        public abstract Rectangle GetRect(Graphics dc, Rectangle titleRect, Rectangle bodyRect, TooltipAlertIcon icon = TooltipAlertIcon.None, System.Drawing.Icon customIcon = null);

        public abstract Rectangle GetCloseButtonRect(Graphics dc, Rectangle rect, TooltipCloseButtonState buttonState);

        public abstract void DrawCloseButton(Graphics dc, Rectangle rect, TooltipCloseButtonState buttonState);

        public abstract Region GetRegion(Graphics dc, Rectangle rect);

    }
}
