using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormsPopupAlerts
{


    [ToolboxBitmapAttribute(@"e:\src\winformspopupalerts\WinFormsPopupAlerts\CustomTooltipAlertRenderer.bmp")]
    public class CustomTooltipAlertRenderer : CustomTooltipAlertRendererBase
    {

        private Color backColor;
        private Color titleForeColor;
        private Color foreColor;
        private Color borderColor;
        private Font font;
        private Font titleFont;
        private CornerRadius cornerRadius;
        private Brush backBrush;
        private Brush titleForeBrush;
        private Brush foreBrush;
        private Pen borderPen;
        private int borderThickness = 1;

        protected override void Draw(System.Drawing.Graphics dc, string title, string text, Rectangle rect, Rectangle titleRect, Rectangle bodyRect, Image img, int iconWidth)
        {
            Rectangle borderRect = new Rectangle(rect.X + borderThickness / 2, rect.Y + borderThickness / 2, rect.Width - borderThickness, rect.Height - borderThickness);
            dc.FillPath(backBrush, GetCapsule(borderRect, CornerRadius));
            dc.DrawPath(borderPen, GetCapsule(borderRect, CornerRadius));
            StringFormat format = new StringFormat(StringFormatFlags.NoWrap);
            format.Trimming = StringTrimming.EllipsisCharacter;
            dc.DrawString(title, titleFont, titleForeBrush,
                new RectangleF(Padding.Left + iconWidth, Padding.Top, Math.Min(titleRect.Width, bodyRect.Width), titleRect.Height), format);
            dc.DrawString(text, font, foreBrush,
                new RectangleF(Padding.Left + iconWidth, Padding.Top + titleRect.Height, bodyRect.Width, bodyRect.Height),
                new StringFormat());
        }

        protected override Rectangle GetBodyTextRect(System.Drawing.Graphics dc, string text, Rectangle rect)
        {
            SizeF size = dc.MeasureString(text, font, rect.Size.Width,
                new StringFormat());
            return new Rectangle(new Point(0, 0), Size.Ceiling(size));
        }

        protected override Rectangle GetTitleTextRect(System.Drawing.Graphics dc, string text, Rectangle rect)
        {
            StringFormat format = new StringFormat(StringFormatFlags.NoWrap);
            format.Trimming = StringTrimming.EllipsisCharacter;
            SizeF size = dc.MeasureString(text, titleFont, rect.Size.Width, format);
            return new Rectangle(new Point(0, 0), Size.Ceiling(size));
        }

        protected override Size GetCloseButtonSize(Graphics dc, TooltipCloseButtonState buttonState)
        {
            return new Size(10, 10);
        }

        public static GraphicsPath GetCapsule(RectangleF r, CornerRadius cr)
        {
            return RoundRect(r, cr.TopLeft, cr.TopRight, cr.BottomRight, cr.BottomLeft);
        }

        public static GraphicsPath RoundRect(RectangleF r, float r1, float r2, float r3, float r4)
        {
            float x = r.X, y = r.Y, w = r.Width, h = r.Height;
            GraphicsPath rr = new GraphicsPath();
            rr.AddBezier(x, y + r1, x, y, x + r1, y, x + r1, y);
            rr.AddLine(x + r1, y, x + w - r2, y);
            rr.AddBezier(x + w - r2, y, x + w, y, x + w, y + r2, x + w, y + r2);
            rr.AddLine(x + w, y + r2, x + w, y + h - r3);
            rr.AddBezier(x + w, y + h - r3, x + w, y + h, x + w - r3, y + h, x + w - r3, y + h);
            rr.AddLine(x + w - r3, y + h, x + r4, y + h);
            rr.AddBezier(x + r4, y + h, x, y + h, x, y + h - r4, x, y + h - r4);
            rr.AddLine(x, y + h - r4, x, y + r1);
            return rr;
        }

        public override Region GetRegion(Graphics dc, Rectangle rect)
        {
            return new Region(rect);
        }

        [Editor(typeof(CornerRadiusEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public CornerRadius CornerRadius
        {
            get { return cornerRadius; }
            set { cornerRadius = value; }
        }

        public Font TitleFont
        {
            get { return titleFont; }
            set { titleFont = value; }
        }

        public Font Font
        {
            get { return font; }
            set { font = value; }
        }


        public Color TitleForeColor
        {
            get { return titleForeColor; }
            set
            {
                titleForeColor = value;
                if (titleForeBrush != null)
                    titleForeBrush.Dispose();
                titleForeBrush = new SolidBrush(titleForeColor);
            }
        }


        public Color ForeColor
        {
            get { return foreColor; }
            set
            {
                foreColor = value;
                if (foreBrush != null)
                    foreBrush.Dispose();
                foreBrush = new SolidBrush(foreColor);
            }
        }

        public Color BackColor
        {
            get { return backColor; }
            set
            {
                backColor = value;
                if (backBrush != null)
                    backBrush.Dispose();
                backBrush = new SolidBrush(backColor);
            }
        }

        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                if (borderPen != null)
                    borderPen.Dispose();
                borderPen = new Pen(borderColor, borderThickness);
            }
        }

        public int BorderThickness
        {
            get
            {
                return borderThickness;
            }
            set
            {
                borderThickness = value;
                if (borderPen != null)
                    borderPen.Dispose();
                borderPen = new Pen(borderColor, borderThickness);
            }
        }

        [Browsable(false)]
        public override Color TransparencyKey
        {
            get { return Color.Cyan; }
        }
    }
}
