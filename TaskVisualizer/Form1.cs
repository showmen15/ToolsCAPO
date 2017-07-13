﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLibrary;
using TaskRunner;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace TaskVisualizer
{
    public partial class Form1 : Form
    {
        //Process visualizer;

        Task runnerTask;
        List<VisualizerConfig> taskList = new List<VisualizerConfig>();
        FreeScreenVideoRecorder recorder = new FreeScreenVideoRecorder();

        bool StopWork;
        Process visualizer;

        private int Timeout;

        public Form1()
        {
            InitializeComponent();
            timerToEnd.Stop();
            txtServerName.SelectedIndex = 0;
        }


        private void run()
        {
            string exeFilePath;
            visualizer = null;

            taskList = getVisualizerConfig();

            foreach (VisualizerConfig item in taskList)
            {
                recorder.StartRecord();

                exeFilePath = @".\Visualizer\OfflineVisualizer.jar";

                visualizer = new Process();

                visualizer.StartInfo.FileName = "java.exe";
                visualizer.StartInfo.Arguments = string.Format(" -jar {0} {1} {2}", exeFilePath, item.ID_Case.ToString(), item.ID_Trials.ToString());

                visualizer.StartInfo.UseShellExecute = false;
                visualizer.StartInfo.CreateNoWindow = true;
                // process.StartInfo.RedirectStandardOutput = true;
                // process.StartInfo.RedirectStandardError = true;
                visualizer.Start();

                try
                {
                    visualizer.WaitForExit();
                }
                catch (Exception)
                {
                    //clearProcess();
                }

                recorder.StopRecord();
                recorder.RenameRecordedFileVisualizer(item);

                visualizer = null;
            }

            butStop.Invoke(new Action(delegate ()
            {
                butStop.PerformClick();
            }));
        }

        private void butRun_Click(object sender, EventArgs e)
        {
            runnerTask = new Task(new Action(run));

            runnerTask.Start();

            butStop.Enabled = true;
            butRun.Enabled = false;
            StopWork = false;
        }

        private void butStop_Click(object sender, EventArgs e)
        {
            try
            {
                StopWork = true;

                butStop.Enabled = false;
                butRun.Enabled = true;

                timerToEnd.Stop();

                if (visualizer != null)
                    visualizer.Kill();
            }
            catch (Exception)
            {
                clearProcess();
                timerToEnd.Stop();
            }
        }

        private void butSkipNext_Click(object sender, EventArgs e)
        {
            clearProcess();
        }

        private List<VisualizerConfig> getVisualizerConfig()
        {
            List<VisualizerConfig> tasks = new List<VisualizerConfig>();

            string sConnectionString = string.Empty;

            txtServerName.Invoke(new Action(delegate ()
            {
                sConnectionString = string.Format("Server={0};Database=Doktorat;User Id={1};Password={2};", txtServerName.Text, txtUser.Text, txtPassword.Text);
            }));


            using (SqlConnection con = new SqlConnection(sConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("", con))
                {
                    cmd.CommandText = "";

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {

                         /*   RunConfig temp = new RunConfig((int)rdr["ID_Program"], (int)rdr["Timeout"], (int)rdr["ID_Case"], rdr["Name_Case"].ToString(), (int)rdr["VisualizerID"]);
                            temp.Name_Program = (string)rdr["Name_Program"];
                            temp.Name_Map = (string)rdr["Name_Map"];
                            temp.Name_Config = (string)rdr["Name_Config"];

                            tasks.Add(temp);*/
                        }
                    }
                }
            }

            return tasks;
        }


        private void timerToEnd_Tick(object sender, EventArgs e)
        {
            checkFreeScreenVideoRecorderWorking(Timeout);
        }

        private void clearProcess()
        {
            try
            {
                if (visualizer != null)
                {
                    visualizer.Kill();
                    visualizer = null;
                }
            }
            catch
            {
                visualizer = null;
            }
        }

        [DllImport("kernel32.dll", EntryPoint = "Beep", SetLastError = true,
    ExactSpelling = true)]
        public static extern bool Beep(uint frequency, uint duration);

        private void button1_Click(object sender, EventArgs e)
        {
            Beep(2500, 1000);
            //  checkFreeScreenVideoRecorderWorking(-1);
        }

        private void checkFreeScreenVideoRecorderWorking(int Timeout)
        {
                if (recorder.IsNotWorking())
                    Beep(2500, 1000);
        }
    }
}