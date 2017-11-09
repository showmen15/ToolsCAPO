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
        //Kw test  analiza testu.
        // https://www.youtube.com/watch?v=BkyGuNuaZYw
        //https://www.youtube.com/watch?v=q1D4Di1KWLc
        //http://prac.im.pwr.wroc.pl/~arokita/SMG/Tablica%20rozkladu%20chi%20kwadrat.pdf

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

        public RExporterResult GetChartPDFTest(DataSet ds, string sChartName,int id_map)
        {
            RExporterResult result = new RExporterResult();
            string rowData = string.Format("{0}\\input.csv", Environment.CurrentDirectory);

            string sJPGPath = string.Format("{0}\\chart.pdf", Environment.CurrentDirectory);
            string sTestDunnResult = string.Format("{0}\\dunnTest.txt", Environment.CurrentDirectory);
            string sTestKwResult = string.Format("{0}\\KwTest.txt", Environment.CurrentDirectory);

            exportToCSV(ds, rowData);

            executeR(string.Format("{0}\\DunnTest.R", Environment.CurrentDirectory), rowData, sTestDunnResult);
            result.DunnTest = File.ReadAllLines(sTestDunnResult);

            executeR(@".\KwTest.R", rowData, sTestKwResult);
            result.KwTest = File.ReadAllLines(sTestKwResult);

            int df = result.GetKwTestDF();
            double kw = result.GetKwchiSquared();

            string testResult = result.GetKwchiDescription(); //formatKwTest(df, kw);

            string sScriptFile = setChartExporter(id_map);
            executeR(sScriptFile, rowData, sJPGPath, sChartName, testResult);
            result.ChartPath = sJPGPath;

            return result;
        }

        private string formatKwTest(int df, double kw)
        {
            double[] prawodpodobienstwMinimalnea = new double[] { 0, 3.8415, 5.9915, 7.8147, 9.4877, 11.0705, 12.5916 };
            double alfa = 0.05;

            double chiCrituc = prawodpodobienstwMinimalnea[df];

            string kwTest = "$(\\alpha = " + alfa.ToString() + "; \\chi^{2}_{CRIT} = " + chiCrituc.ToString() + "; H^{2} = " + kw.ToString() + "; df = " + df.ToString() + ")$";
            return kwTest;
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

        private void executeR(string sScriptFile, string sDataCsv, string sOutputFile,string sChartName, string testResult)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = RscriptRun;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\"", sScriptFile, sDataCsv, sOutputFile, sChartName, testResult);

            using (Process exeProcess = Process.Start(startInfo))
            {
                exeProcess.WaitForExit();
            }
        }

        private string setChartExporter(int id_Map)
        {
            string result = @".\BoxPlotPDF2.R";

            switch (id_Map)
            {

                case 6: //Simulation_Eight_intersectionusun PF i PF+
                case 10: //Simulation_Open_space usun PF+ i PF
                case 13: //A narrow corridor
                case 15: //Skrzy¿owanieRównorzêdneNowe
                case 16: // Circel
                case 18: //Passing place

                case 19: // Robots_Open_space
                case 20: //Robots_Passing_place
                case 21: //LabOtwartaPrzestrzeñ 4 Roboty

                    result = @".\BoxPlotPDF3.R";
                    break;
            }
            return result;
        }
    }
}
