using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeUploader
{
    public class FileItem2
    {
        public static string ParrentDirecotry;


        public string FilePath { get; set; }

        public string FileName { get; }

        public string GetYoutubeAdress
        {
            get
            {
                string result = getFileUrl(FilePath);
                return result;  //"https://youtu.be/wmUIssatzkA"
            }
        }

        public static string getFileUrl(string sFilePath)
        {
            string sFileName = Path.GetFileNameWithoutExtension(sFilePath);
            string sDirName = Path.GetDirectoryName(sFilePath);

            string sLinkName = string.Format("{0}\\{1}.url", sDirName, sFileName);

            string[] lines = File.ReadAllLines(sLinkName);

            string line = lines[3].Replace("URL=", string.Empty);

            return line;
        }

        public string GetPathCD
        {
            get
            {
                DirectoryInfo directoryPath = new DirectoryInfo(FilePath);
                string DirCase = directoryPath.Parent.Name;
                string DirAlgorytm = directoryPath.Parent.Parent.Name;
                string DirMap = directoryPath.Parent.Parent.Parent.Name;

                string sFileName = Path.GetFileName(FilePath);

                return string.Format("{0}/{1}/{2}/{3}/{4}", ParrentDirecotry, DirMap, DirAlgorytm, DirCase, sFileName); //   @"Video/vido.mp4"; //zwraca lokalizacje dla CD
            }
        }

        public string GetCaseName
        {
            get
            {
                DirectoryInfo directoryPath = new DirectoryInfo(FilePath);
                string DirCase = directoryPath.Parent.Name;

                return DirCase; //zwraca Case Name dla CD
            }
        }



    }
}
