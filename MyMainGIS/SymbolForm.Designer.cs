namespace MyMainGIS
{
    partial class SymbolForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SymbolForm));
            this.symbolPictureBox = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.axSymbologyControl1 = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.symbolSize = new System.Windows.Forms.NumericUpDown();
            this.labelSizeCaption = new System.Windows.Forms.Label();
            this.labelFillColorCaption = new System.Windows.Forms.Label();
            this.labelOutColorCaption = new System.Windows.Forms.Label();
            this.btnFillColor = new System.Windows.Forms.Button();
            this.btnOutLineColor = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.symbolPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.symbolSize)).BeginInit();
            this.SuspendLayout();
            // 
            // symbolPictureBox
            // 
            this.symbolPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.symbolPictureBox.Location = new System.Drawing.Point(42, 36);
            this.symbolPictureBox.Name = "symbolPictureBox";
            this.symbolPictureBox.Size = new System.Drawing.Size(93, 35);
            this.symbolPictureBox.TabIndex = 1;
            this.symbolPictureBox.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(320, 345);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 28);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(406, 345);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // axSymbologyControl1
            // 
            this.axSymbologyControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.axSymbologyControl1.Location = new System.Drawing.Point(0, 0);
            this.axSymbologyControl1.Name = "axSymbologyControl1";
            this.axSymbologyControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl1.OcxState")));
            this.axSymbologyControl1.Size = new System.Drawing.Size(303, 380);
            this.axSymbologyControl1.TabIndex = 0;
            this.axSymbologyControl1.OnItemSelected += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnItemSelectedEventHandler(this.axSymbologyControl1_OnItemSelected);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.symbolSize);
            this.groupBox1.Controls.Add(this.labelFillColorCaption);
            this.groupBox1.Controls.Add(this.btnOutLineColor);
            this.groupBox1.Controls.Add(this.btnFillColor);
            this.groupBox1.Controls.Add(this.labelOutColorCaption);
            this.groupBox1.Controls.Add(this.labelSizeCaption);
            this.groupBox1.Controls.Add(this.symbolPictureBox);
            this.groupBox1.Location = new System.Drawing.Point(318, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 222);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "当前符号";
            // 
            // symbolSize
            // 
            this.symbolSize.Location = new System.Drawing.Point(80, 135);
            this.symbolSize.Name = "symbolSize";
            this.symbolSize.Size = new System.Drawing.Size(66, 21);
            this.symbolSize.TabIndex = 3;
            this.symbolSize.ValueChanged += new System.EventHandler(this.symbolSize_ValueChanged);
            // 
            // labelSizeCaption
            // 
            this.labelSizeCaption.AutoSize = true;
            this.labelSizeCaption.Location = new System.Drawing.Point(28, 139);
            this.labelSizeCaption.Name = "labelSizeCaption";
            this.labelSizeCaption.Size = new System.Drawing.Size(41, 12);
            this.labelSizeCaption.TabIndex = 2;
            this.labelSizeCaption.Text = "大小：";
            // 
            // labelFillColorCaption
            // 
            this.labelFillColorCaption.AutoSize = true;
            this.labelFillColorCaption.Location = new System.Drawing.Point(28, 105);
            this.labelFillColorCaption.Name = "labelFillColorCaption";
            this.labelFillColorCaption.Size = new System.Drawing.Size(41, 12);
            this.labelFillColorCaption.TabIndex = 2;
            this.labelFillColorCaption.Text = "颜色：";
            // 
            // labelOutColorCaption
            // 
            this.labelOutColorCaption.AutoSize = true;
            this.labelOutColorCaption.Location = new System.Drawing.Point(16, 177);
            this.labelOutColorCaption.Name = "labelOutColorCaption";
            this.labelOutColorCaption.Size = new System.Drawing.Size(65, 12);
            this.labelOutColorCaption.TabIndex = 2;
            this.labelOutColorCaption.Text = "轮廓颜色：";
            // 
            // btnFillColor
            // 
            this.btnFillColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFillColor.BackColor = System.Drawing.Color.Red;
            this.btnFillColor.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFillColor.Location = new System.Drawing.Point(88, 95);
            this.btnFillColor.Name = "btnFillColor";
            this.btnFillColor.Size = new System.Drawing.Size(49, 28);
            this.btnFillColor.TabIndex = 2;
            this.btnFillColor.UseVisualStyleBackColor = false;
            this.btnFillColor.Click += new System.EventHandler(this.btnFillColor_Click);
            // 
            // btnOutLineColor
            // 
            this.btnOutLineColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutLineColor.BackColor = System.Drawing.Color.Red;
            this.btnOutLineColor.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOutLineColor.Location = new System.Drawing.Point(88, 168);
            this.btnOutLineColor.Name = "btnOutLineColor";
            this.btnOutLineColor.Size = new System.Drawing.Size(49, 28);
            this.btnOutLineColor.TabIndex = 2;
            this.btnOutLineColor.UseVisualStyleBackColor = false;
            this.btnOutLineColor.Click += new System.EventHandler(this.btnOutLineColor_Click);
            // 
            // SymbolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 380);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.axSymbologyControl1);
            this.Name = "SymbolForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SymbolForm";
            this.Load += new System.EventHandler(this.SymbolForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.symbolPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.symbolSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl1;
        private System.Windows.Forms.PictureBox symbolPictureBox;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown symbolSize;
        private System.Windows.Forms.Label labelSizeCaption;
        private System.Windows.Forms.Label labelFillColorCaption;
        private System.Windows.Forms.Button btnOutLineColor;
        private System.Windows.Forms.Button btnFillColor;
        private System.Windows.Forms.Label labelOutColorCaption;
    }
}