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
    /// 保存地图文档
    /// </summary>
    public class SaveDocument : MyPluginEngine.ICommand
    {
        private MyPluginEngine.IApplication hk;
        private System.Drawing.Bitmap m_hBitmap;

        private ESRI.ArcGIS.SystemUI.ICommand cmd = null;
        private IMapControlDefault m_mapControl;
        private string m_mapDocumentName = string.Empty;

        public SaveDocument()
        {
            string str = @"..\Data\Image\CartographicMenuBar\SaveDocument.png";
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
            get { return "保存地图文档"; }
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
            get { return "保存地图文档"; }
        }

        public string Name
        {
            get { return "SaveDocument"; }
        }

        public void OnClick()
        {

            //// 首先确认当前地图文档是否有效  
            //if (null != m_pageLayoutControl.DocumentFilename && m_mapControl.CheckMxFile(m_pageLayoutControl.DocumentFilename))  
            //{  
            //// 创建一个新的地图文档实例  
            //IMapDocument mapDoc = new MapDocumentClass();  // 打开当前地图文档  
            //mapDoc.Open(m_pageLayoutControl.DocumentFilename, string.Empty);  // 用 PageLayout 中的文档替换当前文档中的 PageLayout 部分  
            //mapDoc.ReplaceContents((IMxdContents)m_pageLayoutControl.PageLayout);  // 保存地图文档  
            //mapDoc.Save(mapDoc.UsesRelativePaths, false);  
            //mapDoc.Close();  

            //m_mapDocument = this.hk.Document;
            //if (m_mapDocument.get_IsReadOnly(m_mapDocument.DocumentFilename))
            //{
            //    MessageBox.Show("This map document is read only!");
            //    return;
            //}
            //m_mapDocument.Save(m_mapDocument.UsesRelativePaths, true);
            //MessageBox.Show("Changes saved successfully!");

            //execute Save Document command
            //m_mapDocumentName = m_mapControl.DocumentFilename;
            if (m_mapControl.DocumentFilename != null && m_mapControl.CheckMxFile(m_mapDocumentName))
            {
                //m_mapControl.CheckMxFile(m_mapDocumentName);
                //create a new instance of a MapDocument
                IMapDocument mapDoc = new MapDocumentClass();
                mapDoc.Open(m_mapDocumentName, string.Empty);

                //Make sure that the MapDocument is not readonly
                if (mapDoc.get_IsReadOnly(m_mapDocumentName))
                {
                    MessageBox.Show("Map document is read only!");
                    mapDoc.Close();
                    return;
                }

                //Replace its contents with the current map
                mapDoc.ReplaceContents((IMxdContents)m_mapControl.Map);

                //save the MapDocument in order to persist it
                mapDoc.Save(mapDoc.UsesRelativePaths, false);

                //close the MapDocument
                mapDoc.Close();
            }
            else {
                cmd = new ControlsSaveAsDocCommandClass();
                cmd.OnCreate(this.m_mapControl);
                cmd.OnClick();
            }
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
            get { return "SaveDocument"; }
        }

        #endregion
    }
}
