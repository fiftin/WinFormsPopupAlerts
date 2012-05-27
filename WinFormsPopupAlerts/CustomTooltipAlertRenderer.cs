using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;

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

    public class CustomTooltipAlertRenderer : TooltipAlertRenderer
    {
        public override void Draw(System.Drawing.Graphics dc, string title, string text, TooltipAlertIcon icon = TooltipAlertIcon.None, System.Drawing.Image customIcon = null)
        {
            throw new NotImplementedException();
        }

        public override System.Drawing.Rectangle GetBodyRect(System.Drawing.Graphics dc, string title, string text, TooltipAlertIcon icon = TooltipAlertIcon.None, System.Drawing.Image customIcon = null)
        {
            throw new NotImplementedException();
        }

        public override System.Drawing.Rectangle GetTitleRect(System.Drawing.Graphics dc, string title, string text, TooltipAlertIcon icon = TooltipAlertIcon.None, System.Drawing.Image customIcon = null)
        {
            throw new NotImplementedException();
        }

        public override System.Drawing.Rectangle GetRect(System.Drawing.Graphics dc, System.Drawing.Rectangle titleRect, System.Drawing.Rectangle bodyRect, TooltipAlertIcon icon = TooltipAlertIcon.None, System.Drawing.Image customIcon = null)
        {
            throw new NotImplementedException();
        }

        public override System.Drawing.Rectangle GetCloseButtonRect(System.Drawing.Graphics dc, System.Drawing.Rectangle rect, TooltipCloseButtonState buttonState)
        {
            throw new NotImplementedException();
        }

        public override void DrawCloseButton(System.Drawing.Graphics dc, System.Drawing.Rectangle rect, TooltipCloseButtonState buttonState)
        {
            throw new NotImplementedException();
        }

        public override System.Drawing.Region GetRegion(System.Drawing.Graphics dc, System.Drawing.Rectangle rect)
        {
            throw new NotImplementedException();
        }

        private Color backColor;
        private Color titleBackColor;
        private Color titleForeColor;
        private Color foreColor;
        private Font font;
        private Font titleFont;
        private CornerRadius cornerRadius;

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
            set { titleForeColor = value; }
        }

        public Color TitleBackColor
        {
            get { return titleBackColor; }
            set { titleBackColor = value; }
        }


        public Color ForeColor
        {
            get { return foreColor; }
            set { foreColor = value; }
        }

        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }


    }
}
