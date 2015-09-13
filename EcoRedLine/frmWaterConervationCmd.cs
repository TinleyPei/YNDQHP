using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPluginEngine;

namespace EcoRedLine
{
    class frmWaterConervationCmd : MyPluginEngine.ICommand
    {
        private MyPluginEngine.IApplication hk;
        private System.Drawing.Bitmap m_hBitmap;
        private ESRI.ArcGIS.SystemUI.ICommand cmd = null;
        frmWaterConervation pfrmWaterConervation;

        public frmWaterConervationCmd()
        {
            string str = @"..\Data\Image\EcoRedLine\WaterConervation.png";
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
            get { return "水源涵养"; }
        }

        public string Category
        {
            get { return "EcoRedLineMenu"; }
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
            get { return "水源涵养"; }
        }

        public string Name
        {
            get { return "frmWaterConervation"; }
        }

        public void OnClick()
        {
            //System.Windows.Forms.MessageBox.Show("正在开发中！");
            pfrmWaterConervation = new frmWaterConervation();
            pfrmWaterConervation.ShowDialog();
        }

        public void OnCreate(IApplication hook)
        {
            if (hook != null)
            {
                this.hk = hook;
                pfrmWaterConervation = new frmWaterConervation();
                pfrmWaterConervation.Visible = false;
            }
        }

        public string Tooltip
        {
            get { return "水源涵养"; }
        }
        #endregion
    }
}
