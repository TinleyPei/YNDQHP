namespace EcoRedLine
{
    partial class frmWaterSoilConservation
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWaterSoilConservation));
            this.cmbC = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbtnNDVI = new System.Windows.Forms.RadioButton();
            this.rbtnVegCover = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbSoilOrganic = new System.Windows.Forms.ComboBox();
            this.cmbSoilSand = new System.Windows.Forms.ComboBox();
            this.cmbSoilSlit = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSoilClay = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnPcp = new System.Windows.Forms.RadioButton();
            this.txtPcpSuffix = new System.Windows.Forms.TextBox();
            this.txtPcpPrefix = new System.Windows.Forms.TextBox();
            this.txtPcpPath = new System.Windows.Forms.TextBox();
            this.rbtnR = new System.Windows.Forms.RadioButton();
            this.btnOpenPcp = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbR = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbP = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtCellSize = new System.Windows.Forms.TextBox();
            this.chkbCellsize = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDem = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtSavePath = new System.Windows.Forms.TextBox();
            this.btnSavePath = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbC
            // 
            this.cmbC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbC.FormattingEnabled = true;
            this.cmbC.Location = new System.Drawing.Point(314, 14);
            this.cmbC.Name = "cmbC";
            this.cmbC.Size = new System.Drawing.Size(218, 20);
            this.cmbC.TabIndex = 6;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(457, 454);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(341, 454);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbtnNDVI);
            this.groupBox5.Controls.Add(this.rbtnVegCover);
            this.groupBox5.Controls.Add(this.cmbC);
            this.groupBox5.Location = new System.Drawing.Point(15, 291);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(547, 42);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "地表覆被因子C[0-1]";
            // 
            // rbtnNDVI
            // 
            this.rbtnNDVI.AutoSize = true;
            this.rbtnNDVI.Location = new System.Drawing.Point(151, 15);
            this.rbtnNDVI.Name = "rbtnNDVI";
            this.rbtnNDVI.Size = new System.Drawing.Size(143, 16);
            this.rbtnNDVI.TabIndex = 7;
            this.rbtnNDVI.TabStop = true;
            this.rbtnNDVI.Text = "归一化植被指数，NDVI";
            this.rbtnNDVI.UseVisualStyleBackColor = true;
            // 
            // rbtnVegCover
            // 
            this.rbtnVegCover.AutoSize = true;
            this.rbtnVegCover.Checked = true;
            this.rbtnVegCover.Location = new System.Drawing.Point(31, 18);
            this.rbtnVegCover.Name = "rbtnVegCover";
            this.rbtnVegCover.Size = new System.Drawing.Size(83, 16);
            this.rbtnVegCover.TabIndex = 7;
            this.rbtnVegCover.TabStop = true;
            this.rbtnVegCover.Text = "植被覆盖度";
            this.rbtnVegCover.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbSoilOrganic);
            this.groupBox2.Controls.Add(this.cmbSoilSand);
            this.groupBox2.Controls.Add(this.cmbSoilSlit);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmbSoilClay);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(15, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(547, 93);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "土壤可蚀力因子K";
            // 
            // cmbSoilOrganic
            // 
            this.cmbSoilOrganic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSoilOrganic.FormattingEnabled = true;
            this.cmbSoilOrganic.Location = new System.Drawing.Point(392, 56);
            this.cmbSoilOrganic.Name = "cmbSoilOrganic";
            this.cmbSoilOrganic.Size = new System.Drawing.Size(140, 20);
            this.cmbSoilOrganic.TabIndex = 8;
            // 
            // cmbSoilSand
            // 
            this.cmbSoilSand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSoilSand.FormattingEnabled = true;
            this.cmbSoilSand.Location = new System.Drawing.Point(130, 56);
            this.cmbSoilSand.Name = "cmbSoilSand";
            this.cmbSoilSand.Size = new System.Drawing.Size(140, 20);
            this.cmbSoilSand.TabIndex = 7;
            // 
            // cmbSoilSlit
            // 
            this.cmbSoilSlit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSoilSlit.FormattingEnabled = true;
            this.cmbSoilSlit.Location = new System.Drawing.Point(392, 21);
            this.cmbSoilSlit.Name = "cmbSoilSlit";
            this.cmbSoilSlit.Size = new System.Drawing.Size(140, 20);
            this.cmbSoilSlit.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(25, 62);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 12);
            this.label15.TabIndex = 2;
            this.label15.Text = "土壤砂粒含量(%)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(296, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "土壤粉粒含量(%)";
            // 
            // cmbSoilClay
            // 
            this.cmbSoilClay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSoilClay.FormattingEnabled = true;
            this.cmbSoilClay.Location = new System.Drawing.Point(130, 23);
            this.cmbSoilClay.Name = "cmbSoilClay";
            this.cmbSoilClay.Size = new System.Drawing.Size(140, 20);
            this.cmbSoilClay.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "土壤黏粒含量(%)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(296, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "有机物含量(%)";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightGray;
            this.groupBox1.Controls.Add(this.rbtnPcp);
            this.groupBox1.Controls.Add(this.txtPcpSuffix);
            this.groupBox1.Controls.Add(this.txtPcpPrefix);
            this.groupBox1.Controls.Add(this.txtPcpPath);
            this.groupBox1.Controls.Add(this.rbtnR);
            this.groupBox1.Controls.Add(this.btnOpenPcp);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cmbR);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(15, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(547, 121);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "降雨侵蚀力因子R ";
            // 
            // rbtnPcp
            // 
            this.rbtnPcp.AutoSize = true;
            this.rbtnPcp.Location = new System.Drawing.Point(19, 57);
            this.rbtnPcp.Name = "rbtnPcp";
            this.rbtnPcp.Size = new System.Drawing.Size(191, 16);
            this.rbtnPcp.TabIndex = 9;
            this.rbtnPcp.Text = "多年平均各月降雨量数据 P(mm)";
            this.rbtnPcp.UseVisualStyleBackColor = true;
            this.rbtnPcp.CheckedChanged += new System.EventHandler(this.rbtnPcp_CheckedChanged);
            // 
            // txtPcpSuffix
            // 
            this.txtPcpSuffix.Enabled = false;
            this.txtPcpSuffix.Location = new System.Drawing.Point(423, 88);
            this.txtPcpSuffix.Name = "txtPcpSuffix";
            this.txtPcpSuffix.Size = new System.Drawing.Size(77, 21);
            this.txtPcpSuffix.TabIndex = 27;
            // 
            // txtPcpPrefix
            // 
            this.txtPcpPrefix.Enabled = false;
            this.txtPcpPrefix.Location = new System.Drawing.Point(216, 85);
            this.txtPcpPrefix.Name = "txtPcpPrefix";
            this.txtPcpPrefix.Size = new System.Drawing.Size(77, 21);
            this.txtPcpPrefix.TabIndex = 27;
            // 
            // txtPcpPath
            // 
            this.txtPcpPath.Enabled = false;
            this.txtPcpPath.Location = new System.Drawing.Point(216, 56);
            this.txtPcpPath.Name = "txtPcpPath";
            this.txtPcpPath.Size = new System.Drawing.Size(284, 21);
            this.txtPcpPath.TabIndex = 27;
            this.txtPcpPath.Text = "请输入月降水数据的上一级目录...(格式：pcp_1m)";
            // 
            // rbtnR
            // 
            this.rbtnR.AutoSize = true;
            this.rbtnR.Checked = true;
            this.rbtnR.Location = new System.Drawing.Point(19, 25);
            this.rbtnR.Name = "rbtnR";
            this.rbtnR.Size = new System.Drawing.Size(197, 16);
            this.rbtnR.TabIndex = 9;
            this.rbtnR.TabStop = true;
            this.rbtnR.Text = "降雨侵蚀力(MJ*mm)/(ha*h*year)";
            this.rbtnR.UseVisualStyleBackColor = true;
            // 
            // btnOpenPcp
            // 
            this.btnOpenPcp.Enabled = false;
            this.btnOpenPcp.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenPcp.Image")));
            this.btnOpenPcp.Location = new System.Drawing.Point(507, 53);
            this.btnOpenPcp.Name = "btnOpenPcp";
            this.btnOpenPcp.Size = new System.Drawing.Size(25, 25);
            this.btnOpenPcp.TabIndex = 26;
            this.btnOpenPcp.UseVisualStyleBackColor = true;
            this.btnOpenPcp.Click += new System.EventHandler(this.btnOpenPcp_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(309, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "数据后缀(如：*m)";
            // 
            // cmbR
            // 
            this.cmbR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbR.FormattingEnabled = true;
            this.cmbR.Location = new System.Drawing.Point(216, 22);
            this.cmbR.Name = "cmbR";
            this.cmbR.Size = new System.Drawing.Size(284, 20);
            this.cmbR.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(92, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "数据前缀(如：pcp_*)";
            // 
            // cmbP
            // 
            this.cmbP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbP.FormattingEnabled = true;
            this.cmbP.Location = new System.Drawing.Point(46, 358);
            this.cmbP.Name = "cmbP";
            this.cmbP.Size = new System.Drawing.Size(501, 20);
            this.cmbP.TabIndex = 7;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtCellSize);
            this.groupBox4.Controls.Add(this.chkbCellsize);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.cmbDem);
            this.groupBox4.Location = new System.Drawing.Point(15, 234);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(547, 42);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "地形因子LS ";
            // 
            // txtCellSize
            // 
            this.txtCellSize.Enabled = false;
            this.txtCellSize.Location = new System.Drawing.Point(432, 15);
            this.txtCellSize.Name = "txtCellSize";
            this.txtCellSize.Size = new System.Drawing.Size(100, 21);
            this.txtCellSize.TabIndex = 24;
            // 
            // chkbCellsize
            // 
            this.chkbCellsize.AutoSize = true;
            this.chkbCellsize.Location = new System.Drawing.Point(341, 18);
            this.chkbCellsize.Name = "chkbCellsize";
            this.chkbCellsize.Size = new System.Drawing.Size(90, 16);
            this.chkbCellsize.TabIndex = 25;
            this.chkbCellsize.Text = "栅格大小(m)";
            this.chkbCellsize.UseVisualStyleBackColor = true;
            this.chkbCellsize.CheckedChanged += new System.EventHandler(this.chkbCellsize_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "数字高程模型，DEM(m)";
            // 
            // cmbDem
            // 
            this.cmbDem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDem.FormattingEnabled = true;
            this.cmbDem.Location = new System.Drawing.Point(160, 16);
            this.cmbDem.Name = "cmbDem";
            this.cmbDem.Size = new System.Drawing.Size(166, 20);
            this.cmbDem.TabIndex = 7;
            this.cmbDem.SelectedIndexChanged += new System.EventHandler(this.cmbDem_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(15, 339);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(258, 49);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "水土保持措施因子P[0-1]";
            // 
            // txtSavePath
            // 
            this.txtSavePath.Location = new System.Drawing.Point(26, 27);
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.Size = new System.Drawing.Size(474, 21);
            this.txtSavePath.TabIndex = 27;
            // 
            // btnSavePath
            // 
            this.btnSavePath.Image = ((System.Drawing.Image)(resources.GetObject("btnSavePath.Image")));
            this.btnSavePath.Location = new System.Drawing.Point(507, 24);
            this.btnSavePath.Name = "btnSavePath";
            this.btnSavePath.Size = new System.Drawing.Size(25, 25);
            this.btnSavePath.TabIndex = 26;
            this.btnSavePath.UseVisualStyleBackColor = true;
            this.btnSavePath.Click += new System.EventHandler(this.btnSavePath_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnSavePath);
            this.groupBox6.Controls.Add(this.txtSavePath);
            this.groupBox6.Location = new System.Drawing.Point(15, 394);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(547, 63);
            this.groupBox6.TabIndex = 18;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "水土保持生态红线";
            // 
            // frmWaterSoilConservation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 498);
            this.Controls.Add(this.cmbP);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmWaterSoilConservation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "水土保持生态红线";
            this.Load += new System.EventHandler(this.frmSoilRunOffSpatial_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbC;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbSoilOrganic;
        private System.Windows.Forms.ComboBox cmbSoilClay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbP;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbDem;
        private System.Windows.Forms.TextBox txtCellSize;
        private System.Windows.Forms.ComboBox cmbSoilSand;
        private System.Windows.Forms.ComboBox cmbSoilSlit;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtnNDVI;
        private System.Windows.Forms.RadioButton rbtnVegCover;
        private System.Windows.Forms.CheckBox chkbCellsize;
        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.Button btnSavePath;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnOpenPcp;
        private System.Windows.Forms.RadioButton rbtnR;
        private System.Windows.Forms.ComboBox cmbR;
        private System.Windows.Forms.RadioButton rbtnPcp;
        private System.Windows.Forms.TextBox txtPcpPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPcpPrefix;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPcpSuffix;
        private System.Windows.Forms.Label label6;
    }
}