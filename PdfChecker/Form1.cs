using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filename = "..\\DocPublish.pdf"; //@"C:\testDoc\docpublish.pdf";

            using (PdfTextSharpChecker pdf = new PdfTextSharpChecker(filename))
            {
                string logText = pdf.CheckPDF();
                txtLog.Text = logText;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filename = "..\\DocPublish.pdf"; //@"C:\testDoc\docpublish.pdf";

            using (PdfTextSharpChecker pdf = new PdfTextSharpChecker(filename))
            {
                string logText = pdf.CheckPDF(Convert.ToInt32(numBegin.Value), Convert.ToInt32(numEnd.Value));
                txtLog.Text = logText;
            }


            // string filename = @"C:\testDoc\docpublish.pdf";

            /*   using (OpenFileDialog dlg = new OpenFileDialog())
               {
                   if (dlg.ShowDialog(this) == DialogResult.OK)
                   {
                       PdfTextSharpChecker pdf = new PdfTextSharpChecker(dlg.FileName);

                       string logText = pdf.CheckPDF(Convert.ToInt32(numBegin.Value), Convert.ToInt32(numEnd.Value));
                       txtLog.Text = logText;
                   }
               }*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sTextFileDirectory = @"J:\Doktorat\DocPublish";

            RegexChecker reg = new RegexChecker();
            StringBuilder Log = new StringBuilder();

            List<string> getAllTexFile = new List<string>(Directory.GetFiles(sTextFileDirectory, "*.tex", SearchOption.AllDirectories));
            List<string> getOldTexFile = new List<string>(Directory.GetFiles(sTextFileDirectory, "*_old.tex", SearchOption.AllDirectories));

            //wykluczenia ze sprawdzania 
            getOldTexFile.Add(string.Format("{0}\\{1}", sTextFileDirectory, @"lookup\lookup.tex")); //ze wzgledu na linki
            getOldTexFile.Add(string.Format("{0}\\{1}", sTextFileDirectory, @"06_experimental_results\simulation\experimental-result-simulation-table.tex")); //ze wzgledu na tabele
            getOldTexFile.Add(string.Format("{0}\\{1}", sTextFileDirectory, @"06_experimental_results\robot\experimental-result-robot-table.tex")); //ze wzgledu na tabele

            getOldTexFile.Add(string.Format("{0}\\{1}", sTextFileDirectory, @"05_hybrid_algorithm\FearFactorBase.tex")); //ze wzgledu na wzory
            getOldTexFile.Add(string.Format("{0}\\{1}", sTextFileDirectory, @"05_hybrid_algorithm\FactorPassageBy.tex")); //ze wzgledu na wzory


            foreach (var item in getOldTexFile)      //usun old oraz  wykluczone
                getAllTexFile.Remove(item);

            // string[] sFileList = new string[] { @"J:\Doktorat\DocPublish\01_introduction\introduction_old.tex" }; //getAllTexFile.ToArray(); 
            string[] sFileList = getAllTexFile.ToArray(); 

            foreach (var item in sFileList)            
                Log.AppendLine(reg.CheckFile(item));

            txtRegexLog.AppendText(Log.ToString());

        }

            ///*string sTestedLog;
            //string[] sTestedTexLines;

            //string sPatternCytowania = @"(\s\-\s)";
            ////Regex reg = Regex.

            //foreach (var file in sFileList)
            //{
            //    //sTestedLog = File.ReadAllText(file);

            //    sTestedTexLines = File.ReadAllLines(file);

            //    for (int i = 0; i < sTestedTexLines.Length; i++)
            //    {
            //        MatchCollection matchCollection = Regex.Matches(sTestedTexLines[i], sPatternCytowania);

            //        foreach (var match in matchCollection)
            //        {

            //        }
            //    }

               

            //    //MatchCollection matchCollection =  Regex.Matches(sTestedLog, sPatternCytowania);

            //    //foreach (var match in matchCollection)
            //    /*{

            //    }*/

            //}


       

       

    }
}
