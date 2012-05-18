using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.VisualStyles;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace WinFormsPopupAlerts
{
    [System.ComponentModel.ToolboxItem(false)]
    public class SystemTooltipAlertRenderer : TooltipAlertRenderer
    {

        public override void Draw(Graphics dc, string title, string text, TooltipAlertIcon icon = TooltipAlertIcon.None, Image customIcon = null)
        {
            Rectangle titleRect = GetTitleRect(dc, title, text, icon, customIcon);
            Rectangle bodyRect = GetBodyRect(dc, title, text, icon, customIcon);
            Rectangle rect = GetRect(dc, titleRect, bodyRect, icon, customIcon);

            Image img = GetIcon(icon, customIcon);
            int iconWidth = GetIconSize(icon, customIcon).Width;

            if (IsDefined(VisualStyleElement.ToolTip.BalloonTitle.Normal) && IsDefined(VisualStyleElement.ToolTip.Balloon.Normal))
            {
                VisualStyleRenderer titleRenderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.BalloonTitle.Normal);
                VisualStyleRenderer balloonRenderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.Balloon.Normal);
                balloonRenderer.DrawBackground(dc, rect);


                // drawing title
                titleRenderer.DrawText(dc,
                    new Rectangle(Padding.Left + iconWidth, Padding.Top, rect.Width - (Padding.Left + Padding.Right), titleRect.Height),
                    title, false, TextFormatFlags.Left | TextFormatFlags.WordEllipsis | TextFormatFlags.VerticalCenter);

                // drawing text
                Rectangle balloonTextBounds = new Rectangle(Padding.Left + iconWidth, Padding.Top + titleRect.Height, rect.Width - (Padding.Left + Padding.Right), rect.Height - (Padding.Top + titleRect.Height + Padding.Bottom));
                balloonRenderer.DrawText(dc, balloonTextBounds,
                    text, false, TextFormatFlags.Left | TextFormatFlags.WordBreak | TextFormatFlags.VerticalCenter);

                

            }
            else
            {
                dc.FillRectangle(SystemBrushes.Info, rect);
                dc.DrawRectangle(Pens.Black, new Rectangle(0, 0, rect.Width - 1, rect.Height - 1));

                dc.DrawString(title, new Font(SystemFonts.DefaultFont, FontStyle.Bold), SystemBrushes.InfoText,
                    new PointF(Padding.Left + iconWidth, Padding.Top), new StringFormat(StringFormatFlags.NoWrap));
                dc.DrawString(text, SystemFonts.DefaultFont, SystemBrushes.InfoText,
                    new RectangleF(Padding.Left + iconWidth, Padding.Top + titleRect.Height, bodyRect.Width, bodyRect.Height),
                    new StringFormat());
            }

            // drawing icon
            if (img != null)
            {
                dc.DrawImage(img, new Point(Padding.Left + IconPadding.Left, Padding.Top + IconPadding.Top));
            }
        }

        public override Rectangle GetBodyRect(Graphics dc, string title, string text, TooltipAlertIcon icon = TooltipAlertIcon.None, Image customIcon = null)
        {
            if (icon != TooltipAlertIcon.None)
            {
            }
            Rectangle ret;
            if (text == null)
            {
                ret = new Rectangle(new Point(0, 0), MinSize);
            }
            else
            {
                ret = new Rectangle(new Point(0, 0), MaxSize);
                ret.Width -= Padding.Horizontal + GetIconSize(icon, customIcon).Width;
                Rectangle rect;
                if (IsDefined(VisualStyleElement.ToolTip.Balloon.Normal))
                {
                    VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.Balloon.Normal);
                    rect = renderer.GetTextExtent(dc, ret, text, TextFormatFlags.Left | TextFormatFlags.WordBreak | TextFormatFlags.VerticalCenter);
                }
                else
                {
                    SizeF size = dc.MeasureString(text, SystemFonts.DefaultFont, ret.Size.Width,
                        new StringFormat());
                    rect = new Rectangle(new Point(0, 0), Size.Ceiling(size));

                }

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
            Image img = GetIcon(icon,customIcon);
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

        public override Rectangle GetTitleRect(Graphics dc, string title, string text, TooltipAlertIcon icon = TooltipAlertIcon.None, Image customIcon = null)
        {
            Rectangle ret;
            if (title == null)
            {
                ret = new Rectangle(new Point(0, 0), MinSize);
            }
            else
            {
                ret = new Rectangle(new Point(0, 0), MaxSize);
                ret.Width -= Padding.Horizontal + GetIconSize(icon, customIcon).Width;

                Rectangle rect;
                if (IsDefined(VisualStyleElement.ToolTip.BalloonTitle.Normal))
                {
                    VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.BalloonTitle.Normal);
                    rect = renderer.GetTextExtent(dc, ret, title, TextFormatFlags.Left | TextFormatFlags.WordEllipsis | TextFormatFlags.VerticalCenter);
                }
                else
                {
                    SizeF size = dc.MeasureString(title, new Font(SystemFonts.DefaultFont, FontStyle.Bold), ret.Size.Width,
                        new StringFormat(StringFormatFlags.NoWrap));
                    rect = new Rectangle(new Point(0, 0), Size.Ceiling(size));
                }

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

        public override Rectangle GetRect(Graphics dc, Rectangle titleRect, Rectangle bodyRect, TooltipAlertIcon icon = TooltipAlertIcon.None, Image customIcon = null)
        {
            Size iconSize = GetIconSize(icon, customIcon);
            Rectangle ret = new Rectangle(0, 0, Math.Max(titleRect.Width, bodyRect.Width), titleRect.Height + bodyRect.Height);
            ret.Width += Padding.Horizontal + iconSize.Width;
            if (iconSize.Height > ret.Height)
                ret.Height = iconSize.Height;
            ret.Height += Padding.Vertical;
            return ret;
        }

        public override Rectangle GetCloseButtonRect(Graphics dc, Rectangle rect, TooltipCloseButtonState buttonState)
        {
            VisualStyleElement btn = GetCloseButtonVS(buttonState);
            Size btnSize;
            if (IsDefined(btn))
            {
                VisualStyleRenderer renderer = new VisualStyleRenderer(btn);
                btnSize = renderer.GetPartSize(dc, ThemeSizeType.True);
            }
            else
            {
                btnSize = new Size(10, 10);
            }
            Point btnPos = new Point(rect.Right - Padding.Right - btnSize.Width, rect.Top + Padding.Top);
            Rectangle btnRect = new Rectangle(btnPos, btnSize);
            return btnRect;
        }

        public override void DrawCloseButton(Graphics dc, Rectangle rect, TooltipCloseButtonState buttonState)
        {
            VisualStyleElement btn = GetCloseButtonVS(buttonState);
            Rectangle btnRect = GetCloseButtonRect(dc, rect, buttonState);
            if (IsDefined(btn))
            {
                VisualStyleRenderer renderer = new VisualStyleRenderer(btn);
                renderer.DrawBackground(dc, btnRect);
            }
            else
            {
                dc.DrawRectangle(Pens.Black, btnRect);
                dc.DrawLine(Pens.Black, btnRect.Location, new Point(btnRect.Right, btnRect.Bottom));
                dc.DrawLine(Pens.Black, new Point(btnRect.Right, btnRect.Top), new Point(btnRect.Left, btnRect.Bottom));
            }
        }

        public static VisualStyleElement GetCloseButtonVS(TooltipCloseButtonState buttonState)
        {
            VisualStyleElement btn;
            switch (buttonState)
            {
                case TooltipCloseButtonState.Hot:
                    btn = VisualStyleElement.ToolTip.Close.Hot;
                    break;
                case TooltipCloseButtonState.Pressed:
                    btn = VisualStyleElement.ToolTip.Close.Pressed;
                    break;
                case TooltipCloseButtonState.Normal:
                default:
                    btn = VisualStyleElement.ToolTip.Close.Normal;
                    break;
            }
            return btn;
        }


        private static bool IsDefined(VisualStyleElement element)
        {
            return Application.RenderWithVisualStyles && SafeNativeMethods.IsThemePartDefined(element.ClassName, element.Part, element.State);
        }

        public override Region GetRegion(Graphics dc, Rectangle rect)
        {
            Region ret;
            if (IsDefined(VisualStyleElement.ToolTip.Balloon.Normal))
            {
                VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.Balloon.Normal);
                ret = renderer.GetBackgroundRegion(dc, rect);
            }
            else
            {
                ret = new Region(rect);
                //var path = Create(0, 0, rect.Width, rect.Height, 10, RectangleCorners.All);
                //ret = new Region(path);
                //ret = WindowsNative.GetRoundRectRegion(0, 0, rect.Width, rect.Height, 20, 20);
            }
            return ret;
        }
    }
}
