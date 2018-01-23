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

            public static ConfigItem[] GetExportConfigList(MapItem map)
            {
                List<ConfigItem> result = new List<ConfigItem>();

                checkConnection();

                cmd.CommandText = "SELECT ID_Config,Name FROM dbo.ExportConfigList WHERE ID_Map = @ID_Map ORDER BY ID_Config ASC";
                cmd.Parameters.AddWithValue("@ID_Map", map.ID_Map);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                        result.Add(new ConfigItem(rdr["ID_Config"].GetValue<int>(), rdr["Name"].GetValue<string>()));
                }

                return result.ToArray();
            }

            public static DataSet GetExportResult(int ConfigID)
            {
                string sql = string.Format("SELECT LP,WR,CP,RVO,PD, WR_NEW, CP_NEW FROM dbo.ResultSum where ID_Config = {0} ORDER BY LP", ConfigID);
                SqlDataAdapter dscmd = new SqlDataAdapter(sql, SQL.conn);
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                return ds;
            }


            public static MapItem[] GetExportMapList()                
            {
                List<MapItem> result = new List<MapItem>();

                checkConnection();

                //cmd.CommandText = "SELECT distinct ID_Map,MapName FROM dbo.ExportConfigList where ID_Map IN(15,6,16,21)  order by ID_Map";
                cmd.CommandText = "SELECT distinct ID_Map,MapName FROM dbo.ExportConfigList order by ID_Map";

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                        result.Add(new MapItem(rdr["ID_Map"].GetValue<int>(), rdr["MapName"].GetValue<string>()));
                }

                return result.ToArray();
            }

            private static void DeleteDunnStatistic(MapItem map, ConfigItem config)
            {
                checkConnection();
                cmd.CommandText = @"DELETE FROM [dbo].[DunnValues] WHERE [ID_Map] = @ID_Map AND [ID_Config] = @ID_Config";

                cmd.Parameters.AddWithValue("@ID_Map", map.ID_Map);
                cmd.Parameters.AddWithValue("@ID_Config", config.ConfigID);

                cmd.ExecuteNonQuery();
            }

            public static void InsertDunnStatistic(MapItem map, ConfigItem config, RExporterResultItem[] testResult)
            {
                DeleteDunnStatistic(map, config);

                checkConnection();
                cmd.CommandText = @"INSERT INTO [dbo].[DunnValues] ([ID_Map],[ID_Config],[Dunn_ID_Form],[Dunn_ID_To],[Dunn_Value],[Dunn_Desc],[Dunn_Test_Org],[Kw_Test_Org],[Kw_Test]) 
                    VALUES(@ID_Map, @ID_Config, @Dunn_ID_Form, @Dunn_ID_To, @Dunn_Value, @Dunn_Desc,@Dunn_Test_Org,@Kw_Test_Org,@Kw_Test)";


                cmd.Parameters.AddWithValue("@ID_Map", map.ID_Map);
                cmd.Parameters.AddWithValue("@ID_Config", config.ConfigID);
                

                cmd.Parameters.Add("@Dunn_ID_Form", SqlDbType.Int);
                cmd.Parameters.Add("@Dunn_ID_To", SqlDbType.Int);
                cmd.Parameters.Add("@Dunn_Value", SqlDbType.Real);
                cmd.Parameters.Add("@Dunn_Desc", SqlDbType.VarChar);
                cmd.Parameters.Add("@Dunn_Test_Org", SqlDbType.VarChar);
                cmd.Parameters.Add("@Kw_Test_Org", SqlDbType.VarChar);
                cmd.Parameters.Add("@Kw_Test", SqlDbType.Real);                

                foreach (var item in testResult)
                {
                    cmd.Parameters["@Dunn_ID_Form"].Value = item.Dunn_ID_Form;
                    cmd.Parameters["@Dunn_ID_To"].Value = item.Dunn_ID_To;
                    cmd.Parameters["@Dunn_Value"].Value = item.Dunn_Value;
                    cmd.Parameters["@Dunn_Desc"].Value = item.Dunn_Desc;
                    cmd.Parameters["@Dunn_Test_Org"].Value = item.Dunn_Test_Org;
                    cmd.Parameters["@Kw_Test_Org"].Value = item.Kw_Test_org;
                    cmd.Parameters["@Kw_Test"].Value = item.Kw_Test;

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
