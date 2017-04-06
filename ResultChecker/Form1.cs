using SQLLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResultChecker
{
    public partial class Form1 : Form
    {
        public static double RobotSize = 0.3;
        public static int ErrorCount;

        public Form1()
        {
            InitializeComponent();

            SQL.ConnectionString = @"data source=WR-7-BASE-74\SQLEXPRESS;initial catalog=Doktorat;Integrated Security=SSPI;";
            //  SQL.ConnectionString = @"data source=SZYMON-KOMPUTER;initial catalog=Doktorat;Integrated Security=SSPI;";

            //  SQL.ConnectionString = @"data source=SZSZ\SQLEXPRESS;initial catalog=Doktorat; User Id=szsz; Password=szsz;";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ErrorCount = 0;
            lblErrorCount.Text = ErrorCount.ToString();

            ResultItem[] robotItems = SQL.DataProviderChecker.GetResultCaseList();

            Parallel.ForEach(robotItems, robot =>
            {
                try
                {
                    Monitor.Enter(robotItems);
                    string robotPosition = SQL.DataProviderChecker.GetResultRobotPosition(robot.ID_Case, robot.ID_Trials);
                    Monitor.Exit(robotItems);

                    string[] splitRobotPosition = robotPosition.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                    List<RobotPositionItem> robotPositionItems = new List<RobotPositionItem>();

                    foreach (var item in splitRobotPosition)
                        robotPositionItems.Add(new RobotPositionItem(item));

                    int loop = 1;
                    IEnumerable<RobotPositionItem> result;
                    bool error = false;

                    do
                    {
                        result = robotPositionItems.Where(ss => ss.Loop == loop);

                        foreach (RobotPositionItem state1 in result)
                        {
                            foreach (RobotPositionItem state2 in result)
                            {
                                if (state1.ID_Robot == state2.ID_Robot)
                                    continue;
                                else
                                {
                                    error = (state1.Distance(state2) <= RobotSize);
                                    break;
                                }
                            }
                        }

                        loop++;
                    }
                    while ((result.Count() > 1) && !error);

                    Monitor.Enter(robotItems);
                    SQL.DataProviderChecker.MarkResultRobot(robot.ID_Case, robot.ID_Trials, error);

                    if (error)
                        ErrorCount += 1;

                    Monitor.Exit(robotItems);
                }
                catch(Exception ex)
                {

                }
            });

            lblErrorCount.Text = ErrorCount.ToString();

        }
    }
}
