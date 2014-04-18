using System;
using System.Collections.Generic;
using System.Text;
using WinFormsPopupAlerts;
using System.Drawing;

namespace WinFormsPopupAlerts
{
    public class CustomTooltipAlertRendererBase : TooltipAlertRenderer
    {
        public override void Draw(System.Drawing.Graphics dc, string title, string text, TooltipAlertIcon icon = TooltipAlertIcon.None, System.Drawing.Image customIcon = null)
        {
        }

        protected virtual Rectangle getBodyTextRect(System.Drawing.Graphics dc, string text, Rectangle rect)
        {
            SizeF size = dc.MeasureString(text, SystemFonts.DefaultFont, rect.Size.Width,
                new StringFormat());
            return new Rectangle(new Point(0, 0), Size.Ceiling(size));
        }

        protected virtual Rectangle getTitleTextRect(System.Drawing.Graphics dc, string text, Rectangle rect)
        {
            SizeF size = dc.MeasureString(text, new Font(SystemFonts.DefaultFont, FontStyle.Bold), rect.Size.Width,
                new StringFormat(StringFormatFlags.NoWrap));
            return new Rectangle(new Point(0, 0), Size.Ceiling(size));
        }

        public override System.Drawing.Rectangle GetBodyRect(System.Drawing.Graphics dc, string title, string text, TooltipAlertIcon icon = TooltipAlertIcon.None, System.Drawing.Image customIcon = null)
        {
            Rectangle ret;
            if (text == null)
                ret = new Rectangle(new Point(0, 0), MinSize);
            else
            {
                ret = new Rectangle(new Point(0, 0), MaxSize);
                ret.Width -= Padding.Horizontal + GetIconSize(icon, customIcon).Width;
                Rectangle rect = getBodyTextRect(dc, text, ret);
                if (rect.Width + Padding.Horizontal > MaxSize.Width)
                    ret.Width = MaxSize.Width;
                else if (rect.Width + Padding.Horizontal < MinSize.Width)
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
                ret = new Rectangle(new Point(0, 0), MaxSize);
                ret.Width -= Padding.Horizontal + GetIconSize(icon, customIcon).Width;
                Rectangle rect = getTitleTextRect(dc, title, ret);

                if (rect.Width + Padding.Horizontal > MaxSize.Width)
                    ret.Width = MaxSize.Width;
                else if (rect.Width + Padding.Horizontal < MinSize.Width)
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
            Rectangle ret = new Rectangle(0, 0, Math.Max(titleRect.Width, bodyRect.Width), titleRect.Height + bodyRect.Height);
            ret.Width += Padding.Horizontal + iconSize.Width;
            if (iconSize.Height > ret.Height)
                ret.Height = iconSize.Height;
            ret.Height += Padding.Vertical;
            return ret;
        }

        public override System.Drawing.Rectangle GetCloseButtonRect(System.Drawing.Graphics dc, System.Drawing.Rectangle rect, TooltipCloseButtonState buttonState)
        {
        }

        public override void DrawCloseButton(System.Drawing.Graphics dc, System.Drawing.Rectangle rect, TooltipCloseButtonState buttonState)
        {
        }

        public override System.Drawing.Region GetRegion(System.Drawing.Graphics dc, System.Drawing.Rectangle rect)
        {
            return new Region(rect);
        }
    }
}
