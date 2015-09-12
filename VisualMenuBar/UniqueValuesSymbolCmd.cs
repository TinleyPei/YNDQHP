using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;

namespace VisualMenuBar
{
    class UniqueValuesSymbolCmd : MyPluginEngine.ICommand
    {
        private MyPluginEngine.IApplication hk;
        private System.Drawing.Bitmap m_hBitmap;

 
        public UniqueValuesSymbolCmd()
        {
            string str = @"..\Data\Image\VisualMenuBar\UniqueValuesSymbolCmd.png";
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
            get { return "唯一值"; }
        }

        public string Category
        {
            get { return "专题图"; }
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
            get { return "唯一值"; }
        }

        public string Name
        {
            get { return "UniqueValuesSymbolCmd"; }
        }

        public void OnClick()
        {
            if (this.hk == null) return;
            if (this.hk.MapControl.Map.LayerCount > 0)
            {
                UniqueValuesSymbol UniqueValuesSymbol = new UniqueValuesSymbol(this.hk);
                UniqueValuesSymbol.Show();
            }
          
        }

        public void OnCreate(MyPluginEngine.IApplication hook)
        {
            if (hook != null)
            {
                this.hk = hook;
            }
        }

        public string Tooltip
        {
            get { return "唯一值"; }
        }

        #endregion
    }
}
