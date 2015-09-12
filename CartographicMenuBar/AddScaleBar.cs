using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

namespace MapAndRelatedObjects.地图的组成
{
    /// <summary>
    /// Summary description for AddScaleBar.
    /// </summary>
    [Guid("6e1b4953-2d54-49d0-b92f-93c9c7fd8606")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("MapAndRelatedObjects.地图的组成.AddScaleBar")]
    public sealed class AddScaleBar : BaseTool
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper = null;
     
      //  IActiveView pActiveView;
      //  IMap pMap;
        IPageLayoutControl3 pPageLayoutControl;
      //  IPageLayout3 pPageLayout;
      //  string ScaleBarStyle=null ;


        public AddScaleBar()
        {
            base.m_category = "MapAndRelatedObjects"; 
            base.m_caption = "添加比例尺";
            base.m_message = "添加比例尺";
            base.m_toolTip = "添加比例尺";
            base.m_name = "AddScaleBar";   
            try
            {
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                {
                    m_hookHelper = null;
                }
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;

            pPageLayoutControl = m_hookHelper.Hook as IPageLayoutControl3;
            IPageLayout pPageLayout = pPageLayoutControl.PageLayout;
            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add AddScaleBar.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if(Button ==1)
            {
             IEnvelope pEnv;
             pEnv = pPageLayoutControl .TrackRectangle ();
  
             GetSymbol symbolForm = new GetSymbol(esriSymbologyStyleClass.esriStyleClassScaleBars);
            symbolForm.Text = "选择比例尺";
            IStyleGalleryItem styleGalleryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassNorthArrows);
            symbolForm.Dispose();
            if (styleGalleryItem == null) return;

            IMapFrame mapFrame = (IMapFrame)m_hookHelper.ActiveView.GraphicsContainer.FindFrame(m_hookHelper.ActiveView.FocusMap);
            IMapSurroundFrame mapSurroundFrame = new MapSurroundFrameClass();
            mapSurroundFrame.MapFrame = mapFrame;
            mapSurroundFrame.MapSurround = (IMapSurround)styleGalleryItem.Item;

            IElement element = (IElement)mapSurroundFrame;
            element.Geometry = pEnv ;

            m_hookHelper.ActiveView.GraphicsContainer.AddElement((IElement)mapSurroundFrame, 0);
            m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, mapSurroundFrame, null);
        }
          
          
            }




        

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add AddScaleBar.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
        }
          
        #endregion
    }
}
