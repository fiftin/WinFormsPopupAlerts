using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WinFormsPopupAlerts
{
    public class SystemTooltipAlertRenderer : TooltipAlertRenderer
    {


        public enum RectangleCorners
        {
            None = 0, TopLeft = 1, TopRight = 2, BottomLeft = 4, BottomRight = 8,
            All = TopLeft | TopRight | BottomLeft | BottomRight
        }

        public static GraphicsPath Create(int x, int y, int width, int height,
                                          int radius, RectangleCorners corners)
        {
            int xw = x + width;
            int yh = y + height;
            int xwr = xw - radius;
            int yhr = yh - radius;
            int xr = x + radius;
            int yr = y + radius;
            int r2 = radius * 2;
            int xwr2 = xw - r2;
            int yhr2 = yh - r2;

            GraphicsPath p = new GraphicsPath();
            p.StartFigure();

            //Top Left Corner
            if ((RectangleCorners.TopLeft & corners) == RectangleCorners.TopLeft)
            {
                p.AddArc(x, y, r2, r2, 180, 90);
            }
            else
            {
                p.AddLine(x, yr, x, y);
                p.AddLine(x, y, xr, y);
            }

            //Top Edge
            p.AddLine(xr, y, xwr, y);

            //Top Right Corner
            if ((RectangleCorners.TopRight & corners) == RectangleCorners.TopRight)
            {
                p.AddArc(xwr2, y, r2, r2, 270, 90);
            }
            else
            {
                p.AddLine(xwr, y, xw, y);
                p.AddLine(xw, y, xw, yr);
            }

            //Right Edge
            p.AddLine(xw, yr, xw, yhr);

            //Bottom Right Corner
            if ((RectangleCorners.BottomRight & corners) == RectangleCorners.BottomRight)
            {
                p.AddArc(xwr2, yhr2, r2, r2, 0, 90);
            }
            else
            {
                p.AddLine(xw, yhr, xw, yh);
                p.AddLine(xw, yh, xwr, yh);
            }

            //Bottom Edge
            p.AddLine(xwr, yh, xr, yh);

            //Bottom Left Corner
            if ((RectangleCorners.BottomLeft & corners) == RectangleCorners.BottomLeft)
            {
                p.AddArc(x, yhr2, r2, r2, 90, 90);
            }
            else
            {
                p.AddLine(xr, yh, x, yh);
                p.AddLine(x, yh, x, yhr);
            }

            //Left Edge
            p.AddLine(x, yhr, x, yr);

            p.CloseFigure();
            return p;
        }

        public override void Draw(Graphics dc, string title, string text, TooltipAlertIcon icon = TooltipAlertIcon.None, System.Drawing.Icon customIcon = null)
        {
            Rectangle titleRect = GetTitleRect(dc, title, text, icon, customIcon);
            Rectangle bodyRect = GetBodyRect(dc, title, text, icon, customIcon);
            Rectangle rect = GetRect(dc, titleRect, bodyRect, icon, customIcon);
            BufferedGraphicsContext currentContext;
            BufferedGraphics myBuffer;
            currentContext = BufferedGraphicsManager.Current;
            myBuffer = currentContext.Allocate(dc, rect);

            if (IsDefined(VisualStyleElement.ToolTip.BalloonTitle.Normal) && IsDefined(VisualStyleElement.ToolTip.Balloon.Normal))
            {
                VisualStyleRenderer titleRenderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.BalloonTitle.Normal);
                VisualStyleRenderer balloonRenderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.Balloon.Normal);
                balloonRenderer.DrawBackground(myBuffer.Graphics, rect);
                if (icon == TooltipAlertIcon.None)
                {
                    titleRenderer.DrawText(myBuffer.Graphics, new Rectangle(Padding.Left, Padding.Top, rect.Width - (Padding.Left + Padding.Right), titleRect.Height),
                        title, false, TextFormatFlags.Left | TextFormatFlags.WordEllipsis | TextFormatFlags.VerticalCenter);
                    Rectangle balloonTextBounds = new Rectangle(Padding.Left, Padding.Top + titleRect.Height, rect.Width - (Padding.Left + Padding.Right), rect.Height - (Padding.Top + titleRect.Height + Padding.Bottom));
                    balloonRenderer.DrawText(myBuffer.Graphics, balloonTextBounds,
                        text, false, TextFormatFlags.Left | TextFormatFlags.WordBreak | TextFormatFlags.VerticalCenter);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                myBuffer.Graphics.FillRectangle(SystemBrushes.Info, rect);
                myBuffer.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, rect.Width - 1, rect.Height - 1));
                if (icon == TooltipAlertIcon.None)
                {
                    myBuffer.Graphics.DrawString(title, new Font(SystemFonts.DefaultFont, FontStyle.Bold), SystemBrushes.InfoText,
                        new PointF(Padding.Left, Padding.Top), new StringFormat(StringFormatFlags.NoWrap));
                    myBuffer.Graphics.DrawString(text, SystemFonts.DefaultFont, SystemBrushes.InfoText,
                        new RectangleF(Padding.Left, Padding.Top + titleRect.Height, bodyRect.Width, bodyRect.Height),
                        new StringFormat());
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            myBuffer.Render();
        }

        public override System.Drawing.Rectangle GetBodyRect(Graphics dc, string title, string text, TooltipAlertIcon icon = TooltipAlertIcon.None, System.Drawing.Icon customIcon = null)
        {
            Rectangle ret;
            if (text == null)
            {
                ret = new Rectangle(new Point(0, 0), MinSize);
            }
            else
            {
                ret = new Rectangle(new Point(0, 0), MaxSize);
                ret.Width -= Padding.Horizontal;
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

        public override System.Drawing.Rectangle GetTitleRect(Graphics dc, string title, string text, TooltipAlertIcon icon = TooltipAlertIcon.None, System.Drawing.Icon customIcon = null)
        {
            Rectangle ret;
            if (title == null)
            {
                ret = new Rectangle(new Point(0, 0), MinSize);
            }
            else
            {
                ret = new Rectangle(new Point(0, 0), MaxSize);
                ret.Width -= Padding.Horizontal;

                Rectangle rect;
                if (IsDefined(VisualStyleElement.ToolTip.BalloonTitle.Normal))
                {
                    VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.BalloonTitle.Normal);
                    rect = renderer.GetTextExtent(dc, ret, title, TextFormatFlags.Left | TextFormatFlags.WordEllipsis | TextFormatFlags.VerticalCenter);
                }
                else
                {
                    SizeF size = dc.MeasureString(title, new Font(SystemFonts.DefaultFont, FontStyle.Bold), ret.Size.Width,
                        new StringFormat(System.Drawing.StringFormatFlags.NoWrap));
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

        public override System.Drawing.Rectangle GetRect(Graphics dc, System.Drawing.Rectangle titleRect, System.Drawing.Rectangle bodyRect, TooltipAlertIcon icon = TooltipAlertIcon.None, System.Drawing.Icon customIcon = null)
        {
            return SystemTooltipRenderingHelper.GetRect(dc, MinSize, MaxSize, titleRect, bodyRect, icon, Padding);
        }

        public override System.Drawing.Rectangle GetCloseButtonRect(Graphics dc, System.Drawing.Rectangle rect, TooltipCloseButtonState buttonState)
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

        public override void DrawCloseButton(Graphics dc, System.Drawing.Rectangle rect, TooltipCloseButtonState buttonState)
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

        public override System.Drawing.Region GetRegion(Graphics dc, System.Drawing.Rectangle rect)
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
