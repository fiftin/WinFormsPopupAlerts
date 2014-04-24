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
        private HidingStyle hidingStyle = HidingStyle.Fade;
        private ShowingStyle showingStyle = ShowingStyle.Fade;

        private int showingDuration = 300;
        private int hidingDuration = 300;
        private Size maximumSize = new Size(500, 300);
        private Size minimumSize = new Size(150, 0);
        private Padding padding = new Padding(5, 5, 5, 5);

        internal const int DefaultHidingDelay = 5000;

        private int hidingDelay = DefaultHidingDelay;


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


        [DefaultValue(DefaultHidingDelay)]
        public int HidingDelay
        {
            get { return hidingDelay; }
            set { hidingDelay = value; }
        }

        /// <summary>
        /// Creates a new alert window.
        /// </summary>
        /// <param name="arg">Object passed to method CreateAlertImpl().</param>
        /// <param name="align">screen position where alert windows appear.</param>
        /// <returns></returns>
        public PopupAlert CreateAlert(object arg, PopupAlertAlignment align)
        {
            PopupAlert alert = CreateAlertImpl(arg, align);

            alert.HidingStyle = HidingStyle;

            alert.ShowingStyle = ShowingStyle;

            alert.HidingDelay = HidingDelay;

            alert.ShowingDuration = ShowingDuration;

            alert.HidingDuration = HidingDuration;

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

        /// <summary>
        /// Creates a new alert window. Needs for overriding.
        /// </summary>
        /// <param name="arg">Object passed to method CreateAlertImpl().</param>
        /// <param name="align">screen position where alert windows appear.</param>
        /// <returns></returns>
        protected virtual PopupAlert CreateAlertImpl(object arg, PopupAlertAlignment align)
        {
            return new PopupAlert(align);
        }

        public Padding Padding
        {
            get { return padding; }
            set { padding = value; }
        }

        /// <summary>
        /// Min size of created alert window
        /// </summary>
        public Size MinimumSize
        {
            get { return minimumSize; }
            set { minimumSize = value; }
        }

        /// <summary>
        /// Max size of created alert window
        /// </summary>
        public Size MaximumSize
        {
            get { return maximumSize; }
            set { maximumSize = value; }
        }

        [DefaultValue(300)]
        public int HidingDuration
        {
            get { return hidingDuration; }
            set { hidingDuration = value; }
        }

        [DefaultValue(300)]
        public int ShowingDuration
        {
            get { return showingDuration; }
            set { showingDuration = value; }
        }

        [DefaultValue(ShowingStyle.Fade)]
        public ShowingStyle ShowingStyle
        {
            get { return showingStyle; }
            set { showingStyle = value; }
        }

        [DefaultValue(HidingStyle.Fade)]
        public HidingStyle HidingStyle
        {
            get { return hidingStyle; }
            set { hidingStyle = value; }
        }

        public event EventHandler<System.Windows.Forms.MouseEventArgs> AlertMouseDown;
    }
}
