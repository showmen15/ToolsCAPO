using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultChecker
{
    public class ResultItem
    {
        public int ID_Case { get; set; }
        public int ID_Trials { get; set; }

        /*  public int ID_Program { get; set; }
            public int ID_Map  { get; set; }
            public int ID_Config { get; set; }
            public int ID_Robot { get; set; }
            public string RobotPosition  { get; set; }
            public RobotPositionItem[] RobotP
        */

        public ResultItem(int iID_Case, int iID_Trials)
        {
            ID_Case = iID_Case;
            ID_Trials = iID_Trials;
        }
    }
}
