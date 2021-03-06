﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace WinFormsPopupAlerts
{

    public enum ToolTipBalloonCloseButtonState
    {
        Normal,
        Hot,
        Pressed,
    }

    public static class ToolTipBalloonDrawingHelper
    {

        public static void Draw(IDeviceContext dc, Size minSize, Size maxSize, string title, string text, ToolTipIcon icon, Padding padding)
        {
            Rectangle titleRect;
            Rectangle rect = GetRect(dc, minSize, maxSize, title, text, out titleRect, icon, padding);
            Draw(dc, minSize, maxSize, title, text, titleRect, rect, icon, padding);
        }

        public static void Draw(IDeviceContext dc, Size minSize, Size maxSize, string title, string text, Rectangle titleRect, ToolTipIcon icon, Padding padding)
        {
            Rectangle rect = GetRect(dc, minSize, maxSize, title, text, titleRect, icon, padding);
            Draw(dc, minSize, maxSize, title, text, titleRect, rect, icon, padding);
        }

        public static Rectangle GetRect(IDeviceContext dc, Size minSize, Size maxSize, string title, string text, ToolTipIcon icon, Padding padding)
        {
            Rectangle titleRect;
            return GetRect(dc, minSize, maxSize, title, text, out titleRect, icon, padding);
        }

        public static Rectangle GetRect(IDeviceContext dc, Size minSize, Size maxSize, string title, string text, out Rectangle titleRect, ToolTipIcon icon, Padding padding)
        {
            Rectangle bodyRect;
            return GetRect(dc, minSize, maxSize, title, text, out titleRect, out bodyRect, icon, padding);
        }

        public static Rectangle GetRect(IDeviceContext dc, Size minSize, Size maxSize, string title, string text, Rectangle titleRect, out Rectangle bodyRect, ToolTipIcon icon, Padding padding)
        {
            bodyRect = GetBodyRect(dc, minSize, maxSize, text, titleRect, icon, padding);
            return GetRect(dc, minSize, maxSize, title, text, titleRect, bodyRect, icon, padding);
        }

        public static Rectangle GetRect(IDeviceContext dc, Size minSize, Size maxSize, string title, string text, Rectangle titleRect, ToolTipIcon icon, Padding padding)
        {
            Rectangle bodyRect = GetBodyRect(dc, minSize, maxSize, text, titleRect, icon, padding);
            return GetRect(dc, minSize, maxSize, title, text, titleRect, bodyRect, icon, padding);
        }

        public static Rectangle GetRect(IDeviceContext dc, Size minSize, Size maxSize, string title, string text, out Rectangle titleRect, out Rectangle bodyRect, ToolTipIcon icon, Padding padding)
        {
            titleRect = GetTitleRect(dc, minSize, maxSize, title, icon, padding);
            return GetRect(dc, minSize, maxSize, title, text, titleRect, out bodyRect, icon, padding);
        }

        private static VisualStyleElement GetCloseButtonVS(ToolTipBalloonCloseButtonState buttonState)
        {
            VisualStyleElement btn;
            switch (buttonState)
            {
                case ToolTipBalloonCloseButtonState.Hot:
                    btn = VisualStyleElement.ToolTip.Close.Hot;
                    break;
                case ToolTipBalloonCloseButtonState.Pressed:
                    btn = VisualStyleElement.ToolTip.Close.Pressed;
                    break;
                case ToolTipBalloonCloseButtonState.Normal:
                default:
                    btn = VisualStyleElement.ToolTip.Close.Normal;
                    break;
            }
            return btn;
        }

        public static Rectangle GetCloseButtonRect(IDeviceContext dc, Rectangle rect, Padding padding, ToolTipBalloonCloseButtonState buttonState)
        {
            VisualStyleElement btn = GetCloseButtonVS(buttonState);
            VisualStyleRenderer renderer = new VisualStyleRenderer(btn);
            Size btnSize = renderer.GetPartSize(dc, ThemeSizeType.True);
            Point btnPos = new Point(rect.Right - padding.Right - btnSize.Width, rect.Top + padding.Top);
            Rectangle btnRect = new Rectangle(btnPos, btnSize);
            return btnRect;
        }

        public static void DrawCloseButton(IDeviceContext dc, Rectangle rect, Padding padding, ToolTipBalloonCloseButtonState buttonState)
        {
            VisualStyleElement btn = GetCloseButtonVS(buttonState);
            VisualStyleRenderer renderer = new VisualStyleRenderer(btn);
            Rectangle btnRect = GetCloseButtonRect(dc, rect, padding, buttonState);
            renderer.DrawBackground(dc, btnRect);
        }


        public static Rectangle GetRect(IDeviceContext dc, Size minSize, Size maxSize, string title, string text, Rectangle titleRect, Rectangle bodyRect, ToolTipIcon icon, Padding padding)
        {
            Rectangle ret = new Rectangle(0, 0, Math.Max(titleRect.Width, bodyRect.Width), titleRect.Height + bodyRect.Height);
            ret.Width += padding.Left + padding.Right;
            ret.Height += padding.Top + padding.Bottom;
            if (icon != ToolTipIcon.None)
            {
                throw new NotImplementedException();
            }
            return ret;
        }

        public static Rectangle GetTitleRect(IDeviceContext dc, Size minSize, Size maxSize, string title, ToolTipIcon icon, Padding padding)
        {
            Rectangle ret;
            if (title == null)
            {
                ret = new Rectangle(new Point(0, 0), minSize);
            }
            else
            {
                ret = new Rectangle(new Point(0, 0), maxSize);
                ret.Width -= padding.Horizontal;
                if (Application.RenderWithVisualStyles)
                {
                    if (icon != ToolTipIcon.None)
                        throw new NotImplementedException();

                    VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.BalloonTitle.Normal);
                    Rectangle rect = renderer.GetTextExtent(dc, ret, title, TextFormatFlags.Left | TextFormatFlags.WordEllipsis | TextFormatFlags.VerticalCenter);
                    
                    
                    if (rect.Width + padding.Horizontal > maxSize.Width)
                        ret.Width = maxSize.Width;
                    else if (rect.Width + padding.Horizontal < minSize.Width)
                        ret.Width = minSize.Width;
                    else
                        ret.Width = rect.Width;

                    if (rect.Height > maxSize.Height)
                        ret.Height = maxSize.Height;
                    else
                        ret.Height = rect.Height;
                     

                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            return ret;
        }

        public static Rectangle GetBodyRect(IDeviceContext dc, Size minSize, Size maxSize, string text, Rectangle titleRect, ToolTipIcon icon, Padding padding)
        {
            Rectangle ret;
            if (text == null)
            {
                ret = new Rectangle(new Point(0, 0), minSize);
            }
            else
            {
                ret = new Rectangle(new Point(0, 0), maxSize);
                ret.Width -= padding.Horizontal;

                if (Application.RenderWithVisualStyles)
                {
                    VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.Balloon.Normal);
                    Rectangle rect = renderer.GetTextExtent(dc, ret, text, TextFormatFlags.Left | TextFormatFlags.WordBreak | TextFormatFlags.VerticalCenter);

                    
                    if (rect.Width + padding.Horizontal > maxSize.Width)
                        ret.Width = maxSize.Width;
                    else if (rect.Width + padding.Horizontal < minSize.Width)
                        ret.Width = minSize.Width;
                    else
                        ret.Width = rect.Width;


                    if (rect.Height > maxSize.Height)
                        ret.Height = maxSize.Height;
                    else
                        ret.Height = rect.Height;
                     

                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            return ret;
        }

        public static Region GetRegion(IDeviceContext dc, Size minSize, Size maxSize, string title, string text, ToolTipIcon icon, Padding padding)
        {
            Region ret;
            Rectangle titleRect;
            Rectangle rect = GetRect(dc, minSize, maxSize, title, text, out titleRect, icon, padding);
            if (Application.RenderWithVisualStyles)
            {
                VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.Balloon.Normal);
                ret = renderer.GetBackgroundRegion(dc, rect);
            }
            else
            {
                throw new NotImplementedException();
            }
            return ret;
        }

        public static void Draw(IDeviceContext dc, Size minSize, Size maxSize, string title, string text, Rectangle titleRect, Rectangle rect, ToolTipIcon icon, Padding padding)
        {
            if (Application.RenderWithVisualStyles)
            {
                VisualStyleRenderer titleRenderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.BalloonTitle.Normal);
                VisualStyleRenderer balloonRenderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.Balloon.Normal);
                balloonRenderer.DrawBackground(dc, rect);

                if (icon == ToolTipIcon.None)
                {
                    titleRenderer.DrawText(dc, new Rectangle(padding.Left, padding.Top, rect.Width - (padding.Left + padding.Right), titleRect.Height),
                        title, false, TextFormatFlags.Left | TextFormatFlags.WordEllipsis | TextFormatFlags.VerticalCenter);

                    Rectangle balloonTextBounds = new Rectangle(padding.Left, padding.Top + titleRect.Height, rect.Width - (padding.Left + padding.Right), rect.Height - (padding.Top + titleRect.Height + padding.Bottom));
                    
                    balloonRenderer.DrawText(dc, balloonTextBounds,
                        text, false, TextFormatFlags.Left | TextFormatFlags.WordBreak | TextFormatFlags.VerticalCenter);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }



    }
}
