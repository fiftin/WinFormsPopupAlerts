using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace WinFormsPopupAlerts
{
    /// <summary>
    /// The component that provide control of alert windows queue.
    /// </summary>
    [ToolboxBitmapAttribute(typeof(AlertManager))]
    public class AlertManager : Component
    {
        private class HiddenAlertCollection
        {

            public const int MaxCount = 50;

            public void Add(Alert alert)
            {
                if (items.Count > MaxCount)
                {
                    Alert first = items.First.Value;
                    first.Invoke(new CloseAlertDelegate(CloseAlert), first);
                    first.Close();
                    items.RemoveFirst();
                }
                items.AddLast(alert);
            }

            private delegate void CloseAlertDelegate(object obj);

            private static void CloseAlert(object obj)
            {
                Alert alert = (Alert)obj;
                if (!alert.IsDisposed)
                {
                    alert.Close();
                }
            }

            public bool Contains(Alert alert)
            {
                return items.Contains(alert);
            }

            LinkedList<Alert> items = new LinkedList<Alert>();
        }


        internal delegate void Proc(IAsyncResult prevAsyncRes);

        private const int DefaultVGap = 5;
        private const int DefaultHGap = 5;
        private const int DefaultAlertsMaxCount = 10;
        private const AlertAlignment DefaultAlertAlignment = AlertAlignment.BottomRight;

        private ContainerControl containerControl = null;
        private AlertAlignment alertAlignment = DefaultAlertAlignment;
        private object movingUpLocker = new object();
        private HiddenAlertCollection hiddenAlerts = new HiddenAlertCollection();
        private AlertFactory alertFactory;
        private List<Alert> alerts = new List<Alert>();
        private int alertsMaxCount = DefaultAlertsMaxCount;
        private int vGap = DefaultVGap;
        private int hGap = DefaultHGap;


        public AlertManager()
        {
        }

        public AlertManager(IContainer container)
        {
            container.Add(this);
        }

        [Browsable(false)]
        public ContainerControl ContainerControl
        {
            get { return containerControl; }
            set { containerControl = value; }
        }

        public override ISite Site
        {
            get { return base.Site; }
            set
            {
                base.Site = value;
                if (value == null)
                {
                    return;
                }

                IDesignerHost host = value.GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (host != null)
                {
                    IComponent componentHost = host.RootComponent;
                    if (componentHost is ContainerControl)
                    {
                        ContainerControl = componentHost as ContainerControl;
                    }
                }
            }
        }

        /// <summary>
        /// Create and display new alert window.
        /// </summary>
        /// <param name="arg">Object passed to alert window factory.</param>
        /// <returns>New alert window</returns>
        public Alert Alert(object arg)
        {
            Alert alert = AlertFactory.CreateAlert(arg, AlertAlignment);
            int vOffset = alert.Height + VGap;
            switch (AlertAlignment)
            {
                case AlertAlignment.BottomRight:
                case AlertAlignment.BottomLeft:
                    foreach (Alert x in alerts.ToArray())
                        x.Top -= vOffset;
                    break;
                case AlertAlignment.TopLeft:
                case AlertAlignment.TopRight:
                    foreach (Alert x in alerts.ToArray())
                        x.Top += vOffset;
                    break;
            }
            PushAlert(alert);
            ShowAlert(alert);
            return alert;
        }

        /// <summary>
        /// Display window alert <paramref name="alert"/> on the screen.
        /// </summary>
        public void ShowAlert(Alert alert)
        {
            Rectangle workingRect = Screen.PrimaryScreen.WorkingArea;
            switch (AlertAlignment)
            {
                case AlertAlignment.BottomRight:
                    alert.Show(workingRect.Bottom - (alert.Height + VGap), workingRect.Right - (alert.Width + HGap));
                    break;
                case AlertAlignment.BottomLeft:
                    alert.Show(workingRect.Bottom - (alert.Height + VGap), workingRect.Left + HGap);
                    break;
                case AlertAlignment.TopLeft:
                    alert.Show(workingRect.Top + VGap, workingRect.Left + HGap);
                    break;
                case AlertAlignment.TopRight:
                    alert.Show(workingRect.Top + VGap, workingRect.Right - (alert.Width + HGap));
                    break;
            }
        }

        /// <summary>
        /// Push alert window to queue.
        /// </summary>
        /// <param name="alert"></param>
        private void PushAlert(Alert alert)
        {
            if (alerts.Count >= AlertsMaxCount)
            {
                for (int i = 0; i < alerts.Count - AlertsMaxCount + 1; i++)
                {
                    Alert firstAlert = alerts[i];
                    if (!hiddenAlerts.Contains(firstAlert))
                    {
                        firstAlert.Invoke(new MethodInvoker(delegate()
                        {
                            firstAlert.Hide(delegate(Alert al)
                            {
                                alerts.Remove(al);
                            });
                        }));
                        hiddenAlerts.Add(firstAlert);
                    }
                }
            }
            alerts.Add(alert);
        }

        public AlertFactory AlertFactory
        {
            get { return alertFactory; }
            set { alertFactory = value; }
        }


        /// <summary>
        /// Gap between alert windows and display edge.
        /// </summary>
        [DefaultValue(DefaultHGap)]
        public int HGap
        {
            get { return hGap; }
            set { hGap = value; }
        }

        /// <summary>
        /// Gap between nearest alert windows.
        /// </summary>
        [DefaultValue(DefaultVGap)]
        public int VGap
        {
            get { return vGap; }
            set { vGap = value; }
        }

        /// <summary>
        /// Gets or sets the maximum number of simultaneously displayed alert windows.
        /// </summary>
        [DefaultValue(DefaultAlertsMaxCount)]
        public int AlertsMaxCount
        {
            get { return alertsMaxCount; }
            set { alertsMaxCount = value; }
        }


        /// <summary>
        /// Gets or sets the maximum number of simultaneously displayed alert windows.
        /// </summary>
        [DefaultValue(DefaultAlertAlignment)]
        public AlertAlignment AlertAlignment
        {
            get { return alertAlignment; }
            set { alertAlignment = value; }
        }

    }
}
