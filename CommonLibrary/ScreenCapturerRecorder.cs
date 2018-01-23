using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class ScreenCapturerRecorder
    {
        //Setup Screen Capturer Recorder v0.12.8

        //HKEY_CURRENT_USER >> Software>>screen-capture-recorder

        // HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Screen Capturer Recorder_is1

        //https://github.com/rdp/screen-capture-recorder-to-video-windows-free
        //free screen recorder source

         //Ukrycie ikon pulpitu
        //https://www.youtube.com/watch?v=VN-BDwCv4eU
        

        private string movieDirectoryOutput = @"C:\NagraniaOutput";

        private string sFFmpegPath = @"C:\Program Files (x86)\Screen Capturer Recorder\configuration_setup_utility\vendor\ffmpeg\bin\ffmpeg.exe";
        private string sConfiguration = @"-loglevel info   -f dshow  -video_device_number 0 -i video=""screen-capture-recorder""  -f dshow -audio_device_number 0 -i audio=""virtual-audio-capturer""  -filter_complex amix=inputs=1  -vcodec libx264 -g 30 -qp 10 -tune zerolatency -pix_fmt yuv420p  -preset ultrafast -vsync vfr -acodec libmp3lame  -f mp4";

        private Process recorder;

        private string sFileMovePath;

        private VisualizerConfig vConfig;

        public ScreenCapturerRecorder()
        {
            init();
        }

        private void init()
        { 
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Screen Capturer Recorder_is1");


            //sFFmpegPath = string.Format(@"{0}configuration_setup_utility\vendor\ffmpeg\bin\ffmpeg.exe",(String)myKey.GetValue("InstallLocation"));
            //sFFmpegPath = //string.Format(@"{0}configuration_setup_utility\vendor\ffmpeg\bin\ffmpeg.exe", sFFmpegPath);
        }

        public void StartRecord(RunConfig item)
        {
            VisualizerConfig it = new VisualizerConfig(item.ID_Case, 0, "", item.Name_Case, item.Name_Config, item.Name_Map, item.Name_Program, "");

            StartRecord(it);
        }


        public void StartRecord(VisualizerConfig item)
        {
            vConfig = item;

            string sNewFileName = string.Format("{0}_{1}_{2}_{3}.mp4", item.Name_Map, item.Name_Program, item.Name_Config, item.ID_Trials); 
            //string sNewFileName = string.Format("{0}_{1}_{2}.mp4", item.Name_Map, item.Name_Program, item.Name_Config); //bez trials
            string sFileOutputDirectory = string.Format("{0}\\{1}\\{2}\\{3}", movieDirectoryOutput, item.Name_Map, item.Name_Program, item.Name_Config);

            if (!System.IO.File.Exists(sFileOutputDirectory))
                System.IO.Directory.CreateDirectory(sFileOutputDirectory);

            sFileMovePath = string.Format("{0}\\{1}", sFileOutputDirectory, sNewFileName);

            if (recorder != null && !recorder.HasExited)
                recorder.Kill();

            recorder = new Process();

            string sArgents = string.Format("{0} \"{1}\"", sConfiguration, sFileMovePath);

            recorder.StartInfo.FileName = sFFmpegPath;
            recorder.StartInfo.Arguments = sArgents;
            recorder.StartInfo.UseShellExecute = false;
            recorder.StartInfo.CreateNoWindow = true;

            recorder.Start();
        }

        public string StopRecord()
        {
            if (recorder != null)
            {
                StopProgram(recorder);

                recorder = null;
            }

            addExtensions();

            return sFileMovePath;
        }

        public void RenameRecordedFile(string sNewFile)
        {

        }

        private void addExtensions()
        {
            string titel = Path.GetFileNameWithoutExtension(sFileMovePath);
            string[] tags = vConfig.GetDiscription();

            addDiscriptionToFile(sFileMovePath, titel, tags, vConfig.IdGlobal);
        }

        private void addDiscriptionToFile(string filePath, string titel, string[] tags, string comment)
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


        private void StopProgram(Process proc)
        {
            //This does not require the console window to be visible.
            if (AttachConsole((uint)proc.Id))
            {
                // Disable Ctrl-C handling for our program
                SetConsoleCtrlHandler(null, true);
                GenerateConsoleCtrlEvent(CtrlTypes.CTRL_C_EVENT, 0);

                // Must wait here. If we don't and re-enable Ctrl-C
                // handling below too fast, we might terminate ourselves.
                proc.WaitForExit(2000);

                FreeConsole();

                //Re-enable Ctrl-C handling or any subsequently started
                //programs will inherit the disabled state.
                SetConsoleCtrlHandler(null, false);
            }
        }

        public bool IsNotWorking()
        {
            //return true;
            if (recorder != null && recorder.HasExited)
                return true;
            else
                return false;
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AttachConsole(uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool FreeConsole();

        // Enumerated type for the control messages sent to the handler routine
      

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GenerateConsoleCtrlEvent(CtrlTypes dwCtrlEvent, uint dwProcessGroupId);

        [DllImport("Kernel32")]
        public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);

        public enum CtrlTypes : uint
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        public delegate bool HandlerRoutine(CtrlTypes CtrlType);
    }



}


