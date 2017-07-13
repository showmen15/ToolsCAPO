using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskRunner
{
    public class Job
    {
        public int Timeout { get; set; }

        public string exeFilePath {get;set;}
        public string sMapPath { get; set; }
        public string sArguments { get; set; }

    }
}
