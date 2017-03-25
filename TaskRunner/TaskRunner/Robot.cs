using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace TaskRunner
{
    public delegate void ProcessExit(string output, string error);

    public class Robot
    {
        public event ProcessExit ProcessExitCmd;

        private Process process = new Process();

        public bool HasExited
        {
            get
            {
                return process.HasExited;
            }
        }

        public bool isWorking { get; private set; }

        private Task task;

        public string output { get; private set; }
        public string err { get; private set; }

        public Robot(string exeFilePath, long ConfigId)
        {
            isWorking = false;

            process.StartInfo.FileName = "java.exe";
            process.StartInfo.Arguments = string.Format(" -jar {0} {1}", exeFilePath, ConfigId.ToString());
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            task = new Task(new Action(run));
        }

        private void run()
        {
            process.Start();
            process.WaitForExit();

            output = process.StandardOutput.ReadToEnd();
            err = process.StandardError.ReadToEnd();

            isWorking = false;

            if (ProcessExitCmd != null)
                ProcessExitCmd(output, err);
        }

        public void Start()
        {
            task.Start();
            isWorking = true;
        }

        public void Kill()
        {
            process.Kill();
            isWorking = false;
        }

       /* Process process = new Process();
            process.StartInfo.FileName = "java.exe";
            process.StartInfo.Arguments = @" -jar D:\Desktop\vis.jar"; // Note the /c command (*)
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            //* Read the output (or the error)
            string output = process.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
            string err = process.StandardError.ReadToEnd();
            Console.WriteLine(err);
            process.WaitForExit();*/
    }
}
