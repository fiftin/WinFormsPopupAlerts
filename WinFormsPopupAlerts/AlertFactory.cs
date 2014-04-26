using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace WinFormsPopupAlerts
{
    /// <summary>
    /// Represents a set of methods for creating instances of PopupAlert class inheritors.
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    public abstract class AlertFactory : Component
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

        public AlertFactory()
        {
        }

        public AlertFactory(IContainer container)
        {
            container.Add(this);
        }

        /// <summary>
        /// Gets or sets an alert window's delay, in milliseconds.
        /// </summary>
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
        public Alert CreateAlert(object arg, AlertAlignment align)
        {
            Alert alert = CreateAlertImpl(arg, align);

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
        protected abstract Alert CreateAlertImpl(object arg, AlertAlignment align);

        /// <summary>
        /// Gets or sets padding within the alert window.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the duration of the hiding of an alert window.
        /// </summary>
        [DefaultValue(300)]
        public int HidingDuration
        {
            get { return hidingDuration; }
            set { hidingDuration = value; }
        }

        /// <summary>
        /// Gets or sets the duration of the appearance of an alert window.
        /// </summary>
        [DefaultValue(300)]
        public int ShowingDuration
        {
            get { return showingDuration; }
            set { showingDuration = value; }
        }

        /// <summary>
        /// Gets or sets an animation effect applied when displaying an alert window.
        /// </summary>
        [DefaultValue(ShowingStyle.Fade)]
        public ShowingStyle ShowingStyle
        {
            get { return showingStyle; }
            set { showingStyle = value; }
        }

        /// <summary>
        /// Gets or sets an animation effect applied when displaying an alert window.
        /// </summary>
        [DefaultValue(HidingStyle.Fade)]
        public HidingStyle HidingStyle
        {
            get { return hidingStyle; }
            set { hidingStyle = value; }
        }

        /// <summary>
        /// Occurs when the user presses a mouse button while the mouse pointer is within the boundaries of a alert window.
        /// </summary>
        public event EventHandler<System.Windows.Forms.MouseEventArgs> AlertMouseDown;
    }
}
