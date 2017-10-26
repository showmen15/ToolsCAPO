using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLLibrary;
using System.IO;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace TaskVisualizer
{
    public partial class Form1 : Form
    {
        #region  :: TaskVisualizer ::

        bool Working;
        bool Skip;

        Task runnerTask;
        List<VisualizerConfig> taskList = new List<VisualizerConfig>();
        //FreeScreenVideoRecorder recorder = new FreeScreenVideoRecorder();
        ScreenCapturerRecorder recorder = new ScreenCapturerRecorder();

        Process visualizer;

        public Form1()
        {
            InitializeComponent();

            timerRecorderWorking.Stop();
            txtServerName.SelectedIndex = 2;
        }

        private void initDB(string sServerName, string sUser, string sPass)
        {
            SQL.ConnectionString = string.Format("data source={0};initial catalog=Doktorat; User Id={1}; Password={2};", sServerName, sUser, sPass);
        }

        private void run()
        {
            string exeFilePath;
            visualizer = null;

            string sFileMovePath;

            taskList = SQL.DataProviderTaskVisualizer.GetVisualizerConfig();

            foreach (VisualizerConfig item in taskList)
            {
                try
                {
                    lblCaseName.Invoke(new Action(delegate ()
                    {
                        lblCaseName.Text = String.Format("CaseID: {0}, CaseName: {1}, Program: {2}, Trials: {3}", item.ID_Case, item.Name_Case, item.Name_Program, item.ID_Trials);
                    }));

                    

                    exeFilePath = @".\Visualizer\OfflineVisualizer.jar";

                    visualizer = new Process();

                    visualizer.StartInfo.FileName = "java.exe";
                    visualizer.StartInfo.Arguments = string.Format(" -jar {0} {1} {2}", exeFilePath, item.ID_Case.ToString(), item.ID_Trials.ToString());

                    visualizer.StartInfo.UseShellExecute = false;
                    visualizer.StartInfo.CreateNoWindow = true;
                    // process.StartInfo.RedirectStandardOutput = true;
                    // process.StartInfo.RedirectStandardError = true;

                    visualizer.Start();
                   

                    System.Threading.Thread.Sleep(700);

                    recorder.StartRecord(item);


                    visualizer.WaitForExit();

                    sFileMovePath = recorder.StopRecord();

                    if (Working && Skip)
                        Skip = false;
                    else if (Working && !Skip)
                    {
                        // recorder.RenameRecordedFileVisualizer(item);

                        string titel = Path.GetFileNameWithoutExtension(sFileMovePath);
                        string[] tags = item.GetDiscription();

                        addDiscriptionToFile(sFileMovePath, titel, tags, item.IdGlobal);

                        SQL.DataProviderTaskVisualizer.SetVisualizerConfigAsDone(item);
                    }
                    else
                    {
                        butStop.Invoke(new Action(delegate ()
                        {
                            butStop.PerformClick();
                        }));

                        return;
                    }
                }
                catch (Exception ex)
                {
                    closeVisualizer();
                }
            }

            butStop.PerformClick();
        }

        private void butRun_Click(object sender, EventArgs e)
        {
            Working = true;
            Skip = false;

            runnerTask = new Task(new Action(run));
            runnerTask.Start();
            timerRecorderWorking.Start();

            butRun.Enabled = false;
            butStop.Enabled = true;
            butSkipNext.Enabled = true;
        }

        private void butStop_Click(object sender, EventArgs e)
        {
            Working = false;
            Skip = false;

            closeVisualizer();
            timerRecorderWorking.Stop();

            recorder.StopRecord();

            butRun.Enabled = true;
            butStop.Enabled = false;
            butSkipNext.Enabled = false;
        }

        private void butSkipNext_Click(object sender, EventArgs e)
        {
            Skip = true;
            closeVisualizer();
        }

        private void closeVisualizer()
        {
            if (visualizer != null && !visualizer.HasExited)
                visualizer.Kill();

            visualizer = null;
        }

        [DllImport("kernel32.dll", EntryPoint = "Beep", SetLastError = true, ExactSpelling = true)]
        public static extern bool Beep(uint frequency, uint duration);

        private void checkFreeScreenVideoRecorderWorking()
        {
            if (recorder.IsNotWorking())
                Beep(2500, 1000);
        }

        private void txtServerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            initDB(txtServerName.Text, txtUser.Text, txtPassword.Text);
        }

        private void timerRecorderWorking_Tick(object sender, EventArgs e)
        {
            checkFreeScreenVideoRecorderWorking();
        }

        private void butBeep_Click(object sender, EventArgs e)
        {
            Beep(2500, 1000);
        }


        private void addDiscriptionToFile(string filePath, string titel, string[] tags , string comment)
        {
            var file = ShellFile.FromFilePath(filePath);

            file.Properties.System.Title.Value = titel;
            file.Properties.System.Comment.Value = comment;

            file.Properties.System.Media.Year.Value = 2017;

            using (var writer = file.Properties.GetPropertyWriter())
            {
                writer.WriteProperty(file.Properties.System.Keywords, tags, true);
                writer.Close();
            }

            file.Properties.System.Author.Value = new string[] { "Szymon Szomiński" };

            // Read and Write:

            //foreach (var item in file.Properties.DefaultPropertyCollection)
            //{
            //    ShellProperty<uint?> porp = item as ShellProperty<uint?>;

            //    if (porp != null)
            //    {

            //        Console.WriteLine(string.Format("{0} Value: {1}", "", porp.Value.ToString()));
            //    }
            //}
        }

        #endregion

        private void button1_Click_1(object sender, EventArgs e)
        {
            //string filePath = @"C:\test\szsz\uuu.mp4";

            string filePath = @"D:\NagraniaOutput\OtwartaPrzestrzeń7x5m\FearFactorBase_OtwartaPrzestrzeń7x5m_OtwartaPrzestrzeń 1-H1.mp4";
            string sTitel = "testowy";
            string[] tags  = new string[]
            {
                string.Format("ID_Case = {0}",232),
                string.Format("ID_Trials = {0}",44343),
                string.Format("Name = {0}",Name),
                string.Format("Name_Case = {0}",5454),
                string.Format("Name_Config = {0}",6565),

                string.Format("Name_Map = {0}",6464),
                string.Format("Name_Program = {0}",7675),
                string.Format("IdGlobal = {0}","42435_63_6436_36")
            };

            string sComment = "tags";

            addDiscriptionToFile(filePath, sTitel, tags, sComment);

            // string filePath = @"C:\Nowy\Robot_2017_06_14_15_44_24.avi";



            /*
            List<string> arrHeaders = new List<string>();

            Shell32.Shell shell = new Shell32.Shell();


            Shell32.Folder objFolder;

            objFolder = shell.NameSpace(@"\\dsview.pcoip.ki.agh.edu.pl\Biblioteki-Pracownicy$\szsz\Desktop\Robot_2017_06_14_15_44_24.avi");

            for (int i = 0; i < short.MaxValue; i++)
            {
                string header = objFolder.GetDetailsOf(null, i);
                if (String.IsNullOrEmpty(header))
                    break;
                arrHeaders.Add(header);
            }

            foreach (Shell32.FolderItem2 item in objFolder.Items())
            {
                for (int i = 0; i < arrHeaders.Count; i++)
                {
                    Console.WriteLine("{ 0}\t{1}: {2}", i, arrHeaders[i], objFolder.GetDetailsOf(item, i));
                }
            }

  

            string strFilename = @"\\dsview.pcoip.ki.agh.edu.pl\Biblioteki-Pracownicy$\szsz\Desktop\Robot_2017_06_14_15_44_24.avi";
            FileInfo oFileInfo = new FileInfo(strFilename);*/

            //FileStream stream3 = new FileStream("image2.tif", FileMode.Create);
            //BitmapMetadata myBitmapMetadata = new BitmapMetadata("tiff");
            //TiffBitmapEncoder encoder3 = new TiffBitmapEncoder();
            //myBitmapMetadata.ApplicationName = "Microsoft Digital Image Suite 10";
            //myBitmapMetadata.Author = new ReadOnlyCollection<string>(
            //    new List<string>() { "Lori Kane" });
            //myBitmapMetadata.CameraManufacturer = "Tailspin Toys";
            //myBitmapMetadata.CameraModel = "TT23";
            //myBitmapMetadata.Comment = "Nice Picture";
            //myBitmapMetadata.Copyright = "2010";
            //myBitmapMetadata.DateTaken = "5/23/2010";
            //myBitmapMetadata.Keywords = new ReadOnlyCollection<string>(
            //    new List<string>() { "Lori", "Kane" });
            //myBitmapMetadata.Rating = 5;
            //myBitmapMetadata.Subject = "Lori";
            //myBitmapMetadata.Title = "Lori's photo";

            //// Create a new frame that is identical to the one 
            //// from the original image, except for the new metadata. 
            //encoder3.Frames.Add(
            //    BitmapFrame.Create(
            //    decoder2.Frames[0],
            //    decoder2.Frames[0].Thumbnail,
            //    myBitmapMetadata,
            //    decoder2.Frames[0].ColorContexts));

            //encoder3.Save(stream3);
            //stream3.Close();
        }

    }
}
