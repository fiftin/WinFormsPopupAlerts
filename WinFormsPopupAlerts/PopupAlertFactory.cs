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

        public virtual PopupAlertBase CreateAlert(object[] args)
        {
            return new PopupAlertBase(args);
        }

        
    }
}
