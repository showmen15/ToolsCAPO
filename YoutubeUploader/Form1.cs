using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GoogleYoutubeUploader;
using System.IO;
using Microsoft.WindowsAPICodePack.Shell;

namespace YoutubeUploader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void butUpload_Click(object sender, EventArgs e)
        {
            string idUpload;
            string[] sFilePath = new string[] { @"C:\git\youtube\Test.mp4"};
            GoogleYoutubeUploader.YoutubeUploader upload = new GoogleYoutubeUploader.YoutubeUploader();

            string filePath;
            string Title;
            string Description;
            string[] Tags = null;

            foreach (var item in sFilePath)
            {
                filePath = item;
                Title = "Otwarta przestrzeń PF+ 8-1";
                Description = "";
                Tags = null;

                idUpload = upload.YoutubeUpload(filePath, Title, Description, Tags); 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // setFileUrl(@"C:\testDoc\PHD\mapa\R\1-1\mapa R 1-1.mp4", "www.wp.pl");

            //            var directories = Directory.GetDirectories(@"C:\testDoc\PHD");

            //  string sMovieDirectory = @"C:\testDoc\PHD";
            //  uploadMovie(sMovieDirectory); //wysłanie na youtuba


            mangeMovie();
        }


        private void mangeMovie()
        {
            try
            {
                string sMovieDirectory = @"C:\testDoc\PHD";
                FileItem.ParrentDirecotry = "Video/Simulation"; //zmieniac w zaleznosci od tego co exportujemy Simulation | Robot | Visualization 

                //renameMovie(sMovieDirectory); //standaryzacja nazw plików

                uploadMovie(sMovieDirectory); //wysłanie na youtuba

                List<MapItem> item = loadFileData(sMovieDirectory); //pobranie informacji z katalogów o plikach wysłanych na youtuba

                string output = createTable(item); //tworzenie tabeli 
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString(),  "Bład przetwarzania danych", MessageBoxButtons.OK ,MessageBoxIcon.Error);
            }

            MessageBox.Show(this, "Wykonano!!!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void uploadMovie(string sFileDirectory)
        {
            try
            {
                string idUpload;
                List<string> sFilePath = new List<string>(System.IO.Directory.GetFiles(sFileDirectory, "*.mp4*", System.IO.SearchOption.AllDirectories));

                sFilePath = removeUploded(sFilePath);

                string filePath;
                string Title;
                string Description;
                string[] Tags = null;

                foreach (var item in sFilePath)
                {
                    GoogleYoutubeUploader.YoutubeUploader upload = new GoogleYoutubeUploader.YoutubeUploader();

                    filePath = item;
                    Title = Path.GetFileName(item); // //"Otwarta przestrzeń PF+ 8-1";
                    Description = "";
                    Tags = null;

                    idUpload = upload.YoutubeUpload(filePath, Title, Description, Tags);

                    setFileUrl(item, idUpload);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private List<string> removeUploded(List<string> sFilePath)
        {
            for(int i = 0; i < sFilePath.Count(); i++)
            {
                var file = ShellFile.FromFilePath(sFilePath[i]);

                if(!string.IsNullOrWhiteSpace(file.Properties.System.Media.AuthorUrl.Value))
                    sFilePath.RemoveAt(i);
            }

            return sFilePath;
        }

        private void setFileUrl(string sFilePath,string idUpload)
        {
            var file = ShellFile.FromFilePath(sFilePath);

            file.Properties.System.Media.AuthorUrl.Value = string.Format("https://youtu.be/{0}", idUpload);
        }

        private void renameMovie(string sFileDirectory)
        {
            string[] allfiles = System.IO.Directory.GetFiles(sFileDirectory, "*.mp4*", System.IO.SearchOption.AllDirectories);

            foreach (var item in allfiles)
            {
                DirectoryInfo info = new DirectoryInfo(item);
                string DirCase = info.Parent.Name;
                string DirAlgorytm = info.Parent.Parent.Name;
                string DirMap = info.Parent.Parent.Parent.Name;

                string FileDir = Path.GetDirectoryName(item);
                string newFileName = string.Format("{0}\\{1} {2} {3}.mp4", FileDir, DirMap, DirAlgorytm, DirCase);
                
                System.IO.File.Move(item, newFileName);
            }
        }

        private List<MapItem> loadFileData(string sDirectory)
        {
            List<MapItem> result = new List<MapItem>();
            MapItem currentMap;

            var directories = Directory.GetDirectories(sDirectory);

            foreach (var mainItem in directories)
            {
                currentMap = new MapItem();

                DirectoryInfo mapName = new DirectoryInfo(mainItem); //Path. Directory. (mainItem);

                currentMap.Name = mapName.Name;
                result.Add(currentMap);

                var mapsDirectory = Directory.GetDirectories(mainItem);

                foreach (var algoDirectory in mapsDirectory)
                {
                    var caseDirectory = Directory.GetDirectories(algoDirectory);

                    foreach (var fileItem in caseDirectory)
                    {
                        string[] files = Directory.GetFiles(fileItem, "*.mp4");

                        if (files.Length != 1)
                            throw new Exception("W katalogu jest wiecje niz jeden plik lub brak pliku katalog: " + fileItem);

                        DirectoryInfo algName = new DirectoryInfo(algoDirectory);
                        FileItem file;

                        switch (algName.Name)
                        {
                            case "R":
                                file = new FileItem();
                                file.FilePath = files[0];                               
                                currentMap.R.Add(file);
                                break;

                            case "PF":
                                file = new FileItem();
                                file.FilePath = files[0];
                                currentMap.PF.Add(file);
                                break;

                            case "RVO":
                                file = new FileItem();
                                file.FilePath = files[0];
                                currentMap.RVO.Add(file);
                                break;

                            case "PR":
                                file = new FileItem();
                                file.FilePath = files[0];
                                currentMap.PR.Add(file);
                                break;

                            case "R+":
                                file = new FileItem();
                                file.FilePath = files[0];
                                currentMap.Rplus.Add(file);
                                break;

                            case "PF+":
                                file = new FileItem();
                                file.FilePath = files[0];
                                currentMap.PFplus.Add(file);
                                break;

                            default:
                                break;
                        }
                    }
                }

            }

            return result;
        }

        private string createTable(List<MapItem>  mapTree) 
        {
            StringBuilder table = new StringBuilder();

            foreach (var item in mapTree)
            {
                var temp = formatMapItem(item);

                table.AppendLine(temp);

                table.AppendLine();
                table.Append(@"\hline");
                table.AppendLine();
            }

            return table.ToString(); 
        }

        private string formatMapItem(MapItem mapItem)
        {
            StringBuilder map = new StringBuilder();
            string sTemp = string.Empty;
            bool apendUperSand = false;

            map.Append(@"\multirow{");
            map.Append(mapItem.MapRows.ToString());
            map.Append(@"}{*}{");
            map.Append(mapItem.Name);
            map.Append(@"} 	& ");

            if (mapItem.ExistsR)
            {
                sTemp = formatAlgorytm("R", mapItem.R);
                map.Append(sTemp);
                apendUperSand = true;
            }

            if (mapItem.ExistsPF)
            {
                if (apendUperSand)
                    map.Append(" & ");

                sTemp = formatAlgorytm("PF", mapItem.PF);
                map.Append(sTemp);
            }

            if (mapItem.ExistsRVO)
            {
                if (apendUperSand)
                    map.Append(" & ");

                sTemp = formatAlgorytm("RVO", mapItem.RVO);
                map.Append(sTemp);
            }

            if (mapItem.ExistsPR)
            {
                if (apendUperSand)
                    map.Append(" & ");

                sTemp = formatAlgorytm("PR", mapItem.PR);
                map.Append(sTemp);
            }

            if (mapItem.ExistsRplus)
            {
                if (apendUperSand)
                    map.Append(" & ");


                sTemp = formatAlgorytm("R+", mapItem.Rplus);
                map.Append(sTemp);
            }

            if (mapItem.ExistsPFplus)
            {
                if (apendUperSand)
                    map.Append(" & ");


                sTemp = formatAlgorytm("PF+", mapItem.PFplus);
                map.Append(sTemp);
            }

            return map.ToString();
        }

        private string formatAlgorytm(string sAlgorytmName, List<FileItem> fileList)
        {
            StringBuilder file = new StringBuilder();

            for (int i = 0; i < fileList.Count(); i++)
            {
                file.Append(@"\multirow{");
                file.Append(fileList.Count());
                file.Append(@"}{*}{");

                if (i == 0)
                    file.Append(sAlgorytmName);

                file.Append(@"} & \href{run:");
                file.Append(fileList[i].GetPathCD);
                file.Append(@"}{");
                file.Append(fileList[i].GetCaseName);
                file.Append(@"} & \url{");
                file.Append(fileList[i].GetYoutubeAdress);
                file.Append(@"} \\");

                if (i == fileList.Count() - 1)
                    file.Append(@"\cline{2-4}");
                else
                {
                    file.Append(@"\cline{3-4}");
                    file.Append(" & ");
                }

                file.AppendLine();

            }

            file.AppendLine();

            return file.ToString();
        }		
    }
}
