using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPluginEngine;


namespace CartographicMenuBar
{
    public class CartographicMenuBar : MyPluginEngine.IMenuDef
    {
        #region IMenuDef 成员

        public string Caption
        {
            get { return "制图"; }
        }

        public void GetItemInfo(int pos, ItemDef itemDef)
        {
            switch (pos)
            {
                case 0:
                    itemDef.ID = "CartographicMenuBar.CreateNewDocument";
                    itemDef.Group = false;
                    break;
                case 1:
                    itemDef.ID = "CartographicMenuBar.OpenDocument";
                    itemDef.Group = false;
                    break;
                case 2:
                    itemDef.ID = "CartographicMenuBar.SaveDocument";
                    itemDef.Group = false;
                    break;
                case 3:
                    itemDef.ID = "CartographicMenuBar.SaveAsDocment";
                    itemDef.Group = false;
                    break;
                case 4:
                    itemDef.ID = "CartographicMenuBar.Northarrow";
                    itemDef.Group = false;
                    break;
                case 5:
                    itemDef.ID = "CartographicMenuBar.SaveDocument";
                    itemDef.Group = false;
                    break;
                case 6:
                    itemDef.ID = "CartographicMenuBar.SaveDocument";
                    itemDef.Group = false;
                    break;
                case 7:
                    itemDef.ID = "CartographicMenuBar.SaveDocument";
                    itemDef.Group = false;
                    break;     
                default:
                    break;

            }
        }

        public long ItemCount
        {
            get { return 8; }
        }

        public string Name
        {
            get { return "CartographicMenuBar"; }
        }

        #endregion
    }
}
