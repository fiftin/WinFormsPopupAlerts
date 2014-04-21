using System;
using System.Collections.Generic;
using System.Text;
using WinFormsPopupAlerts;
using System.Drawing;

namespace WinFormsPopupAlerts
{
    public abstract class CustomTooltipAlertRendererBase : TooltipAlertRenderer
    {
        protected abstract void Draw(System.Drawing.Graphics dc, string title, string text, Rectangle rect, Rectangle titleRect, Rectangle bodyRect, 
            Image img, int iconWidth);

        protected abstract Rectangle GetBodyTextRect(System.Drawing.Graphics dc, string text, Rectangle rect);

        protected abstract Rectangle GetTitleTextRect(System.Drawing.Graphics dc, string text, Rectangle rect);

        protected abstract Size GetCloseButtonSize(Graphics dc, TooltipCloseButtonState buttonState);

        public override void Draw(System.Drawing.Graphics dc, string title, string text, TooltipAlertIcon icon = TooltipAlertIcon.None, System.Drawing.Image customIcon = null)
        {
            Rectangle titleRect = GetTitleRect(dc, title, text, icon, customIcon);
            Rectangle bodyRect = GetBodyRect(dc, title, text, icon, customIcon);
            Rectangle rect = GetRect(dc, titleRect, bodyRect, icon, customIcon);
            Image img = GetIcon(icon, customIcon);
            Draw(dc, title, text, rect, titleRect, bodyRect, img, GetIconSize(icon, customIcon).Width);
            // drawing icon
            if (img != null)
                dc.DrawImage(img, new Point(Padding.Left + IconPadding.Left, rect.Height / 2 - (img.Height + IconPadding.Vertical) / 2));
        }

        public override System.Drawing.Rectangle GetBodyRect(System.Drawing.Graphics dc, string title, string text, TooltipAlertIcon icon = TooltipAlertIcon.None, System.Drawing.Image customIcon = null)
        {
            Rectangle ret;
            if (text == null)
                ret = new Rectangle(new Point(0, 0), MinSize);
            else
            {
                ret = new Rectangle(new Point(0, 0), MaxSize);
                int iconWidth = GetIconSize(icon, customIcon).Width;
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

        public override System.Drawing.Rectangle GetTitleRect(System.Drawing.Graphics dc, string title, string text, TooltipAlertIcon icon = TooltipAlertIcon.None, System.Drawing.Image customIcon = null)
        {
            Rectangle ret;
            if (title == null)
                ret = new Rectangle(new Point(0, 0), MinSize);
            else
            {
                int iconWidth =  GetIconSize(icon, customIcon).Width;
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

        private Size GetIconSize(TooltipAlertIcon icon, Image customIcon)
        {
            Image img = GetIcon(icon, customIcon);
            if (img == null)
                return new Size(0, 0);
            else
            {
                return new Size(img.Size.Width + IconPadding.Horizontal, img.Height + IconPadding.Vertical);
            }
        }

        private Image GetIcon(TooltipAlertIcon icon, Image customIcon)
        {
            switch (icon)
            {
                case TooltipAlertIcon.Custom:
                    return customIcon;
                case TooltipAlertIcon.None:
                default:
                    return null;
            }
        }

        public override System.Drawing.Rectangle GetRect(System.Drawing.Graphics dc, System.Drawing.Rectangle titleRect, System.Drawing.Rectangle bodyRect, TooltipAlertIcon icon = TooltipAlertIcon.None, System.Drawing.Image customIcon = null)
        {
            Size iconSize = GetIconSize(icon, customIcon);
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
