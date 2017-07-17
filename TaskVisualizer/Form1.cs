using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                try
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
                        clearProcess();
                    }

                    recorder.StopRecord();
                    recorder.RenameRecordedFileVisualizer(item);
                    setVisualizerConfigAsDone(item);

                    visualizer = null;
                }
                catch(Exception ex)
                {

                }
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
                    cmd.CommandText = @"select t.ID_Case,t.ID_Trials,t.Name,a.Name_Case,a.Name_Config,a.Name_Map,a.Name_Program, 
                                        (SELECT TOP 1 IdGlobal FROM Result r WHERE r.ID_Case = t.ID_Case AND r.ID_Trials = t.ID_Trials) AS IdGlobal
                                        from dbo.TaskVisualizerList t INNER JOIN dbo.TasksAll a ON t.ID_Case = a.ID_Case AND t.ID_Trials = a.ID_Trials   WHERE VisualizeCompleted = 0";

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            VisualizerConfig temp = new VisualizerConfig((int)rdr["ID_Case"], (int)rdr["ID_Trials"], rdr["Name"].ToString(),
                                (string)rdr["Name_Case"],
                                 (string)rdr["Name_Config"],
                                 (string)rdr["Name_Map"],
                                 (string)rdr["Name_Program"],
                                 (string)rdr["IdGlobal"]);

                            tasks.Add(temp);
                        }
                    }
                }
            }

            return tasks;
        }

        private void setVisualizerConfigAsDone(VisualizerConfig task)
        {
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
                    cmd.CommandText = "update dbo.TaskVisualizerList SET  VisualizeCompleted = 1 WHERE ID_Case = @ID_Case  AND ID_Trials = @ID_Trials ";

                    cmd.Parameters.AddWithValue("@ID_Case", task.ID_Case);
                    cmd.Parameters.AddWithValue("@ID_Trials", task.ID_Trials);

                    cmd.ExecuteNonQuery();
                }
            }
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
