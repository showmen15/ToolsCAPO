using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTest
{
    public class RExporterResult
    {
        public string[] KwTest { get; set; }
        public string[] DunnTest { get; set; }
        public string ChartPath { get; set; }

        public string[] GetSplitTests
        {
            get
            {
                List<string> temp = new List<string>();
                temp.AddRange(KwTest);
                temp.AddRange(DunnTest);

                return temp.ToArray();
            }
        }

        public RExporterResult()
        {
        }

    }
}
