using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace WinFormsPopupAlerts
{
    /// <summary>
    /// Represents the radii of a rectangle's corners.
    /// </summary>
    [TypeConverter(typeof(CornerRadiusConverter))]
    public class CornerRadius
    {

        public CornerRadius()
        {
        }
        public CornerRadius(int topLeft, int topRight, int bottomLeft, int bottomRight)
            : this()
        {
            this.topLeft = topLeft;
            this.topRight = topRight;
            this.bottomRight = bottomRight;
            this.bottomLeft = bottomLeft;
        }

        public CornerRadius(int radius)
            : this(radius, radius, radius, radius)
        {
        }

        private int bottomLeft;
        private int bottomRight;
        private int topLeft;
        private int topRight;

        /// <summary>
        /// Gets or sets the radius of the top-right corner. 
        /// </summary>
        public int TopRight
        {
            get { return topRight; }
            set { topRight = value; }
        }

        /// <summary>
        /// Gets or sets the radius of the top-left corner. 
        /// </summary>
        public int TopLeft
        {
            get { return topLeft; }
            set { topLeft = value; }
        }

        /// <summary>
        /// Gets or sets the radius of the bottom-right corner. 
        /// </summary>
        public int BottomRight
        {
            get { return bottomRight; }
            set { bottomRight = value; }
        }

        /// <summary>
        /// Gets or sets the radius of the bottom-left corner. 
        /// </summary>
        public int BottomLeft
        {
            get { return bottomLeft; }
            set { bottomLeft = value; }
        }


        public bool Equals(int r)
        {
            return r == TopLeft && r == TopRight && r == BottomLeft && r == BottomRight;
        }
    }
}
