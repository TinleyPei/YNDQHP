using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;

using MyPluginEngine;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace CartographicMenuBar
{
    /// <summary>
    /// 另存地图文档
    /// </summary>
    public class SaveAsDocment : MyPluginEngine.ICommand
    {
        private MyPluginEngine.IApplication hk;
        private System.Drawing.Bitmap m_hBitmap;

        private ESRI.ArcGIS.SystemUI.ICommand cmd = null;
        private IMapControlDefault m_mapControl;
        public SaveAsDocment()
        {
            string str = @"..\Data\Image\CartographicMenuBar\SaveAsDocment.png";
            if (System.IO.File.Exists(str))
                m_hBitmap = new Bitmap(str);
            else
                m_hBitmap = null;
        }

        #region ICommand 成员

        public System.Drawing.Bitmap Bitmap
        {
            get { return m_hBitmap; }
        }

        public string Caption
        {
            get { return "另存地图文档"; }
        }

        public string Category
        {
            get { return "制图"; }
        }

        public bool Checked
        {
            get { return false; }
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
            get { return "另存地图文档"; }
        }

        public string Name
        {
            get { return "SaveAsDocment"; }
        }

        public void OnClick()
        {
            cmd = new ControlsSaveAsDocCommandClass();
            cmd.OnCreate(this.m_mapControl);
            cmd.OnClick();
        }

        public void OnCreate(MyPluginEngine.IApplication hook)
        {
            if (hook != null)
            {
                this.hk = hook;
                m_mapControl = this.hk.MapControl;
            }
        }

        public string Tooltip
        {
            get { return "SaveAsDocment"; }
        }

        #endregion
    }
}
