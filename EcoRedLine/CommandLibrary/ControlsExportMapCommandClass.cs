using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;

namespace CommandLibrary
{
    /// <summary>
    /// Command that works in ArcMap/Map/PageLayout
    /// </summary>
    [Guid("bf555ccc-d311-4a62-a8d2-ed0e55f0aa27")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CommandLibrary.ControlsExportMapCommandClass")]
    public sealed class ControlsExportMapCommandClass : BaseCommand
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
        public ControlsExportMapCommandClass()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CommandLibrary"; //localizable text
            base.m_caption = "输出";  //localizable text 
            base.m_message = "地图输出";  //localizable text
            base.m_toolTip = "地图输出";  //localizable text
            base.m_name = "ExportMap";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overriden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                    m_hookHelper = null;
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add ControlsExportMapCommandClass.OnClick implementation
            if (m_hookHelper == null) return;
            try
            {
                SaveFileDialog saveFileDlg = new SaveFileDialog();
                saveFileDlg.Filter = "(*jpeg)|*jpeg|(*.tif)|*tif|(*.pdf)|*.pdf|(*.bmp)|*.bmp|(*.gif)|*.gif|(*.png)|*.png";
                if (saveFileDlg.ShowDialog() == DialogResult.OK)
                {
                    IExport export = null;
                    if (1 == saveFileDlg.FilterIndex)
                    { export = new ExportJPEGClass(); export.ExportFileName = saveFileDlg.FileName + ".jpeg"; }
                    else if (2 == saveFileDlg.FilterIndex)
                    { export = new ExportTIFFClass(); export.ExportFileName = saveFileDlg.FileName + ".tif"; }
                    else if (3 == saveFileDlg.FilterIndex)
                    { export = new ExportPDFClass(); export.ExportFileName = saveFileDlg.FileName/* + ".pdf"*/; }
                    else if (4 == saveFileDlg.FilterIndex)
                    { export = new ExportBMPClass(); export.ExportFileName = saveFileDlg.FileName/* + ".bmp"*/; }
                    else if (5 == saveFileDlg.FilterIndex)
                    { export = new ExportGIFClass(); export.ExportFileName = saveFileDlg.FileName/* + ".gif"*/; }
                    else if (6 == saveFileDlg.FilterIndex)
                    { export = new ExportPNGClass(); export.ExportFileName = saveFileDlg.FileName/* + ".png"*/; }
                    int res = 96;
                    export.Resolution = res;
                    tagRECT exportRECT = (m_hookHelper.Hook as IMapControl3).ActiveView.ExportFrame;
                    IEnvelope pENV = new EnvelopeClass();
                    pENV.PutCoords(exportRECT.left, exportRECT.top, exportRECT.right, exportRECT.bottom);
                    export.PixelBounds = pENV;
                    int Hdc = export.StartExporting();
                    IEnvelope pVisibleBounds = null;
                    ITrackCancel pTrack = null;
                    (m_hookHelper.Hook as IMapControl3).ActiveView.Output(Hdc, (int)export.Resolution, ref exportRECT, pVisibleBounds, pTrack);
                    Application.DoEvents();
                    export.FinishExporting();
                    export.Cleanup();
                }
            }
            catch
            {
            }
        }

        #endregion
    }
}
