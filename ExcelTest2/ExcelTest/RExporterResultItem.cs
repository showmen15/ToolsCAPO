using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTest
{
    public class RExporterResultItem
    {
        public int Dunn_ID_Form { get; set; }
        public int Dunn_ID_To { get; set; }
        public string Dunn_Desc { get; set; }
        public string Dunn_Test_Org { get; set; }
        public double Dunn_Value { get; set; }

        public double Kw_Test { get; set; }
        public string Kw_Test_org { get; set; }
    }
}
