using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsPopupAlerts
{
    public class BasicPopupAlertFactory : PopupAlertFactory
    {
        public BasicPopupAlertFactory()
        {

        }

        public BasicPopupAlertFactory(System.ComponentModel.IContainer container)
            : base(container)
        {

        }

        public override PopupAlertBase CreateAlert(object[] args)
        {
            BasicPopupAlert alert = CreateAlertImpl(args);

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

        protected virtual BasicPopupAlert CreateAlertImpl(object[] args)
        {
            return new BasicPopupAlert(args);
        }



        private HiddingStyle hiddingStyle = HiddingStyle.Fade;
        private ShowingStyle showingStyle = ShowingStyle.Fade;
        private int hiddingDelay = 5000;
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

        [DefaultValue(5000)]
        public int HiddingDelay
        {
            get { return hiddingDelay; }
            set { hiddingDelay = value; }
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
