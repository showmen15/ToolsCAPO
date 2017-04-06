using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTest
{
    public class MapItem
    {
        public int ID_Map { get; set; }
        public string MapName { get; set; }

        public MapItem(int iid_Map, string sMapName)
        {
            ID_Map = iid_Map;
            MapName = sMapName;
        }    
    }
}
