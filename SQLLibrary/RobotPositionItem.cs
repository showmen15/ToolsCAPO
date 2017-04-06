using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultChecker
{
    public class RobotPositionItem
    {
        public int Loop { get; set; }
        public int ID_Robot { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public RobotPositionItem(string sRobotPosition)
        {
            string[] splitRobotPosition = sRobotPosition.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            ID_Robot = int.Parse(splitRobotPosition[0]);
            Loop = int.Parse(splitRobotPosition[1]);
            X = double.Parse(splitRobotPosition[2].Replace(".", ","));
            Y = double.Parse(splitRobotPosition[3].Replace(".", ","));
        }

        public double Distance(RobotPositionItem item)
        {
            return Math.Sqrt(Math.Pow(X - item.X, 2) + Math.Pow(Y - item.X, 2));
        }
    }
}
