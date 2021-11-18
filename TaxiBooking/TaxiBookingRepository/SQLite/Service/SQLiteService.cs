using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDC.SQLite.Service
{
    public class SQLiteService
    {
        //Dictionary: Column Name - Column Type wiht limitation
        public static bool CheckOrCreateTable(IDbConnection conn, string tableName, Dictionary<string, string> colMap)
        {
            if (colMap == null || colMap.Count == 0 || tableName == null || conn == null)
                return false;

            using (IDbCommand cmd = conn.CreateCommand())
            {
                //conn.Open();
                cmd.CommandText = "SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = '" + tableName + "'";
                object ob = cmd.ExecuteScalar();
                long tableCount = Convert.ToInt64(ob);
                if (tableCount == 0)
                {
                    string strCol = "(";
                    int curIndex = 0;
                    foreach (KeyValuePair<string, string> col in colMap)
                    {
                        if (curIndex > 0)
                        {
                            strCol += ", ";
                        }
                        curIndex++;

                        strCol += col.Key + " " + col.Value;
                    }
                    strCol += ");";

                    string insertStr = "BEGIN; create table " + tableName + strCol + " COMMIT;";
                    cmd.CommandText = insertStr;

                    int rowCount = cmd.ExecuteNonQuery();

                }
                return true;
            }
        }
        public static bool CheckOrCreateTable(string tableName, Dictionary<string, string> colMap)
        {
            using (IDbConnection conn = SQLiteHelper.CreateConnection())
            {
                return CheckOrCreateTable(conn, tableName, colMap);
            }
        }
    }
}
