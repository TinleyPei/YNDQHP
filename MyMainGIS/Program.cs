using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;

namespace MyMainGIS
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!RuntimeManager.Bind(ProductCode.Engine))
            {
                if (!RuntimeManager.Bind(ProductCode.Desktop))
                {
                    MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.");
                    return;
                }
            }
            initlicense();
            //IAoInitialize m_AoInitialize = new AoInitialize();
            //esriLicenseStatus licenseStatus = esriLicenseStatus.esriLicenseUnavailable;
            //licenseStatus = m_AoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB);
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainGIS());
        }
        public static void initlicense()
        {
            AoInitialize aoi = new AoInitialize();
            esriLicenseExtensionCode extensionCodes = esriLicenseExtensionCode.esriLicenseExtensionCodeSpatialAnalyst; //这是解决的办法
            esriLicenseExtensionCode extensionCode = esriLicenseExtensionCode.esriLicenseExtensionCode3DAnalyst;
            esriLicenseProductCode pro = esriLicenseProductCode.esriLicenseProductCodeEngine;
            if (aoi.IsProductCodeAvailable(pro) == esriLicenseStatus.esriLicenseAvailable &&
                aoi.IsExtensionCodeAvailable(pro, extensionCode) == esriLicenseStatus.esriLicenseAvailable &&
                aoi.IsExtensionCodeAvailable(pro, extensionCodes) == esriLicenseStatus.esriLicenseAvailable
                 )
            {
                aoi.Initialize(pro);
                aoi.CheckOutExtension(extensionCode);
                aoi.CheckOutExtension(extensionCodes);
            }
        }
    }
}
