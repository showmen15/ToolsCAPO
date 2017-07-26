using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;

namespace CommonLibrary
{
    public class FreeScreenVideoRecorder
    {
        //[DllImport("user32.dll")]
        //public static extern int SetForegroundWindow(IntPtr hWnd);

        private string movieDirectoryInput = @"D:\Nagrania";
        private string movieDirectoryOutput = @"D:\NagraniaOutput";

        private bool isWrongInit = false;

        public string FreeScreenVideoRecorderExe = @"FreeScreenVideoRecorder";

        public FreeScreenVideoRecorder()
        {
            clearDirecotry();
        }

        private void clearDirecotry()
        {

            if (System.IO.Directory.Exists(movieDirectoryInput))
            {


                DirectoryInfo taskDirectory = new DirectoryInfo(movieDirectoryInput);
                FileInfo[] taskFiles = taskDirectory.GetFiles("*");

                for (int i = 0; i < taskFiles.Length; i++)
                    System.IO.File.Delete(taskFiles[i].FullName);
            }
        }

        public void init()
        {
            //   btnRecStartPauseWindow

           // var frameTitleWindow = Win32.FindWindowEx(centrWidgetWindow, 0, "Qt5QWindowIcon", "frameTitleWindow");

           // var btnExitWindow = Win32.FindWindowEx(frameTitleWindow, 0, "Qt5QWindowIcon", "btnRecActWinWindow");

            //Win32.SendMessage(btnExitWindow, 0x0084, 0x00000000, 0x01700481);
            //Win32.SendMessage(btnExitWindow, 0x0020, 0x000803b8, 0x02000001);
            //Win32.SendMessage(btnExitWindow, 0x0084, 0x00000000, 0x01700481);
            //Win32.SendMessage(btnExitWindow, 0x0021, 0x000603A2, 0x02010001);
            //Win32.SendMessage(btnExitWindow, 0x0020, 0x000803B8, 0x02010001);
            
            //Win32.SendMessage(btnExitWindow, 0x0215, 0x00000000, 0x00000000);
            //Win32.SendMessage(btnExitWindow, 0x02A3, 0x00000000, 0x00000000);






            //Win32.SendMessage(btnExitWindow, Win32.WM_MOUSEMOVE, 0x00000001, 0x0009000B);
            //Win32.SendMessage(btnExitWindow, Win32.WM_LBUTTONUP, 0x00000001, 0x0009000B);


            //Win32.SendMessage(btnExitWindow, Win32.WM_LBUTTONDOWN, 0x00000001,0x0009000B);
            //Win32.SendMessage(btnExitWindow, Win32.WM_MOUSEMOVE, 0x00000001, 0x0009000B);
            //Win32.SendMessage(btnExitWindow, Win32.WM_LBUTTONUP, 0x00000001, 0x0009000B);

            //   var frmCentralWindow = Win32.FindWindowEx(centrWidgetWindow, 0, "Qt5QWindowIcon", "frmCentralWindow");

            //   var btnRecActWinWindow = Win32.FindWindowEx(frmCentralWindow, 0, "Qt5QWindowIcon", "btnRecActWinWindow");


            //  Win32.SendMessage(btnRecActWinWindow, Win32.WM_COMMAND, 1, null);

            //  Win32.SendMessage(btnRecActWinWindow, Win32.WM_LBUTTONDOWN, Win32.MK_LBUTTON, MAKELPARAM(0, 0));
            //  Win32.SendMessage(btnRecActWinWindow, Win32.WM_LBUTTONUP, Win32.MK_LBUTTON, MAKELPARAM(0, 0));

            //  Win32.SendMessage(btnRecActWinWindow, Win32.WM_KEYDOWN, 0, 0);
            //  Win32.SendMessage(btnRecActWinWindow, Win32.WM_KEYDOWN, 0, 0);

            // Win32.SendMessage(btnRecActWinWindow, Win32.BM_CLICK, 0, 0);

        }

        public void RenameRecordedFileVisualizer(VisualizerConfig item)
        {
            if (isWrongInit)
                return;

            bool isMoving = true;

            do
            {
                string[] sDircotryList = Directory.GetFiles(movieDirectoryInput);

                if (sDircotryList != null && (sDircotryList.Length > 0))
                {
                    string sInputFile = sDircotryList[0];

                    string sNewFileName = string.Format("{0}_{1}_{2}_{3}.mp4",item.Name_Map, item.Name_Program, item.Name_Config,item.ID_Trials);
                    string sFileOutputDirectory = string.Format("{0}\\{1}\\{2}\\{3}", movieDirectoryOutput, item.Name_Map, item.Name_Program, item.Name_Config);

                    if (!System.IO.File.Exists(sFileOutputDirectory))
                        System.IO.Directory.CreateDirectory(sFileOutputDirectory);

                    string sFileMovePath = string.Format("{0}\\{1}", sFileOutputDirectory, sNewFileName);

                    if (System.IO.File.Exists(sFileMovePath))
                        System.IO.File.Delete(sFileMovePath);

                    System.IO.File.Move(sInputFile, sFileMovePath);

                    isMoving = false;
                }
                else
                    System.Threading.Thread.Sleep(100);

            }
            while (isMoving);
        }

        public void StartRecord()
        {
            clearDirecotry();
             //Start nagrania 

             Int32 lblAppnameWindow = Win32.FindWindow("Qt5QWindowIcon", "FreeScreenVideoRecorder");

            if (lblAppnameWindow == 0)
            {
                isWrongInit = true;
                return;
            }

            isWrongInit = false;

            var centrWidgetWindow = Win32.FindWindowEx(lblAppnameWindow, 0, "Qt5QWindowIcon", "centrWidgetWindow");
            var frmCentralWindow = Win32.FindWindowEx(centrWidgetWindow, 0, "Qt5QWindowIcon", "frmCentralWindow");
            var btnRecActWinWindow = Win32.FindWindowEx(frmCentralWindow, 0, "Qt5QWindowIcon", "btnRecActWinWindow");

            Win32.SendMessage(btnRecActWinWindow, 0x0201, 0x00000001, 0x00100011);
            Win32.SendMessage(btnRecActWinWindow, 0x0202, 0x00000000, 0x00100010);

            Int32 Panelsterowania = 0;

            for (int i = 0; i < 200; i++)
            {
                var Dialog = Win32.FindWindow("Qt5QWindowIcon", "Dialog");
                var recWidgetWindow = Win32.FindWindowEx(Dialog, 0, "Qt5QWindowIcon", "recWidgetWindow");
                var btnRecStartPauseWindow = Win32.FindWindowEx(recWidgetWindow, 0, "Qt5QWindowIcon", "btnRecStartPauseWindow");

                Win32.SendMessage(btnRecStartPauseWindow, 0x0201, 0x00000001, 0x00100011);
                Win32.SendMessage(btnRecStartPauseWindow, 0x0202, 0x00000000, 0x00100010);

                System.Threading.Thread.Sleep(200);

                Panelsterowania = Win32.FindWindow("Qt5QWindowIcon", "Panel sterowania");

                if (Panelsterowania != 0)
                    break;
            }

            var qt_msgbox_buttonboxWindow = Win32.FindWindowEx(Panelsterowania, 0, "Qt5QWindowIcon", "qt_msgbox_buttonboxWindow");

            var QPushButtonClassWindow = Win32.FindWindowEx(qt_msgbox_buttonboxWindow, 0, "Qt5QWindowIcon", "QPushButtonClassWindow");

            Win32.SendMessage(QPushButtonClassWindow, 0x0201, 0x00000001, 0x00100011);
            Win32.SendMessage(QPushButtonClassWindow, 0x0202, 0x00000000, 0x00100010);
        }

        public void StopRecord()
        {
            if (isWrongInit)
                return;

            //Stop nagrywania
            var Dialog1 = Win32.FindWindow("Qt5QWindowIcon", "Dialog");

            if (Dialog1 == 0)
                return;


            var recWidgetWindow1 = Win32.FindWindowEx(Dialog1, 0, "Qt5QWindowIcon", "recWidgetWindow");

            var btnRecStopWindow = Win32.FindWindowEx(recWidgetWindow1, 0, "Qt5QWindowIcon", "btnRecStopWindow");

            Win32.SendMessage(btnRecStopWindow, 0x0201, 0x00000001, 0x00100011);
            Win32.SendMessage(btnRecStopWindow, 0x0202, 0x00000000, 0x00100010);
        }
        
        //public void RenameRecordedFile(RunConfig conf)
        //{
        //    string sNewFile = conf.Name_Case;

        //    if (isWrongInit)
        //        return;
            
        //    for (int i = 0; i < 300; i++)
        //    {
        //        string pattern = "CASE";

        //        DirectoryInfo taskDirectory = new DirectoryInfo(movieDirectory);
        //        FileInfo[] taskFiles = taskDirectory.GetFiles(pattern + "*");

        //        if (taskFiles.Length > 0)
        //        {
        //            foreach (var item in taskFiles)
        //            {
        //                string newFileName = string.Format("{0}\\{1}", item.DirectoryName, item.Name.Replace(pattern, sNewFile));
        //                System.IO.File.Move(item.FullName, newFileName);
        //            }
        //            break;
        //        }

        //        System.Threading.Thread.Sleep(200);
        //    }
        //}


        public void RenameRecordedFile1(RunConfig conf)
        {
            if (isWrongInit)
                return;

            string sNewFileName = string.Format("{0}_{1}_{2}.mp4", conf.Name_Program, conf.Name_Map, conf.Name_Config);
            string sNewFileDirectory = string.Format("{0}\\{1}", movieDirectoryOutput, sNewFileName);

            if (System.IO.File.Exists(sNewFileDirectory))
            {
                sNewFileName = string.Format("{0}_{1}_{2}_{3}.mp4", conf.Name_Program, conf.Name_Map, conf.Name_Config, DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss"));
                sNewFileDirectory = string.Format("{0}\\{1}", movieDirectoryOutput, sNewFileName);
            }

            for (int j = 0; j < 300; j++)
            {
                DirectoryInfo taskDirectory = new DirectoryInfo(movieDirectoryInput);
                FileInfo[] taskFiles = taskDirectory.GetFiles("*");

                if (taskFiles.Length > 0)
                {
                    System.IO.File.Move(taskFiles[0].FullName, sNewFileDirectory);

                    for (int i = 1; i < taskFiles.Length; i++)
                        System.IO.File.Delete(taskFiles[i].FullName);
                    break;
                }

                System.Threading.Thread.Sleep(200);
            }

            //    //for (int i = 0; i < 300; i++)
            //    //{
            //    //string pattern = "CASE";

            //    DirectoryInfo taskDirectory = new DirectoryInfo(movieDirectoryInput);
            //    FileInfo[] taskFiles = taskDirectory.GetFiles("*");

            //    if (taskFiles.Length > 0)
            //    {
            //        foreach (var item in taskFiles)
            //        {
            //            //string newFileName = string.Format("{0}\\{1}", item.DirectoryName, item.Name.Replace(pattern, sNewFile));
            //            System.IO.File.Move(item.FullName, newFileName);
            //        }
            //        //break;
            //    }

            //    System.Threading.Thread.Sleep(200);
            ////}
        }

        public bool IsNotWorking()
        {
            Process[] pname = Process.GetProcessesByName(FreeScreenVideoRecorderExe);
            if (pname.Length == 0)
                return true;
            else
                return false;
        }

        public void StartRecordWindow()
        {



            //Start nagrania 

            Int32 lblAppnameWindow = Win32.FindWindow("Qt5QWindowIcon", "FreeScreenVideoRecorder");

            if (lblAppnameWindow == 0)
            {
                isWrongInit = true;
                return;
            }

            isWrongInit = false;

            var centrWidgetWindow = Win32.FindWindowEx(lblAppnameWindow, 0, "Qt5QWindowIcon", "centrWidgetWindow");
            var frmCentralWindow = Win32.FindWindowEx(centrWidgetWindow, 0, "Qt5QWindowIcon", "frmCentralWindow");
            var btnRecWinWindow = Win32.FindWindowEx(frmCentralWindow, 0, "Qt5QWindowIcon", "btnRecWinWindow");

            //symulacja klikniecia
            Win32.SendMessage(btnRecWinWindow, 0x0201, 0x00000001, 0x00100011);
            Win32.SendMessage(btnRecWinWindow, 0x0202, 0x00000000, 0x00100010);

            Int32 Panelsterowania = 0;


            //System.Threading.Thread.Sleep(400);

            int lblSimmulatorWindow = Win32.FindWindow("Notepad", "Bez tytułu — Notatnik");

            Win32.Rect SimmulatorWindowRect = new Win32.Rect();
            Win32.GetWindowRect((IntPtr) lblSimmulatorWindow, ref SimmulatorWindowRect);

            Win32.SetCursorPos(SimmulatorWindowRect.Left + 30, SimmulatorWindowRect.Top + 30);

        

            Win32.SetActiveWindow((IntPtr) lblSimmulatorWindow);
            Win32.SetFocus((IntPtr)lblSimmulatorWindow);
            Win32.SetForegroundWindow((IntPtr)lblSimmulatorWindow);


            Win32.MouseLeftClick(new System.Drawing.Point(SimmulatorWindowRect.Left + 30, SimmulatorWindowRect.Top + 30), lblSimmulatorWindow);

           // Win32.SendMessage(lblSimmulatorWindow,Win32.WM_LBUTTONDOWN,Win32.MK_LBUTTON,

            //SendMessage(hWnd, WM_LBUTTONDOWN, MK_LBUTTON, MAKELPARAM(mousePosX, mousePosY));

            //Win32.SendMessage(lblSimmulatorWindow, 0x0201, 0x00000001, 0x00100011);
            //Win32.SendMessage(lblSimmulatorWindow, 0x0202, 0x00000000, 0x00100010);


            // SetForegroundWindow((Int32) lblSimmulatorWindow);



            //symulacja klikniecia
            //Win32.SendMessage(lblSimmulatorWindow, 0x0201, 0x00000001, 0x00100011);
            //Win32.SendMessage(lblSimmulatorWindow, 0x0202, 0x00000000, 0x00100010);


            // Win32.SetForegroundWindowNative((IntPtr)lblSimmulatorWindow);

            //Win32.SendMessage(lblSimmulatorWindow, 0x0201, 0x00000001, 0x00100011);
            //Win32.SendMessage(lblSimmulatorWindow, 0x0202, 0x00000000, 0x00100010);


            //Win32.SendMessage(lblAppnameWindow,Win32.WM_ACTIVATEAPP, 0x00000000, 0x000053d8);




            //Win32.SendMessage(lblAppnameWindow, Win32.WM_WINDOWPOSCHANGING, 0x00000000, 0x0040a850);
            //Win32.SendMessage(lblAppnameWindow, Win32.WM_WINDOWPOSCHANGED, 0x00000000, 0x0040a84c);
            //Win32.SendMessage(lblAppnameWindow, Win32.WM_ACTIVATEAPP, 0x00000001, 0x00000000);
            //Win32.SendMessage(lblAppnameWindow, Win32.WM_ACTIVATEAPP, 0x00000001, 0x00000000);
            //Win32.SendMessage(lblAppnameWindow, Win32.WM_ACTIVATEAPP, 0x00000000, 0x000053d8);


            /* for (int i = 0; i < 200; i++)
             {
                 var Dialog = Win32.FindWindow("Qt5QWindowIcon", "Dialog");
                 var recWidgetWindow = Win32.FindWindowEx(Dialog, 0, "Qt5QWindowIcon", "recWidgetWindow");
                 var btnRecStartPauseWindow = Win32.FindWindowEx(recWidgetWindow, 0, "Qt5QWindowIcon", "btnRecStartPauseWindow");

                 Win32.SendMessage(btnRecStartPauseWindow, 0x0201, 0x00000001, 0x00100011);
                 Win32.SendMessage(btnRecStartPauseWindow, 0x0202, 0x00000000, 0x00100010);

                 System.Threading.Thread.Sleep(200);

                 Panelsterowania = Win32.FindWindow("Qt5QWindowIcon", "Panel sterowania");

                 if (Panelsterowania != 0)
                     break;
             }

             var qt_msgbox_buttonboxWindow = Win32.FindWindowEx(Panelsterowania, 0, "Qt5QWindowIcon", "qt_msgbox_buttonboxWindow");

             var QPushButtonClassWindow = Win32.FindWindowEx(qt_msgbox_buttonboxWindow, 0, "Qt5QWindowIcon", "QPushButtonClassWindow");

             Win32.SendMessage(QPushButtonClassWindow, 0x0201, 0x00000001, 0x00100011);
             Win32.SendMessage(QPushButtonClassWindow, 0x0202, 0x00000000, 0x00100010);*/
        }


    }
}
