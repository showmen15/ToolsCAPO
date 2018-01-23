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
                Title = "Test 1";
                Description = "Test 2";
                Tags = null;

                idUpload = upload.YoutubeUpload(filePath, Title, Description, Tags); 
            }
        }
    }
}
