using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskRunner
{
    public class RunConfig
    {
        public int ID_Program { get; set; }
        public int Timeout { get; set; }
        public int ID_Case { get; set; }
        public string Name_Case { get; set; }
        public int Visualizer { get; set; }

        public string Name_Program { get; set; }
        public string Name_Map { get; set; }
        public string Name_Config { get; set; }

        public RunConfig(int iID_Program, int itimeout, int iID_Case, string name_Case, int visualizer)
        {
            ID_Program = iID_Program;
            Timeout = itimeout;
            ID_Case = iID_Case;
            Name_Case = name_Case;
            Visualizer = visualizer;
        }
    }
}
