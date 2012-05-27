using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WinFormsPopupAlerts
{
    internal partial class CornerRadiusEditorControl : UserControl
    {
        public CornerRadiusEditorControl(object value)
        {
            InitializeComponent();
        }

        private void num_VisibleChanged(object sender, EventArgs e)
        {

        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }


    }
}
