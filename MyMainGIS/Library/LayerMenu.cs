using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.ADF;
using System.Windows.Forms;
using System.Data;

/**
 * author lk 
 * 2015/7/17
 * 插件式gis应用框架的设计与实现
 * toccontrol layer 上右键时弹出快捷菜单
 * 
 * */
namespace MyMainGIS.Library
{
    public class LayerMenu : ESRI.ArcGIS.ADF.BaseClasses.BaseCommand, ICommandSubType
    {
        public delegate void SymbolChangedHandler();
        private IHookHelper m_hookHelper = new HookHelperClass();
        private long m_subType;
        private ILayer m_layer;
        private IMapControlDefault m_mapControl;

        public event SymbolChangedHandler SymbolChanged;

        public LayerMenu(IMapControlDefault _mapControl)
        {
            this.m_mapControl = _mapControl;
            //this.m_layer = (ILayer)_mapControl.CustomProperty;
        }

        public override void OnClick()
        {
            this.m_layer = (ILayer)this.m_mapControl.CustomProperty;
            switch (m_subType)
            {
                case 1:
                    m_hookHelper.FocusMap.DeleteLayer(this.m_layer);
                    break;
                case 2:
                    this.m_mapControl.Extent = this.m_layer.AreaOfInterest;
                    break;
                case 3:
                    if (this.m_layer is IRasterLayer)
                    {
                        return;
                    }
                    
                    frmAttributeTable fmAttriTable = new frmAttributeTable(this.m_layer,this.m_mapControl as MapControl);
                    fmAttriTable.ShowDialog();
                    break;
                default:
                    break;
            }
        }

        public override void OnCreate(object hook)
        {
            m_hookHelper.Hook = hook;
        }

        #region ICommandSubType 成员

        public int GetCount()
        {
            return 4;
        }

        public void SetSubType(int SubType)
        {
            m_subType = SubType;
        }

        #endregion

        public override string Caption
        {
            get
            {
                switch (m_subType)
                {
                    case 1:
                        return "删除图层";
                    case 2:
                        return "缩放至图层";
                    case 3:
                        return "打开属性表";
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// 显示符号选择器
        /// </summary>
        private void ShowSymbolSelectForm()
        {
            ISimpleRenderer currentSimpleRender;

            IMapControlDefault pMapControl = m_hookHelper.Hook as IMapControlDefault;
            IGeoFeatureLayer pGeoFeatureLayer = pMapControl.CustomProperty as IGeoFeatureLayer;
            if (pGeoFeatureLayer == null)
            {
                return;
            }

            currentSimpleRender = pGeoFeatureLayer.Renderer as ISimpleRenderer;
            SymbolForm symbolForm = new SymbolForm(currentSimpleRender);
            IStyleGalleryItem styleGallryItem = null;
            switch (pGeoFeatureLayer.FeatureClass.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    styleGallryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassMarkerSymbols);
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    styleGallryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassLineSymbols);
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    styleGallryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassFillSymbols);
                    break;
            }

            symbolForm.Dispose();
            if (styleGallryItem == null)
            {
                return;
            }

            ISimpleRenderer simpleRender = new SimpleRendererClass();
            simpleRender.Symbol = styleGallryItem.Item as ISymbol;
            pGeoFeatureLayer.Renderer = simpleRender as IFeatureRenderer;

            pMapControl.Refresh(esriViewDrawPhase.esriViewGeography, null, null);
            SymbolChanged();
        }


    }
}
