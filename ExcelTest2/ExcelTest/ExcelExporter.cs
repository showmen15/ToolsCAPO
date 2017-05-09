using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office;

using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace ExcelTest
{

    // http://csharp.net-informations.com/excel/csharp-create-excel.htm
    //https://support.office.com/en-US/article/PERCENTILE-function-91B43A53-543C-4708-93DE-D626DEBDDDCA
    //https://msdn.microsoft.com/en-us/library/bb404904(v=office.12).aspx

    //tworzenie wykresu Box w Execelu
    //https://www.youtube.com/watch?v=ucWmfmXb1kk
    public class ExcelExporter
    {
        private string[] ExcelColumnFormater = new string[] { "A", "B", "C", "D", "E" };

        private string[] MathodColumnName = new string[] { "WR", "CP", "RVO", "PD" };
        
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        
        Excel.Worksheet xlWorkSheet;
        
        object misValue;

        public ExcelExporter()
        {
            misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
        }

        ~ExcelExporter()
        {
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        /// <summary>
        /// Tworzy klasycznego exela z wykresem
        /// </summary>
        public void ExportDate(string sheetName, DataSet ds)
        {
            Excel.Worksheet xlWorkSheet = createNewSheet(sheetName);
            
            exportRowDate(xlWorkSheet,ds);
            insertSummary(xlWorkSheet,ds);
            insertChartBoxValue(xlWorkSheet);
            createNewChart(xlWorkSheet);
        }

        /// <summary>
        /// Tworzy exela oraz dodaje wykres z R
        /// </summary>
        public void ExportDateUsingR(string sheetName, DataSet ds)
        {
            RExporter r = new RExporter();

            //string sJPGPath = string.Format("{0}\\chart.jpg", Environment.CurrentDirectory);
            //string sTestDunnResult = string.Format("{0}\\dunnTest.txt", Environment.CurrentDirectory);
            //string sTestKwResult = string.Format("{0}\\KwTest.txt", Environment.CurrentDirectory);

            //r.GetJPGChart(ds, sJPGPath, sTestDunnResult, sTestKwResult);

            RExporterResult chartAndTestResult = r.GetChartTest(ds);

            Excel.Worksheet xlWorkSheet = createNewSheet(sheetName);
            exportRowDate(xlWorkSheet, ds);
            insertSummary(xlWorkSheet,ds);
            insertChartImage(xlWorkSheet, chartAndTestResult.ChartPath);
            insertTestResult(xlWorkSheet, chartAndTestResult);
        }

        private void insertTestResult(Excel.Worksheet xlWorkSheet, RExporterResult chartAndTestResult)
        {
            string[] testResult = chartAndTestResult.GetSplitTests;

            if (testResult.Length > 0)
            {
                for (int i = 0; i < testResult.Length; i++)
                    SetCellValue(xlWorkSheet, string.Format("N{0}", i + 1), testResult[i]);

                //czcionka powinna być "Curier New"

                Excel.Range formatRange;
                formatRange = xlWorkSheet.get_Range("N1", string.Format("N{0}", testResult.Length));
                formatRange.Font.Name = "Courier New";
                // formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                //formatRange.Font.Size = 10;
                //xlWorkSheet.Cells[1, 2] = "Red";
            }
        }

        private void insertChartImage(Excel.Worksheet xlWorkSheet, string sJPGPath)
        {
            xlWorkSheet.Shapes.AddPicture(sJPGPath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 400, 100, 300, 250);
        }

        private void insertSummary(Excel.Worksheet xlWorkSheet, DataSet ds)
        {
            SetCellValue(xlWorkSheet, "A1", "Metoda");

            int rowBeginIndex = 1;
            //column name
            for (int i = 1; i < ds.Tables[0].Columns.Count; i++)
                xlWorkSheet.Cells[rowBeginIndex, i + 1] = ds.Tables[0].Columns[i].Caption;

         /*   SetCellValue(xlWorkSheet, "B1", MathodColumnName[0]);
            SetCellValue(xlWorkSheet, "C1", MathodColumnName[1]);
            SetCellValue(xlWorkSheet, "D1", MathodColumnName[2]);
            SetCellValue(xlWorkSheet, "E1", MathodColumnName[3]);*/

            SetCellValue(xlWorkSheet, "A2", "Minimum");
            SetCellValue(xlWorkSheet, "A3", "Kwartyl1");
            SetCellValue(xlWorkSheet, "A4", "Mediana");
            SetCellValue(xlWorkSheet, "A5", "Kwartyl3");
            SetCellValue(xlWorkSheet, "A6", "Maksimum");

            //mminimum
            SetCellValue(xlWorkSheet, "B2","=MIN(B15:B44)");
            SetCellValue(xlWorkSheet, "C2", "=MIN(C15:C44)");
            SetCellValue(xlWorkSheet, "D2", "=MIN(D15:D44)");
            SetCellValue(xlWorkSheet, "E2", "=MIN(E15:E44)");

            SetCellValue(xlWorkSheet, "F2", "=MIN(F15:F44)");
            SetCellValue(xlWorkSheet, "G2", "=MIN(G15:G44)");

            //kwartyl 1
            SetCellValue(xlWorkSheet, "B3", "=QUARTILE(B15:B44,1)");
            SetCellValue(xlWorkSheet, "C3", "=QUARTILE(C15:C44,1)");
            SetCellValue(xlWorkSheet, "D3", "=QUARTILE(D15:D44,1)");
            SetCellValue(xlWorkSheet, "E3", "=QUARTILE(E15:E44,1)");

            SetCellValue(xlWorkSheet, "F3", "=QUARTILE(F15:F44,1)");
            SetCellValue(xlWorkSheet, "G3", "=QUARTILE(G15:G44,1)");

            //mediana
            SetCellValue(xlWorkSheet, "B4", "=MEDIAN(B15:B44)");
            SetCellValue(xlWorkSheet, "C4", "=MEDIAN(C15:C44)");
            SetCellValue(xlWorkSheet, "D4", "=MEDIAN(D15:D44)");
            SetCellValue(xlWorkSheet, "E4", "=MEDIAN(E15:E44)");

            SetCellValue(xlWorkSheet, "F4", "=MEDIAN(F15:F44)");
            SetCellValue(xlWorkSheet, "G4", "=MEDIAN(G15:G44)");

            //kwartyl 3
            SetCellValue(xlWorkSheet, "B5", "=QUARTILE(B15:B44,3)");
            SetCellValue(xlWorkSheet, "C5", "=QUARTILE(C15:C44,3)");
            SetCellValue(xlWorkSheet, "D5", "=QUARTILE(D15:D44,3)");
            SetCellValue(xlWorkSheet, "E5", "=QUARTILE(E15:E44,3)");

            SetCellValue(xlWorkSheet, "F5", "=QUARTILE(F15:F44,3)");
            SetCellValue(xlWorkSheet, "G5", "=QUARTILE(G15:G44,3)");

            //maksimum
            SetCellValue(xlWorkSheet, "B6", "=MAX(B15:B44)");
            SetCellValue(xlWorkSheet, "C6", "=MAX(C15:C44)");
            SetCellValue(xlWorkSheet, "D6", "=MAX(D15:D44)");
            SetCellValue(xlWorkSheet, "E6", "=MAX(E15:E44)");

            SetCellValue(xlWorkSheet, "F6", "=MAX(F15:F44)");
            SetCellValue(xlWorkSheet, "G6", "=MAX(G15:G44)");

        }

        private void exportRowDate(Excel.Worksheet xlWorkSheet, DataSet ds)
        {
            int rowBeginIndex = 14;
            string data = null;

            //column name
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                xlWorkSheet.Cells[rowBeginIndex, i + 1] = ds.Tables[0].Columns[i].Caption;

            rowBeginIndex = 15;

            /*SetCellValue(xlWorkSheet, "A14", "Lp.");
            SetCellValue(xlWorkSheet, "B14", MathodColumnName[0]);
            SetCellValue(xlWorkSheet, "C14", MathodColumnName[1]);
            SetCellValue(xlWorkSheet, "D14", MathodColumnName[2]);
            SetCellValue(xlWorkSheet, "E14", MathodColumnName[3]);*/

            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                for (int j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                {
                    data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                    xlWorkSheet.Cells[i + rowBeginIndex, j + 1] = data;
                }
            }
        }

        private Excel.Worksheet createNewSheet(string sheetName)
        {
            var xlNewSheet = (Excel.Worksheet)xlWorkBook.Worksheets.Add(xlWorkBook.Worksheets[1], Type.Missing, Type.Missing, Type.Missing);
            xlNewSheet.Name = sheetName;

            return xlNewSheet;
        }

        private void createNewChart(Excel.Worksheet targetSheet)
        {
            String kwartyl1Desc, medianaDesc, kwartyl3Desc;
            
            Excel.Range ErrorMaksimum, ErrorMadiana, ErrorMinimum;
            Excel.Range xValues;

            Excel.Range kwartyl1Values, medianaValues, kwartyl3Values;

            xValues = targetSheet.get_Range("B1", "E1");

            ErrorMaksimum = targetSheet.get_Range("B11", "E11");
            ErrorMadiana = targetSheet.get_Range("B1", "E1");
            ErrorMinimum = targetSheet.get_Range("B12", "E12");

            kwartyl1Desc = "Box1";
            kwartyl1Values = targetSheet.get_Range("B8", "E8");

            medianaDesc = "Box2";
            medianaValues = targetSheet.get_Range("B9", "E9");

            kwartyl3Desc = "Box3";
            kwartyl3Values = targetSheet.get_Range("B10", "E10");

            createBoxCart(targetSheet, xValues, kwartyl1Desc, kwartyl1Values, medianaDesc, medianaValues, kwartyl3Desc, kwartyl3Values, ErrorMaksimum, ErrorMadiana, ErrorMinimum);
        }

        private static void createBoxCart(Excel.Worksheet targetSheet, Excel.Range xValues,
                                         String kwartyl1Desc, Excel.Range kwartyl1Values,
                                         String medianaDesc, Excel.Range medianaValues,
                                         String kwartyl3Desc, Excel.Range kwartyl3Values,
                                         Excel.Range ErrorMaksimum, Excel.Range ErrorMadiana, Excel.Range ErrorMinimum)
        {
            Excel.ChartObjects xlCharts = (Excel.ChartObjects)targetSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(300, 100, 300, 250);
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

            series1 = seriesCollection.NewSeries();
            series1.Name = kwartyl3Desc;
            series1.XValues = xValues;
            series1.Values = kwartyl3Values;
            series1.HasErrorBars = true;
            series1.ErrorBar(XlErrorBarDirection.xlY, XlErrorBarInclude.xlErrorBarIncludePlusValues, XlErrorBarType.xlErrorBarTypeCustom, ErrorMaksimum, ErrorMaksimum);

            chartPage.ChartType = Excel.XlChartType.xlColumnStacked;
        }


        private void insertChartBoxValue(Excel.Worksheet xlWorkSheet)
        {
            SetCellValue(xlWorkSheet, "A8", "Box 1");
            SetCellValue(xlWorkSheet, "A9", "Box 2");
            SetCellValue(xlWorkSheet, "A10", "Box 3");
            SetCellValue(xlWorkSheet, "A11", "Error plus");
            SetCellValue(xlWorkSheet, "A12", "Error minus");


            SetCellValue(xlWorkSheet, "B8", "=B3");
            SetCellValue(xlWorkSheet, "B9", "=B4-B3");
            SetCellValue(xlWorkSheet, "B10", "=B5-B4");
            SetCellValue(xlWorkSheet, "B11", "=B6-B5");
            SetCellValue(xlWorkSheet, "B12", "=B3-B2");

            SetCellValue(xlWorkSheet, "C8", "=C3");
            SetCellValue(xlWorkSheet, "C9", "=C4-C3");
            SetCellValue(xlWorkSheet, "C10", "=C5-C4");
            SetCellValue(xlWorkSheet, "C11", "=C6-C5");
            SetCellValue(xlWorkSheet, "C12", "=C3-C2");

            SetCellValue(xlWorkSheet, "D8", "=D3");
            SetCellValue(xlWorkSheet, "D9", "=D4-D3");
            SetCellValue(xlWorkSheet, "D10", "=D5-D4");
            SetCellValue(xlWorkSheet, "D11", "=D6-D5");
            SetCellValue(xlWorkSheet, "D12", "=D3-D2");

            SetCellValue(xlWorkSheet, "E8", "=E3");
            SetCellValue(xlWorkSheet, "E9", "=E4-E3");
            SetCellValue(xlWorkSheet, "E10", "=E5-E4");
            SetCellValue(xlWorkSheet, "E11", "=E6-E5");
            SetCellValue(xlWorkSheet, "E12", "=E3-E2");

        }


        public void Save(string filePath)
        {
            xlWorkBook.SaveAs(filePath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();        
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
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

        private static void SetCellValue(Worksheet targetSheet, string Cell, object Value)
        {
            targetSheet.get_Range(Cell, Cell).set_Value(XlRangeValueDataType.xlRangeValueDefault,Value);
        }
      

    }
}
