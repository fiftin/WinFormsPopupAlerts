using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace WinFormsPopupAlerts
{
    public class BasicPopupAlert : PopupAlertBase
    {
        public BasicPopupAlert()
        {

        }

        public BasicPopupAlert(object[] args)
            : base(args)
        {

        }

        public override void Hide(Action<PopupAlertBase> callback)
        {
            if (HiddingStyle == HiddingStyle.Fade)
            {
                Action<PopupAlertBase> hidding = new Action<PopupAlertBase>(delegate(PopupAlertBase obj)
                {
                    float v = 1f / (float)HiddingDuration;
                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    while (obj.Opacity > 0)
                    {
                        int msec = (int)sw.ElapsedMilliseconds;
                        obj.Invoke(new MethodInvoker(delegate() { obj.Opacity = 1 - v * msec; }));
                        System.Threading.Thread.Sleep(10);
                    }
                    obj.Invoke(new MethodInvoker(delegate() { ((Control)obj).Hide(); }));
                    callback(obj);
                });
                hidding.BeginInvoke(this, null, null);
            }
            else if (HiddingStyle == HiddingStyle.Simple)
            {
                ((Control)this).Hide();
            }
            else
            {
                throw new Exception("Unknown HiddingStyle");
            }

        }

        /*
        private GraphicsPath GetCapsule(RectangleF baseRect)
        {
            float diameter;
            RectangleF arc;
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            try
            {
                if (baseRect.Width > baseRect.Height)
                {
                    // return horizontal capsule 
                    diameter = baseRect.Height;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 90, 180);
                    arc.X = baseRect.Right - diameter;
                    path.AddArc(arc, 270, 180);
                }
                else if (baseRect.Width < baseRect.Height)
                {
                    // return vertical capsule 
                    diameter = baseRect.Width;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 180, 180);
                    arc.Y = baseRect.Bottom - diameter;
                    path.AddArc(arc, 0, 180);
                }
                else
                {
                    // return circle 
                    path.AddEllipse(baseRect);
                }
            }
            catch (Exception)
            {
                path.AddEllipse(baseRect);
            }
            finally
            {
                path.CloseFigure();
            }
            return path;
        }

        protected GraphicsPath GetRoundedRect(RectangleF baseRect, float radius)
        {
            // if corner radius is less than or equal to zero, 
            // return the original rectangle 
            if (radius <= 0.0F)
            {
                GraphicsPath mPath = new GraphicsPath();
                mPath.AddRectangle(baseRect);
                mPath.CloseFigure();
                return mPath;
            }

            // if the corner radius is greater than or equal to 
            // half the width, or height (whichever is shorter) 
            // then return a capsule instead of a lozenge 
            if (radius >= (Math.Min(baseRect.Width, baseRect.Height)) / 2.0)
                return GetCapsule(baseRect);

            // create the arc for the rectangle sides and declare 
            // a graphics path object for the drawing 
            float diameter = radius * 2.0F;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            // top left arc 
            path.AddArc(arc, 180, 90);

            // top right arc 
            arc.X = baseRect.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc 
            arc.Y = baseRect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc
            arc.X = baseRect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
         */
        protected override void BeforeShown()
        {
            if (ShowingStyle == WinFormsPopupAlerts.ShowingStyle.Fade)
            {
                this.Opacity = 0;
                Action<PopupAlertBase> showing = new Action<PopupAlertBase>(delegate(PopupAlertBase obj)
                {
                    float v = 1f / (float)ShowingDuration;
                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    while (obj.Opacity < 1)
                    {
                        int msec = (int)sw.ElapsedMilliseconds;
                        obj.Invoke(new MethodInvoker(delegate() { obj.Opacity = v * msec; }));
                        System.Threading.Thread.Sleep(10);
                    }
                });
                showing.BeginInvoke(this, null, null);
            }
            /*
            if (RoundedCornerRadius > 0)
            {
                GraphicsPath path = GetRoundedRect(ClientRectangle, RoundedCornerRadius);
                Region = new Region(path);
            }
             */
        }

        private HiddingStyle hiddingStyle = HiddingStyle.Fade;
        private ShowingStyle showingStyle = ShowingStyle.Fade;
        //private int roundedCornerRadius = 5;
        private int showingDuration = 300;
        private int hiddingDuration =  300;
        private Padding padding = new Padding(5, 5, 5, 5);

        public new virtual Padding Padding
        {
            get { return padding; }
            set { padding = value; }
        }

        public int HiddingDuration
        {
            get { return hiddingDuration; }
            set { hiddingDuration = value; }
        }

        public int ShowingDuration
        {
            get { return showingDuration; }
            set { showingDuration = value; }
        }
        /*
        public int RoundedCornerRadius
        {
            get { return roundedCornerRadius; }
            set
            {
                roundedCornerRadius = value;
            }
        }
         */

        /// <summary>
        /// 
        /// </summary>
        public ShowingStyle ShowingStyle
        {
            get { return showingStyle; }
            set { showingStyle = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public HiddingStyle HiddingStyle
        {
            get { return hiddingStyle; }
            set { hiddingStyle = value; }
        }

    }
}
