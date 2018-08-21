using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PdfChecker
{
    public class PdftoText : IDisposable
    {
      //  https://www.systutorials.com/docs/linux/man/1-pdftotext/

        Regex regSingleLetterEndLine = new Regex(@"\s.$");

        private string FileName;

        public PdftoText(string sFileName)
        {
            FileName = sFileName;
        }

        public void Dispose()
        {

        }

        public string CheckPDF(int begin, int end)
        {
            string result = string.Empty;

            string sTempDirectory = string.Format("{0}\\TempPDFPage", Environment.CurrentDirectory);

            if (Directory.Exists(sTempDirectory))
                Directory.Delete(sTempDirectory, true);

            System.Threading.Thread.Sleep(1000);

            Directory.CreateDirectory(sTempDirectory);

            runCheckPDF(sTempDirectory, begin, end);

            result = checkPDF(sTempDirectory, begin, end);

            /*
            List<int> pageList = new List<int>();

            for (int page = begin; page <= end; page++)
                pageList.Add(page);

            return checkPDF(pageList.ToArray());*/

            return result;
        }

        private void runCheckPDF(string sTempDirectory, int begin, int end)
        {
            string sPdfToTextLocation = string.Format("{0}\\Tools\\pdftotext.exe", Environment.CurrentDirectory);

            Parallel.For(begin, end,
                   index =>
                   {
                       string sTempFile = string.Format("{0}\\{1}.txt", sTempDirectory, index + 1);


                       Process process = new Process();
                       process.StartInfo.FileName = sPdfToTextLocation;
                       process.StartInfo.Arguments = string.Format(" -enc UTF-8 -layout -f {0} -l {0} \"{1}\" \"{2}\"", index + 1, FileName, sTempFile);

                       process.StartInfo.UseShellExecute = false;
                       process.StartInfo.CreateNoWindow = true;
                       // process.StartInfo.RedirectStandardOutput = true;
                       // process.StartInfo.RedirectStandardError = true;
                       process.Start();

                       try
                       {
                           if (!process.WaitForExit(10000))
                               process.Kill();
                       }
                       catch (Exception)
                       {
                           clearProcess(process);
                       }

                   });
        }

        private void clearProcess(Process process)
        {
            try
            {
                if (process != null)
                {
                    process.Kill();
                    process = null;
                }
            }
            catch
            {
                process = null;
            }
        }


        private string checkPDF(string sTempDirectory, int begin, int end)
        {
            StringBuilder log = new StringBuilder();
            MatchCollection matchCollection;
            string input;

            for (int PdfPageIndex = begin; PdfPageIndex < end; PdfPageIndex++)
            {
                string sCurrentFileName = string.Format("{0}\\{1}.txt", sTempDirectory, PdfPageIndex + 1);

                string[] sFileConntent = File.ReadAllLines(sCurrentFileName);

                for (int i = 0; i < sFileConntent.Length; i++)
                {
                    matchCollection = regSingleLetterEndLine.Matches(sFileConntent[i]);
                    if (matchCollection.Count > 0)
                        log.AppendLine(logPattern(PdfPageIndex + 1, matchCollection, "regPojedynczeLiterki", i, sFileConntent[i]));
                }
            }

            return log.ToString();
        }


        private string logPattern(int sPageNumber, MatchCollection matchCollection, string sPattern, int lineNr, string sLine)
        {
            return string.Format("({0}: {1}) ==> {2} ->> ({3})", sPageNumber, lineNr, sPattern, sLine);
        }

        //public string CheckFile(string sFilePath)
        //{


        //    string sFileName = sFilePath; //Path.GetFileNameWithoutExtension(sFilePath);
        //    string[] sTextLines = File.ReadAllLines(sFilePath);


        //}


        //private string regexAnalizer(int pdfPageIndex, string sFileConntent, string regexPattern)
        //{

        //    if(Regex.IsMatch(sFileConntent, regexPattern))
        //    {

        //    }

        //}

        //private string logLine(int index, int lineID, string text)
        //{
        //    return string.Format("{0} ({2}) -> {1}", index, text, lineID);
        //}

    }
}
