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
    public partial class PopupAlertBase : Form
    {
        private object tag1;

        internal object Tag1
        {
            get { return tag1; }
            set { tag1 = value; }
        }

        public PopupAlertBase()
        {
            InitializeComponent();
        }

        public PopupAlertBase(object[] args)
        {
            InitializeComponent();
        }

        private int top;
        private int left;

        public void Show(int top, int left)
        {
            this.top = top;
            this.left = left;
            Show();
        }

        public new void Hide()
        {
            Hide(delegate(PopupAlertBase alert) { });
        }

        public virtual void Hide(Action<PopupAlertBase> callback)
        {
            Hide();
            callback(this);
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
            Hide();
        }

        protected virtual void BeforeShown() { }

        public int HiddingDelay
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

        public new object Invoke(Delegate method)
        {
            if (IsDisposed)
            {
                return null;
            }
            try
            {
                return base.Invoke(method);
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

        public virtual event EventHandler<MouseEventArgs> AlertMouseDown;
    }
}
