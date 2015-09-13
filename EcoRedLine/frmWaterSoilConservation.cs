using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class frmWaterSoilConservation : DevComponents.DotNetBar.OfficeForm
    {
        private IMap pMap = null;
        IMapAlgebraOp pMapAlgebraOp = new RasterMapAlgebraOpClass();

        private List<IGeoDataset> listRasterDataset = new List<IGeoDataset>();

        public frmWaterSoilConservation()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
        }

        public frmWaterSoilConservation(IMap _pMap)
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

        private void frmSoilRunOffSpatial_Load(object sender, EventArgs e)
        {
            addRasterLayerToCombobox();
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
                            this.cmbR.Items.Add(pLayer.Name);

                            this.cmbSoilClay.Items.Add(pLayer.Name);
                            this.cmbSoilSand.Items.Add(pLayer.Name);
                            this.cmbSoilSlit.Items.Add(pLayer.Name);
                            this.cmbSoilOrganic.Items.Add(pLayer.Name);

                            this.cmbDem.Items.Add(pLayer.Name);
                            this.cmbC.Items.Add(pLayer.Name);
                            this.cmbP.Items.Add(pLayer.Name);

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
                                this.cmbR.Items.Add(strBandName);
                                this.cmbSoilClay.Items.Add(strBandName);
                                this.cmbSoilSand.Items.Add(strBandName);
                                this.cmbSoilSlit.Items.Add(strBandName);
                                this.cmbSoilOrganic.Items.Add(strBandName);
                                this.cmbDem.Items.Add(strBandName);
                                this.cmbC.Items.Add(strBandName);
                                this.cmbP.Items.Add(strBandName);
                            }
                        }
                        this.cmbR.SelectedIndex = 0;
                        this.cmbSoilClay.SelectedIndex = 0;
                        this.cmbSoilSand.SelectedIndex = 0;
                        this.cmbSoilSlit.SelectedIndex = 0;
                        this.cmbSoilOrganic.SelectedIndex = 0;
                        this.cmbDem.SelectedIndex = 0;
                        this.cmbC.SelectedIndex = 0;
                        this.cmbP.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("请先添加数据！", "提示", MessageBoxButtons.OK);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.pMap.LayerCount<= 0)
            {
                MessageBox.Show("请先在地图中加载要操作的栅格图像.");
                return;
            }
            IRasterLayer pRsL= calWSCons(this.txtSavePath.Text);
            if (pRsL != null)
            {
                pMap.AddLayer(pRsL);
                MessageBox.Show("计算完毕！");
            }
        }

        private IRasterLayer calWSCons(string  _outPath)
        {
            //绑定栅格到 pMapAlgebraOp
            try
            {
                if (pMapAlgebraOp==null)
                {
                    pMapAlgebraOp = new RasterMapAlgebraOpClass();
                }
                string strExp = "";
                string sRainPath = "";

                //是直接用降雨侵蚀力数据还是降雨数据计算
                IRaster pRasterR = null; 
                if (rbtnR.Checked)
                {
                    pRasterR = (pMap.get_Layer(LayerOprate.getLayerIndexByName(pMap, this.cmbR.Text)) as IRasterLayer).Raster;
                    pMapAlgebraOp.BindRaster(pRasterR as IGeoDataset, "R");
                }
                else
                {
                    sRainPath = this.txtPcpPath.Text;
                    //
                    IWorkspaceFactory pWsF = new RasterWorkspaceFactoryClass();
                    IRasterWorkspace pRWs;
                    IRasterDataset pRDs = new RasterDatasetClass();
                    IRasterLayer pRLyr = new RasterLayerClass();
                    
                    pRWs = pWsF.OpenFromFile(sRainPath, 0) as IRasterWorkspace;
                    string sFileName="";
                    for (int i = 1; i < 13; i++)
                    {
                        sFileName = this.txtPcpPrefix.Text + i.ToString() + this.txtPcpSuffix.Text;
                        pRDs = pRWs.OpenRasterDataset(sFileName);
                        pMapAlgebraOp.BindRaster(pRDs as IGeoDataset, sFileName);
                        strExp = strExp + "[" + sFileName + "] + ";
                    }
                    //cal total pcp
                    strExp = strExp.TrimEnd("+ ".ToCharArray());
                    IGeoDataset pGeoDsPcp = pMapAlgebraOp.Execute(strExp);
                    pMapAlgebraOp.BindRaster(pGeoDsPcp, "Pcp");
                        
                    //cal Ri
                    strExp = "";
                    string sFileNameE = "",sDsName="",strExpR="";
                    for (int i = 1; i < 13; i++)
                    {
                        sFileName = this.txtPcpPrefix.Text + i.ToString() + this.txtPcpSuffix.Text;
                        sFileNameE="["+sFileName+"]";
                        strExp = "1.735 * pow(10,1.5 * log10((" + sFileNameE + " * " + sFileNameE + ") / [Pcp]) - 0.08188)";
                        IGeoDataset pGeoDsRi = pMapAlgebraOp.Execute(strExp);
                        sDsName = "Pcp" + i.ToString();
                        pMapAlgebraOp.BindRaster(pGeoDsRi, sDsName);
                        pMapAlgebraOp.UnbindRaster(sFileName);
                        strExpR = strExpR + "[" + sDsName + "] + ";
                    }
                    //cal R
                    strExpR = strExpR.TrimEnd("+ ".ToCharArray());
                    IGeoDataset pGeoDsR = pMapAlgebraOp.Execute(strExpR);
                    pMapAlgebraOp.BindRaster(pGeoDsR, "R");
                    for (int i = 1; i < 13; i++)
                    {
                        sDsName = "Pcp" + i.ToString();
                        pMapAlgebraOp.UnbindRaster(sDsName);
                    }
                }

                //计算K因子
                IRaster pRasterSclay = (pMap.get_Layer(LayerOprate.getLayerIndexByName(pMap, this.cmbSoilClay.Text)) as IRasterLayer).Raster;
                pMapAlgebraOp.BindRaster(pRasterSclay as IGeoDataset, "clay");

                IRaster pRasterSsand = (pMap.get_Layer(LayerOprate.getLayerIndexByName(pMap, this.cmbSoilSand.Text)) as IRasterLayer).Raster;
                pMapAlgebraOp.BindRaster(pRasterSsand as IGeoDataset, "sand");

                IRaster pRasterSslit = (pMap.get_Layer(LayerOprate.getLayerIndexByName(pMap, this.cmbSoilSlit.Text)) as IRasterLayer).Raster;
                pMapAlgebraOp.BindRaster(pRasterSslit as IGeoDataset, "slit");

                IRaster pRasterSOrg = (pMap.get_Layer(LayerOprate.getLayerIndexByName(pMap, this.cmbSoilOrganic.Text)) as IRasterLayer).Raster;
                pMapAlgebraOp.BindRaster(pRasterSOrg as IGeoDataset, "org");

                //cal K
                //K =(0.2 + 0.3 * exp(-0.0256 * soil_sand * (1.0 - soil_silt / 100.0))) * pow((soil_silt * 1.0 / (soil_clay * 1.0 + soil_silt * 1.0)),0.3) * (1.0 - 0.25 * soil_oc / (soil_oc * 1.0 + exp(3.72 - 2.95 * soil_oc))) * (1.0 - (0.7 * ksd) / (ksd + exp(-5.51 + 22.9 * ksd)))
                strExp = "(0.2 + 0.3 * Exp(-0.0256 * [sand] * (1.0 - [slit] / 100.0))) * Pow(([slit] * 1.0 / ([clay] * 1.0 + [slit] * 1.0)), 0.3) * (1.0 - 0.25 * [org] * 0.58 / ([org] * 0.58 + Exp(3.72 - 2.95 * [org] * 0.58))) * (1.0 - (0.7 * (1.0 - [sand] / 100.0)) / ((1.0 - [sand] / 100.0) + Exp(-5.51 + 22.9 * (1.0 - [sand] / 100.0))))";
                IGeoDataset pGeoDsK = pMapAlgebraOp.Execute(strExp);
                pMapAlgebraOp.BindRaster(pGeoDsK, "K");
                pMapAlgebraOp.UnbindRaster("clay");
                pMapAlgebraOp.UnbindRaster("sand");
                pMapAlgebraOp.UnbindRaster("slit");
                pMapAlgebraOp.UnbindRaster("org");

                //cal L*S
                IHydrologyOp pHydrologyOp = new RasterHydrologyOpClass();
                object objZLimit = System.Type.Missing;
                IRaster pRasterDem = (pMap.get_Layer(LayerOprate.getLayerIndexByName(pMap, this.cmbDem.Text)) as IRasterLayer).Raster;
                //Fill Dem
                IGeoDataset pGeoDsDemFill =pHydrologyOp.Fill(pRasterDem as IGeoDataset,ref objZLimit);
                // cal FlowDirection
                IGeoDataset pGeoDsFlowDir = pHydrologyOp.Fill(pGeoDsDemFill, ref objZLimit);
                //cal FlowAccumulation
                IGeoDataset pGeoDsFlowAcc = pHydrologyOp.Fill(pGeoDsFlowDir, ref objZLimit);
                pMapAlgebraOp.BindRaster(pGeoDsFlowAcc, "FlowAcc");

                //cal Slope with Deg
                ISurfaceOp pSurfaceOp = new RasterSurfaceOpClass();
                object objZFactor = System.Type.Missing;
                IGeoDataset pGeoDsSlope = pSurfaceOp.Slope(pGeoDsDemFill, esriGeoAnalysisSlopeEnum.esriGeoAnalysisSlopeDegrees, ref objZFactor);
                // bind raster data "Slope"
                pMapAlgebraOp.BindRaster(pGeoDsSlope, "Slope");
                //cal S
                strExp = "Con([Slope] < 5,10.8 * Sin([Slope] * 3.14 / 180) + 0.03,[Slope] >= 10,21.9 * Sin([Slope] * 3.14 / 180) - 0.96,16.8 * Sin([Slope] * 3.14 / 180) - 0.5)";
                IGeoDataset pGeoDsS = pMapAlgebraOp.Execute(strExp);
                pMapAlgebraOp.BindRaster(pGeoDsS, "S");

                //cal m
                strExp = "Con([Slope] <= 1,0.2,([Slope] > 1 & [Slope] <= 3),0.3,[Slope] >= 5,0.5,0.4)";
                IGeoDataset pGeoDsM = pMapAlgebraOp.Execute(strExp);
                pMapAlgebraOp.BindRaster(pGeoDsM, "m");
                //cal ls
                strExp = "[S] * Pow(([FlowAcc] * "+this.txtCellSize.Text+" / 22.1),[m])";

                IGeoDataset pGeoDsLS = pMapAlgebraOp.Execute(strExp);
                pMapAlgebraOp.BindRaster(pGeoDsLS, "LS");

                pMapAlgebraOp.UnbindRaster("m");
                pMapAlgebraOp.UnbindRaster("S");
                pMapAlgebraOp.UnbindRaster("Slope");
                pMapAlgebraOp.UnbindRaster("FlowAcc");


                IRaster pRasterC = null;
                if (rbtnVegCover.Checked)
                {
                    pRasterC = (pMap.get_Layer(LayerOprate.getLayerIndexByName(pMap, this.cmbC.Text)) as IRasterLayer).Raster;
                    pMapAlgebraOp.BindRaster(pRasterC as IGeoDataset, "C");
                }
                else
                {
                    //cal vegetation cover 
                    IRaster pRasterNDVI = (pMap.get_Layer(LayerOprate.getLayerIndexByName(pMap, this.cmbC.Text)) as IRasterLayer).Raster;
                    IRasterBandCollection pRasterBandCollection = pRasterNDVI as IRasterBandCollection;
                    IRasterBand pRasterBand = pRasterBandCollection.Item(0);
                    pRasterBand.ComputeStatsAndHist();
                    IRasterStatistics pRasterStatistics = pRasterBand.Statistics;
                    double dMax = pRasterStatistics.Maximum;
                    double dMin = pRasterStatistics.Minimum;
                    pMapAlgebraOp.BindRaster(pRasterNDVI as IGeoDataset, "NDVI");
                    if (dMin < 0)
                    {
                        dMin = 0;
                    }
                    //veg%yr% = (ndvi%yr% - ndvi%yr%min) / (ndvi%yr%max - ndvi%yr%min)
                    strExp = "([NDVI] - "+dMin+") / ("+dMax +" - "+dMin+")";
                    IGeoDataset pGeoDsC = pMapAlgebraOp.Execute(strExp);
                    pMapAlgebraOp.BindRaster(pGeoDsC, "C");
                    pMapAlgebraOp.UnbindRaster("NDVI");
                }
                //计算P因子
                IRaster pRasterP = (pMap.get_Layer(LayerOprate.getLayerIndexByName(pMap, this.cmbP.Text)) as IRasterLayer).Raster;
                pMapAlgebraOp.BindRaster(pRasterP as IGeoDataset, "P");
                if (_outPath != "")
                {
                    strExp = _outPath + " = [R] * [K] * [LS] * (1 - [C]) * [P]";
                }
                else
                {
                    strExp = "[R] * [K] * [LS] * (1 - [C]) * [P]";
                }

                IGeoDataset pGeoDsSr = pMapAlgebraOp.Execute(strExp);
                IRaster pOutRaster = pGeoDsSr as IRaster;
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

        //栅格重分类
        private IRasterLayer calMValve(IRaster pInRaster, string sField)
        {
            try
            {
                if (sField.Trim() == "")
                    return null;
                IRasterDescriptor pRD = new RasterDescriptorClass();
                pRD.Create(pInRaster, null, sField);
                IReclassOp pReclassOp = new RasterReclassOpClass();

                IGeoDataset pGeodataset = pInRaster as IGeoDataset;
                IRasterAnalysisEnvironment pEnv = pReclassOp as IRasterAnalysisEnvironment;

                IRasterBandCollection pRsBandCol = pGeodataset as IRasterBandCollection;
                IRasterBand pRasterBand = pRsBandCol.Item(0);
                pRasterBand.ComputeStatsAndHist();
                IRasterStatistics pRasterStatistic = pRasterBand.Statistics;
                double dMaxValue = pRasterStatistic.Maximum;
                double dMinValue = pRasterStatistic.Minimum;

                INumberRemap pNumRemap = new NumberRemapClass();
                //m——常数， 在坡度为 0-7.5; 7.5-12.5; 12.5-17.5; 17.5--22.5; >22.5;
                //m值分别为： 0.1;0.15;0.2;0.25,0.3
                //数值扩大了100倍
                pNumRemap.MapRange(dMinValue, 7.5, 10);
                pNumRemap.MapRange(7.5, 12.5, 15);
                pNumRemap.MapRange(12.5, 17.5, 20);
                pNumRemap.MapRange(17.5, 22.5, 25);
                pNumRemap.MapRange(22.5, dMaxValue, 30);
                IRemap pRemap = pNumRemap as IRemap;

                IGeoDataset pGeoDs = new RasterDatasetClass();
                pGeoDs = pReclassOp.ReclassByRemap(pGeodataset, pRemap, false);
                IRaster pOutRaster = pGeoDs as IRaster;
                IRasterLayer pRLayer = new RasterLayerClass();
                pRLayer.CreateFromRaster(pOutRaster);
                return pRLayer;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return null;
            }
        }
       
        //绑定栅格数据到，栅格代数计算的列表中
        private void setBandName(ComboBox cmb)
        {
            pMapAlgebraOp.BindRaster(listRasterDataset[cmb.SelectedIndex], cmb.Text.Trim());
        }
        //栅格大小文本框的是否可用
        private void chkbCellsize_CheckedChanged(object sender, EventArgs e)
        {
            txtCellSize.Enabled = chkbCellsize.Checked;
        }
        //开打降雨数据的文件夹
        private void btnOpenPcp_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog pFBD = new FolderBrowserDialog();
            pFBD.Description = "请选择降水栅格数据所在的文件夹";
            DialogResult pDr = pFBD.ShowDialog();
            if (pDr == DialogResult.OK)
            {
                IWorkspaceFactory pWsF;
                pWsF = new RasterWorkspaceFactoryClass();
                if(pWsF.IsWorkspace(pFBD.SelectedPath))
                {
                    this.txtPcpPath.Text = pFBD.SelectedPath;
                }
                else
                {
                    MessageBox.Show("您所选择的路径不含有栅格数据，请重新选择！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }       
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

        private void rbtnPcp_CheckedChanged(object sender, EventArgs e)
        {
            this.txtPcpPath.Enabled = ((RadioButton)sender).Checked;
            this.btnOpenPcp.Enabled = ((RadioButton)sender).Checked;
            this.txtPcpPrefix.Enabled = ((RadioButton)sender).Checked;
            this.txtPcpSuffix.Enabled = ((RadioButton)sender).Checked;
        }

        public string dSclay { get; set; }

        private void cmbDem_SelectedIndexChanged(object sender, EventArgs e)
        {
            IRaster pRasterDem = (pMap.get_Layer(LayerOprate.getLayerIndexByName(pMap, this.cmbDem.Text)) as IRasterLayer).Raster;
            IRasterProps pRasterProps = (IRasterProps)pRasterDem;
            double dX = pRasterProps.MeanCellSize().X;
            double dY = pRasterProps.MeanCellSize().Y;
            this.txtCellSize.Text = ((dX + dY) / 2).ToString();
        }
    }
}