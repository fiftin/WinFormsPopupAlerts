using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace WinFormsPopupAlerts
{
    [TypeConverter(typeof(CornerRadiusConverter))]
    public struct CornerRadius
	{
        public CornerRadius(int topLeft, int topRight, int bottomRight, int bottomLeft)
            : this()
        {
            this.topLeft = topLeft;
            this.topRight = topRight;
            this.bottomRight = bottomRight;
            this.bottomLeft = bottomLeft;
        }
        
        private int bottomLeft;
        private int bottomRight;
        private int topLeft;
        private int topRight;

        public int TopRight
        {
            get { return topRight; }
            set { topRight = value; }
        }

        public int TopLeft
        {
            get { return topLeft; }
            set { topLeft = value; }
        }

        public int BottomRight
        {
            get { return bottomRight; }
            set { bottomRight = value; }
        }

        public int BottomLeft
        {
            get { return bottomLeft; }
            set { bottomLeft = value; }
        }
	}

    [ToolboxBitmapAttribute(@"e:\src\winformspopupalerts\WinFormsPopupAlerts\CustomTooltipAlertRenderer.bmp")]
    public class CustomTooltipAlertRenderer : CustomTooltipAlertRendererBase
    {

        private Color backColor;
        private Color titleBackColor;
        private Color titleForeColor;
        private Color foreColor;
        private Color borderColor;
        private Font font;
        private Font titleFont;
        private CornerRadius cornerRadius;
        private Brush backBrush;
        private Brush titleBackBrush;
        private Brush titleForeBrush;
        private Brush foreBrush;
        private Pen borderPen;
        private int borderThickness = 1;

        protected override void Draw(System.Drawing.Graphics dc, string title, string text, Rectangle rect, Rectangle titleRect, Rectangle bodyRect, Image img, int iconWidth)
        {
            dc.SmoothingMode = SmoothingMode.HighQuality;
            dc.FillRectangle(backBrush, rect);
            Rectangle borderRect = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
            Rectangle borderRect2 = new Rectangle(rect.X, rect.Y - 1, rect.Width, rect.Height);
            Rectangle borderRect3 = new Rectangle(rect.X - 1, rect.Y, rect.Width, rect.Height);
            dc.DrawPath(borderPen, GetCapsule(borderRect, CornerRadius));
            dc.DrawPath(borderPen, GetCapsule(borderRect2, CornerRadius));
            dc.DrawPath(borderPen, GetCapsule(borderRect3, CornerRadius));
            dc.DrawString(title, titleFont, titleForeBrush,
                new PointF(Padding.Left + iconWidth, Padding.Top), new StringFormat(StringFormatFlags.NoWrap));
            dc.DrawString(text, font, foreBrush,
                new RectangleF(Padding.Left + iconWidth, Padding.Top + titleRect.Height, bodyRect.Width, bodyRect.Height),
                new StringFormat());
        }

        protected override Rectangle getBodyTextRect(System.Drawing.Graphics dc, string text, Rectangle rect)
        {
            SizeF size = dc.MeasureString(text, font, rect.Size.Width,
                new StringFormat());
            return new Rectangle(new Point(0, 0), Size.Ceiling(size));
        }

        protected override Rectangle getTitleTextRect(System.Drawing.Graphics dc, string text, Rectangle rect)
        {
            SizeF size = dc.MeasureString(text, titleFont, rect.Size.Width,
                new StringFormat(StringFormatFlags.NoWrap));
            return new Rectangle(new Point(0, 0), Size.Ceiling(size));
        }

        protected override Size getCloseButtonSize(Graphics dc, TooltipCloseButtonState buttonState)
        {
            return new Size(10, 10);
        }

        private static GraphicsPath GetCapsule(RectangleF rect, CornerRadius cornerRadius)
        {
            GraphicsPath ret = new GraphicsPath();
            ret.AddArc(rect.X, rect.Y, cornerRadius.TopLeft, cornerRadius.TopLeft, 180, 90);
            
            ret.AddArc(rect.X + rect.Width - cornerRadius.TopRight, rect.Y, cornerRadius.TopRight, cornerRadius.TopRight, 270, 90);
            ret.AddArc(rect.X + rect.Width - cornerRadius.BottomRight, rect.Y + rect.Height - cornerRadius.BottomRight, cornerRadius.BottomRight, cornerRadius.BottomRight, 0, 90);
            ret.AddArc(rect.X, rect.Y + rect.Height - cornerRadius.BottomLeft, cornerRadius.BottomLeft, cornerRadius.BottomLeft, 90, 90);
            ret.CloseFigure(); 
            return ret;
        } 

        public override Region GetRegion(Graphics dc, Rectangle rect)
        {
            if (CornerRadius.BottomLeft == 0 && CornerRadius.TopLeft == 0 && CornerRadius.TopRight == 0 && CornerRadius.BottomRight == 0)
                return base.GetRegion(dc, rect);
            return new Region(GetCapsule(rect, CornerRadius));

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

        public Color TitleBackColor
        {
            get { return titleBackColor; }
            set
            {
                titleBackColor = value;
                if (titleBackBrush != null)
                    titleBackBrush.Dispose();
                titleBackBrush = new SolidBrush(titleBackColor);
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

    }
}
