using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTest
{
    public class RExporter
    {
        private string RscriptRun;

        public RExporter()
        {
            string InstallPath = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\R-core\R", "InstallPath", null);

            RscriptRun = string.Format("{0}\\bin\\Rscript.exe", InstallPath);
        }

        //public void GetPdfChart(DataSet ds, string fileName, string sTestResult)
        //{
        //    string rowData = @".\input.csv";

        //    exportToCSV(ds, rowData);
        //    executeR(@".\BoxPlotPDF.R", rowData, fileName, sTestResult);
        //}

        //public void GetJPGChart(DataSet ds, string fileName, string sTestDunnResult,string sTestKwResult)
        //{
        //    string rowData = @".\input.csv";

        //    exportToCSV(ds, rowData);
        //    executeR(@".\BoxPlotJPG.R", rowData, fileName, "");

        //    //executeR(@".\DunnTest.R", rowData, fileName, sTestDunnResult);
        //}

        private void exportToCSV(DataSet ds, string rowData)
        {
            StringBuilder csv = new StringBuilder();
            string date = string.Empty;

            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                for (int j = 1; j <= ds.Tables[0].Columns.Count - 1; j++)
                {
                    date = ds.Tables[0].Rows[i].ItemArray[j].ToString();

                    if (date == "0")
                        csv.Append(string.Format("{0},", string.Empty));
                    else
                        csv.Append(string.Format("{0},", date));
                }

                csv.Remove(csv.Length - 1, 1);
                csv.AppendLine();
            }

            File.WriteAllText(rowData, csv.ToString());
        }

        public RExporterResult GetChartTest(DataSet ds)
        {
            RExporterResult result = new RExporterResult();
            string rowData = string.Format("{0}\\input.csv", Environment.CurrentDirectory);

            string sJPGPath = string.Format("{0}\\chart.jpg", Environment.CurrentDirectory);
            string sTestDunnResult = string.Format("{0}\\dunnTest.txt", Environment.CurrentDirectory);
            string sTestKwResult = string.Format("{0}\\KwTest.txt", Environment.CurrentDirectory);

            exportToCSV(ds, rowData);
            executeR(@".\BoxPlotJPG.R", rowData, sJPGPath);
            result.ChartPath = sJPGPath;

            executeR(string.Format("{0}\\DunnTest.R", Environment.CurrentDirectory), rowData, sTestDunnResult);
            result.DunnTest = File.ReadAllLines(sTestDunnResult);

            executeR(@".\KwTest.R", rowData, sTestKwResult);
            result.KwTest = File.ReadAllLines(sTestKwResult);

            return result;
        }

        private void executeR(string sScriptFile, string sDataCsv, string sOutputFile)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = RscriptRun;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\"", sScriptFile, sDataCsv, sOutputFile);

            using (Process exeProcess = Process.Start(startInfo))
            {
                exeProcess.WaitForExit();
            }
        }
    }
}
