using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsPopupAlerts
{
    public class SimplePopupAlertFactory : PopupAlertFactory
    {
        public SimplePopupAlertFactory()
        {

        }

        public SimplePopupAlertFactory(System.ComponentModel.IContainer container)
            : base(container)
        {

        }

        public override PopupAlert CreateAlert(object[] args)
        {
            SimplePopupAlert alert;
            switch (AlertStyle)
            {
                case SimplePopupAlertStyle.System:
                    alert = new SystemPopupAlert(args);
                    break;
                case SimplePopupAlertStyle.Defualt:
                default:
                    alert = new SimplePopupAlert(args);
                    break;
            }

            alert.HiddingStyle = HiddingStyle;

            alert.ShowingStyle = ShowingStyle;

            alert.HiddingDelay = HiddingDelay;

            alert.RoundedCornerRadius = RoundedCornerRadius;

            alert.ShowingDuration = ShowingDuration;

            alert.HiddingDuration = HiddingDuration;

            alert.MaximumSize = MaximumSize;

            alert.MinimumSize = MinimumSize;

            alert.Padding = Padding;

            alert.MouseDown += new MouseEventHandler(alert_MouseDown);

            return alert;
        }

        void alert_MouseDown(object sender, MouseEventArgs e)
        {
            AlertClick(sender, e);
        }


        private HiddingStyle hiddingStyle = HiddingStyle.Fade;
        private ShowingStyle showingStyle = ShowingStyle.Fade;
        private int hiddingDelay = 5000;
        private int roundedCornerRadius = 5;
        private int showingDuration = 300;
        private int hiddingDuration = 300;
        private SimplePopupAlertStyle alertStyle = SimplePopupAlertStyle.System;
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


        [DefaultValue(SimplePopupAlertStyle.System)]
        public SimplePopupAlertStyle AlertStyle
        {
            get { return alertStyle; }
            set { alertStyle = value; }
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


        public event EventHandler AlertClick;
    }
}
