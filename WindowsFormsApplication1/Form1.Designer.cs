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
            this.popupAlertManager1 = new WinFormsPopupAlerts.PopupAlertManager(this.components);
            this.tooltipAlertFactory1 = new WinFormsPopupAlerts.TooltipAlertFactory(this.components);
            this.popupAlertManager2 = new WinFormsPopupAlerts.PopupAlertManager(this.components);
            this.customTooltipAlertRenderer1 = new WinFormsPopupAlerts.CustomTooltipAlertRenderer();
            this.customTooltipAlertRenderer2 = new WinFormsPopupAlerts.CustomTooltipAlertRenderer();
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
            // popupAlertManager1
            // 
            this.popupAlertManager1.AlertFactory = this.tooltipAlertFactory1;
            this.popupAlertManager1.AlertsMaxCount = 12;
            this.popupAlertManager1.ContainerControl = this;
            this.popupAlertManager1.PopupStyle = WinFormsPopupAlerts.PopupStyle.Slide;
            // 
            // tooltipAlertFactory1
            // 
            this.tooltipAlertFactory1.HiddingDuration = 500;
            this.tooltipAlertFactory1.MaximumSize = new System.Drawing.Size(500, 300);
            this.tooltipAlertFactory1.MinimumSize = new System.Drawing.Size(150, 0);
            this.tooltipAlertFactory1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // popupAlertManager2
            // 
            this.popupAlertManager2.AlertFactory = null;
            this.popupAlertManager2.ContainerControl = this;
            // 
            // customTooltipAlertRenderer1
            // 
            this.customTooltipAlertRenderer1.BackColor = System.Drawing.Color.Empty;
            this.customTooltipAlertRenderer1.Font = null;
            this.customTooltipAlertRenderer1.ForeColor = System.Drawing.Color.Empty;
            this.customTooltipAlertRenderer1.IconPadding = new System.Windows.Forms.Padding(3, 4, 5, 4);
            this.customTooltipAlertRenderer1.MaxSize = new System.Drawing.Size(0, 0);
            this.customTooltipAlertRenderer1.MinSize = new System.Drawing.Size(0, 0);
            this.customTooltipAlertRenderer1.Padding = new System.Windows.Forms.Padding(0);
            this.customTooltipAlertRenderer1.TitleBackColor = System.Drawing.Color.Empty;
            this.customTooltipAlertRenderer1.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.customTooltipAlertRenderer1.TitleForeColor = System.Drawing.Color.Empty;
            // 
            // customTooltipAlertRenderer2
            // 
            this.customTooltipAlertRenderer2.BackColor = System.Drawing.Color.Empty;
            this.customTooltipAlertRenderer2.Font = null;
            this.customTooltipAlertRenderer2.ForeColor = System.Drawing.Color.Empty;
            this.customTooltipAlertRenderer2.IconPadding = new System.Windows.Forms.Padding(3, 4, 5, 4);
            this.customTooltipAlertRenderer2.MaxSize = new System.Drawing.Size(0, 0);
            this.customTooltipAlertRenderer2.MinSize = new System.Drawing.Size(0, 0);
            this.customTooltipAlertRenderer2.Padding = new System.Windows.Forms.Padding(0);
            this.customTooltipAlertRenderer2.TitleBackColor = System.Drawing.Color.Empty;
            this.customTooltipAlertRenderer2.TitleFont = null;
            this.customTooltipAlertRenderer2.TitleForeColor = System.Drawing.Color.Empty;
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
        private WinFormsPopupAlerts.PopupAlertManager popupAlertManager2;
        private WinFormsPopupAlerts.CustomTooltipAlertRenderer customTooltipAlertRenderer1;
        private WinFormsPopupAlerts.CustomTooltipAlertRenderer customTooltipAlertRenderer2;
    }
}

