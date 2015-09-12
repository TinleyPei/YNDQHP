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
    class SingleSymbolCmd : MyPluginEngine.ICommand
    {
        private MyPluginEngine.IApplication hk;
        private System.Drawing.Bitmap m_hBitmap;

        public SingleSymbolCmd()
        {
            string str = @"..\Data\Image\VisualMenuBar\SingleSymbolCmd.png";
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
            get { return "单一符号"; }
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
            get { return "单一符号"; }
        }

        public string Name
        {
            get { return "SingleSymbolCmd"; }
        }

        public void OnClick()
        {
            if (this.hk == null) return;
            if (this.hk.MapControl.Map.LayerCount > 0)
            {
                SingleSymbol singleSymbol = new SingleSymbol(this.hk);
                singleSymbol.Show(this.hk as System.Windows.Forms.IWin32Window);
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
            get { return "单一符号"; }
        }

        #endregion
    }
}
