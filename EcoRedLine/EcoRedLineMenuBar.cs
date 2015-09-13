using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPluginEngine;

namespace EcoRedLine
{
    class EcoRedLineMenuBar : MyPluginEngine.IMenuDef
    {
        #region IMenuDef 成员
        public string Caption
        {
            get { return "生态红线"; }
        }

        public string Name
        {
            get { return "EcoRedLineMenu"; }
        }

        public long ItemCount
        {
            get { return 6; }
        }

        public void GetItemInfo(int pos, MyPluginEngine.ItemDef itemDef)
        {
            switch (pos)
            {
                case 0:
                    itemDef.ID = "EcoRedLine.frmBiodiversityCmd";
                    itemDef.Group = false;
                    break;
                case 1:
                    itemDef.ID = "EcoRedLine.frmFloodCmd";
                    itemDef.Group = false;
                    break;
                case 2:
                    itemDef.ID = "EcoRedLine.frmGeoHazardsCmd";
                    itemDef.Group = false;
                    break;
                case 3:
                    itemDef.ID = "EcoRedLine.frmTerrainCmd";
                    itemDef.Group = false;
                    break;
                case 4:
                    itemDef.ID = "EcoRedLine.frmWaterConervationCmd";
                    itemDef.Group = false;
                    break;
                case 5:
                    itemDef.ID = "EcoRedLine.frmWaterSoilConservationCmd";
                    itemDef.Group = false;
                    break;
                default:
                    break;

            }
        }
        #endregion
    }
}
