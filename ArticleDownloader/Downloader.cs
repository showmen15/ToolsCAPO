using BookmarksManager;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDownloader
{
    public class Downloader
    {
        private string OutputDirectory;

        private ConcurrentBag<string> Log;

        public Downloader(string sOutputDirectory)
        {
            OutputDirectory = sOutputDirectory;
            Log = new ConcurrentBag<string>();
        }

        private List<ArticlePDF> getDirectoryFileList()
        {
            List<ArticlePDF> lista = new List<ArticlePDF>();

            string[] entries = Directory.GetFileSystemEntries(OutputDirectory, "*", SearchOption.AllDirectories);
            string sFileName;
            string sDirectoryPah;

            foreach (var item in entries)
            {
                FileAttributes attr = File.GetAttributes(item);

                if (!attr.HasFlag(FileAttributes.Directory))
                {
                    sFileName = Path.GetFileNameWithoutExtension(item);
                    sDirectoryPah = Path.GetDirectoryName(item) + "\\";

                    lista.Add(new ArticlePDF(sFileName, sDirectoryPah));
                }
            }

            return lista;
        }
        private List<ArticlePDF> getBookMark(string sBookmarkFilePath)
        {
            List<ArticlePDF> lista = new List<ArticlePDF>();

            string bookmarksString = File.ReadAllText(sBookmarkFilePath);
            NetscapeBookmarksReader reader = new NetscapeBookmarksReader();
            BookmarkFolder bookmarksAll = reader.Read(bookmarksString);

            string rootFolder = "Doktorat";
            IBookmarkFolder bookmarksDoc = getDoktoratFolder(bookmarksAll, rootFolder); //pobierz wszystkie z bookmark Doktorat
            getArticleFromFolder(lista, "", bookmarksDoc);

            return lista;
        }

        private IBookmarkFolder getDoktoratFolder(BookmarkFolder bookmarksAll, string findPattern)
        {
            IBookmarkFolder result = null;

            foreach (var item in bookmarksAll.AllFolders)
            {
                if (item.Title == findPattern)
                {
                    result = item;
                    break;
                }
            }

            return result;
        }

        private void getArticleFromFolder(List<ArticlePDF> lista, string rootDirecory, IBookmarkFolder bookmarks)
        {
            rootDirecory += string.Format("{0}\\", bookmarks.Title);

            foreach (var item in bookmarks)
            {
                if (item is BookmarkLink)
                {
                    BookmarkLink link = item as BookmarkLink;
                    string extansion = link.Url.Substring(link.Url.Length - 4, 4);

                    //if (extansion.ToLower() == ".pdf")
                    lista.Add(new ArticlePDF(link.Title, rootDirecory, link.Url));
                }
                else if (item is BookmarkFolder)
                    getArticleFromFolder(lista, rootDirecory, (IBookmarkFolder)item);
            }

            return;
        }

        private void moveArticle(ArticlePDF from, ArticlePDF to)
        {
            string sFromPath = from.FilePath;
            string sToPath = to.FilePath;

            if (File.Exists(sToPath))
                sToPath = string.Format("{0}}_Copy{1}", sToPath, DateTime.Now.ToString("dd_mm_yyyy_HH_MM_ss"));

            File.Move(sFromPath, sToPath);
        }

        private string downloadFile(ArticlePDF from)
        {


           // string fileOutDirectory = from.FilePath; //string.Format("{0}{1}", OutputDirectory, from.FilePath);
            string sDirecorty = Path.GetDirectoryName(from.FilePath);

            if (!Directory.Exists(sDirecorty))
                Directory.CreateDirectory(sDirecorty);
            
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(from.Url, from.FilePath);

                return string.Format("Download: {0} URL: {1}", from.FilePath, from.Url);
            }
            /*
            Uri address = new Uri(from.Url);

            ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            using (WebClient webClient = new WebClient())
            {
                var stream = webClient.OpenRead(address);
                using (StreamReader sr = new StreamReader(stream))
                {
                    var page = sr.ReadToEnd();
                }
            }
            return "";*/
        }

        /// <summary>
        /// Certificate validation callback.
        /// </summary>
        private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            // If the certificate is a valid, signed certificate, return true.
            if (error == System.Net.Security.SslPolicyErrors.None)
            {
                return true;
            }

            Console.WriteLine("X509Certificate [{0}] Policy Error: '{1}'",
                cert.Subject,
                error.ToString());

            return false;
        }

        public void DonloadBookMark(string sBookmarkFilePath)
        {
            List<ArticlePDF> toDownload = new List<ArticlePDF>();
            List<ArticlePDF> bookMark = getBookMark(sBookmarkFilePath);
            List<ArticlePDF> articlePDF = getDirectoryFileList();

            List<ArticlePDF> temp;

            foreach (var item in bookMark)
            {
                item.DirectoryFile = string.Format("{0}{1}", OutputDirectory, item.DirectoryFile);

                temp = articlePDF.FindAll(pp => (pp.TitelFileName == item.TitelFileName));

                if (temp.Count > 0)
                {
                    foreach (var it in temp)
                    {
                        if (it.DirectoryFile != item.DirectoryFile)
                            moveArticle(it, item);
                    }
                }
                else
                    toDownload.Add(item);
            }

            for (int index = 0; index < toDownload.Count; index++)
            {
                try
                {
                    string sLog = downloadFile(toDownload[index]);

                    Log.Add(sLog);
                }
                catch (Exception ex)
                {
                    Log.Add(ex.Message);
                }
            }


          /* Parallel.For(0, toDownload.Count,
               index =>
               {
                   try
                   {
                       string sLog = downloadFile(toDownload[index]);

                       Log.Add(sLog);
                   }
                   catch (Exception ex)
                   {
                       Log.Add(ex.Message);
                   }
               });*/
               
            Log.Add("DonloadBookMark done");
        }
    }
}


