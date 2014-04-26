using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WinFormsPopupAlerts
{
    /// <summary>
    /// Control for editing the value of objects of CornerRadius class.
    /// </summary>
    [ToolboxItem(false)]
    internal partial class CornerRadiusEditorControl : UserControl
    {
        private CornerRadius crc;
        private bool initialized;

        public CornerRadiusEditorControl(CornerRadius value)
        {
            this.crc = value;
            InitializeComponent();
            numLeftTop.Value = crc.TopLeft;
            numTopRight.Value = crc.TopRight;
            numBottomLeft.Value = crc.BottomLeft;
            numBottomRight.Value = crc.BottomRight;
            if (crc.Equals(crc.TopLeft))
                numericUpDown1.Value = crc.TopLeft;
            initialized = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private void numLeftTop_ValueChanged(object sender, EventArgs e)
        {
            if (initialized)
                crc.TopLeft = (int)numLeftTop.Value;
        }

        private void numTopRight_ValueChanged(object sender, EventArgs e)
        {
            if (initialized)
                crc.TopRight = (int)numTopRight.Value;
        }

        private void numBottomLeft_ValueChanged(object sender, EventArgs e)
        {
            if (initialized)
                crc.BottomLeft = (int)numBottomLeft.Value;
        }

        private void numBottomRight_ValueChanged(object sender, EventArgs e)
        {
            if (initialized)
                crc.BottomRight = (int)numBottomRight.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                numLeftTop.Value = (int)numericUpDown1.Value;
                numTopRight.Value = (int)numericUpDown1.Value;
                numBottomLeft.Value = (int)numericUpDown1.Value;
                numBottomRight.Value = (int)numericUpDown1.Value;
            }
        }

    }
}
