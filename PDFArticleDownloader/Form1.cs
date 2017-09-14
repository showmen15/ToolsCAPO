using ArticleDownloader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication25
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Downloader down = new Downloader(@"D:\Desktop\OutputDirectory\Nowy folder\");
            //down.getBookMark(@"D:\Desktop\OutputDirectory\bookmarks44.html");

            down.DonloadBookMark(@"D:\Desktop\OutputDirectory\bookmarks44.html");

        }
    }
}
