using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

using CommandLibrary;

namespace EcoRedLine
{
    public partial class frmWaterConervation : DevComponents.DotNetBar.OfficeForm
    {
        private IMap pMap;
        private IMapAlgebraOp pMapAlgebraOp = null;
        private List<IGeoDataset> listRasterDataset = new List<IGeoDataset>();

        public frmWaterConervation()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
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
        public frmWaterConervation(IMap _pMap)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            if (_pMap != null)
            {
                pMap = _pMap;
                addRasterLayerToCombobox();
            }
            else
            {
                MessageBox.Show("请先添加数据！", "提示", MessageBoxButtons.OK);
            }
        }

        private void addRasterLayerToCombobox()
        {
            try
            {
                ILayer pLayer = null;

                for (int index = 0; index < this.pMap.LayerCount; index++)
                {
                    
                    pLayer = this.pMap.get_Layer(index);
                    if (pLayer is IRasterLayer)
                    {
                        IRasterLayer pRastetLayer = pLayer as IRasterLayer;
                        IRasterBandCollection pRasterBandColection = pRastetLayer.Raster as IRasterBandCollection;
                        if (pRasterBandColection.Count == 1)
                        {
                            listRasterDataset.Add((IGeoDataset)pRasterBandColection.Item(0).RasterDataset);
                            this.cmbPcp.Items.Add(pLayer.Name);
                        }
                        else
                        {
                            string strBandName;
                            for (int i = 0; i < pRasterBandColection.Count; i++)
                            {
                                listRasterDataset.Add((IGeoDataset)pRasterBandColection.Item(i).RasterDataset);
                                if (pLayer.Name.Length < 15)
                                {
                                    strBandName = pLayer.Name + "_" + pRasterBandColection.Item(i).Bandname;
                                }
                                else
                                {
                                    strBandName = pLayer.Name.Substring(0, 10) + "..." + "_" + pRasterBandColection.Item(i).Bandname;
                                }
                                this.cmbPcp.Items.Add(strBandName);
                            }
                        }
                        this.cmbPcp.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                 pMapAlgebraOp= new RasterMapAlgebraOpClass();

                string strExpression = "";
                string dP = "",dCN="";
                dP = "[" + this.cmbPcp.Text + "]";
                dCN = "[" + this.cmbCN.Text + "]";
                
                ILayer pLayer = null;
                IRaster pRater = null;
                IGeoDataset pGeoDataset = null;
                pLayer = this.pMap.get_Layer(LayerOprate.getLayerIndexByName(pMap,this.cmbPcp.Text));
                pRater=(pLayer as IRasterLayer).Raster;
                pGeoDataset = pRater as IGeoDataset;
                pMapAlgebraOp.BindRaster(pGeoDataset, pLayer.Name);

                pLayer = this.pMap.get_Layer(LayerOprate.getLayerIndexByName(pMap, this.cmbCN.Text));
                pRater = (pLayer as IRasterLayer).Raster;
                pGeoDataset = pRater as IGeoDataset;
                pMapAlgebraOp.BindRaster(pGeoDataset, pLayer.Name);
                pLayer = null;
                pRater = null;
                pGeoDataset = null;
                // S=25400/CN-254
                // Q=(P-0.2S)^2/(P+0.8S)  P>0.2S
                // Q=0  P>0.2S

                //Con(P>0.2S,(P-0.2S)^2/(P+0.8S),0)
                //Con(P>0.2*(25400/CN-254),(P-0.2*(25400/CN-254))^2/(P+0.8*(25400/CN-254)),0)

                strExpression = "25400.0 / ("+dCN+" * 1.0) - 254";
                IGeoDataset pGeoDsS = pMapAlgebraOp.Execute(strExpression);
                pMapAlgebraOp.BindRaster(pGeoDsS, "S");

                string dS="[S]";
                string saveFileName = this.txtSavePath.Text.Trim();
                if (saveFileName == "")
                {
                    strExpression = "Con(" + dP + " > 0.2 * " + dS + ",(" + dP + " - 0.2 * " + dS + ") * (" + dP + " - 0.2 * " + dS + ") / (" + dP + " + 0.8 * " + dS + "),0)";

                }
                else
                {
                    strExpression =saveFileName+" = " +strExpression;
                }
                IGeoDataset pGeoDsQ = pMapAlgebraOp.Execute(strExpression);
                
                IRaster pOutRaster = pGeoDsQ as IRaster;
                IRasterLayer pOutRasterLayer = new RasterLayerClass();
                //pOutRasterLayer = SetStretchRenderer(pOutRaster);
                pOutRasterLayer.CreateFromRaster(pOutRaster);
                pMap.AddLayer(pOutRasterLayer);
                pGeoDsQ = null;
                pGeoDsS = null;
                pMapAlgebraOp = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnSavePath_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.CheckPathExists = true;
            saveDlg.Filter = "所有文件(*.*)|*.*";
            saveDlg.OverwritePrompt = true;
            saveDlg.Title = "保存";
            saveDlg.RestoreDirectory = true;
            saveDlg.FileName = "waterCons";

            DialogResult dr = saveDlg.ShowDialog();
            if (dr == DialogResult.OK)
                txtSavePath.Text = saveDlg.FileName;
        }
    }
}
