using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WinFormsPopupAlerts
{
    /// <summary>
    /// Base class for each alert window.
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    public abstract partial class Alert : TopFormBase
    {
        private int top;
        private int left;
        private AlertAlignment align;
        private HidingStyle hidingStyle = HidingStyle.Fade;
        private ShowingStyle showingStyle = ShowingStyle.Fade;
        private int showingDuration = 300;
        private int hidingDuration = 300;
        private Padding padding = new Padding(5, 5, 5, 5);

        protected Alert(AlertAlignment align)
        {
            InitializeComponent();
            this.align = align;
        }

        /// <summary>
        /// Shows the alert in specified position.
        /// </summary>
        public void Show(int top, int left)
        {
            this.top = top;
            this.left = left;
            Show();
        }

        protected internal void InternalHide()
        {
            Hide();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                Location = new Point(left, top);
                BeforeShown();
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (!DesignMode)
            {
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Hide(delegate(Alert alert) { });
        }


        /// <summary>
        /// Executes the specified delegate on the thread that owns the control's underlying
        /// window handle.
        /// </summary>
        /// <param name="method">A delegate that contains a method to be called in the control's thread context.</param>
        /// <returns>
        /// The return value from the delegate being invoked, or null if the delegate
        /// has no return value.
        /// </returns>
        public new object Invoke(Delegate method)
        {
            if (IsDisposed)
                return null;
            try
            {
                return base.Invoke(method);
            }
            catch (ObjectDisposedException)
            {
                return null;
            }
        }

        public new object Invoke(Delegate method, params object[] args)
        {
            if (IsDisposed)
                return null;
            try
            {
                return base.Invoke(method, args);
            }
            catch (ObjectDisposedException)
            {
                return null;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            OnAlertMouseDown(e);
        }


        protected virtual void OnAlertMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (AlertMouseDown != null)
                AlertMouseDown(this, e);
        }

        /// <summary>
        /// Occurs when the user presses a mouse button while the mouse pointer is within the boundaries of a alert window.
        /// </summary>
        public virtual event EventHandler<MouseEventArgs> AlertMouseDown;

        /// <summary>
        /// Hide alert window and call callback delegate after that.
        /// </summary>
        /// <param name="callback"></param>
        public virtual void Hide(Action<Alert> callback)
        {
            if (HidingStyle == HidingStyle.Fade)
            {
                Action<Alert> hiding = new Action<Alert>(delegate(Alert obj)
                {
                    float v = 1f / (float)HidingDuration;
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
                hiding.BeginInvoke(this, null, null);
            }
            else if (HidingStyle == WinFormsPopupAlerts.HidingStyle.Slide)
            {
                Action<Alert> hiding = new Action<Alert>(delegate(Alert obj)
                {
                    int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                    int startLeft = obj.Left;
                    
                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    if (align == AlertAlignment.BottomRight || align == AlertAlignment.TopRight)
                    {
                        float v = (screenWidth - obj.Left) / (float)HidingDuration;
                        while (screenWidth - obj.Left > 0)
                        {
                            int msec = (int)sw.ElapsedMilliseconds;
                            obj.Invoke(new MethodInvoker(delegate() { obj.Left = (int)((float)startLeft + v * msec); }));
                            System.Threading.Thread.Sleep(10);
                        }
                    }
                    else
                    {
                        float v = (obj.Left + obj.Width) / (float)HidingDuration;
                        while (obj.Left + obj.Width > 0)
                        {
                            int msec = (int)sw.ElapsedMilliseconds;
                            obj.Invoke(new MethodInvoker(delegate() { obj.Left = (int)((float)startLeft - v * msec); }));
                            System.Threading.Thread.Sleep(10);
                        }
                    }
                    obj.Invoke(new MethodInvoker(delegate() { obj.InternalHide(); }));
                    callback(obj);
                });
                hiding.BeginInvoke(this, null, null);
            }
            else if (HidingStyle == HidingStyle.Simple)
                this.InternalHide();
            else
                throw new Exception("Unknown HidingStyle");
        }

        /// <summary>
        /// Starts showing effect.
        /// </summary>
        protected virtual void BeforeShown()
        {
            if (ShowingStyle == WinFormsPopupAlerts.ShowingStyle.Fade)
            {
                this.Opacity = 0;
                Action<Alert> showing = new Action<Alert>(delegate(Alert obj)
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

        /// <summary>
        /// Gets or sets an alert window's delay, in milliseconds.
        /// </summary>
        public int HidingDelay
        {
            get
            {
                return timer1.Interval;
            }
            set
            {
                timer1.Interval = value;
            }
        }

        /// <summary>
        /// Gets or sets padding within the alert window.
        /// </summary>
        public new virtual Padding Padding
        {
            get { return padding; }
            set
            {
                padding = value;
            }
        }

        /// <summary>
        /// Gets or sets the duration of the hiding of an alert window.
        /// </summary>
        public int HidingDuration
        {
            get { return hidingDuration; }
            set { hidingDuration = value; }
        }

        /// <summary>
        /// Gets or sets the duration of the appearance of an alert window.
        /// </summary>
        public int ShowingDuration
        {
            get { return showingDuration; }
            set { showingDuration = value; }
        }

        /// <summary>
        /// Gets or sets an animation effect applied when displaying an alert window.
        /// </summary>
        public ShowingStyle ShowingStyle
        {
            get { return showingStyle; }
            set { showingStyle = value; }
        }

        /// <summary>
        /// Gets or sets an animation effect applied when displaying an alert window.
        /// </summary>
        public HidingStyle HidingStyle
        {
            get { return hidingStyle; }
            set { hidingStyle = value; }
        }
    }
}
