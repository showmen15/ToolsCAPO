using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication16
{
    public class FreeScreenVideoRecorder
    {
        private string movieDirectory = @"C:\Users\Szymon\Videos";

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

        public void StartRecord()
        {
            //Start nagrania 

            Int32 lblAppnameWindow = Win32.FindWindow("Qt5QWindowIcon", "FreeScreenVideoRecorder");

            if (lblAppnameWindow == 0)
                return;

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
            //Stop nagrywania
            var Dialog1 = Win32.FindWindow("Qt5QWindowIcon", "Dialog");

            if (Dialog1 == 0)
                return;


            var recWidgetWindow1 = Win32.FindWindowEx(Dialog1, 0, "Qt5QWindowIcon", "recWidgetWindow");

            var btnRecStopWindow = Win32.FindWindowEx(recWidgetWindow1, 0, "Qt5QWindowIcon", "btnRecStopWindow");

            Win32.SendMessage(btnRecStopWindow, 0x0201, 0x00000001, 0x00100011);
            Win32.SendMessage(btnRecStopWindow, 0x0202, 0x00000000, 0x00100010);
        }
        
        public void RenameRecordedFile(string sNewFile)
        {
            for (int i = 0; i < 100; i++)
            {
                string pattern = "TEST";

                DirectoryInfo taskDirectory = new DirectoryInfo(movieDirectory);
                FileInfo[] taskFiles = taskDirectory.GetFiles(pattern + "*");

                if (taskFiles.Length > 0)
                {
                    foreach (var item in taskFiles)
                    {
                        string newFileName = string.Format("{0}\\{1}", item.DirectoryName, item.Name.Replace(pattern, sNewFile));
                        System.IO.File.Move(item.FullName, newFileName);
                    }
                    break;
                }

                System.Threading.Thread.Sleep(200);
            }
        } 
    }
}
