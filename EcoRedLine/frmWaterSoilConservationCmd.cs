using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using MyPluginEngine;

namespace EcoRedLine
{
    class frmWaterSoilConservationCmd : MyPluginEngine.ICommand
    {
        private MyPluginEngine.IApplication hk;
        private System.Drawing.Bitmap m_hBitmap;
        private ESRI.ArcGIS.SystemUI.ICommand cmd = null;
        frmWaterSoilConservation pfrmWaterSoilConservation;

        public frmWaterSoilConservationCmd()
        {
            string str = @"..\Data\Image\EcoRedLine\WaterSoilConservation.png";
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
            get { return "水土保持"; }
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
            get { return "水土保持"; }
        }

        public string Name
        {
            get { return "frmWaterSoilConservation"; }
        }

        public void OnClick()
        {
            //System.Windows.Forms.MessageBox.Show("正在开发中！");
            pfrmWaterSoilConservation = new frmWaterSoilConservation();
            pfrmWaterSoilConservation.ShowDialog();
        }

        public void OnCreate(IApplication hook)
        {
            if (hook != null)
            {
                this.hk = hook;
                pfrmWaterSoilConservation = new frmWaterSoilConservation();
                pfrmWaterSoilConservation.Visible = false;
            }
        }

        public string Tooltip
        {
            get { return "水土保持"; }
        }
        #endregion
    }
}
