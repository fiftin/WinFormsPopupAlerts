using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WinFormsPopupAlerts;
using System.Windows.Forms.VisualStyles;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //using (var g = CreateGraphics())
            //{
                //VisualStyleElement btn = ToolTipBalloonDrawingHelper.GetCloseButtonVS(ToolTipBalloonCloseButtonState.Hot);
                //VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.Standard.Normal);
                //renderer.Get
                //var btnRect = ToolTipBalloonDrawingHelper.GetCloseButtonRect(g, new Rectangle(10, 10, 10, 10), new System.Windows.Forms.Padding(0, 0, 0, 0), ToolTipBalloonCloseButtonState.Hot);
                
            //    SystemTooltipRenderingHelper.DrawCloseButton(g, new Rectangle(10, 10, 100, 100), new System.Windows.Forms.Padding(0, 0, 0, 0), TooltipCloseButtonState.Hot);
            //}
            //PopupAlert al = new TooltipAlert();
            //((TopFormBase)al).Show();
            
            popupAlertManager1.Alert("Informationon", "vvvvvvvvvvvvvHello World!!! Hello World!!! sdfgsdfgsdfg dsg dsfgsdfg sdfg", ToolTipIcon.None);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(10, "ddd", "ffffffffffffffffffffffffffgggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggffffffffffffffffffffffffffffffffffffff", ToolTipIcon.None);
            //IntPtr ptr = WindowsNative.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Shell_TrayWnd", null);

            this.Icon = SystemIcons.Warning;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TopFormBase f = new TopFormBase();
            f.Show();
            //toolTip1.Show("Hllo", this);
            //Region = SafeNativeMethods.GetRoundRectRegion(0, 0, Width, Height, 20, 20);
        }
    }
}
