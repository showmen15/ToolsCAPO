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

        public VisualizerConfig(int iID_Case, int iID_Trials, string sName)
        {
            ID_Case = iID_Case;
            ID_Trials = iID_Trials;
            Name = sName;
        }
    }
}
