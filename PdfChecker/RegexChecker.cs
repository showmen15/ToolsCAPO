using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PdfChecker
{
    public class RegexChecker
    {
        
        Regex regCytowania = new Regex(@"\\cite\{[A-Za-z0-9,\s]+\}\s+\.");
        Regex regMyslniki = new Regex(@"(\s\-\s)");

        Regex regProcent = new Regex(@"%"); // %Jakis komentarz
        Regex regCudzyslow = new Regex(@"(''[A-Za-z0-9,\s]+'')"); //''tekst''

        Regex regCytowaniaPoprzedzoneLiterom = new Regex(@"[A-Za-z]+\\cite"); // ala\cite


        Regex regAlgorytmyLinkReplace = new Regex(@"((((\\lookupGetHref)|(\\lookupGetUrl)|(\\lookupGetHrefURL)|(\\lookupPut))\{[\w+\s-\n\.]+\}))|(\$\sR\s\$)");
        Regex regAlgorytmy = new Regex(@"(\s)((R)|(R\+)|(PF)|(PF\+)|(RVO)|(PR))((\s)|(\.))");


        Regex regJednostki = new Regex(@"\s[0-9\.]+[A-Za-z,{}]+\s"); // 20 cm itd

        public RegexChecker()
        {

        }

        public string CheckFile(string sFilePath)
        {
            StringBuilder log = new StringBuilder();
            MatchCollection matchCollection;
            string input;

            string sFileName = sFilePath; //Path.GetFileNameWithoutExtension(sFilePath);
            string[] sTextLines = File.ReadAllLines(sFilePath);

            for (int i = 0; i < sTextLines.Length; i++)
            {
                matchCollection = regCytowania.Matches(sTextLines[i]);
                if (matchCollection.Count > 0)
                    log.AppendLine(logPattern(sFileName, matchCollection, "regCytowania", i));

                matchCollection = regMyslniki.Matches(sTextLines[i]);
                if (matchCollection.Count > 0)
                    log.AppendLine(logPattern(sFileName, matchCollection, "regMyslniki", i));


                matchCollection = regProcent.Matches(sTextLines[i]);
                if (matchCollection.Count > 0)
                    log.AppendLine(logPattern(sFileName, matchCollection, "regProcent", i));


                matchCollection = regCudzyslow.Matches(sTextLines[i]);
                if (matchCollection.Count > 0)
                    log.AppendLine(logPattern(sFileName, matchCollection, "regPrzecinki", i));

                matchCollection = regCytowaniaPoprzedzoneLiterom.Matches(sTextLines[i]);
                if (matchCollection.Count > 0)
                    log.AppendLine(logPattern(sFileName, matchCollection, "regCytowaniaPoprzedzoneLiterom", i));

                input = sTextLines[i];
                input = regAlgorytmyLinkReplace.Replace(input, "REPLACE:LINK");
                matchCollection = regAlgorytmy.Matches(input);
                if (matchCollection.Count > 0)
                    log.AppendLine(logPattern(sFileName, matchCollection, "regAlgorytmy", i));


                matchCollection = regJednostki.Matches(sTextLines[i]);
                if (matchCollection.Count > 0)
                    log.AppendLine(logPattern(sFileName, matchCollection, "regJednostki", i));

            }

            return log.ToString();
        }

        private string logPattern(string sFileName, MatchCollection matchCollection, string sPattern, int lineNr)
        {
            return string.Format("({0}: {1}) ==> {2}", sFileName, lineNr, sPattern);
        }
    }
}
