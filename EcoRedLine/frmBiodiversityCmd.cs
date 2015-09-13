using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPluginEngine;

namespace EcoRedLine
{
    class frmBiodiversityCmd : MyPluginEngine.ICommand
    {
        private MyPluginEngine.IApplication hk;
        private System.Drawing.Bitmap m_hBitmap;
        private ESRI.ArcGIS.SystemUI.ICommand cmd = null;
        frmBiodiversity pfrmBiodiversity;

        public frmBiodiversityCmd()
        {
            string str = @"..\Data\Image\EcoRedLine\Biodiversity.png";
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
            get { return "生物多样性"; }
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
            get { return "生物多样性"; }
        }

        public string Name
        {
            get { return "frmBiodiversity"; }
        }

        public void OnClick()
        {
            System.Windows.Forms.MessageBox.Show("正在开发中！");
            //pfrmBiodiversity = new frmBiodiversity();
            //pfrmBiodiversity.ShowDialog();
        }

        public void OnCreate(IApplication hook)
        {
            if (hook != null)
            {
                this.hk = hook;
                pfrmBiodiversity = new frmBiodiversity();
                pfrmBiodiversity.Visible = false;
            }
        }

        public string Tooltip
        {
            get { return "生物多样性"; }
        }
        #endregion
    }
}
