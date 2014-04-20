namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.popupAlertManager1 = new WinFormsPopupAlerts.PopupAlertManager(this.components);
            this.tooltipAlertFactory1 = new WinFormsPopupAlerts.TooltipAlertFactory(this.components);
            this.customTooltipAlertRenderer1 = new WinFormsPopupAlerts.CustomTooltipAlertRenderer();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 70);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.toolTip1.ToolTipTitle = "TEstTest";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // popupAlertManager1
            // 
            this.popupAlertManager1.AlertAlignment = WinFormsPopupAlerts.PopupAlertAlignment.TopRight;
            this.popupAlertManager1.AlertFactory = this.tooltipAlertFactory1;
            this.popupAlertManager1.AlertsMaxCount = 5;
            this.popupAlertManager1.ContainerControl = this;
            this.popupAlertManager1.PopupDuration = 100;
            // 
            // tooltipAlertFactory1
            // 
            this.tooltipAlertFactory1.AlertStyle = WinFormsPopupAlerts.TooltipAlertStyle.Custom;
            this.tooltipAlertFactory1.CustomRenderer = this.customTooltipAlertRenderer1;
            this.tooltipAlertFactory1.HiddingDuration = 100;
            this.tooltipAlertFactory1.HiddingStyle = WinFormsPopupAlerts.HiddingStyle.Slide;
            this.tooltipAlertFactory1.MaximumSize = new System.Drawing.Size(300, 300);
            this.tooltipAlertFactory1.MinimumSize = new System.Drawing.Size(250, 0);
            this.tooltipAlertFactory1.Padding = new System.Windows.Forms.Padding(5);
            this.tooltipAlertFactory1.ShowingDuration = 100;
            // 
            // customTooltipAlertRenderer1
            // 
            this.customTooltipAlertRenderer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.customTooltipAlertRenderer1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.customTooltipAlertRenderer1.BorderThickness = 5;
            this.customTooltipAlertRenderer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.customTooltipAlertRenderer1.ForeColor = System.Drawing.Color.Black;
            this.customTooltipAlertRenderer1.IconPadding = new System.Windows.Forms.Padding(3, 4, 5, 4);
            this.customTooltipAlertRenderer1.MaxSize = new System.Drawing.Size(0, 0);
            this.customTooltipAlertRenderer1.MinSize = new System.Drawing.Size(0, 0);
            this.customTooltipAlertRenderer1.Padding = new System.Windows.Forms.Padding(10);
            this.customTooltipAlertRenderer1.TitleBackColor = System.Drawing.Color.Green;
            this.customTooltipAlertRenderer1.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.customTooltipAlertRenderer1.TitleForeColor = System.Drawing.Color.White;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private WinFormsPopupAlerts.PopupAlertManager popupAlertManager1;
        private WinFormsPopupAlerts.TooltipAlertFactory tooltipAlertFactory1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer1;
        private WinFormsPopupAlerts.CustomTooltipAlertRenderer customTooltipAlertRenderer1;
    }
}

