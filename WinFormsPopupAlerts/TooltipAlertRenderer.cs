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
        private Padding iconPadding = new Padding(3, 4, 5, 4);

        public virtual Padding IconPadding
        {
            get { return iconPadding; }
            set { iconPadding = value; }
        }

        internal Padding Padding
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

        public abstract void Draw(Graphics dc, string title, string text, ToolTipIcon icon = ToolTipIcon.None, System.Drawing.Image customImage = null);

        public abstract Rectangle GetBodyRect(Graphics dc, string title, string text, ToolTipIcon icon = ToolTipIcon.None, System.Drawing.Image customImage = null);

        public abstract Rectangle GetTitleRect(Graphics dc, string title, string text, ToolTipIcon icon = ToolTipIcon.None, System.Drawing.Image customImage = null);

        public Rectangle GetRect(Graphics dc, string title, string text, ToolTipIcon icon = ToolTipIcon.None, System.Drawing.Image customImage = null)
        {
            Rectangle titleRect = GetTitleRect(dc, title, text, icon, customImage);
            Rectangle bodyRect = GetBodyRect(dc, title, text, icon, customImage);
            return GetRect(dc, titleRect, bodyRect, icon, customImage);
        }

        public abstract Rectangle GetRect(Graphics dc, Rectangle titleRect, Rectangle bodyRect, ToolTipIcon icon = ToolTipIcon.None, System.Drawing.Image customImage = null);

        public abstract Rectangle GetCloseButtonRect(Graphics dc, Rectangle rect, TooltipCloseButtonState buttonState);

        public abstract void DrawCloseButton(Graphics dc, Rectangle rect, TooltipCloseButtonState buttonState);

        public abstract Region GetRegion(Graphics dc, Rectangle rect);

        public abstract Color TransparencyKey { get; }

    }
}
