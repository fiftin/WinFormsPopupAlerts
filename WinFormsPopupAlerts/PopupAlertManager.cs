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
            //if (PopupStyle == WinFormsPopupAlerts.PopupStyle.Simple)
            //{
            int vOffset = alert.Height + VGap;
            switch (AlertAlignment)
            {
                case PopupAlertAlignment.BottomRight:
                case PopupAlertAlignment.BottomLeft:
                    foreach (PopupAlert x in alerts)
                    {
                        x.Top -= vOffset;
                    }
                    break;
                case PopupAlertAlignment.TopLeft:
                case PopupAlertAlignment.TopRight:
                    foreach (PopupAlert x in alerts)
                    {
                        x.Top += vOffset;
                    }
                    break;
            }
            PushAlert(alert);
            ShowAlertNormal(alert);
            //}
            //else if (PopupStyle == WinFormsPopupAlerts.PopupStyle.Slide)
            //{
            //    ShowAlertSlide(alert);
            //}
            return alert;
        }

        private object movingUpLocker = new object();
        private IAsyncResult movingUpAsyncResult = null;
        private bool completeForword = false;
        internal delegate void Proc(IAsyncResult prevAsyncRes);

        private void ShowAlertSlide(PopupAlert alert)
        {
            Proc movingUp = delegate(IAsyncResult prevAsyncRes)
            {
                if (prevAsyncRes != null && !prevAsyncRes.IsCompleted)
                {
                    prevAsyncRes.AsyncWaitHandle.WaitOne();
                }

                Rectangle rect = SafeNativeMethods.GetWorkArea();
                ContainerControl.Invoke(new MethodInvoker(delegate()
                {
                    alert.Show(rect.Bottom, rect.Right - (alert.Width + HGap));
                }));
                
                int vOffset = alert.Height + VGap;
                PushAlert(alert);

                int currentOffset = 0;
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                float v = vOffset / (float)PopupDuration;
                
                for (int i = 0; i < alerts.Count; i++)
                {
                    PopupAlert x = alerts[i];
                    x.Tag1 = x.Top;
                }
                 
                while (currentOffset < vOffset && !completeForword)
                {
                    for (int i = 0; i < alerts.Count; i++)
                    {
                        int currentTime = (int)sw.ElapsedMilliseconds;
                        currentOffset = (int)(currentTime * v);

                        PopupAlert x = alerts[i];
                        x.Invoke(new MethodInvoker(delegate()
                        {
                            if (x.Tag1 == null)
                                x.Tag1 = x.Top;
                            x.Top = ((int)x.Tag1) - currentOffset;
                        }));
                    }
                    System.Threading.Thread.Sleep(1);
                }

                //alert.Invoke(new MethodInvoker(delegate() { alert.TopMost = true; }));
            };

            movingUpAsyncResult = movingUp.BeginInvoke(movingUpAsyncResult, null, null);
        }

        HiddenAlertCollection hiddenAlerts = new HiddenAlertCollection();


        public void ShowAlertNormal(PopupAlert alert)
        {
            Rectangle rect = SafeNativeMethods.GetWorkArea();
            //alert.Invoke(new MethodInvoker(delegate() { alert.TopMost = true; }));
            switch (AlertAlignment)
            {
                case PopupAlertAlignment.BottomRight:
                    alert.Show(rect.Bottom - (alert.Height + VGap), rect.Right - (alert.Width + HGap));
                    break;
                case PopupAlertAlignment.BottomLeft:
                    alert.Show(rect.Bottom - (alert.Height + VGap), HGap);
                    break;
                case PopupAlertAlignment.TopLeft:
                    alert.Show(VGap, HGap);
                    break;
                case PopupAlertAlignment.TopRight:
                    alert.Show(VGap, rect.Right - (alert.Width + HGap));
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

        //private PopupStyle popupStyle = DefaultPopupStyle;
        //
        //[DefaultValue(DefaultPopupStyle)]
        //public PopupStyle PopupStyle
        //{
        //    get { return popupStyle; }
        //    set { popupStyle = value; }
        //}


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
        //private const PopupStyle DefaultPopupStyle = PopupStyle.Simple;
        private const PopupAlertAlignment DefaultAlertAlignment = PopupAlertAlignment.BottomRight;
    }
}
