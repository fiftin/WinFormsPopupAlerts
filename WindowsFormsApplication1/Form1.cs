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
            
            popupAlertManager1.Alert("Informationon", "vvvvvvvvvvvvvHello World!!! Hello World!!! sdfgsdfgsdfg dsg dsfgsdfg sdfg", ToolTipIcon.Warning);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(10, "ddd", "ffffffffffffffffffffffffffgggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggffffffffffffffffffffffffffffffffffffff", ToolTipIcon.None);
            //IntPtr ptr = WindowsNative.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Shell_TrayWnd", null);

            this.Icon = SystemIcons.Warning;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
        }

        string[] s = new string[] {
            "You will get a perfect and easy help with many examples so that you can start using “Image Tiger” within a few minutes.",
            "“.NET Image Tiger Component” is an advanced image manipulation component for .NET that will help you to complete most of the image manipulation needs.",
            "It will help you with all the basic image handling like resize, crop, rotate, scale, flip, save in different formats, but in addition it comes with many fascinating image effects like jitter, sharpen, blur, watermark, brightness, contrast, sepia, emboss, EXIF data reading, and many more.",
            "Asp.Net MVC Thread was being aborted during long running operation",
            "NetworkStream AccessViolationException on thread",
            "But how to read it into a signed byte array? Could somebody give me some ideas?",
            "Thanks for the reply. The size of the file is around 4000 kb. It's a small file as you can see. About the BlockCopy method, could I also use it to move the data from a float[] into a sbyte[], like var byteArray",
            "Or if you prefer to read the file directly as sbytes, you can do something like that:",
            "I m using devexpress tools and while using treelist i want to apply this. I want to add a row after every child node which could take text values and the size of the row should be stretched to the full width.How can i do this..?",
        };

        Random r = new Random();

        private void timer1_Tick(object sender, EventArgs e)
        {
            popupAlertManager1.Alert("Informationon", s[r.Next(s.Length)], ToolTipIcon.Warning);
        }
    }
}
