using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTest
{
    public static partial class SQL
    {
        public static class DataProviderExport
        {

            public static ConfigItem[] GetExportConfigList()
            {
                List<ConfigItem> result = new List<ConfigItem>();

                checkConnection();

                cmd.CommandText = "SELECT ID_Config,Name FROM dbo.ExportConfigList ORDER BY ID_Config DESC";

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                        result.Add(new ConfigItem(rdr["ID_Config"].GetValue<int>(), rdr["Name"].GetValue<string>()));
                }

                return result.ToArray();
            }

            public static DataSet GetExportResult(int ConfigID)
            {
                string sql = string.Format("SELECT LP,WR,CP,RVO,PD FROM dbo.ResultSum where ID_Config = {0} ORDER BY LP", ConfigID);
                SqlDataAdapter dscmd = new SqlDataAdapter(sql, SQL.conn);
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                return ds;
            }
        }
    }
}
