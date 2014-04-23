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
            cbHiddingStyle.SelectedIndex = (int)tooltipAlertFactory1.HidingStyle;
            cbShowingStyle.SelectedIndex = (int)tooltipAlertFactory1.ShowingStyle;
            btnTitleFont.Font = customTooltipAlertRenderer1.TitleFont;
            picTitleColor.BackColor = customTooltipAlertRenderer1.TitleForeColor;
            btnTextFont.Font = customTooltipAlertRenderer1.Font;
            picTitleColor.BackColor = customTooltipAlertRenderer1.TitleForeColor;
            picBackColor.BackColor = customTooltipAlertRenderer1.BackColor;
            picTextColor.BackColor = customTooltipAlertRenderer1.ForeColor;
            picBorderColor.BackColor = customTooltipAlertRenderer1.BorderColor;
            numBorderThickenss.Value = customTooltipAlertRenderer1.BorderThickness;
            numVGap.Value = popupAlertManager1.VGap;
            numAlertMaxCount.Value = popupAlertManager1.AlertsMaxCount;
            numTopLeft.Value = customTooltipAlertRenderer1.CornerRadius.TopLeft;
            numTopRight.Value = customTooltipAlertRenderer1.CornerRadius.TopRight;
            numBottomLeft.Value = customTooltipAlertRenderer1.CornerRadius.BottomLeft;
            numBottomRight.Value = customTooltipAlertRenderer1.CornerRadius.BottomRight;
            numMaxWidth.Value = tooltipAlertFactory1.MaximumSize.Width;
            numMinWidth.Value = tooltipAlertFactory1.MinimumSize.Width;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Alert();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                btnStartAlerts.Text = "Start alerts";
            }
            else
            {
                timer1.Enabled = true;
                btnStartAlerts.Text = "Stop alerts";
            }
        }

        Random r = new Random();
        ToolTipIcon[] icons = (ToolTipIcon[])Enum.GetValues(typeof(ToolTipIcon));
        private void Alert()
        {
            ToolTipIcon icon = icons[r.Next(icons.Length)];
            if (rbCustomAlert.Checked)
                popupAlertManager1.Alert(new TooltipAlertArg(txtTitle.Text, txtText.Text, GetCustomIcon()));
            else
                popupAlertManager1.Alert(new TooltipAlertArg(titles[r.Next(titles.Length)], texts[r.Next(texts.Length)], icon));
        }

        private ToolTipIcon GetCustomIcon()
        {
            if (rbIconError.Checked)
                return ToolTipIcon.Error;
            else if (rbIconWarning.Checked)
                return ToolTipIcon.Warning;
            else if (rbIconInfo.Checked)
                return ToolTipIcon.Info;
            else
                return ToolTipIcon.None;
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            Alert();
        }

        private void rbSystemStyle_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = !rbSystemStyle.Checked;
            tooltipAlertFactory1.AlertStyle = rbSystemStyle.Checked ? TooltipAlertStyle.System : TooltipAlertStyle.Custom;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Gray, 150, 5, 150, Height-35);
            e.Graphics.DrawLine(Pens.White, 151, 5, 151, Height-35);
        }

        private void SetAlignButtonColor(Button button)
        {
            Button[] alignButtons = { btnTopLeft, btnTopRight, btnBottomLeft, btnBottomRight };
            foreach (Button x in alignButtons)
            {
                if (x == button)
                    x.BackColor = Color.Red;
                else
                    x.BackColor = Color.Transparent;
            }
        }

        private void btnTopLeft_Click(object sender, EventArgs e)
        {
            SetAlignButtonColor((Button)sender);
            popupAlertManager1.AlertAlignment = PopupAlertAlignment.TopLeft;
        }

        private void btnTopRight_Click(object sender, EventArgs e)
        {
            SetAlignButtonColor((Button)sender);
            popupAlertManager1.AlertAlignment = PopupAlertAlignment.TopRight;
        }

        private void btnBottomLeft_Click(object sender, EventArgs e)
        {
            SetAlignButtonColor((Button)sender);
            popupAlertManager1.AlertAlignment = PopupAlertAlignment.BottomLeft;
        }

        private void btnBottomRight_Click(object sender, EventArgs e)
        {
            SetAlignButtonColor((Button)sender);
            popupAlertManager1.AlertAlignment = PopupAlertAlignment.BottomRight;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = trackBar1.Value;
        }

        private void numHiddingDelay_ValueChanged(object sender, EventArgs e)
        {
            tooltipAlertFactory1.HiddingDelay = (int)((NumericUpDown)sender).Value;
        }

        private void numHiddingDur_ValueChanged(object sender, EventArgs e)
        {
            tooltipAlertFactory1.HidingDuration = (int)((NumericUpDown)sender).Value;
        }

        private void numShowingDur_ValueChanged(object sender, EventArgs e)
        {
            tooltipAlertFactory1.ShowingDuration = (int)((NumericUpDown)sender).Value;
        }

        private void cbHiddingStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            tooltipAlertFactory1.HidingStyle = (HidingStyle)((ComboBox)sender).SelectedIndex;
        }

        private void cbShowingStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            tooltipAlertFactory1.ShowingStyle = (ShowingStyle)((ComboBox)sender).SelectedIndex;
        }

        private void btnTitleFont_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = btnTitleFont.Font;
            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btnTitleFont.Font = fontDialog1.Font;
                customTooltipAlertRenderer1.TitleFont = fontDialog1.Font;
            }
        }

        private void btnTextFont_Click(object sender, EventArgs e)
        {

        }

        private void picTitleColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = customTooltipAlertRenderer1.TitleForeColor;
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                picTitleColor.BackColor = colorDialog1.Color;
                customTooltipAlertRenderer1.TitleForeColor = colorDialog1.Color;
            }
        }

        private void picTextColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = customTooltipAlertRenderer1.ForeColor;
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                picTextColor.BackColor = colorDialog1.Color;
                customTooltipAlertRenderer1.ForeColor = colorDialog1.Color;
            }

        }

        private void picBackColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = customTooltipAlertRenderer1.BackColor;
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                picBackColor.BackColor = colorDialog1.Color;
                customTooltipAlertRenderer1.BackColor = colorDialog1.Color;
            }

        }

        private void picBorderColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = customTooltipAlertRenderer1.BorderColor;
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                picBorderColor.BackColor = colorDialog1.Color;
                customTooltipAlertRenderer1.BorderColor = colorDialog1.Color;
            }
        }

        private void numBorderThickenss_ValueChanged(object sender, EventArgs e)
        {
            customTooltipAlertRenderer1.BorderThickness = (int)numBorderThickenss.Value;
        }

        private void numVGap_ValueChanged(object sender, EventArgs e)
        {
            popupAlertManager1.VGap = (int)numVGap.Value;
        }

        private void numAlertMaxCount_ValueChanged(object sender, EventArgs e)
        {
            popupAlertManager1.AlertsMaxCount = (int)numAlertMaxCount.Value;
        }

        private void numAll_ValueChanged(object sender, EventArgs e)
        {
            numTopLeft.Value = numAll.Value;
            numTopRight.Value = numAll.Value;
            numBottomLeft.Value = numAll.Value;
            numBottomRight.Value = numAll.Value;
        }

        private void numTopLeft_ValueChanged(object sender, EventArgs e)
        {
            customTooltipAlertRenderer1.CornerRadius.TopLeft = (int)((NumericUpDown)sender).Value;
        }

        private void numTopRight_ValueChanged(object sender, EventArgs e)
        {

            customTooltipAlertRenderer1.CornerRadius.TopRight = (int)((NumericUpDown)sender).Value;
        }

        private void numBottomLeft_ValueChanged(object sender, EventArgs e)
        {
            customTooltipAlertRenderer1.CornerRadius.BottomLeft = (int)((NumericUpDown)sender).Value;
        }

        private void numBottomRight_ValueChanged(object sender, EventArgs e)
        {
            customTooltipAlertRenderer1.CornerRadius.BottomRight = (int)((NumericUpDown)sender).Value;
        }

        private void rbRandomAlert_CheckedChanged(object sender, EventArgs e)
        {
            panel3.Enabled = !rbRandomAlert.Checked;
        }

        private void numMaxWidth_ValueChanged(object sender, EventArgs e)
        {
            tooltipAlertFactory1.MaximumSize = new Size((int)numMaxWidth.Value, tooltipAlertFactory1.MaximumSize.Height);
        }

        private void numMinWidth_ValueChanged(object sender, EventArgs e)
        {
            tooltipAlertFactory1.MinimumSize = new Size((int)numMinWidth.Value, tooltipAlertFactory1.MinimumSize.Height);
        }


    }
}
