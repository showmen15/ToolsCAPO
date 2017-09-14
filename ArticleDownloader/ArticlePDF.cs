using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ArticleDownloader
{
    public class ArticlePDF
    {
        public string TitelFileName { get; set; }

        public string DirectoryFile { get; set; }
        public string Url { get; set; }

        public string FilePath
        {
            get
            {
                return string.Format("{0}{1}.pdf", DirectoryFile, TitelFileName);
            }
        }

        public  ArticlePDF(string sDirectory)
        {
            DirectoryFile = sDirectory;
        }

        public ArticlePDF(string sTitelFileName, string sDirectory) : this(sDirectory)
        {        
            TitelFileName = sTitelFileName.Replace("\\", string.Empty).Replace("/", string.Empty).Replace(":", string.Empty)
                                           .Replace("?",string.Empty).Replace("\"",string.Empty).Replace("<", string.Empty).Replace(">", string.Empty).Replace("|", string.Empty)
                                           .Replace("-", string.Empty).Replace("“", string.Empty).Replace("”",string.Empty);
        }
        public ArticlePDF(string sTitelFileName, string sDirectory, string sUrl) : this(sTitelFileName, sDirectory)
        {
            Url = sUrl;
        }
    }
}
