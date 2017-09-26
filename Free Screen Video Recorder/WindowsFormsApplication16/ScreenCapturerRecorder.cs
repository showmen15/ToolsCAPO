using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication16
{
    public class ScreenCapturerRecorder
    {
        //Setup Screen Capturer Recorder v0.12.8

        //HKEY_CURRENT_USER >> Software>>screen-capture-recorder

        // HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Screen Capturer Recorder_is1

        private string movieDirectory = @"C:\Users\Szymon\Videos";

        private string sFFmpegPath = @"C:\Program Files (x86)\Screen Capturer Recorder\configuration_setup_utility\vendor\ffmpeg\bin\ffmpeg.exe";
        private string sConfiguration = @"-loglevel info   -f dshow  -video_device_number 0 -i video=""screen-capture-recorder""  -f dshow -audio_device_number 0 -i audio=""virtual-audio-capturer""  -filter_complex amix=inputs=1  -vcodec libx264 -g 30 -qp 10 -tune zerolatency -pix_fmt yuv420p  -preset ultrafast -vsync vfr -acodec libmp3lame  -f mp4";

        private Process recorder;

        public ScreenCapturerRecorder()
        {
            init();
        }

        private void init()
        { 
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Screen Capturer Recorder_is1", false);
            sFFmpegPath = string.Format(@"{0}configuration_setup_utility\vendor\ffmpeg\bin\ffmpeg.exe",(String)myKey.GetValue("InstallLocation"));
        }

        public void StartRecord(string sOutName)
        {
            if (File.Exists(sOutName))
                File.Delete(sOutName);

            if (recorder != null && !recorder.HasExited)
                recorder.Kill();

            recorder = new Process();

            string sArgents = string.Format("{0} \"{1}\"", sConfiguration, sOutName);

            recorder.StartInfo.FileName = sFFmpegPath;
            recorder.StartInfo.Arguments = sArgents;
            recorder.StartInfo.UseShellExecute = false;
            recorder.StartInfo.CreateNoWindow = true;

            recorder.Start();
        }

        public void StopRecord()
        {
            StopProgram(recorder);

        }

        public void RenameRecordedFile(string sNewFile)
        {

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


