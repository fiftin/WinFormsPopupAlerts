using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace WinFormsPopupAlerts
{
    //[ToolboxBitmapAttribute(typeof(PopupAlertFactory), @"PopupAlertFactory.bmp")]
    [System.ComponentModel.ToolboxItem(false)]
    public class PopupAlertFactory : Component
    {
        public PopupAlertFactory()
        {
            InitializeComponent();
        }

        public PopupAlertFactory(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
        }


        private int hiddingDelay = DefaultHiddingDelay;

        [DefaultValue(DefaultHiddingDelay)]
        public int HiddingDelay
        {
            get { return hiddingDelay; }
            set { hiddingDelay = value; }
        }

        internal const int DefaultHiddingDelay = 5000;

        public PopupAlert CreateAlert(object arg, PopupAlertAlignment align)
        {
            PopupAlert alert = CreateAlertImpl(arg, align);

            alert.HiddingStyle = HiddingStyle;

            alert.ShowingStyle = ShowingStyle;

            alert.HiddingDelay = HiddingDelay;

            alert.ShowingDuration = ShowingDuration;

            alert.HiddingDuration = HiddingDuration;

            alert.MaximumSize = MaximumSize;

            alert.MinimumSize = MinimumSize;

            alert.Padding = Padding;

            alert.AlertMouseDown += new EventHandler<MouseEventArgs>(alert_AlertMouseDown);

            return alert;
        }

        void alert_AlertMouseDown(object sender, MouseEventArgs e)
        {
            if (AlertMouseDown != null)
                AlertMouseDown(sender, e);
        }

        protected virtual PopupAlert CreateAlertImpl(object arg, PopupAlertAlignment align)
        {
            return new PopupAlert(align);
        }

        private HiddingStyle hiddingStyle = HiddingStyle.Fade;
        private ShowingStyle showingStyle = ShowingStyle.Fade;

        private int roundedCornerRadius = 5;
        private int showingDuration = 300;
        private int hiddingDuration = 300;
        private Size maximumSize = new Size(500, 300);
        private Size minimumSize = new Size(150, 0);
        private Padding padding = new Padding(5, 5, 5, 5);

        public Padding Padding
        {
            get { return padding; }
            set { padding = value; }
        }

        public Size MinimumSize
        {
            get { return minimumSize; }
            set { minimumSize = value; }
        }

        public Size MaximumSize
        {
            get { return maximumSize; }
            set { maximumSize = value; }
        }

        [DefaultValue(300)]
        public int HiddingDuration
        {
            get { return hiddingDuration; }
            set { hiddingDuration = value; }
        }

        [DefaultValue(300)]
        public int ShowingDuration
        {
            get { return showingDuration; }
            set { showingDuration = value; }
        }

        [DefaultValue(5)]
        public int RoundedCornerRadius
        {
            get { return roundedCornerRadius; }
            set
            {
                roundedCornerRadius = value;
            }
        }


        [DefaultValue(ShowingStyle.Fade)]
        public ShowingStyle ShowingStyle
        {
            get { return showingStyle; }
            set { showingStyle = value; }
        }

        [DefaultValue(HiddingStyle.Fade)]
        public HiddingStyle HiddingStyle
        {
            get { return hiddingStyle; }
            set { hiddingStyle = value; }
        }


        public event EventHandler<System.Windows.Forms.MouseEventArgs> AlertMouseDown;
    }
}
