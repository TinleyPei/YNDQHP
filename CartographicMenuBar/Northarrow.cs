using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using MyPluginEngine;
using System.Runtime.InteropServices;

namespace CartographicMenuBar
{
    public class Northarrow : MyPluginEngine.ITool
    {
        private MyPluginEngine.IApplication hk;
        private System.Drawing.Bitmap m_hBitmap;

        private INewEnvelopeFeedback m_Feedback;
        private IPoint m_Point;
        private bool m_InUse;

        private ESRI.ArcGIS.SystemUI.ITool tool = null;
        private ESRI.ArcGIS.SystemUI.ICommand cmd = null;


        [DllImport("User32", CharSet = CharSet.Auto)]
        private static extern int SetCapture(int hWnd);
        [DllImport("User32", CharSet = CharSet.Auto)]
        private static extern int GetCapture();
        [DllImport("User32", CharSet = CharSet.Auto)]
        private static extern int ReleaseCapture();

        public Northarrow()
        {
            string str = @"..\Data\Image\CartographicMenuBar\Northarrow.png";
            if (System.IO.File.Exists(str))
                m_hBitmap = new Bitmap(str);
            else
                m_hBitmap = null;
        }

        #region ITool 成员

        public System.Drawing.Bitmap Bitmap
        {
            get { return m_hBitmap; }
        }

        public string Caption
        {
            get { return "指北针"; }
        }

        public string Category
        {
            get { return "制图"; }
        }

        public bool Checked
        {
            get { return false; }
        }

        public int Cursor
        {
            get { return (int)ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerArrow; }
        }

        public bool Deactivate()
        {
            return false;
        }

        public bool Enabled
        {
            get { return true; }
        }

        public int HelpContextId
        {
            get { return 0; }
        }

        public string HelpFile
        {
            get { return ""; }
        }

        public string Message
        {
            get { return "添加指北针"; }
        }

        public string Name
        {
            get { return "Northarrow"; }
        }
        public bool OnContextMenu(int x, int y)
        {
            return false;
        }
        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public void OnCreate(MyPluginEngine.IApplication hook)
        {
            try
            {
                //m_hookHelper = new HookHelperClass();
                this.hk = hook;
            }
            catch
            {
                this.hk = null;
            }

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public  void OnClick()
        {
            // TODO: Add AddNortharrow.OnClick implementation
        }

        public  void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add AddNortharrow.OnMouseDown implementation
            m_Point = this.hk.MapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            SetCapture(this.hk.MapControl.ActiveView.ScreenDisplay.hWnd);
            m_InUse = true;
        }
        public void OnDblClick()
        {

        }
        public void OnKeyDown(int keyCode, int shift)
        {

        }

        public void OnKeyUp(int keyCode, int shift)
        {

        }
        public  void OnMouseMove(int Button, int Shift, int X, int Y)
        {
              if (m_InUse == false) return;
            if (m_Feedback == null)
            {
                m_Feedback = new NewEnvelopeFeedbackClass();
                m_Feedback.Display = this.hk.MapControl.ActiveView.ScreenDisplay;
                m_Feedback.Start(m_Point);
            }

            m_Feedback.MoveTo(this.hk.MapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y));
        }

        public  void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            if (m_InUse == false) return;

            if (GetCapture() == this.hk.MapControl.ActiveView.ScreenDisplay.hWnd)
                ReleaseCapture();

            if (m_Feedback == null)
            {
                m_Feedback = null;
                m_InUse = false;
                return;
            }
            IEnvelope envelope = m_Feedback.Stop();
            if ((envelope.IsEmpty) || (envelope.Width == 0) || (envelope.Height == 0))
            {
                m_Feedback = null;
                m_InUse = false;
                return;
            }

            GetSymbol symbolForm = new GetSymbol(esriSymbologyStyleClass.esriStyleClassNorthArrows);
            symbolForm.Text = "选择指北针";
            IStyleGalleryItem styleGalleryItem = symbolForm.GetItem(esriSymbologyStyleClass.esriStyleClassNorthArrows);
            symbolForm.Dispose();
            if (styleGalleryItem == null) return;

            IMapFrame mapFrame = (IMapFrame)this.hk.MapControl.ActiveView.GraphicsContainer.FindFrame(this.hk.MapControl.ActiveView.FocusMap);
            IMapSurroundFrame mapSurroundFrame = new MapSurroundFrameClass();
            mapSurroundFrame.MapFrame = mapFrame;
            mapSurroundFrame.MapSurround = (IMapSurround)styleGalleryItem.Item;

            IElement element = (IElement)mapSurroundFrame;
            element.Geometry = envelope;

            this.hk.MapControl.ActiveView.GraphicsContainer.AddElement((IElement)mapSurroundFrame, 0);
            this.hk.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, mapSurroundFrame, null);
            m_Feedback = null;
            m_InUse = false; 
        }
       

        public void Refresh(int hDC)
        {
          
        }

        public string Tooltip
        {
            get { return "指北针"; }
        }

        #endregion
    }
}
