using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication16
{
    public partial class Form1 : Form
    {
        private System.Int32 iHandle;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Int32 iHandle2;

            iHandle = Win32.FindWindow("PuTTYConfigBox", "PuTTY Configuration");

            Win32.SetWindowPos((IntPtr) iHandle, 0, 0, 0, 0, 0, Win32.SWP_NOZORDER | Win32.SWP_NOSIZE | Win32.SWP_SHOWWINDOW);



            iHandle2 = Win32.FindWindowEx(iHandle, 0, "Button", "&Cancel");

            Win32.SendMessage(iHandle2, Win32.BM_CLICK, 0, 0);
           



            // Win32.SendMessage(iHandle2, Win32.WM_LBUTTONUP, 0, 0);

            System.Threading.Thread.Sleep(1000);

            var iHandle3 = Win32.FindWindow("#32770 (Dialog)", "About PuTTY");

            var iHandle22 = Win32.FindWindowEx(iHandle3, 0, "Button", "&Close");

            Win32.SendMessage(iHandle22, Win32.WM_LBUTTONDOWN, 0, 0);
            Win32.SendMessage(iHandle22, Win32.WM_LBUTTONUP, 0, 0);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FreeScreenVideoRecorder tt = new FreeScreenVideoRecorder();
            tt.StartRecord();

            System.Threading.Thread.Sleep(2000);

            tt.StopRecord();

            tt.RenameRecordedFile("Kocham Cie Edi");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FreeScreenVideoRecorder tt = new FreeScreenVideoRecorder();
            tt.StopRecord();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FreeScreenVideoRecorder tt = new FreeScreenVideoRecorder();
            tt.RenameRecordedFile("");
        }

        ScreenCapturerRecorder rec;

        private void button5_Click(object sender, EventArgs e)
        {
            rec = new ScreenCapturerRecorder();
            rec.StartRecord(@"C:\test\szsz/uuu.mp4");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            rec.StopRecord();
        }
    }
}
