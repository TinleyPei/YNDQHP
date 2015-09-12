using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using System.IO;
using System.Drawing.Drawing2D;

namespace VisualMenuBar
{
    public partial class BrowseStyleSymbols : DevComponents.DotNetBar.OfficeForm
    {
        public IStyleGalleryItem m_styleGalleryItem = null;

        string stylesPath = string.Empty;
     
        string strStyleClass = "Marker Symbols";

        public BrowseStyleSymbols()
        {
            InitializeComponent();
            //禁用Glass主题
            this.EnableGlass = false;
            //不显示最大化最小化按钮
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            //去除图标
            this.ShowIcon = false;
        }

        private void BrowseStyleSymbols_Load(object sender, EventArgs e)
        {
            //Get the ArcGIS install location
            string sInstall = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path;
            string defaultStyle = System.IO.Path.Combine(sInstall, "Styles\\ESRI.ServerStyle");
            if (System.IO.File.Exists(defaultStyle))
            {
                //Load the ESRI.ServerStyle file into the SymbologyControl
                axSymbologyControl1.LoadStyleFile(defaultStyle);
                //Set the style class
                axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassMarkerSymbols;
                //Select the color ramp item
                axSymbologyControl1.GetStyleClass(axSymbologyControl1.StyleClass).SelectItem(0);
                cbxStyles.Text = defaultStyle;
            }
            stylesPath = sInstall + "\\Styles";
            cbxStyles.Items.Clear();
            cbxStylesAddItems(stylesPath);
        }

        private void cbxStylesAddItems(string path)
        {
            string[] serverstyleFiles = System.IO.Directory.GetFiles(stylesPath, "*.serverstyle", SearchOption.AllDirectories);
            string[] styleFiles = System.IO.Directory.GetFiles(stylesPath, "*.style", SearchOption.AllDirectories);

            cbxStylesAddItems(serverstyleFiles);
            cbxStylesAddItems(styleFiles);
        }

        private void cbxStylesAddItems(string[] files)
        {
            if (files.GetLength(0) == 0) return;
            foreach (string file in files)
            {
                cbxStyles.Items.Add(file);
            }
        }

        private void btnOtherStyles_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                stylesPath = folderBrowserDialog1.SelectedPath;
                cbxStylesAddItems(stylesPath);
            }
        }

        private void cbxStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxStyles.SelectedItem == null) return;
            axSymbologyControl1.Clear();
            stylesPath = cbxStyles.SelectedItem.ToString();
            string ext = System.IO.Path.GetExtension(stylesPath).ToLower();
            if (ext == ".serverstyle")
                axSymbologyControl1.LoadStyleFile(stylesPath);
            if (ext == ".style")
                axSymbologyControl1.LoadDesktopStyleFile(stylesPath);
        }

        private void cbxStyleClass_SelectedIndexChanged(object sender, EventArgs e)
        {
              if (cbxStyleClass.SelectedItem == null) return;
            strStyleClass = cbxStyleClass.SelectedItem.ToString();
            //axSymbologyControl1.Clear();
            switch (strStyleClass)
            {
                case "Reference Systems":
                    try
                    {
                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassReferenceSystems;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message );
                        break;
                    }
                case "Maplex Labels":
                    try
                    {
                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassMaplexLabels;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex .Message );
                        break;
                    }
                case "Shadows":
                    try
                    {
                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassShadows;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message );
                        break;
                    }
                case "Area Patches":
                    try
                    {
                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassAreaPatches;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        break;
                    }
                case "Line Patches":
                    try
                    {
                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassLinePatches;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        break;
                    }
                case "Labels":
                    try
                    {
                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassLabels;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        break;
                    }
                case "North Arrows":
                    try
                    {
                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassNorthArrows;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        break;
                    }
                case "Scale Bars":
                    try
                    {

                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassScaleBars;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        break;
                    }
                case "Legend Items":
                    try
                    {

                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassLegendItems;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        break;
                    }
                case "Scale Texts":
                    try
                    {

                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassScaleTexts;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        break;
                    }
                case "Color Ramps":
                    try
                    {
                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassColorRamps;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        break;
                    }
                case "Borders":
                    try
                    {
                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassBorders;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message );
                        break;
                    }
                case "Backgrounds":
                    try
                    {

                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassBackgrounds;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        break;
                    }
                case "Colors":
                    try
                    {

                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassColors;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        break;
                    }
                case "Vectorization Settings":
                    try
                    {

                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassVectorizationSettings;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        break;
                    }
                case "Fill Symbols":
                    try
                    {

                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassFillSymbols;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        break;
                    }
                case "Line Symbols":
                    try
                    {
                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassLineSymbols;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        break;
                    }
                case "Marker Symbols":
                    try
                    {
                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassMarkerSymbols;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        break;
                    }
                case "Text Symbols":
                    try
                    {
                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassTextSymbols;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        break;
                    }
                case "Hatches":
                    try
                    {
                        axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassHatches;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        break;
                    }

                default:
                    break;
            }
            axSymbologyControl1.Update();
            axSymbologyControl1.Refresh();

        }

        private void axSymbologyControl1_OnItemSelected(object sender, ESRI.ArcGIS.Controls.ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            //Get the selected item
            m_styleGalleryItem = (IStyleGalleryItem)e.styleGalleryItem;
        }

        public bool IsInteger(string s)
        {
            try
            {
                Int32.Parse(s);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DrawToPictureBox(ISymbol pSym, PictureBox pBox)
        {
            IGeometry pGeometry = null;
            IntPtr hDC;
            IEnvelope pEnvelope;
            pEnvelope = new EnvelopeClass();
            // Reset the PictureBox
            pBox.CreateGraphics().Clear(pBox.BackColor);
            try
            {
                if (pSym is IMarkerSymbol)
                {
                    pEnvelope.PutCoords(pBox.Width / 2, pBox.Height / 2, pBox.Width / 2, pBox.Height / 2);
                    IArea pArea = pEnvelope as IArea;
                    pGeometry = pArea.Centroid;
                }
                else if (pSym is ILineSymbol)
                {
                    pEnvelope.PutCoords(0, pBox.Height / 2, pBox.Width, pBox.Height / 2);
                    IPolyline pPolyline = new PolylineClass();
                    pPolyline.FromPoint = pEnvelope.LowerLeft;
                    pPolyline.ToPoint = pEnvelope.UpperRight;
                    pGeometry = pPolyline;
                }
                else if (pSym is IFillSymbol)
                {
                    if (pSym is SimpleFillSymbol)
                    {
                        pEnvelope.PutCoords(5, 5, pBox.Width - 5, pBox.Height - 5);
                        pGeometry = pEnvelope;
                    }
                    else if (pSym is MultiLayerFillSymbol)
                    {
                        IMultiLayerFillSymbol pMultiLayerFillSymbol = pSym as IMultiLayerFillSymbol;
                        //For mLayers As Integer = 0 To pMultiLayerFillSymbol.LayerCount - 1
                        if (pMultiLayerFillSymbol.get_Layer(0) is PictureFillSymbol)
                        {
                            IPictureFillSymbol pPictureFillSymbol = pMultiLayerFillSymbol.get_Layer(0) as IPictureFillSymbol;
                            Bitmap m = Bitmap.FromHbitmap(new IntPtr(pPictureFillSymbol.Picture.Handle));
                            //m.MakeTransparent(System.Drawing.ColorTranslator.FromOle(pPictureFillSymbol.Color.RGB))
                            pBox.Image = m;
                            return;
                        }
                        else if (pMultiLayerFillSymbol.get_Layer(0) is SimpleFillSymbol)
                        {
                            pEnvelope.PutCoords(5, 5, pBox.Width - 5, pBox.Height - 5);
                            pGeometry = pEnvelope;
                        }
                        else if (pMultiLayerFillSymbol.get_Layer(0) is GradientFillSymbol)
                        {
                            IGradientFillSymbol pGradientFillSymbol = pMultiLayerFillSymbol.get_Layer(0) as IGradientFillSymbol;
                            IAlgorithmicColorRamp pRamp = pGradientFillSymbol.ColorRamp as IAlgorithmicColorRamp;
                            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(new System.Drawing.Point(0, 0), new System.Drawing.Size(pBox.Width, pBox.Height));
                            LinearGradientBrush lgBrush = new LinearGradientBrush(rect,
                                System.Drawing.ColorTranslator.FromOle(pRamp.FromColor.RGB),
                                System.Drawing.ColorTranslator.FromOle(pRamp.ToColor.RGB),
                                45);
                            Graphics g = pBox.CreateGraphics();
                            g.FillRectangle(lgBrush, rect);
                            rect.Width = rect.Width - 1;
                            rect.Height = rect.Height - 1;
                            g.DrawRectangle(new Pen(ColorTranslator.FromOle(pGradientFillSymbol.Outline.Color.RGB),
                                (float)pGradientFillSymbol.Outline.Width), rect);
                        }
                    }
                    else if (pSym is PictureFillSymbol)
                    {
                        IPictureFillSymbol pPictureFillSymbol = pSym as IPictureFillSymbol;
                        Bitmap m = Bitmap.FromHbitmap(new IntPtr(pPictureFillSymbol.Picture.Handle));
                        //m.MakeTransparent(System.Drawing.ColorTranslator.FromOle(pPictureFillSymbol.Color.RGB))
                        pBox.Image = m;
                        return;
                    }
                }

                hDC = GetDC(pBox.Handle);
                pSym.SetupDC(hDC.ToInt32(), null);
                pSym.ROP2 = esriRasterOpCode.esriROPCopyPen;
                if (pGeometry != null)
                    pSym.Draw(pGeometry);

                pSym.ResetDC();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        

    }
}
