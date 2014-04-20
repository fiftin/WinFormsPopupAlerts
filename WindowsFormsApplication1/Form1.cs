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
            customTooltipAlertRenderer1.CornerRadius = new CornerRadius(25, 25, 25, 25);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ToolTipIcon icon = icons[r.Next(icons.Length)];
            popupAlertManager1.Alert(null, texts[r.Next(texts.Length)], icon);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ToolTipIcon icon = icons[r.Next(icons.Length)];
            notifyIcon1.ShowBalloonTip(10, titles[r.Next(titles.Length)], texts[r.Next(texts.Length)], icon);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
        }

        string[] titles = new string[] {
            "WinFormsPopupAlerts Very simple messanger Very simple messanger",
            "Author Dashboard",
            "Сайт бесплатных объявлений Сайт бесплатных объявлений Сайт бесплатных объявлений",
            "Very simple messanger Very simple messanger Very simple messanger Very simple messangerVery simple messangerVery simple messanger",
            "Select a category for your upload Select a category for your upload Select a category for your upload",
            "Our free Git",
            "slightly improved FileChooser",
            "Plans & pricing ",
            "Terms of service",
            "Privacy policy",
            "Subscribe to this newsfeed",
            "Tweet about it",
            null,
            null
        };

        string[] texts = new string[] {
            "You will get a perfect and easy help with many examples so that you can start using “Image Tiger” within a few minutes.",
            "“.NET Image Tiger Component” is an advanced image manipulation component for .NET that will help you to complete most of the image manipulation needs.",
            "It will help you with all the basic image handling like resize, crop, rotate, scale, flip, save in different formats, but in addition it comes with many fascinating image effects like jitter, sharpen, blur, watermark, brightness, contrast, sepia, emboss, EXIF data reading, and many more.",
            "На рейтинги «большой тройки» ориентируются не только западные инвесторы, но и российские компании. От рейтинга в том числе зависит стоимость заемных средств. Отказаться от них и перейти на российские рейтинги практически невозможно, говорит «Газете.Ru» источник на банковском рынке.",
            "Asp.Net MVC Thread was being aborted during long running operation",
            "NetworkStream AccessViolationException on thread",
            "But how to read it into a signed byte array? Could somebody give me some ideas?",
            "Thanks for the reply. The size of the file is around 4000 kb. It's a small file as you can see. About the BlockCopy method, could I also use it to move the data from a float[] into a sbyte[], like var byteArray",
            "Or if you prefer to read the file directly as sbytes, you can do something like that:",
            "I m using devexpress tools and while using treelist i want to apply this. I want to add a row after every child node which could take text values and the size of the row should be stretched to the full width.How can i do this..?",
            "Hey guys, You may have noticed that you've been logged out from the Marketplaces on all devices you used previously. This is a security measure in reaction to an OpenSSL vulnerability. Please take a moment to read through this article via the Notes: http://enva.to/1lQ2myl and if you haven't done so already, be sure to change your account password ASAP. Thank you! ^Contrastblack"
        };

        Random r = new Random();
        ToolTipIcon[] icons = (ToolTipIcon[])Enum.GetValues(typeof(ToolTipIcon));
        private void timer1_Tick(object sender, EventArgs e)
        {
            ToolTipIcon icon = icons[r.Next(icons.Length)];
            popupAlertManager1.Alert(titles[r.Next(titles.Length)], texts[r.Next(texts.Length)], icon);
        }
    }
}
