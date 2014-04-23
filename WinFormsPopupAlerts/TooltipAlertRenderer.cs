using System;
using System.Collections.Generic;
using System.Text;
using WinFormsPopupAlerts;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace WinFormsPopupAlerts
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class TooltipAlertRenderer : Component
    {
        private static Bitmap ErrorIcon = SystemIcons.Error.ToBitmap();
        private static Bitmap WarningIcon = SystemIcons.Warning.ToBitmap();
        private static Bitmap InformationIcon = SystemIcons.Information.ToBitmap();

        private Size maxSize;
        private Size minSize;
        private Padding padding;
        private Padding iconPadding = new Padding(3, 4, 5, 4);

        protected abstract void Draw(System.Drawing.Graphics dc, string title, string text, Rectangle rect, Rectangle titleRect, Rectangle bodyRect, 
            Image img, int iconWidth);

        protected abstract Rectangle GetBodyTextRect(System.Drawing.Graphics dc, string text, Rectangle rect);

        protected abstract Rectangle GetTitleTextRect(System.Drawing.Graphics dc, string text, Rectangle rect);

        protected abstract Size GetCloseButtonSize(Graphics dc, TooltipCloseButtonState buttonState);

        /// <summary>
        /// Drawing content of alert window.
        /// </summary>
        /// <param name="dc">Graphics object of alert window.</param>
        /// <param name="title">Title of alert.</param>
        /// <param name="text">Text of alert.</param>
        /// <param name="icon">Icon of alert.</param>
        /// <param name="customImage">Custom image of alert.</param>
        public virtual void Draw(System.Drawing.Graphics dc, string title, string text, ToolTipIcon icon = ToolTipIcon.None, System.Drawing.Image customImage = null)
        {
            Rectangle titleRect = GetTitleRect(dc, title, text, icon, customImage);
            Rectangle bodyRect = GetBodyRect(dc, title, text, icon, customImage);
            Rectangle rect = GetRect(dc, titleRect, bodyRect, icon, customImage);
            Image img = GetIcon(icon, customImage);
            Draw(dc, title, text, rect, titleRect, bodyRect, img, GetIconSize(icon, customImage).Width);
            // drawing icon
            if (img != null)
                dc.DrawImage(img, new Point(Padding.Left + IconPadding.Left, rect.Height / 2 - (img.Height + IconPadding.Vertical) / 2));
        }

        public virtual System.Drawing.Rectangle GetBodyRect(System.Drawing.Graphics dc, string title, string text, ToolTipIcon icon = ToolTipIcon.None, System.Drawing.Image customImage = null)
        {
            int iconWidth = GetIconSize(icon, customImage).Width;
            int closeButtonWidth = GetCloseButtonSize(dc, TooltipCloseButtonState.Normal).Width;
            int w = Padding.Horizontal + iconWidth + closeButtonWidth;
            Rectangle ret;
            if (string.IsNullOrEmpty(text))
                ret = new Rectangle(new Point(0, 0), new Size(MinSize.Width - w, MinSize.Height));
            else
            {
                ret = new Rectangle(new Point(0, 0), MaxSize);
                ret.Width -= w;
                Rectangle rect = GetBodyTextRect(dc, text, ret);
                if (rect.Width + w > MaxSize.Width)
                    ret.Width = MaxSize.Width - w;
                else if (rect.Width + w < MinSize.Width)
                    ret.Width = MinSize.Width - w;
                else
                    ret.Width = rect.Width;

                if (rect.Height > MaxSize.Height)
                    ret.Height = MaxSize.Height;
                else
                    ret.Height = rect.Height;
            }
            return ret;
        }

        public virtual System.Drawing.Rectangle GetTitleRect(System.Drawing.Graphics dc, string title, string text, ToolTipIcon icon = ToolTipIcon.None, System.Drawing.Image customImage = null)
        {
            Rectangle ret;
            if (string.IsNullOrEmpty(title))
                ret = new Rectangle(new Point(0, 0), MinSize);
            else
            {
                int iconWidth =  GetIconSize(icon, customImage).Width;
                int closeButtonWidth = GetCloseButtonSize(dc, TooltipCloseButtonState.Normal).Width;
                int w = Padding.Horizontal + iconWidth + closeButtonWidth;
                ret = new Rectangle(new Point(0, 0), MaxSize);
                ret.Width -= w;
                Rectangle rect = GetTitleTextRect(dc, title, ret);
                if (rect.Width + w > MaxSize.Width)
                    ret.Width = MaxSize.Width - w;
                else if (rect.Width + w < MinSize.Width)
                    ret.Width = MinSize.Width - w;
                else
                    ret.Width = rect.Width;

                if (rect.Height > MaxSize.Height)
                    ret.Height = MaxSize.Height;
                else
                    ret.Height = rect.Height;
            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="icon"></param>
        /// <param name="customImage"></param>
        /// <returns></returns>
        private Size GetIconSize(ToolTipIcon icon, Image customImage)
        {
            Image img = GetIcon(icon, customImage);
            if (img == null)
                return new Size(0, 0);
            else
            {
                return new Size(img.Size.Width + IconPadding.Horizontal, img.Height + IconPadding.Vertical);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="icon">Standardized icon.</param>
        /// <param name="customImage"></param>
        /// <returns></returns>
        private Image GetIcon(ToolTipIcon icon, Image customImage)
        {
            switch (icon)
            {
                case ToolTipIcon.Error:
                    return ErrorIcon;
                case ToolTipIcon.Info:
                    return InformationIcon;
                case ToolTipIcon.Warning:
                    return WarningIcon;
                case ToolTipIcon.None:
                    if (customImage != null)
                        return customImage;
                    else
                        return null;
                default:
                    return null;
            }
        }

        public virtual System.Drawing.Rectangle GetRect(System.Drawing.Graphics dc, System.Drawing.Rectangle titleRect, System.Drawing.Rectangle bodyRect, ToolTipIcon icon = ToolTipIcon.None, System.Drawing.Image customImage = null)
        {
            Size iconSize = GetIconSize(icon, customImage);
            Rectangle ret = new Rectangle(0, 0, bodyRect.Width, titleRect.Height + bodyRect.Height);
            ret.Width += Padding.Horizontal + iconSize.Width;
            if (iconSize.Height > ret.Height)
                ret.Height = iconSize.Height;
            ret.Height += Padding.Vertical;
            return ret;
        }

        public virtual System.Drawing.Rectangle GetCloseButtonRect(System.Drawing.Graphics dc, System.Drawing.Rectangle rect, TooltipCloseButtonState buttonState)
        {
            Size btnSize = GetCloseButtonSize(dc, buttonState);
            Point btnPos = new Point(rect.Right - Padding.Right - btnSize.Width, rect.Top + Padding.Top);
            Rectangle btnRect = new Rectangle(btnPos, btnSize);
            return btnRect;
        }

        public virtual void DrawCloseButton(System.Drawing.Graphics dc, System.Drawing.Rectangle rect, TooltipCloseButtonState buttonState)
        {
            Rectangle btnRect = GetCloseButtonRect(dc, rect, buttonState);
            dc.DrawRectangle(Pens.Black, btnRect);
            dc.DrawLine(Pens.Black, btnRect.Location, new Point(btnRect.Right, btnRect.Bottom));
            dc.DrawLine(Pens.Black, new Point(btnRect.Right, btnRect.Top), new Point(btnRect.Left, btnRect.Bottom));
        }

        public Rectangle GetRect(Graphics dc, string title, string text, ToolTipIcon icon = ToolTipIcon.None, System.Drawing.Image customImage = null)
        {
            Rectangle titleRect = GetTitleRect(dc, title, text, icon, customImage);
            Rectangle bodyRect = GetBodyRect(dc, title, text, icon, customImage);
            return GetRect(dc, titleRect, bodyRect, icon, customImage);
        }

        public abstract Region GetRegion(Graphics dc, Rectangle rect);
        public abstract Color TransparencyKey { get; }

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

        [Browsable(false)]
        internal Size MinSize
        {
            get { return minSize; }
            set { minSize = value; }
        }

        [Browsable(false)]
        internal Size MaxSize
        {
            get { return maxSize; }
            set { maxSize = value; }
        }
    }
}
