namespace EcoRedLine
{
    partial class frmWaterConervation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWaterConervation));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPcp = new System.Windows.Forms.ComboBox();
            this.btnOpenPcp = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCN = new System.Windows.Forms.ComboBox();
            this.btnOpenCN = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtSavePath = new System.Windows.Forms.TextBox();
            this.btnSavePath = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "总降水量（mm）：";
            // 
            // cmbPcp
            // 
            this.cmbPcp.FormattingEnabled = true;
            this.cmbPcp.Location = new System.Drawing.Point(27, 50);
            this.cmbPcp.Name = "cmbPcp";
            this.cmbPcp.Size = new System.Drawing.Size(348, 20);
            this.cmbPcp.TabIndex = 1;
            // 
            // btnOpenPcp
            // 
            this.btnOpenPcp.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenPcp.Image")));
            this.btnOpenPcp.Location = new System.Drawing.Point(391, 49);
            this.btnOpenPcp.Name = "btnOpenPcp";
            this.btnOpenPcp.Size = new System.Drawing.Size(25, 25);
            this.btnOpenPcp.TabIndex = 2;
            this.btnOpenPcp.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(180, 341);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确 定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(321, 341);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取 消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "无因次参数（CN）值[30-100]：";
            // 
            // cmbCN
            // 
            this.cmbCN.FormattingEnabled = true;
            this.cmbCN.Location = new System.Drawing.Point(27, 100);
            this.cmbCN.Name = "cmbCN";
            this.cmbCN.Size = new System.Drawing.Size(348, 20);
            this.cmbCN.TabIndex = 1;
            // 
            // btnOpenCN
            // 
            this.btnOpenCN.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenCN.Image")));
            this.btnOpenCN.Location = new System.Drawing.Point(390, 97);
            this.btnOpenCN.Name = "btnOpenCN";
            this.btnOpenCN.Size = new System.Drawing.Size(25, 25);
            this.btnOpenCN.TabIndex = 5;
            this.btnOpenCN.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBox1.Location = new System.Drawing.Point(16, 217);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(380, 94);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "添加土地利用对应的CN值功能";
            // 
            // txtSavePath
            // 
            this.txtSavePath.Location = new System.Drawing.Point(27, 160);
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.Size = new System.Drawing.Size(348, 21);
            this.txtSavePath.TabIndex = 6;
            // 
            // btnSavePath
            // 
            this.btnSavePath.Image = ((System.Drawing.Image)(resources.GetObject("btnSavePath.Image")));
            this.btnSavePath.Location = new System.Drawing.Point(390, 156);
            this.btnSavePath.Name = "btnSavePath";
            this.btnSavePath.Size = new System.Drawing.Size(25, 25);
            this.btnSavePath.TabIndex = 5;
            this.btnSavePath.UseVisualStyleBackColor = true;
            this.btnSavePath.Click += new System.EventHandler(this.btnSavePath_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "水源涵养生态红线数据保存";
            // 
            // frmWaterConervation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(433, 387);
            this.Controls.Add(this.txtSavePath);
            this.Controls.Add(this.btnSavePath);
            this.Controls.Add(this.btnOpenCN);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnOpenPcp);
            this.Controls.Add(this.cmbCN);
            this.Controls.Add(this.cmbPcp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmWaterConervation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "水源涵养生态红线";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPcp;
        private System.Windows.Forms.Button btnOpenPcp;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCN;
        private System.Windows.Forms.Button btnOpenCN;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.Button btnSavePath;
        private System.Windows.Forms.Label label3;
    }
}