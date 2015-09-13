using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace CommandLibrary
{
    /// <summary>
    /// Command that works in ArcMap/Map/PageLayout
    /// </summary>
    [Guid("6f639b28-8350-46e2-a5a3-fd69eef19e0f")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CommandLibrary.ControlsPrintDocCommandClass")]
    public sealed class ControlsPrintMapCommandClass : BaseCommand
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
        public ControlsPrintMapCommandClass()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CommandLibrary"; //localizable text
            base.m_caption = "打印...";  //localizable text 
            base.m_message = "地图打印";  //localizable text
            base.m_toolTip = "地图打印";  //localizable text
            base.m_name = "MapPrint";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

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
            // TODO: Add ControlsPrintDocCommandClass.OnClick implementation
            PrintAuto(m_hookHelper.ActiveView);
        }

        // <summary>
        /// 按纸张打印地图 //by yl landgis@126.com 2008.6.18
        /// </summary>
        /// <param name="pActiveView"></param>
        /// <param name="pscale"></param>
        private void PrintAuto(IActiveView pActiveView)
        {
            try
            {
                IPaper pPaper = new Paper();
                IPrinter pPrinter = new EmfPrinterClass();

                System.Drawing.Printing.PrintDocument sysPrintDocumentDocument = new System.Drawing.Printing.PrintDocument();

                pPaper.PrinterName = sysPrintDocumentDocument.PrinterSettings.PrinterName;
                pPrinter.Paper = pPaper;

                int Resolution = pPrinter.Resolution;

                double w, h;
                IEnvelope PEnvelope = pActiveView.Extent;
                w = PEnvelope.Width;
                h = PEnvelope.Height;
                double pw, ph;//纸张
                pPrinter.QueryPaperSize(out pw, out ph);
                tagRECT userRECT = pActiveView.ExportFrame;

                userRECT.left = (int)(pPrinter.PrintableBounds.XMin * Resolution);
                userRECT.top = (int)(pPrinter.PrintableBounds.YMin * Resolution);

                if ((w / h) > (pw / ph))//以宽度来调整高度
                {
                    userRECT.right = userRECT.left + (int)(pPrinter.PrintableBounds.Width * Resolution);
                    userRECT.bottom = userRECT.top + (int)((pPrinter.PrintableBounds.Width * Resolution) * h / w);
                }
                else
                {
                    userRECT.bottom = userRECT.top + (int)(pPrinter.PrintableBounds.Height * Resolution);
                    userRECT.right = userRECT.left + (int)(pPrinter.PrintableBounds.Height * Resolution * w / h);

                }

                IEnvelope pDriverBounds = new EnvelopeClass();
                pDriverBounds.PutCoords(userRECT.left, userRECT.top, userRECT.right, userRECT.bottom);

                ITrackCancel pCancel = new CancelTrackerClass();
                int hdc = pPrinter.StartPrinting(pDriverBounds, 0);

                pActiveView.Output(hdc, pPrinter.Resolution,
                ref userRECT, pActiveView.Extent, pCancel);

                pPrinter.FinishPrinting();
            }
            catch (Exception e)
            {
                
            }

        }

        #endregion
    }
}
