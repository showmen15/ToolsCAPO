using ResultChecker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;

namespace SQLLibrary
{
    public static partial class SQL
    {
        public static class DataProviderTaskVisualizer
        {
            //public static void GetResultCaseList()
            //{
            //    // List<ResultItem> result = new List<ResultItem>();

            //    checkConnection();

            //    cmd.CommandText = "SELECT distinct ID_Case,ID_Trials FROM dbo.ResultChecker WHERE RobotPosition IS NOT NULL ";

            //    using (SqlDataReader rdr = cmd.ExecuteReader())
            //    {
            //        while (rdr.Read())
            //            result.Add(new ResultItem(rdr["ID_Case"].GetValue<int>(), rdr["ID_Trials"].GetValue<int>()));
            //    }

            //    return result.ToArray();
            //}


            public static List<VisualizerConfig> GetVisualizerConfig()
            {
                List<VisualizerConfig> tasks = new List<VisualizerConfig>();

                checkConnection();

                cmd.CommandText = @"select t.ID_Case,t.ID_Trials,t.Name,a.Name_Case,a.Name_Config,a.Name_Map,a.Name_Program, 
                                        (SELECT TOP 1 IdGlobal FROM Result r WHERE r.ID_Case = t.ID_Case AND r.ID_Trials = t.ID_Trials) AS IdGlobal
                                        from dbo.TaskVisualizerList t INNER JOIN dbo.TasksAll a ON t.ID_Case = a.ID_Case AND t.ID_Trials = a.ID_Trials   WHERE VisualizeCompleted = 0 order by t.ID_Case,t.ID_Trials";

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

                return tasks;
            }

            public static void SetVisualizerConfigAsDone(VisualizerConfig task)
            {
                checkConnection();

                cmd.CommandText = "update dbo.TaskVisualizerList SET  VisualizeCompleted = 1 WHERE ID_Case = @ID_Case  AND ID_Trials = @ID_Trials ";

                cmd.Parameters.AddWithValue("@ID_Case", task.ID_Case);
                cmd.Parameters.AddWithValue("@ID_Trials", task.ID_Trials);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
