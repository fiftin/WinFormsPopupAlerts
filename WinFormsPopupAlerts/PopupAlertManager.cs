using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace WinFormsPopupAlerts
{
    public class PopupAlertManager : Component
    {

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

        public PopupAlertBase Alert(params object[] args)
        {
            PopupAlertBase alert = AlertFactory.CreateAlert(args);

            if (PopupStyle == WinFormsPopupAlerts.PopupStyle.Simple)
            {
                int vOffset = alert.Height + VGap;
                foreach (PopupAlertBase x in alerts)
                {
                    x.Top -= vOffset;
                }
                PushAlert(alert);
                ShowAlertNormal(alert);
            }
            else if (PopupStyle == WinFormsPopupAlerts.PopupStyle.Slide)
            {
                ShowAlertSlide(alert);
            }
            return alert;
        }

        private object movingUpLocker = new object();
        private IAsyncResult movingUpAsyncResult = null;
        private bool completeForword = false;
        internal delegate void Proc(IAsyncResult prevAsyncRes);

        private void ShowAlertSlide(PopupAlertBase alert)
        {
            
            Proc movingUp = delegate(IAsyncResult prevAsyncRes)
            {

                //lock (movingUpLocker)
                //{
                if (prevAsyncRes != null && !prevAsyncRes.IsCompleted)
                {
                    //completeForword = true;
                    prevAsyncRes.AsyncWaitHandle.WaitOne();
                }
                //}

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
                float v = vOffset / 300f;
                
                for (int i = 0; i < alerts.Count; i++)
                {
                    PopupAlertBase x = alerts[i];
                    x.Tag1 = x.Top;
                }
                 
                while (currentOffset < vOffset && !completeForword)
                {
                    for (int i = 0; i < alerts.Count; i++)
                    {
                        int currentTime = (int)sw.ElapsedMilliseconds;
                        currentOffset = (int)(currentTime * v);

                        PopupAlertBase x = alerts[i];
                        x.Invoke(new MethodInvoker(delegate()
                        {
                            if (x.Tag1 == null)
                                x.Tag1 = x.Top;
                            x.Top = ((int)x.Tag1) - currentOffset;
                        }));
                    }
                    System.Threading.Thread.Sleep(1);
                }

                /*
                if (completeForword)
                {
                    for (int i = 0; i < alerts.Count; i++)
                    {
                        PopupAlert x = alerts[i];
                        x.Invoke(new MethodInvoker(delegate()
                        {
                            x.Top -= vOffset - currentOffset;
                        }));
                    }

                    completeForword = false;
                }
                 */

                alert.Invoke(new MethodInvoker(delegate() { alert.TopMost = true; }));
            };

            //lock (movingUpLocker)
            //{
            movingUpAsyncResult = movingUp.BeginInvoke(movingUpAsyncResult, null, null);
            //}
        }

        List<PopupAlertBase> hiddenAlerts = new List<PopupAlertBase>();


        public void ShowAlertNormal(PopupAlertBase alert)
        {
            Rectangle rect = SafeNativeMethods.GetWorkArea();
            alert.Show(rect.Bottom - (alert.Height + VGap), rect.Right - (alert.Width + HGap));
        }

        private void PushAlert(PopupAlertBase alert)
        {
            if (alerts.Count >= AlertsMaxCount)
            {
                for (int i = 0; i < alerts.Count - AlertsMaxCount + 1; i++)
                {
                    PopupAlertBase firstAlert = alerts[i];
                    if (!hiddenAlerts.Contains(firstAlert))
                    {
                        firstAlert.Invoke(new MethodInvoker(delegate() { firstAlert.Hide(delegate(PopupAlertBase al) { alerts.Remove(al); }); }));
                        hiddenAlerts.Add(firstAlert);
                    }
                }
            }
            alerts.Add(alert);
        }

        private PopupAlertFactory alertFactory;

        private List<PopupAlertBase> alerts = new List<PopupAlertBase>();

        public PopupAlertFactory AlertFactory
        {
            get { return alertFactory; }
            set { alertFactory = value; }
        }


        private int alertsMaxCount = 5;
        private int vGap = 5;
        private int hGap = 5;

        [DefaultValue(5)]
        public int HGap
        {
            get { return hGap; }
            set { hGap = value; }
        }

        [DefaultValue(5)]
        public int VGap
        {
            get { return vGap; }
            set { vGap = value; }
        }

        [DefaultValue(5)]
        public int AlertsMaxCount
        {
            get { return alertsMaxCount; }
            set { alertsMaxCount = value; }
        }

        private PopupStyle popupStyle = PopupStyle.Simple;

        [DefaultValue(PopupStyle.Simple)]
        public PopupStyle PopupStyle
        {
            get { return popupStyle; }
            set { popupStyle = value; }
        }
    }
}
