using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace WinFormsPopupAlerts
{
    [ToolboxBitmapAttribute(@"e:\src\winformspopupalerts\WinFormsPopupAlerts\PopupAlertManager.bmp")]
    public class PopupAlertManager : Component
    {
        private class HiddenAlertCollection
        {

            public const int MaxCount = 50;

            public void Add(PopupAlert alert)
            {
                if (items.Count > MaxCount)
                {
                    PopupAlert first = items.First.Value;
                    first.Invoke(new CloseAlertDelegate(CloseAlert), first);
                    first.Close();
                    items.RemoveFirst();
                }
                items.AddLast(alert);
            }

            private delegate void CloseAlertDelegate(object obj);

            private static void CloseAlert(object obj)
            {
                PopupAlert alert = (PopupAlert)obj;
                if (!alert.IsDisposed)
                {
                    alert.Close();
                }
            }

            public bool Contains(PopupAlert alert)
            {
                return items.Contains(alert);
            }

            LinkedList<PopupAlert> items = new LinkedList<PopupAlert>();
        }


        public PopupAlertManager()
        {
            InitializeComponent();
        }

        public PopupAlertManager(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
        }

        private ContainerControl containerControl = null;

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

        public PopupAlert Alert(params object[] args)
        {
            PopupAlert alert = AlertFactory.CreateAlert(args);
            int vOffset = alert.Height + VGap;
            switch (AlertAlignment)
            {
                case PopupAlertAlignment.BottomRight:
                case PopupAlertAlignment.BottomLeft:
                    foreach (PopupAlert x in alerts.ToArray())
                        x.Top -= vOffset;
                    break;
                case PopupAlertAlignment.TopLeft:
                case PopupAlertAlignment.TopRight:
                    foreach (PopupAlert x in alerts.ToArray())
                        x.Top += vOffset;
                    break;
            }
            PushAlert(alert);
            ShowAlertNormal(alert);
            return alert;
        }

        private object movingUpLocker = new object();
        private IAsyncResult movingUpAsyncResult = null;
        private bool completeForword = false;
        internal delegate void Proc(IAsyncResult prevAsyncRes);
        HiddenAlertCollection hiddenAlerts = new HiddenAlertCollection();

        public void ShowAlertNormal(PopupAlert alert)
        {
            Rectangle workingRect = Screen.PrimaryScreen.WorkingArea;
            switch (AlertAlignment)
            {
                case PopupAlertAlignment.BottomRight:
                    alert.Show(workingRect.Bottom - (alert.Height + VGap), workingRect.Right - (alert.Width + HGap));
                    break;
                case PopupAlertAlignment.BottomLeft:
                    alert.Show(workingRect.Bottom - (alert.Height + VGap), workingRect.Left + HGap);
                    break;
                case PopupAlertAlignment.TopLeft:
                    alert.Show(workingRect.Top + VGap, workingRect.Left + HGap);
                    break;
                case PopupAlertAlignment.TopRight:
                    alert.Show(workingRect.Top + VGap, workingRect.Right - (alert.Width + HGap));
                    break;
            }
        }

        private void PushAlert(PopupAlert alert)
        {
            if (alerts.Count >= AlertsMaxCount)
            {
                for (int i = 0; i < alerts.Count - AlertsMaxCount + 1; i++)
                {
                    PopupAlert firstAlert = alerts[i];
                    if (!hiddenAlerts.Contains(firstAlert))
                    {
                        firstAlert.Invoke(new MethodInvoker(delegate()
                        {
                            firstAlert.Hide(delegate(PopupAlert al)
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

        private PopupAlertFactory alertFactory;

        private List<PopupAlert> alerts = new List<PopupAlert>();

        public PopupAlertFactory AlertFactory
        {
            get { return alertFactory; }
            set { alertFactory = value; }
        }


        private int alertsMaxCount = DefaultAlertsMaxCount;
        private int vGap = DefaultVGap;
        private int hGap = DefaultHGap;

        [DefaultValue(DefaultHGap)]
        public int HGap
        {
            get { return hGap; }
            set { hGap = value; }
        }

        [DefaultValue(DefaultVGap)]
        public int VGap
        {
            get { return vGap; }
            set { vGap = value; }
        }

        [DefaultValue(DefaultAlertsMaxCount)]
        public int AlertsMaxCount
        {
            get { return alertsMaxCount; }
            set { alertsMaxCount = value; }
        }

        private int popupDuration = DefualtPopupDuration;

        [DefaultValue(DefualtPopupDuration)]
        public int PopupDuration
        {
            get { return popupDuration; }
            set { popupDuration = value; }
        }
        private PopupAlertAlignment alertAlignment = DefaultAlertAlignment;

        [DefaultValue(DefaultAlertAlignment)]
        public PopupAlertAlignment AlertAlignment
        {
            get { return alertAlignment; }
            set { alertAlignment = value; }
        }

        private const int DefualtPopupDuration = 300;
        private const int DefaultVGap = 5;
        private const int DefaultHGap = 5;
        private const int DefaultAlertsMaxCount = 10;
        private const PopupAlertAlignment DefaultAlertAlignment = PopupAlertAlignment.BottomRight;
    }
}
