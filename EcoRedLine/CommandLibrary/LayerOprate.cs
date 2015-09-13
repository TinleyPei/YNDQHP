using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Carto;

namespace CommandLibrary
{
    public static class LayerOprate
    {
        /// <summary>
        /// 通过图层的名称获得图层在Map中的Index
        /// </summary>
        /// <param name="_pMap">地图对象</param>
        /// <param name="_LayerName">图层名称</param>
        /// <returns></returns>
        public static int getLayerIndexByName(IMap _pMap,string _LayerName)
        {
            int iIndex = -1;
            for (int i = 0; i < _pMap.LayerCount; i++)
            {
                if (_pMap.get_Layer(i).Name == _LayerName)
                {
                    iIndex =i;
                    return iIndex;
                }
            }
            return iIndex;
        }

    }
}
