using System;
using System.Collections.Generic;
using System.Text;
using WinFormsPopupAlerts;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsPopupAlerts
{
    public abstract class CustomTooltipAlertRendererBase : TooltipAlertRenderer
    {
        protected abstract void Draw(System.Drawing.Graphics dc, string title, string text, Rectangle rect, Rectangle titleRect, Rectangle bodyRect, 
            Image img, int iconWidth);

        protected abstract Rectangle GetBodyTextRect(System.Drawing.Graphics dc, string text, Rectangle rect);

        protected abstract Rectangle GetTitleTextRect(System.Drawing.Graphics dc, string text, Rectangle rect);

        protected abstract Size GetCloseButtonSize(Graphics dc, TooltipCloseButtonState buttonState);

        private static Bitmap ErrorIcon = SystemIcons.Error.ToBitmap();
        private static Bitmap WarningIcon = SystemIcons.Warning.ToBitmap();
        private static Bitmap InformationIcon = SystemIcons.Information.ToBitmap();

        public override void Draw(System.Drawing.Graphics dc, string title, string text, ToolTipIcon icon = ToolTipIcon.None, System.Drawing.Image customImage = null)
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

        public override System.Drawing.Rectangle GetBodyRect(System.Drawing.Graphics dc, string title, string text, ToolTipIcon icon = ToolTipIcon.None, System.Drawing.Image customImage = null)
        {
            Rectangle ret;
            if (text == null)
                ret = new Rectangle(new Point(0, 0), MinSize);
            else
            {
                ret = new Rectangle(new Point(0, 0), MaxSize);
                int iconWidth = GetIconSize(icon, customImage).Width;
                int closeButtonWidth = GetCloseButtonSize(dc, TooltipCloseButtonState.Normal).Width;
                int w = Padding.Horizontal + iconWidth + closeButtonWidth;
                ret.Width -= w;
                Rectangle rect = GetBodyTextRect(dc, text, ret);
                if (rect.Width + w > MaxSize.Width)
                    ret.Width = MaxSize.Width;
                else if (rect.Width + w < MinSize.Width)
                    ret.Width = MinSize.Width;
                else
                    ret.Width = rect.Width;

                if (rect.Height > MaxSize.Height)
                    ret.Height = MaxSize.Height;
                else
                    ret.Height = rect.Height;
            }
            return ret;
        }

        public override System.Drawing.Rectangle GetTitleRect(System.Drawing.Graphics dc, string title, string text, ToolTipIcon icon = ToolTipIcon.None, System.Drawing.Image customImage = null)
        {
            Rectangle ret;
            if (title == null)
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
                    ret.Width = MaxSize.Width;
                else if (rect.Width + w < MinSize.Width)
                    ret.Width = MinSize.Width;
                else
                    ret.Width = rect.Width;

                if (rect.Height > MaxSize.Height)
                    ret.Height = MaxSize.Height;
                else
                    ret.Height = rect.Height;
            }
            return ret;
        }

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

        public override System.Drawing.Rectangle GetRect(System.Drawing.Graphics dc, System.Drawing.Rectangle titleRect, System.Drawing.Rectangle bodyRect, ToolTipIcon icon = ToolTipIcon.None, System.Drawing.Image customImage = null)
        {
            Size iconSize = GetIconSize(icon, customImage);
            Rectangle ret = new Rectangle(0, 0, bodyRect.Width, titleRect.Height + bodyRect.Height);
            ret.Width += Padding.Horizontal + iconSize.Width;
            if (iconSize.Height > ret.Height)
                ret.Height = iconSize.Height;
            ret.Height += Padding.Vertical;
            return ret;
        }

        public override System.Drawing.Rectangle GetCloseButtonRect(System.Drawing.Graphics dc, System.Drawing.Rectangle rect, TooltipCloseButtonState buttonState)
        {
            Size btnSize = GetCloseButtonSize(dc, buttonState);
            Point btnPos = new Point(rect.Right - Padding.Right - btnSize.Width, rect.Top + Padding.Top);
            Rectangle btnRect = new Rectangle(btnPos, btnSize);
            return btnRect;
        }

        public override void DrawCloseButton(System.Drawing.Graphics dc, System.Drawing.Rectangle rect, TooltipCloseButtonState buttonState)
        {
            Rectangle btnRect = GetCloseButtonRect(dc, rect, buttonState);
            dc.DrawRectangle(Pens.Black, btnRect);
            dc.DrawLine(Pens.Black, btnRect.Location, new Point(btnRect.Right, btnRect.Bottom));
            dc.DrawLine(Pens.Black, new Point(btnRect.Right, btnRect.Top), new Point(btnRect.Left, btnRect.Bottom));
        }
    }
}
