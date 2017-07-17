using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class VisualizerConfig
    {
        public int ID_Case { get; set; }
        public int ID_Trials { get; set; }
        public string Name { get; set; }

        public string Name_Case { get; set; }
        public string Name_Config { get; set; }
        public string Name_Map { get; set; }
        public string Name_Program { get; set; }
        public string IdGlobal { get; set; }

        public VisualizerConfig(int iID_Case, int iID_Trials, string sName, string sName_Case,
                                string sName_Config, string sName_Map, string sName_Program, string sIdGlobal)
        {
            ID_Case = iID_Case;
            ID_Trials = iID_Trials;
            Name = sName;

            Name_Case = sName_Case;
            Name_Config = sName_Config;
            Name_Map = sName_Map;
            Name_Program = sName_Program;
            IdGlobal = sIdGlobal;
        }
    }
}
