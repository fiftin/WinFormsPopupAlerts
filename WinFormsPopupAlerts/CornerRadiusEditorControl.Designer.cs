namespace WinFormsPopupAlerts
{
    partial class CornerRadiusEditorControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.numLeftTop = new System.Windows.Forms.NumericUpDown();
            this.numTopRight = new System.Windows.Forms.NumericUpDown();
            this.numBottomLeft = new System.Windows.Forms.NumericUpDown();
            this.numBottomRight = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numLeftTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTopRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBottomLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBottomRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // numLeftTop
            // 
            this.numLeftTop.Location = new System.Drawing.Point(19, 20);
            this.numLeftTop.Name = "numLeftTop";
            this.numLeftTop.Size = new System.Drawing.Size(42, 20);
            this.numLeftTop.TabIndex = 0;
            this.numLeftTop.VisibleChanged += new System.EventHandler(this.num_VisibleChanged);
            // 
            // numTopRight
            // 
            this.numTopRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numTopRight.Location = new System.Drawing.Point(111, 20);
            this.numTopRight.Name = "numTopRight";
            this.numTopRight.Size = new System.Drawing.Size(42, 20);
            this.numTopRight.TabIndex = 1;
            // 
            // numBottomLeft
            // 
            this.numBottomLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numBottomLeft.Location = new System.Drawing.Point(19, 95);
            this.numBottomLeft.Name = "numBottomLeft";
            this.numBottomLeft.Size = new System.Drawing.Size(42, 20);
            this.numBottomLeft.TabIndex = 2;
            this.numBottomLeft.VisibleChanged += new System.EventHandler(this.num_VisibleChanged);
            // 
            // numBottomRight
            // 
            this.numBottomRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numBottomRight.Location = new System.Drawing.Point(111, 95);
            this.numBottomRight.Name = "numBottomRight";
            this.numBottomRight.Size = new System.Drawing.Size(42, 20);
            this.numBottomRight.TabIndex = 3;
            this.numBottomRight.VisibleChanged += new System.EventHandler(this.num_VisibleChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(65, 59);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(42, 20);
            this.numericUpDown1.TabIndex = 4;
            // 
            // CornerRadiusEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.numBottomRight);
            this.Controls.Add(this.numBottomLeft);
            this.Controls.Add(this.numTopRight);
            this.Controls.Add(this.numLeftTop);
            this.Name = "CornerRadiusEditorControl";
            this.Size = new System.Drawing.Size(172, 133);
            this.VisibleChanged += new System.EventHandler(this.num_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.numLeftTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTopRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBottomLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBottomRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numLeftTop;
        private System.Windows.Forms.NumericUpDown numTopRight;
        private System.Windows.Forms.NumericUpDown numBottomLeft;
        private System.Windows.Forms.NumericUpDown numBottomRight;
        private System.Windows.Forms.NumericUpDown numericUpDown1;

    }
}
