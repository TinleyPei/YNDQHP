namespace VisualMenuBar
{
    partial class BrowseStyleSymbols
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowseStyleSymbols));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxStyleClass = new System.Windows.Forms.ComboBox();
            this.cbxStyles = new System.Windows.Forms.ComboBox();
            this.btnOtherStyles = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.axSymbologyControl1 = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.btnOK = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.Largeimage = new System.Windows.Forms.ImageList(this.components);
            this.Smallimage = new System.Windows.Forms.ImageList(this.components);
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "选择符号类：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "选择样式库：";
            // 
            // cbxStyleClass
            // 
            this.cbxStyleClass.FormattingEnabled = true;
            this.cbxStyleClass.Items.AddRange(new object[] {
            "Reference Systems",
            "Maplex Labels",
            "Shadows",
            "Area Patches",
            "Line Patches",
            "Labels",
            "North Arrows",
            "Scale Bars",
            "Legend Items",
            "Scale Texts",
            "Color Ramps",
            "Borders",
            "Backgrounds",
            "Colors",
            "Vectorization Settings",
            "Fill Symbols",
            "Line Symbols",
            "Marker Symbols",
            "Text Symbols",
            "Hatches"});
            this.cbxStyleClass.Location = new System.Drawing.Point(94, 34);
            this.cbxStyleClass.Name = "cbxStyleClass";
            this.cbxStyleClass.Size = new System.Drawing.Size(305, 20);
            this.cbxStyleClass.TabIndex = 11;
            this.cbxStyleClass.SelectedIndexChanged += new System.EventHandler(this.cbxStyleClass_SelectedIndexChanged);
            // 
            // cbxStyles
            // 
            this.cbxStyles.FormattingEnabled = true;
            this.cbxStyles.Location = new System.Drawing.Point(94, 6);
            this.cbxStyles.Name = "cbxStyles";
            this.cbxStyles.Size = new System.Drawing.Size(305, 20);
            this.cbxStyles.TabIndex = 12;
            this.cbxStyles.SelectedIndexChanged += new System.EventHandler(this.cbxStyles_SelectedIndexChanged);
            // 
            // btnOtherStyles
            // 
            this.btnOtherStyles.Location = new System.Drawing.Point(405, 4);
            this.btnOtherStyles.Name = "btnOtherStyles";
            this.btnOtherStyles.Size = new System.Drawing.Size(40, 25);
            this.btnOtherStyles.TabIndex = 9;
            this.btnOtherStyles.Text = "其它";
            this.btnOtherStyles.Click += new System.EventHandler(this.btnOtherStyles_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(451, 247);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(65, 25);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "关 闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.axSymbologyControl1);
            this.groupBox3.Location = new System.Drawing.Point(14, 57);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(431, 310);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Symbology";
            // 
            // axSymbologyControl1
            // 
            this.axSymbologyControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axSymbologyControl1.Location = new System.Drawing.Point(3, 17);
            this.axSymbologyControl1.Name = "axSymbologyControl1";
            this.axSymbologyControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl1.OcxState")));
            this.axSymbologyControl1.Size = new System.Drawing.Size(425, 290);
            this.axSymbologyControl1.TabIndex = 0;
            this.axSymbologyControl1.OnItemSelected += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnItemSelectedEventHandler(this.axSymbologyControl1_OnItemSelected);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(451, 149);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(65, 25);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确 定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // Largeimage
            // 
            this.Largeimage.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.Largeimage.ImageSize = new System.Drawing.Size(16, 16);
            this.Largeimage.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Smallimage
            // 
            this.Smallimage.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.Smallimage.ImageSize = new System.Drawing.Size(16, 16);
            this.Smallimage.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // BrowseStyleSymbols
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(523, 374);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxStyleClass);
            this.Controls.Add(this.cbxStyles);
            this.Controls.Add(this.btnOtherStyles);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnOK);
            this.DoubleBuffered = true;
            this.Name = "BrowseStyleSymbols";
            this.Text = "浏览样式符号";
            this.Load += new System.EventHandler(this.BrowseStyleSymbols_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxStyleClass;
        private System.Windows.Forms.ComboBox cbxStyles;
        private System.Windows.Forms.Button btnOtherStyles;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox3;
        private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ImageList Largeimage;
        private System.Windows.Forms.ImageList Smallimage;
    }
}