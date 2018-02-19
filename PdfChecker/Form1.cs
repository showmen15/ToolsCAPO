using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filename = @"C:\testDoc\docpublish.pdf";
            
            PdfTextSharpChecker pdf = new PdfTextSharpChecker(filename);

            string logText = pdf.CheckPDF();
            txtLog.Text = logText;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // string filename = @"C:\testDoc\docpublish.pdf";

            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    PdfTextSharpChecker pdf = new PdfTextSharpChecker(dlg.FileName);

                    string logText = pdf.CheckPDF(Convert.ToInt32(numBegin.Value), Convert.ToInt32(numEnd.Value));
                    txtLog.Text = logText;
                }
            }
        }
    }

}
