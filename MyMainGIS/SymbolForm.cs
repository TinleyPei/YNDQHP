using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Controls;
using MyMainGIS.Library;
using ESRI.ArcGIS.Carto;

namespace MyMainGIS
{
    public partial class SymbolForm : Form
    {
        public IStyleGalleryItem m_styleGalleryItem;
        private esriSymbologyStyleClass _symbolStyle;//符号样式的类型：点、线、面
        

        public SymbolForm(ISimpleRenderer currentSimpleRender)
        {
            InitializeComponent();
            m_styleGalleryItem = new ServerStyleGalleryItemClass();
            m_styleGalleryItem.Item = (object)currentSimpleRender.Symbol;//窗体初始加载时显示当前图层的当前样式图标

        }

        private void SymbolForm_Load(object sender, EventArgs e)
        {
            //获取ArcGIS的安装路径
            //string sInstall = ReadRegisty("SOFTWARE\\ESRI\\ArcGIS\\CoreRuntime");
            string sInstall = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path; //SysConfig.ArcGISDesktopeInstallDir;
            //将ESRI.ServerStyle文件载入控件中
            axSymbologyControl1.LoadStyleFile(sInstall + "\\Styles\\ESRI.ServerStyle");

            //初始化属性控件
            InitControls();

            PreviewImage();
        }

        /// <summary>
        /// 设置控件的初始值
        /// </summary>
        private void InitControls()
        {
            double symbolSize = 0;
            string sizeCaption = "大小：";//大小控件的标签文本
            Point locationPoint;
            Color fillColor;
            Color outLineColor;
            IRgbColor rgbColor = null;
            IRgbColor outRgbColor = null;
            
            this.symbolSize.Minimum = 0;            
            this.symbolSize.Maximum = 100;
            this.symbolSize.DecimalPlaces = 2;//小数点位数

            ISymbol pSymbol = m_styleGalleryItem.Item as ISymbol;
            if (pSymbol == null)
            {
                return;
            }

            if (pSymbol is IMarkerSymbol)
            {
                sizeCaption = "大小：";
                locationPoint = new Point(28, 139);
                symbolSize = (pSymbol as IMarkerSymbol).Size;
                rgbColor = (pSymbol as IMarkerSymbol).Color as IRgbColor;
            }
            else if (pSymbol is ILineSymbol)
            {
                sizeCaption = "宽度：";
                locationPoint = new Point(28, 139);
                symbolSize = (pSymbol as ILineSymbol).Width;
                rgbColor = (pSymbol as ILineSymbol).Color as IRgbColor;
            }
            else if (pSymbol is IFillSymbol)
            {
                sizeCaption = "轮廓宽度：";
                locationPoint = new Point(12, 139);
                symbolSize = (pSymbol as IFillSymbol).Outline.Width;
                rgbColor = (pSymbol as IFillSymbol).Color as IRgbColor;
                outRgbColor = (pSymbol as IFillSymbol).Outline.Color as IRgbColor;
            }
            else
            {
                sizeCaption = "大小：";
                locationPoint = new Point(28, 139);
                symbolSize = 0;
                
            }

            //颜色控件赋值
            if (rgbColor != null)
            {
                fillColor = Color.FromArgb(rgbColor.Red, rgbColor.Green, rgbColor.Blue);
                btnFillColor.BackColor = fillColor;
            }

            if (outRgbColor != null)
            {
                outLineColor = Color.FromArgb(outRgbColor.Red, outRgbColor.Green, outRgbColor.Blue);
                btnOutLineColor.BackColor = outLineColor;
            }
            else
            {
                labelOutColorCaption.Visible = false;
                btnOutLineColor.Visible = false;
            }

            this.labelSizeCaption.Text = sizeCaption;
            this.labelSizeCaption.Location = locationPoint;

            //SetLabelCaptionBySymbolStyle(m_styleGalleryItem.Item as ISymbol);
            this.symbolSize.Value = Convert.ToDecimal(symbolSize);          
            
        }

        private string ReadRegisty(string sKey)
        {
            //打开注册表键
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sKey,true);
            if (rk == null)
            {
                return "";
            }
            //获取ArcGIS的安装路径
            return (string)rk.GetValue("InstallDir");
        }

        private void axSymbologyControl1_OnItemSelected(object sender, ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnItemSelectedEvent e)
        {           
            m_styleGalleryItem = e.styleGalleryItem as IStyleGalleryItem;

            //预览被选择的样式项
            PreviewImage();

            //更新大小属性控件的值
            this.symbolSize.Value = Convert.ToDecimal(GetSizeFromSymbol(m_styleGalleryItem.Item as ISymbol));
            //更新颜色控件的颜色

        }

        private void PreviewImage()
        {
            //获得样式类
            ISymbologyStyleClass symbolgyStyleClass = axSymbologyControl1.GetStyleClass(axSymbologyControl1.StyleClass);
            stdole.IPictureDisp picture = symbolgyStyleClass.PreviewItem(m_styleGalleryItem, symbolPictureBox.Width, symbolPictureBox.Height);
            System.Drawing.Image image = System.Drawing.Image.FromHbitmap(new System.IntPtr(picture.Handle));
            symbolPictureBox.Image = image;
        }

        public IStyleGalleryItem GetItem(esriSymbologyStyleClass styleClass)
        {
            //m_styleGalleryItem = null;
            _symbolStyle = styleClass;

            this.axSymbologyControl1.StyleClass = styleClass;
            this.axSymbologyControl1.GetStyleClass(styleClass).UnselectItem();

            //显示模式对话框
            this.ShowDialog();

            return m_styleGalleryItem;
        }
               
        /// <summary>
        /// 符号大小改变时，符号预览随之变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void symbolSize_ValueChanged(object sender, EventArgs e)
        {
            ISymbol pSymbol = m_styleGalleryItem.Item as ISymbol;
            if (symbolSize.Value < 0 || symbolSize.Value > 100)
            {
                MessageBox.Show("请输入一个0和100之间的数字。");
            }
            double tempSize = Convert.ToDouble(symbolSize.Value);

            if (pSymbol is IMarkerSymbol)
            {
                IMarkerSymbol markerSymbol = pSymbol as IMarkerSymbol;
                markerSymbol.Size = tempSize;

            }
            else if (pSymbol is ILineSymbol)
            {
                ILineSymbol lineSymbol = pSymbol as ILineSymbol;
                lineSymbol.Width = tempSize;
            }
            else if (pSymbol is IFillSymbol)
            {
                IFillSymbol fillSymbol = pSymbol as IFillSymbol;
                ILineSymbol pLineSymbol = fillSymbol.Outline;
                pLineSymbol.Width = tempSize;
                fillSymbol.Outline = pLineSymbol;
            }
            else
                return;

            PreviewImage();

        }

        private void SetLabelCaptionBySymbolStyle(ISymbol pSymbol)
        {
            if (pSymbol == null)
            {
                return;
            }

            string sizeCaption = "大小：";//大小控件的标签文本
            Point locationPoint;
            if (pSymbol is ILineSymbol)
            {
                sizeCaption = "宽度：";
                locationPoint = new Point(28, 139);
            }
            else if (pSymbol is IFillSymbol)
            {
                sizeCaption = "轮廓宽度：";
                locationPoint = new Point(12, 139);

            }
            else
            {
                sizeCaption = "大小：";
                locationPoint = new Point(28, 139);
            }

            this.labelSizeCaption.Text = sizeCaption;
            this.labelSizeCaption.Location = locationPoint;
        }


        private double GetSizeFromSymbol(ISymbol pSymbol)
        {
            if (pSymbol == null)
            {
                return -1;
            }

            if (pSymbol is IMarkerSymbol)
            {
                return (pSymbol as IMarkerSymbol).Size;

            }
            else if (pSymbol is ILineSymbol)
            {
                return (pSymbol as ILineSymbol).Width;
            }
            else if (pSymbol is IFillSymbol)
            {
                return (pSymbol as IFillSymbol).Outline.Width;
            }
            else
                return 0;
        }

        private void btnFillColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                btnFillColor.BackColor = colorDialog.Color;

                ISymbol pSymbol = m_styleGalleryItem.Item as ISymbol;
                if (pSymbol == null) return;

                if (pSymbol is IMarkerSymbol)
                {
                    IMarkerSymbol markerSymbol = pSymbol as IMarkerSymbol;
                    markerSymbol.Color = GetRGBColor(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
                }
                else if (pSymbol is ILineSymbol)
                {
                    ILineSymbol lineSymbol = pSymbol as ILineSymbol;
                    lineSymbol.Color = GetRGBColor(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
                }
                else if (pSymbol is IFillSymbol)
                {
                    IFillSymbol fillSymbol = pSymbol as IFillSymbol;
                    fillSymbol.Color = GetRGBColor(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
                }
                else
                    return;

                PreviewImage();
            }
        }

        private void btnOutLineColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                btnOutLineColor.BackColor = colorDialog.Color;
                ISymbol pSymbol = m_styleGalleryItem.Item as ISymbol;
                if (pSymbol is IFillSymbol)
                {
                    IFillSymbol fillSymbol = pSymbol as IFillSymbol;
                    ILineSymbol pLineSymbol = fillSymbol.Outline;
                    pLineSymbol.Color = GetRGBColor(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
                    fillSymbol.Outline = pLineSymbol;
                }
                else
                    return;

                PreviewImage();
            }
        }

        private IRgbColor GetRGBColor(int red, int green, int blue)
        {
            IRgbColor pRgbColor;
            pRgbColor = new RgbColorClass();
            pRgbColor.Red = red;
            pRgbColor.Green = green;
            pRgbColor.Blue = blue;

            return pRgbColor;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_styleGalleryItem = null;
            this.Hide();
        }
    }
}
