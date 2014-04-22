using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace WinFormsPopupAlerts
{
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
        public bool Equals(int r)
        {
            return r == TopLeft && r == TopRight && r == BottomLeft && r == BottomRight;
        }
        //public override bool Equals(object obj)
        //{
        //    if (obj is CornerRadius)
        //    {
        //        CornerRadius other = (CornerRadius)obj;
        //        return other.TopLeft == TopLeft && other.TopRight == TopRight && other.BottomLeft == BottomLeft && other.BottomRight == BottomRight;
        //    }
        //    else if (obj is int)
        //    {
        //        int r = (int)obj;
        //        return r == TopLeft && r == TopRight && r == BottomLeft && r == BottomRight;
        //    }
        //    throw new ArgumentException();
        //}
        //
        //public override int GetHashCode()
        //{
        //    string s = string.Format("{0:00}{1:00}{2:00}{3:00}", TopLeft, TopRight, BottomLeft, BottomRight);
        //    if (s.Length > 8)
        //        s = s.Substring(1, 8);
        //    return int.Parse(s);
        //}
    }
}
