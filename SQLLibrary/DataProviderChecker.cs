using ResultChecker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLLibrary
{
    public static partial class SQL
    {
        public static class DataProviderChecker
        {
            public static ResultItem[] GetResultCaseList()
            {
                List<ResultItem> result = new List<ResultItem>();

                checkConnection();

                cmd.CommandText = "SELECT distinct ID_Case,ID_Trials FROM dbo.ResultChecker WHERE RobotPosition IS NOT NULL ";              

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                        result.Add(new ResultItem(rdr["ID_Case"].GetValue<int>(), rdr["ID_Trials"].GetValue<int>()));
                }

                return result.ToArray();
            }


            public static string GetResultRobotPosition(int iID_Case, int iID_Trials)
            {
                StringBuilder result = new StringBuilder();

                checkConnection();

                cmd.CommandText = "SELECT RobotPosition FROM dbo.ResultChecker WHERE ID_Case = @ID_Case AND ID_Trials = @ID_Trials AND RobotPosition IS NOT NULL";
                cmd.Parameters.AddWithValue("@ID_Case", iID_Case);
                cmd.Parameters.AddWithValue("@ID_Trials", iID_Trials);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                        result.Append(rdr["RobotPosition"].GetValue<string>());
                }

                return result.ToString();
            }

            public static void MarkResultRobot(int iID_Case, int iID_Trials,bool error)
            {
                checkConnection();

                cmd.CommandText = "UPDATE dbo.Result SET IsCollide = @ISCOLLIDE  WHERE ID_Case = @ID_Case AND ID_Trials = @ID_Trials";

                cmd.Parameters.AddWithValue("@ID_Case", iID_Case);
                cmd.Parameters.AddWithValue("@ID_Trials", iID_Trials);
                cmd.Parameters.AddWithValue("@ISCOLLIDE", error == true ? 1 : 0);

                cmd.ExecuteNonQuery();
            }

            public static void DataOptimizationChart()
            {
                checkConnection();

                cmd.CommandText = "dbo.p_DeleteResult";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
    }
}
