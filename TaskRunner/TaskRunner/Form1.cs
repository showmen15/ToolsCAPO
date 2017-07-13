using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Media;
using System.Runtime.InteropServices;
using CommonLibrary;

namespace TaskRunner
{
    public partial class Form1 : Form
    {
        Process visualizer;
        
        Task runnerTask;
        List<RunConfig> taskList = new List<RunConfig>();
        FreeScreenVideoRecorder recorder = new FreeScreenVideoRecorder();

        bool StopWork;
        Process process;

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
            process = null;

            do
            {

                taskList = getTaskConfig();

                foreach (RunConfig item in taskList)
                {
                    try
                    {
                        if (chkVisualizer.Checked)
                        {
                            createVisualizer(item.Visualizer, item.ID_Case);
                            System.Threading.Thread.Sleep(2000);
                        }

                        lblCaseName.Invoke(new Action(delegate ()
                        {
                            lblCaseName.Text = String.Format("CaseID: {0} ProgramID: {1}, CaseName: {2}", item.ID_Case, item.ID_Program, item.Name_Case);
                        }));

                        if (chkRecord.Checked)
                        {
                            recorder.StartRecord();
                        }

                        exeFilePath = getProgramPath(item.ID_Program);

                        process = new Process();

                        process.StartInfo.FileName = "java.exe";
                        process.StartInfo.Arguments = string.Format(" -jar {0} {1} {2}", exeFilePath, item.ID_Case.ToString(), item.ID_Program);

                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.CreateNoWindow = true;
                        // process.StartInfo.RedirectStandardOutput = true;
                        // process.StartInfo.RedirectStandardError = true;
                        process.Start();

                        Timeout = item.Timeout;

                        label5.Invoke(new Action(delegate ()
                        {
                            timerToEnd.Start();
                        }));


                        if (item.Timeout == 0)
                            process.WaitForExit();
                        else
                        {
                            try
                            {
                                if (!process.WaitForExit(item.Timeout * 1000))
                                    process.Kill();
                            }
                            catch (Exception)
                            {
                                clearProcess();
                            }
                        }

                        if (chkRecord.Checked)
                        {
                            recorder.StopRecord();
                            recorder.RenameRecordedFile1(item);
                        }

                        if (chkVisualizer.Checked)
                        {
                            clearVisualizer();
                        }

                        process = null;

                        label5.Invoke(new Action(delegate ()
                        {
                            timerToEnd.Stop();
                        }));

                        if (StopWork)
                            break;

                    }
                    catch (Exception)
                    {
                        clearProcess();
                        clearVisualizer();
                    }
                }
            }
            while (!StopWork && (taskList.Count > 0));

            butStop.Invoke(new Action(delegate ()
            {
                butStop.PerformClick();
            }));
        }

        private void createVisualizer(int iVisualizer,int ID_Case)
        {
            visualizer = new Process();
            String exeFilePath = string.Empty;

            //old
            //if(iVisualizer == 0)
            //    exeFilePath = @".\Visualizer\Fear.jar";
            //else if (iVisualizer == 1)
            //    exeFilePath = @".\Visualizer\RVO.jar";

            exeFilePath = @".\Visualizer\RVO.jar";

            visualizer.StartInfo.FileName = "java.exe";
            visualizer.StartInfo.Arguments = string.Format(" -jar {0} {1}", exeFilePath, ID_Case.ToString());

            visualizer.StartInfo.UseShellExecute = false;
            visualizer.StartInfo.RedirectStandardOutput = true;
            visualizer.StartInfo.RedirectStandardError = true;
            visualizer.Start();
        }

        private string getProgramPath(long p)
        {
            //old
            //switch (p)
            //{
            //    case 0:
            //        return @".\Programs\FearFactorBase.jar";
            //    case 1:
            //        return @".\Programs\FearFactorWithPassageThroughTheDoor.jar";
            //    case 2:
            //        return @".\Programs\RVOBase.jar";
            //    case 3:
            //        return @".\Programs\RVOWithRightHand.jar";
                    
            //    default:
            //        return string.Empty;
            //}

            return @".\Programs\HybridRVO.jar"; 
        }

        private void butRun_Click(object sender, EventArgs e)
        {
           
            runnerTask = new Task(new Action(run));

            runnerTask.Start();

            butStop.Enabled = true;
            butRun.Enabled = false;
            StopWork = false;
        }

        private List<RunConfig> getTaskConfig()
        {
            List<RunConfig> tasks = new List<RunConfig>();

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
                    cmd.CommandText = "SELECT top 10 ID_Program,Timeout,ID_Case,Name_Case,VisualizerID, Name_Program, Name_Map, Name_Config FROM dbo.TasksList ORDER BY ID_Config,id_trials,ID_Program";

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            RunConfig temp = new RunConfig((int)rdr["ID_Program"], (int)rdr["Timeout"], (int)rdr["ID_Case"], rdr["Name_Case"].ToString(), (int)rdr["VisualizerID"]);
                            temp.Name_Program = (string)rdr["Name_Program"];
                            temp.Name_Map = (string)rdr["Name_Map"];
                            temp.Name_Config = (string)rdr["Name_Config"];

                            tasks.Add(temp);
                        }
                    }
                }
            }

            return tasks;
        }

        private void butStop_Click(object sender, EventArgs e)
        {
            try
            {
                StopWork = true;

                butStop.Enabled = false;
                butRun.Enabled = true;

                timerToEnd.Stop();

                if (process != null)
                    process.Kill();
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

        private void timerToEnd_Tick(object sender, EventArgs e)
        {
            Timeout--;
            label5.Text = Timeout.ToString();

            checkFreeScreenVideoRecorderWorking(Timeout);
        }

        private void clearProcess()
        {
            try
            {
                if (process != null)
                {
                    process.Kill();
                    process = null;
                }
            }
            catch
            {
                process = null;
            }
        }

        private void clearVisualizer()
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
            if (chkRecord.Checked)
            {
                if (recorder.IsNotWorking())
                    Beep(2500, 1000);

                if (Timeout < 0)
                    Beep(2500, 1000);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            recorder.StartRecordWindow();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            recorder.StopRecord();
        }
    }


}
