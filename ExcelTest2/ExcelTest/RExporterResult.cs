using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            int value = 0;

            if (KwTest != null && KwTest.Length > 0)
            {

                string[] split = KwTest[4].Split(',');
                value = int.Parse(split[1].Replace("df =", string.Empty));
           }

            return value;
        }

        public double GetKwchiSquared()
        {
            double value = 0.0;

            if (KwTest != null && KwTest.Length > 0)
            {
                string[] split = KwTest[4].Split(',');
                value = double.Parse(split[0].Replace("Kruskal-Wallis chi-squared =", string.Empty).Replace(".", ","));
            }

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

        public string GetKwchiDescription()
        {
            string result = "null";//string.Empty;

            if(KwTest.Length > 4)
                result = KwTest[4];

            return result;
        }

        public RExporterResult()
        {
        }

        public RExporterResultItem[] GetDunnTestResult()
        {
            List<RExporterResultItem> result = new List<RExporterResultItem>();
            string dunnTestInput = string.Concat(DunnTest);

            StringBuilder dunnOrg = new StringBuilder();

            foreach (var item in DunnTest)
                dunnOrg.AppendLine(item);


            StringBuilder kwTestOrg = new StringBuilder();

            foreach (var item in KwTest)
                kwTestOrg.AppendLine(item);

            int pValue_IndexStartKw_Test = kwTestOrg.ToString().IndexOf("p-value");

            string kwTestInput = kwTestOrg.ToString().Substring(pValue_IndexStartKw_Test);
             kwTestInput = kwTestInput.Replace("p-value", string.Empty);
            kwTestInput = kwTestInput.Replace("=", string.Empty);
            kwTestInput = kwTestInput.Replace("<", string.Empty);
            kwTestInput = kwTestInput.Replace(">", string.Empty);
            kwTestInput = kwTestInput.Replace("\n", string.Empty);
            kwTestInput = kwTestInput.Replace("\r", string.Empty);
            kwTestInput = kwTestInput.Replace(".", ",");

            double dkwTest = double.Parse(kwTestInput);


            Regex removeNumbers = new Regex(@"\[[0-9]+\]");

            dunnTestInput = removeNumbers.Replace(dunnTestInput, string.Empty);

            int pValue_IndexStart = dunnTestInput.IndexOf("$P");
            int pValue_IndexEnd = dunnTestInput.IndexOf("$P.adjusted");

            string pValuesString = dunnTestInput.Substring(pValue_IndexStart, pValue_IndexEnd - pValue_IndexStart);
            pValuesString = pValuesString.Replace("$P", string.Empty);

            int comparisons_IndexStart = dunnTestInput.IndexOf("$comparisons");

            string comparisons = dunnTestInput.Substring(comparisons_IndexStart);
            comparisons = comparisons.Replace("$comparisons", string.Empty);

            string[] pValuesList = pValuesString.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            string[] comparisonsList = comparisons.Split(new string[] { "\" \"" }, StringSplitOptions.RemoveEmptyEntries);

            if (pValuesList.Length != comparisonsList.Length)
                throw new Exception("Zle sparsowane wyniki");
            else
            {
                for (int i = 0; i < pValuesList.Length; i++)
                {
                    double p_value = double.Parse(pValuesList[i].Replace(".",","));
                    string compItem = comparisonsList[i].Replace("\"", string.Empty).Replace("\\", string.Empty);

                    string[] subListr = compItem.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                    int id_value_from = int.Parse(subListr[0]);
                    int id_value_to = int.Parse(subListr[1]);

                    RExporterResultItem temp = new RExporterResultItem();
                    temp.Dunn_Value = p_value;
                    temp.Dunn_ID_Form = id_value_from;
                    temp.Dunn_ID_To = id_value_to;
                    temp.Dunn_Desc = compItem;
                    temp.Dunn_Test_Org = dunnOrg.ToString();
                    temp.Kw_Test_org = kwTestOrg.ToString();
                    temp.Kw_Test = dkwTest;

                    result.Add(temp);
                }
            }

            return result.ToArray();
        }

       

    }
}
