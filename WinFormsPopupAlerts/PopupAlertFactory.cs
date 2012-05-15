using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace WinFormsPopupAlerts
{
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

        public virtual PopupAlert CreateAlert(object[] args)
        {
            PopupAlert ret = new PopupAlert(args);
            ret.HiddingDelay = DefaultHiddingDelay;
            return ret;
        }

        private int hiddingDelay = DefaultHiddingDelay;

        [DefaultValue(DefaultHiddingDelay)]
        public int HiddingDelay
        {
            get { return hiddingDelay; }
            set { hiddingDelay = value; }
        }

        internal const int DefaultHiddingDelay = 5000;
    }
}
