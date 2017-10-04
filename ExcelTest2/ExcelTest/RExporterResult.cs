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

        public int GetKwTestDF()
        {
            string[] split = KwTest[4].Split(',');
            int value = int.Parse(split[1].Replace("df =", string.Empty));

            return value;
        }

        public double GetKwchiSquared()
        {
            string[] split = KwTest[4].Split(',');
            double value = double.Parse(split[0].Replace("Kruskal-Wallis chi-squared =", string.Empty).Replace(".",","));

            return value;
        }

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
