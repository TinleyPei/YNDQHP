namespace CartographicMenuBar
{
    partial class GetSymbol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetSymbol));
            this.label1 = new System.Windows.Forms.Label();
            this.cbxStyles = new System.Windows.Forms.ComboBox();
            this.btnOtherStyles = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.axSymbologyControl1 = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "选择样式库：";
            // 
            // cbxStyles
            // 
            this.cbxStyles.FormattingEnabled = true;
            this.cbxStyles.Location = new System.Drawing.Point(87, 16);
            this.cbxStyles.Name = "cbxStyles";
            this.cbxStyles.Size = new System.Drawing.Size(262, 20);
            this.cbxStyles.TabIndex = 15;
            this.cbxStyles.SelectedIndexChanged += new System.EventHandler(this.cbxStyles_SelectedIndexChanged);
            // 
            // btnOtherStyles
            // 
            this.btnOtherStyles.Location = new System.Drawing.Point(391, 13);
            this.btnOtherStyles.Name = "btnOtherStyles";
            this.btnOtherStyles.Size = new System.Drawing.Size(54, 25);
            this.btnOtherStyles.TabIndex = 14;
            this.btnOtherStyles.Text = "其  它";
            this.btnOtherStyles.Click += new System.EventHandler(this.btnOtherStyles_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(367, 282);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(103, 25);
            this.cmdCancel.TabIndex = 13;
            this.cmdCancel.Text = "取  消";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(367, 349);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(103, 27);
            this.cmdOK.TabIndex = 12;
            this.cmdOK.Text = "确  定";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(355, 64);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(142, 175);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // axSymbologyControl1
            // 
            this.axSymbologyControl1.Location = new System.Drawing.Point(6, 76);
            this.axSymbologyControl1.Name = "axSymbologyControl1";
            this.axSymbologyControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl1.OcxState")));
            this.axSymbologyControl1.Size = new System.Drawing.Size(343, 412);
            this.axSymbologyControl1.TabIndex = 10;
            this.axSymbologyControl1.OnItemSelected += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnItemSelectedEventHandler(this.axSymbologyControl1_OnItemSelected);
            // 
            // GetSymbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(509, 436);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxStyles);
            this.Controls.Add(this.btnOtherStyles);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.axSymbologyControl1);
            this.Name = "GetSymbol";
            this.Text = "选择图式符号";
            this.Load += new System.EventHandler(this.GetSmybol_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxStyles;
        private System.Windows.Forms.Button btnOtherStyles;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.PictureBox pictureBox1;
        public ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}