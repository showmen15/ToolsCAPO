using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office;

using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ExcelTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //SQL.ConnectionString = @"data source=WR-7-BASE-74\SQLEXPRESS;initial catalog=Doktorat;Integrated Security=SSPI;";
          //  SQL.ConnectionString = @"data source=WR-7-BASE-74\SQLEXPRESS;initial catalog=DoktoratSymulacja;Integrated Security=SSPI;";

            SQL.ConnectionString = @"data source=WR-7-BASE-74\SQLEXPRESS;initial catalog=DoktoratRobot;Integrated Security=SSPI;";


            // SQL.ConnectionString = @"data source=SZYMON-KOMPUTER;initial catalog=Doktorat;Integrated Security=SSPI;";

            //  SQL.ConnectionString = @"data source=SZSZ\SQLEXPRESS;initial catalog=Doktorat; User Id=szsz; Password=szsz;";
        }

        // http://csharp.net-informations.com/excel/csharp-create-excel.htm
        //https://support.office.com/en-US/article/PERCENTILE-function-91B43A53-543C-4708-93DE-D626DEBDDDCA
        //https://msdn.microsoft.com/en-us/library/bb404904(v=office.12).aspx

        //tworzenie wykresu Box w Execelu
        //https://www.youtube.com/watch?v=ucWmfmXb1kk

        private void button1_Click(object sender, EventArgs e)
        {

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return;
            }


            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            Excel.Worksheet targetSheet = xlWorkSheet;

            SetCellValue(targetSheet, "A1", "");
            SetCellValue(targetSheet, "A2", "Minimum");
            SetCellValue(targetSheet, "A3", "Kwartyl1");
            SetCellValue(targetSheet, "A4", "Mediana");
            SetCellValue(targetSheet, "A5", "Kwartyl3");
            SetCellValue(targetSheet, "A6", "Maksimum");

            SetCellValue(targetSheet, "B1", "Q1");
            SetCellValue(targetSheet, "B2", "0.21");
            SetCellValue(targetSheet, "B3", "0.25");
            SetCellValue(targetSheet, "B4", "0.37");
            SetCellValue(targetSheet, "B5", "0.22");
            SetCellValue(targetSheet, "B6", "0.13");

            SetCellValue(targetSheet, "C1", "Q2");
            SetCellValue(targetSheet, "C2", "0.16");
            SetCellValue(targetSheet, "C3", "0.18");
            SetCellValue(targetSheet, "C4", "0.17");
            SetCellValue(targetSheet, "C5", "0.29");
            SetCellValue(targetSheet, "C6", "0.31");

            SetCellValue(targetSheet, "D1", "Q3");
            SetCellValue(targetSheet, "D2", "0.13");
            SetCellValue(targetSheet, "D3", "0.2");
            SetCellValue(targetSheet, "D4", "0.24");
            SetCellValue(targetSheet, "D5", "0.25");
            SetCellValue(targetSheet, "D6", "0.28");


            //  Excel.Range chartRange,chartRangeX;
            Excel.Range ErrorMaksimum, ErrorMadiana, ErrorMinimum;
            Excel.Range xValues;

            Excel.Range kwartyl1Values, medianaValues, kwartyl3Values;

            String kwartyl1Desc, medianaDesc, kwartyl3Desc;
            //chartRange = targetSheet.get_Range("A3", "D5"); //zakres dla danych wykresu warosci y
            // chartRangeX = targetSheet.get_Range("B1", "D1");

            xValues = targetSheet.get_Range("B1", "D1");

            ErrorMaksimum = targetSheet.get_Range("B6", "D6");
            ErrorMadiana = targetSheet.get_Range("B1", "D1");
            ErrorMinimum = targetSheet.get_Range("B2", "D2");

            kwartyl1Desc = "Kwartyl1";
            kwartyl1Values = targetSheet.get_Range("B3", "D3");

            medianaDesc = "Mediana";
            medianaValues = targetSheet.get_Range("B4", "D4");

            kwartyl3Desc = "Kwartyl3";
            kwartyl3Values = targetSheet.get_Range("B5", "D5");


            CreateBoxCart(targetSheet, xValues, kwartyl1Desc, kwartyl1Values, medianaDesc, medianaValues, kwartyl3Desc, kwartyl3Values, ErrorMaksimum, ErrorMadiana, ErrorMinimum);

            //chartPage.ChartWizard(chartRange, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);





            //chartRangeX = targetSheet.get_Range("B1", "D1");



            // chartPage.SetSourceData(chartRange, misValue);


            // chartPage.set_HasAxis(chartRangeX);

            //chartPage.HasLegend = false;
            //chartPage.HasTitle = true;

            //chartPage.ChartTitle.Text = "Tytul wykresu";



            xlWorkBook.SaveAs("\\\\dsview.pcoip.ki.agh.edu.pl\\Biblioteki-Pracownicy$\\szsz\\Desktop\\TEST1.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            MessageBox.Show("Excel file created , you can find the file d:\\csharp-Excel.xls");



        }

        private static void CreateBoxCart(Excel.Worksheet targetSheet, Excel.Range xValues,
                                           String kwartyl1Desc, Excel.Range kwartyl1Values,
                                           String medianaDesc, Excel.Range medianaValues,
                                           String kwartyl3Desc, Excel.Range kwartyl3Values,
                                           Excel.Range ErrorMaksimum, Excel.Range ErrorMadiana, Excel.Range ErrorMinimum)
        {
            Excel.ChartObjects xlCharts = (Excel.ChartObjects)targetSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(10, 100, 300, 250);
            Excel.Chart chartPage = myChart.Chart;
            chartPage.HasLegend = false;
            chartPage.HasTitle = true;

            SeriesCollection seriesCollection = chartPage.SeriesCollection();

            Series series1 = seriesCollection.NewSeries();
            series1.Name = kwartyl1Desc;
            series1.XValues = xValues;
            series1.Values = kwartyl1Values;
            series1.HasErrorBars = true;
            series1.ErrorBar(XlErrorBarDirection.xlY, XlErrorBarInclude.xlErrorBarIncludeMinusValues, XlErrorBarType.xlErrorBarTypeCustom, ErrorMinimum, ErrorMinimum);
            series1.Format.Fill.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;

            series1 = seriesCollection.NewSeries();
            series1.Name = medianaDesc;
            series1.XValues = xValues;
            series1.Values = medianaValues;

            //series1.Format.Line.Parent

            series1.Format.Line.Weight = 2.0F;
            series1.Format.Line.Visible = Microsoft.Office.Core.MsoTriState.msoTriStateMixed;  //Tri-State 
            series1.Format.Line.ForeColor.RGB = (int)Microsoft.Office.Interop.Excel.XlRgbColor.rgbBlack;

            //series1.Format.Line.ForeColor.RGB = (int)XlRgbColor.rgbBlack;
            //series1.Format.Line.Weight = 5;

            series1 = seriesCollection.NewSeries();
            series1.Name = kwartyl3Desc;
            series1.XValues = xValues;
            series1.Values = kwartyl3Values;
            series1.Format.Line.Weight = 2.0F;
            series1.Format.Line.Visible = Microsoft.Office.Core.MsoTriState.msoTriStateMixed;  //Tri-State 
            series1.Format.Line.ForeColor.RGB = (int)Microsoft.Office.Interop.Excel.XlRgbColor.rgbBlack;
            series1.HasErrorBars = true;
            series1.ErrorBar(XlErrorBarDirection.xlY, XlErrorBarInclude.xlErrorBarIncludePlusValues, XlErrorBarType.xlErrorBarTypeCustom, ErrorMaksimum, ErrorMaksimum);


            chartPage.ChartType = Excel.XlChartType.xlColumnStacked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MapItem[] mapList = SQL.DataProviderExport.GetExportMapList();

            foreach (var map in mapList)
            {
                ConfigItem[] itemConfigList = SQL.DataProviderExport.GetExportConfigList(map);
                DataSet ds;

                ExcelExporter export = new ExcelExporter();

                foreach (var item in itemConfigList)
                {
                    ds = SQL.DataProviderExport.GetExportResult(item.ConfigID);
                    //export.ExportDate(item.Name, ds); //tworzy klasycznego exela z wykresem
                    export.ExportDateUsingR(item.Name, ds); //tworzy exela oraz dodaje wykres z R

                }

                string sOutputDir =  string.Format("{0}\\ExcelExport", Environment.CurrentDirectory);

                if(!Directory.Exists(sOutputDir))
                    Directory.CreateDirectory(sOutputDir);

                export.Save(string.Format("{1}\\{0}.xls", map.MapName, sOutputDir));

                //export.Save(string.Format("\\\\dsview.pcoip.ki.agh.edu.pl\\Biblioteki-Pracownicy$\\szsz\\Desktop\\{0}.xls", map.MapName));
                //export.Save(string.Format("D:\\Desktop\\{0}.xls", map.MapName));

                //export.Save("D:\\Desktop\\csharp-Excel12.xls");
            }

            MessageBox.Show(this, "Excel file created , you can find the file");
        }




        private void button3_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);






            //add data 
            //xlWorkSheet.Cells[1, 1] = "";
            //xlWorkSheet.Cells[1, 2] = "Student1";
            //xlWorkSheet.Cells[1, 3] = "Student2";
            //xlWorkSheet.Cells[1, 4] = "Student3";

            //xlWorkSheet.Cells[2, 1] = "Term1";
            //xlWorkSheet.Cells[2, 2] = "80";
            //xlWorkSheet.Cells[2, 3] = "65";
            //xlWorkSheet.Cells[2, 4] = "45";

            //xlWorkSheet.Cells[3, 1] = "Term2";
            //xlWorkSheet.Cells[3, 2] = "78";
            //xlWorkSheet.Cells[3, 3] = "72";
            //xlWorkSheet.Cells[3, 4] = "60";

            //xlWorkSheet.Cells[4, 1] = "Term3";
            //xlWorkSheet.Cells[4, 2] = "82";
            //xlWorkSheet.Cells[4, 3] = "80";
            //xlWorkSheet.Cells[4, 4] = "65";

            //xlWorkSheet.Cells[5, 1] = "Term4";
            //xlWorkSheet.Cells[5, 2] = "75";
            //xlWorkSheet.Cells[5, 3] = "82";
            //xlWorkSheet.Cells[5, 4] = "68";

            Excel.Range chartRange;

            Excel.ChartObjects xlCharts = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(10, 80, 300, 250);
            Excel.Chart chartPage = myChart.Chart;

            chartRange = xlWorkSheet.get_Range("A1", "d5");
            chartPage.SetSourceData(chartRange, misValue);
            chartPage.ChartType = Excel.XlChartType.xl3DArea;

            xlWorkBook.SaveAs("\\\\dsview.pcoip.ki.agh.edu.pl\\Biblioteki-Pracownicy$\\szsz\\Desktop\\csharp-Excel.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            MessageBox.Show("Excel file created , you can find the file c:\\csharp.net-informations.xls");

        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }



        static void SetCellValue(Worksheet targetSheet, string Cell, object Value)
        {
            targetSheet.get_Range(
                Cell, Cell).set_Value(XlRangeValueDataType.xlRangeValueDefault,
                Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ExcelExporter export = new ExcelExporter();
            DataSet ds;


            ds = SQL.DataProviderExport.GetExportResult(310);
            export.ExportDate("nazwaTestowa", ds);

            export.Save(@"D:\Desktop\Robot AGH\Tools\ExcelTest\csharp-Excel12.xls");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Export danych symulacyjnych
            ExportSimulationDataPDF();

            //Export danych roboty
            ExportRobotDataPDF();
        }


        private void ExportSimulationDataPDF()
        {
            SQL.ConnectionString = @"data source=WR-7-BASE-74\SQLEXPRESS;initial catalog=DoktoratSymulacja;Integrated Security=SSPI;";

            MapItem[] mapList = SQL.DataProviderExport.GetExportMapList();
            string sTempInputDir = string.Format("{0}\\TempInput", Environment.CurrentDirectory);
            string sOutputDir = string.Format("{0}\\Output", Environment.CurrentDirectory);


            foreach (var map in mapList)
            {
                ConfigItem[] itemConfigList = SQL.DataProviderExport.GetExportConfigList(map);
                DataSet ds;
                List<string> pdfsFile = new List<string>();
                List<string> dunnTest = new List<string>();
                List<string> kwTest = new List<string>();
                List<string> LatexImageDescriptions = new List<string>();

                StringBuilder sLatexFile = new StringBuilder();

                double[] prawodpodobienstwMinimalnea = new double[] { 0, 3.8415, 5.9915, 7.8147, 9.4877, 11.0705, 12.5916 };
                double alfa = 0.05;
                string sCombineNamePDF = "Simulation " + map.MapName + ".pdf";
                string sCombineNameTex = "Simulation " + map.MapName + ".tex";

                sCombineNamePDF = sCombineNamePDF.Replace(" ", "_"); //usuniecie spacje w nazwie pliku wynikowym pdf
                sCombineNameTex = sCombineNameTex.Replace(" ", "_"); //usuniecie spcaji w nazwie poliku wynikowego tex

                if (Directory.Exists(sTempInputDir))
                {
                    Directory.Delete(sTempInputDir, true);
                    Directory.CreateDirectory(sTempInputDir);
                }
                else
                    Directory.CreateDirectory(sTempInputDir);


                if (!Directory.Exists(sOutputDir))
                    Directory.CreateDirectory(sOutputDir);


                for (int i = 0; i < itemConfigList.Length;i++)
                {
                    string sOutputPdfFile = string.Format("{0}\\{1}.pdf", sTempInputDir, i.ToString());
                    string sChartTitel = string.Format("{0}", itemConfigList[i].Name); // ""; //Gdy zajedzie potrzeba to nadamy w tym mijscy nazwy wykresu 
                    string sLatexImageDescriptions = string.Format("Eksperymenty symulacyjne: Mapa {0} Konfiguracja: {1}", map.MapName, itemConfigList[i].Name);

                    ds = SQL.DataProviderExport.GetExportResult(itemConfigList[i].ConfigID);

                    ds = removeUnnecessaryData(ds,map.ID_Map);

                    RExporter r = new RExporter();

                    RExporterResult chartAndTestResult = r.GetChartPDFTest(ds, sChartTitel, map.ID_Map);

                    File.Move(chartAndTestResult.ChartPath, sOutputPdfFile);

                    pdfsFile.Add(sOutputPdfFile);

                    int df = chartAndTestResult.GetKwTestDF();
                    double kw = chartAndTestResult.GetKwchiSquared();

                    string outLatex = formatLatexFile(sChartTitel, string.Format("img/{0}", sCombineNamePDF),i,alfa, prawodpodobienstwMinimalnea[df],df,kw);
                    sLatexFile.Append(outLatex);
                }

                concatAndAddContent(pdfsFile, string.Format("{0}\\{1}", sOutputDir, sCombineNamePDF));
                File.AppendAllText(string.Format("{0}\\{1}", sOutputDir, sCombineNameTex), sLatexFile.ToString());
            }

            MessageBox.Show(this, "Excel file created , you can find the file");
        }

        private void ExportRobotDataPDF()
        {
            SQL.ConnectionString = @"data source=WR-7-BASE-74\SQLEXPRESS;initial catalog=DoktoratRobot;Integrated Security=SSPI;";

            MapItem[] mapList = SQL.DataProviderExport.GetExportMapList();
            string sTempInputDir = string.Format("{0}\\TempInput", Environment.CurrentDirectory);
            string sOutputDir = string.Format("{0}\\Output", Environment.CurrentDirectory);



            foreach (var map in mapList)
            {
                ConfigItem[] itemConfigList = SQL.DataProviderExport.GetExportConfigList(map);
                DataSet ds;
                List<string> pdfsFile = new List<string>();
                List<string> dunnTest = new List<string>();
                List<string> kwTest = new List<string>();
                List<string> LatexImageDescriptions = new List<string>();

                StringBuilder sLatexFile = new StringBuilder();

                double[] prawodpodobienstwMinimalnea = new double[] { 0, 3.8415, 5.9915, 7.8147, 9.4877, 11.0705, 12.5916 };
                double alfa = 0.05;
                string sCombineNamePDF = "Robots " + map.MapName + ".pdf";
                string sCombineNameTex = "Robots " + map.MapName + ".tex";

                sCombineNamePDF = sCombineNamePDF.Replace(" ", "_"); //usuniecie spacje w nazwie pliku wynikowym pdf
                sCombineNameTex = sCombineNameTex.Replace(" ", "_"); //usuniecie spcaji w nazwie poliku wynikowego tex

                if (Directory.Exists(sTempInputDir))
                {
                    Directory.Delete(sTempInputDir, true);
                    Directory.CreateDirectory(sTempInputDir);
                }
                else
                    Directory.CreateDirectory(sTempInputDir);


                if (!Directory.Exists(sOutputDir))
                    Directory.CreateDirectory(sOutputDir);


                for (int i = 0; i < itemConfigList.Length; i++)
                {
                    string sOutputPdfFile = string.Format("{0}\\{1}.pdf", sTempInputDir, i.ToString());
                    string sChartTitel = string.Format("{0}", itemConfigList[i].Name); // ""; //Gdy zajedzie potrzeba to nadamy w tym mijscy nazwy wykresu 
                    string sLatexImageDescriptions = string.Format("Eksperymenty Roboty: Mapa {0} Konfiguracja: {1}", map.MapName, itemConfigList[i].Name);

                    ds = SQL.DataProviderExport.GetExportResult(itemConfigList[i].ConfigID);

                    ds = removeUnnecessaryData(ds, map.ID_Map);

                    RExporter r = new RExporter();

                    RExporterResult chartAndTestResult = r.GetChartPDFTest(ds, sChartTitel, map.ID_Map);

                    File.Move(chartAndTestResult.ChartPath, sOutputPdfFile);

                    pdfsFile.Add(sOutputPdfFile);

                    int df = chartAndTestResult.GetKwTestDF();
                    double kw = chartAndTestResult.GetKwchiSquared();

                    string outLatex = formatLatexFile(sChartTitel, string.Format("img/{0}", sCombineNamePDF), i, alfa, prawodpodobienstwMinimalnea[df], df, kw);
                    sLatexFile.Append(outLatex);
                }

                concatAndAddContent(pdfsFile, string.Format("{0}\\{1}", sOutputDir, sCombineNamePDF));
                File.AppendAllText(string.Format("{0}\\{1}", sOutputDir, sCombineNameTex), sLatexFile.ToString());
            }

            MessageBox.Show(this, "Excel file created , you can find the file");
        }

        private DataSet removeUnnecessaryData(DataSet input, int id_Map)
        {
            switch (id_Map)
            {

                case 6: //Simulation_Eight_intersectionusun PF i PF+
                case 10: //Simulation_Open_space usun PF+ i PF
                case 13: //A narrow corridor
                case 15: //SkrzyżowanieRównorzędneNowe
                case 16: // Circel
                case 18: //Passing place

                case 19: // Robots_Open_space
                case 20: //Robots_Passing_place
                case 21: //LabOtwartaPrzestrzeń 4 Roboty

                    input.Tables[0].Columns.Remove("CP");
                    input.Tables[0].Columns.Remove("CP_NEW");
                    break;
            }

            return input;
        }

      

        public string formatLatexFile(string sChartTitel,string sFileName,int iPage, double alfa, double chiCrituc, int df, double kw )
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine();
            result.AppendLine();
            result.AppendLine("\\begin{figure}[ht]");
            result.AppendLine("\t\\centering");

            result.AppendLine("\t\\includegraphics[page = " + (iPage + 1).ToString() + ", width =\\columnwidth / 2]{" + sFileName + "}");

            string kwTest = "$(\\alpha = " + alfa.ToString() + "; \\chi^{2}_{CRIT} = " + chiCrituc.ToString() + "; H^{2} = " + kw.ToString() + "; df = " + df.ToString() + ")$";

            result.AppendLine("\t\\caption{" + sChartTitel.ToString() + ". " + "\\protect\\footnotemark" + "}");

            result.AppendLine("\t\\label{fig:" + sChartTitel.ToString() + "}");
            result.AppendLine("\\end{figure}");
            result.AppendLine("\\footnotetext{" + kwTest.ToString() + "}");
            result.AppendLine();
            result.AppendLine();


            string dd = result.ToString();
            return dd;
        }

        public void concatAndAddContent(List<string> pdfsFile,string sOutputFile)
        {
            byte[] result;

            using (var ms = new MemoryStream())
            {
                using (var doc = new Document())
                {
                    using (var copy = new PdfSmartCopy(doc, ms))
                    {
                        doc.Open();

                        foreach (var item in pdfsFile)
                        {
                            using (var reader = new PdfReader(item))
                            {
                                copy.AddDocument(reader);
                            }
                        }

                        doc.Close();
                    }
                }

                result = ms.ToArray();
            }

            using (var fs = new FileStream(sOutputFile, FileMode.Create, FileAccess.Write))
            {
                fs.Write(result, 0, result.Length);
            }
        }


    }
}
