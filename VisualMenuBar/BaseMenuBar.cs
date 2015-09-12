using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPluginEngine;

namespace VisualMenuBar
{
    public class VisualMenuBar : MyPluginEngine.IMenuDef
    {
        #region IMenuDef 成员

        public string Caption
        {
            get { return "空间数据可视化"; }
        }

        public void GetItemInfo(int pos, ItemDef itemDef)
        {
            switch (pos)
            {
                case 0:
                    itemDef.ID = "VisualMenuBar.SingleSymbolCmd";
                    itemDef.Group = false;
                    break;
                case 1:
                    itemDef.ID = "VisualMenuBar.DotDensitySymbolsCmd";
                    itemDef.Group = false;
                    break;
                case 2:
                    itemDef.ID = "VisualMenuBar.GraduatedSymbolsCmd";
                    itemDef.Group = false;
                    break;
                case 3:
                    itemDef.ID = "VisualMenuBar.ProportionalSymbolsCmd";
                    itemDef.Group = false;
                    break;
                case 4:
                    itemDef.ID = "VisualMenuBar.StatisticsSymbolsCmd";
                    itemDef.Group = false;
                    break;
                case 5:
                    itemDef.ID = "VisualMenuBar.UniqueValuesSymbolCmd";
                    itemDef.Group = false;
                    break;
                case 6:
                    itemDef.ID = "VisualMenuBar.ClassBreaksRendererSymbolCmd";
                    itemDef.Group = false;
                    break;
                case 7:
                    itemDef.ID = "VisualMenuBar.StatisticsSymbolsCmd";
                    itemDef.Group = false;
                    break;
                case 8:
                    itemDef.ID = "VisualMenuBar.SymbolizationByLayerPropPageCmd";
                    itemDef.Group = false;
                    break;
                    
                default:
                    break;

            }
        }

        public long ItemCount
        {
            get { return 9; }
        }

        public string Name
        {
            get { return "VisualMenuBar"; }
        }

        #endregion
    }
}
