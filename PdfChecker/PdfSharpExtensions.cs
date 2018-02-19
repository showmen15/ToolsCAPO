using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;
using System.Text.RegularExpressions;

namespace PdfChecker
{
    //https://stackoverflow.com/questions/2550796/reading-pdf-content-with-itextsharp-dll-in-vb-net-or-c-sharp

    public class PdfTextSharpChecker
    {

        PdfReader pdfReader;
      //  ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();

        public PdfTextSharpChecker(string fileName)
        {
            pdfReader = new PdfReader(fileName);
         //   strategy = new SimpleTextExtractionStrategy();
        }


        private string getFormPage(int index)
        {
            string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, index);

            return currentText;
        }

        public string CheckPDF(int begin, int  end)
        {
            List<int> pageList = new List<int>();

            for (int page = begin; page <= end; page++)
                pageList.Add(page);

            return checkPDF(pageList.ToArray());
        }

        public string CheckPDF()
        {
            return CheckPDF(1, pdfReader.NumberOfPages);
        }

        private string checkPDF(int [] pageList)
        {
            StringBuilder result = new StringBuilder();

            //for (int page = 1; page <= pdfReader.NumberOfPages; page++)
            for(int i =0; i < pageList.Length; i++)
            {
                string text = getFormPage(pageList[i]);
                string[] linses = text.Split(new string[] { "\n" }, StringSplitOptions.None);

                for(int j = 0; j < linses.Length; j++)
                { 
                    bool isOk = checkTest(linses[j]);

                    if(!isOk)
                        result.AppendLine(logLine(pageList[i], j, linses[j]));
                }
            }

            return result.ToString();
        }

        private string logLine(int index, int lineID, string text)
        {
            return string.Format("{0} ({2}) -> {1}", index, text, lineID);
        }

        public bool checkTest(string text)
        {
            if (text.Length >= 3)
            {
                string beforeLastLetter = text.Substring(text.Length - 2, 1);

                if (beforeLastLetter == " ")
                    return false;
                else
                    return true;

            }
            else
                return true;
        }
    }
}
