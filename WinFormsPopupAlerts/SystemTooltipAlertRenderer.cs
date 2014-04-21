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
    public class SystemTooltipAlertRenderer : CustomTooltipAlertRendererBase
    {
        private static bool IsDefined(VisualStyleElement element)
        {
            return Application.RenderWithVisualStyles && SafeNativeMethods.IsThemePartDefined(element.ClassName, element.Part, element.State);
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
                base.DrawCloseButton(dc, rect, buttonState);
        }

        protected override void Draw(Graphics dc, string title, string text, Rectangle rect, Rectangle titleRect, Rectangle bodyRect, Image img, int iconWidth)
        {
            if (IsDefined(VisualStyleElement.ToolTip.BalloonTitle.Normal) && IsDefined(VisualStyleElement.ToolTip.Balloon.Normal))
            {
                VisualStyleRenderer titleRenderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.BalloonTitle.Normal);
                VisualStyleRenderer balloonRenderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.Balloon.Normal);
                balloonRenderer.DrawBackground(dc, rect);
                // drawing title
                titleRenderer.DrawText(dc,
                    new Rectangle(Padding.Left + iconWidth, Padding.Top, rect.Width - iconWidth - (Padding.Left + Padding.Right), titleRect.Height),
                    title, false, TextFormatFlags.Left | TextFormatFlags.WordEllipsis | TextFormatFlags.VerticalCenter);
                // drawing text
                Rectangle balloonTextBounds = new Rectangle(Padding.Left + iconWidth, Padding.Top + titleRect.Height, rect.Width - iconWidth - (Padding.Left + Padding.Right), rect.Height - (Padding.Top + titleRect.Height + Padding.Bottom));
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
        }

        protected override Rectangle GetBodyTextRect(Graphics dc, string text, Rectangle rect)
        {
            if (IsDefined(VisualStyleElement.ToolTip.Balloon.Normal))
            {
                VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.Balloon.Normal);
                return renderer.GetTextExtent(dc, rect, text, TextFormatFlags.Left | TextFormatFlags.WordBreak | TextFormatFlags.VerticalCenter);
            }
            else
            {
                SizeF size = dc.MeasureString(text, SystemFonts.DefaultFont, rect.Size.Width,
                    new StringFormat());
                return new Rectangle(new Point(0, 0), Size.Ceiling(size));
            }
        }

        protected override Rectangle GetTitleTextRect(Graphics dc, string title, Rectangle rect)
        {
            if (IsDefined(VisualStyleElement.ToolTip.BalloonTitle.Normal))
            {
                VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.BalloonTitle.Normal);
                return renderer.GetTextExtent(dc, rect, title, TextFormatFlags.Left | TextFormatFlags.WordEllipsis | TextFormatFlags.VerticalCenter);
            }
            else
            {
                SizeF size = dc.MeasureString(title, new Font(SystemFonts.DefaultFont, FontStyle.Bold), rect.Size.Width,
                    new StringFormat(StringFormatFlags.NoWrap));
                return new Rectangle(new Point(0, 0), Size.Ceiling(size));
            }
        }

        protected override Size GetCloseButtonSize(Graphics dc, TooltipCloseButtonState buttonState)
        {
            VisualStyleElement btn = GetCloseButtonVS(buttonState);
            if (IsDefined(btn))
            {
                VisualStyleRenderer renderer = new VisualStyleRenderer(btn);
                return renderer.GetPartSize(dc, ThemeSizeType.True);
            }
            else
                return new Size(10, 10);
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
                ret = new Region(rect);
            return ret;
        }

        public override Color TransparencyKey
        {
            get { return Color.Transparent; }
        }
    }
}
