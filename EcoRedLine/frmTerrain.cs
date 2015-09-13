using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SpatialAnalyst;

using CommandLibrary;

namespace EcoRedLine
{
    public partial class frmTerrain : DevComponents.DotNetBar.OfficeForm
    {
        private IMap pMap;
        IMapAlgebraOp pMapAlgebraOp = new RasterMapAlgebraOpClass();
        private List<IGeoDataset> listRasterDataset = new List<IGeoDataset>();

        public frmTerrain()
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

        public frmTerrain(IMap _pMap)
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
            }
            
        }

        private void frmTerrain_Load(object sender, EventArgs e)
        {
            addRasterLayerToCombobox();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.pMap.LayerCount <= 0)
            {
                MessageBox.Show("请先在地图中加载要操作的栅格图像.");
                return;
            }
            IRasterLayer pRsL =calTerrain (this.txtSavePath.Text);
            if (pRsL != null)
            {
                DialogResult dr= MessageBox.Show(this,"计算完毕,1为限制区，0为非限制区,数据是否添加到当前工程中？","提示",MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    pMap.AddLayer(pRsL);
                }
                this.Close();
                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

                            this.cmbDem.Items.Add(pLayer.Name);

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
                                this.cmbDem.Items.Add(strBandName);
                            }
                        }
                        this.cmbDem.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("请先添加数据！", "提示", MessageBoxButtons.OK);
            }
        }

        //绑定栅格数据到，栅格代数计算的列表中
        private void setBandName(ComboBox cmb)
        {
            pMapAlgebraOp.BindRaster(listRasterDataset[cmb.SelectedIndex], cmb.Text.Trim());
        }

        private IRasterLayer calTerrain(string _outPath)
        {
            //绑定栅格到 pMapAlgebraOp
            try
            {
                if (pMapAlgebraOp == null)
                {
                    pMapAlgebraOp = new RasterMapAlgebraOpClass();
                }
                string strExp = "";
                //dem
                IRaster pRasterDem = (pMap.get_Layer(LayerOprate.getLayerIndexByName(pMap, this.cmbDem.Text)) as IRasterLayer).Raster;
                IGeoDataset pGeoDsDem = pRasterDem as IGeoDataset;
                // bind raster data "Dem"
                pMapAlgebraOp.BindRaster(pGeoDsDem, "Dem");

                //cal Slope with Deg
                ISurfaceOp pSurfaceOp = new RasterSurfaceOpClass();
                object objZFactor = System.Type.Missing;
                IGeoDataset pGeoDsSlope = pSurfaceOp.Slope(pRasterDem as IGeoDataset, esriGeoAnalysisSlopeEnum.esriGeoAnalysisSlopeDegrees, ref objZFactor);
                
                // bind raster data "Slope"
                pMapAlgebraOp.BindRaster(pGeoDsSlope, "Slope");
                
                //cal Slo dem
                string demStart = this.txtDemStart.Text;
                string demEnd = this.txtDemEnd.Text;

                string sloStart = this.txtSloStart.Text;
                string sloEnd = this.txtSloEnd.Text;

                //strExp = "Con([Dem] < " + demStart + ",0,[Dem] > " + demEnd + ",0,1)";
                //IGeoDataset pGeoDsDemC = pMapAlgebraOp.Execute(strExp);
                //pMapAlgebraOp.BindRaster(pGeoDsDemC, "DemC");

                ////限制区为1，非限制区为0
                //strExp = "Con([Slope] < " + sloStart + ",0,[Slope] > "+sloEnd+",0,1)";
                //IGeoDataset pGeoDsSloC = pMapAlgebraOp.Execute(strExp);
                //pMapAlgebraOp.BindRaster(pGeoDsSloC, "SloC");

                //cal in together
                strExp = "Con((([Slope] > " + sloStart + " & [Slope] < " + sloEnd + ") & ([Dem] > " + demStart + " & [Dem] < " + demEnd + ")),1,0)";
                IGeoDataset pGeoDsC = pMapAlgebraOp.Execute(strExp);
                pMapAlgebraOp.BindRaster(pGeoDsC, "TerrainRedL");

                pMapAlgebraOp.UnbindRaster("Dem");
                pMapAlgebraOp.UnbindRaster("Slope");

                IRaster pOutRaster = pGeoDsC as IRaster;
                IRasterLayer pOutRasterLayer = new RasterLayerClass();
                pOutRasterLayer.CreateFromRaster(pOutRaster);
                //数据名称
                //string strOutDir = _outPath.Substring(0, _outPath.LastIndexOf("\\"));
                int startX = _outPath.LastIndexOf("\\");
                int endX = _outPath.Length;
                string sRLyrName = _outPath.Substring(startX + 1, endX - startX - 1);
                pOutRasterLayer.Name = sRLyrName;
                pMapAlgebraOp = null;
                return pOutRasterLayer;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return null;
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
            saveDlg.FileName = "SWCons";

            DialogResult dr = saveDlg.ShowDialog();
            if (dr == DialogResult.OK)
                this.txtSavePath.Text = saveDlg.FileName;
        }
    }
}
