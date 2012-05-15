using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace WinFormsPopupAlerts
{
    public class BasicPopupAlert : PopupAlert
    {
        public BasicPopupAlert()
        {

        }

        public BasicPopupAlert(object[] args)
            : base(args)
        {

        }

        public override void Hide(Action<PopupAlert> callback)
        {
            if (HiddingStyle == HiddingStyle.Fade)
            {
                Action<PopupAlert> hidding = new Action<PopupAlert>(delegate(PopupAlert obj)
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
                    obj.Invoke(new MethodInvoker(delegate() { obj.InternalHide(); }));
                    callback(obj);
                });
                hidding.BeginInvoke(this, null, null);
            }
            else if (HiddingStyle == WinFormsPopupAlerts.HiddingStyle.Slide)
            {
                Action<PopupAlert> hidding = new Action<PopupAlert>(delegate(PopupAlert obj)
                {
                    int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                    int startLeft = obj.Left;
                    float v = (screenWidth - obj.Left) / (float)HiddingDuration;
                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    while (screenWidth - obj.Left > 0)
                    {
                        int msec = (int)sw.ElapsedMilliseconds;
                        obj.Invoke(new MethodInvoker(delegate() { obj.Left = (int)((float)startLeft + v * msec); }));
                        System.Threading.Thread.Sleep(10);
                    }
                    obj.Invoke(new MethodInvoker(delegate() { obj.InternalHide(); }));
                    callback(obj);
                });
                hidding.BeginInvoke(this, null, null);
            }
            else if (HiddingStyle == HiddingStyle.Simple)
            {
                this.InternalHide();
            }
            else
            {
                throw new Exception("Unknown HiddingStyle");
            }

        }

        protected override void BeforeShown()
        {
            if (ShowingStyle == WinFormsPopupAlerts.ShowingStyle.Fade)
            {
                this.Opacity = 0;
                Action<PopupAlert> showing = new Action<PopupAlert>(delegate(PopupAlert obj)
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

        }

        private HiddingStyle hiddingStyle = HiddingStyle.Fade;
        private ShowingStyle showingStyle = ShowingStyle.Fade;
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
