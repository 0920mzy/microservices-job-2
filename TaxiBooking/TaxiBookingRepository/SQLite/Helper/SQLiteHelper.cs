using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MDC.SQLite
{
    public class SQLiteHelper
    {
        private readonly static string connStr = ConfigurationManager.ConnectionStrings["mdc_db"].ConnectionString;

        //get connection object
        public static IDbConnection CreateConnection()
        {
            IDbConnection conn = new SQLiteConnection(connStr);
            conn.Open();
            return conn;
        }

        //exec non query statement
        public static int ExecuteNonQuery(IDbConnection conn, string sql, Dictionary<string, object> parameters)
        {
            using (IDbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                foreach (KeyValuePair<string, object> keyValuePair in parameters)
                {
                    IDbDataParameter parameter = cmd.CreateParameter();
                    parameter.ParameterName = keyValuePair.Key;
                    parameter.Value = keyValuePair.Value;
                    cmd.Parameters.Add(parameter);
                }
                return cmd.ExecuteNonQuery();
            }
        }

        //exec non query statement - create connection
        public static int ExecuteNonQuery(string sql, Dictionary<string, object> parameters)
        {
            using (IDbConnection conn = CreateConnection())
            {
                return ExecuteNonQuery(conn, sql, parameters);
            }
        }

        //Get first line data
        public static object ExecuteScalar(IDbConnection conn, string sql, Dictionary<string, object> parameters)
        {
            using (IDbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                foreach (KeyValuePair<string, object> keyValuePair in parameters)
                {
                    IDbDataParameter parameter = cmd.CreateParameter();
                    parameter.ParameterName = keyValuePair.Key;
                    parameter.Value = keyValuePair.Value;
                    cmd.Parameters.Add(parameter);
                }
                return cmd.ExecuteScalar();
            }
        }

        //Get first line data - create connection
        public static object ExecuteScalar(string sql, Dictionary<string, object> parameters)
        {
            using (IDbConnection conn = CreateConnection())
            {
                return ExecuteScalar(conn, sql, parameters);
            }
        }

        //query table
        public static DataTable ExecuteQuery(IDbConnection conn, string sql, Dictionary<string, object> parameters)
        {
            DataTable dt = new DataTable();
            using (IDbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                foreach (KeyValuePair<string, object> keyValuePair in parameters)
                {
                    IDbDataParameter parameter = cmd.CreateParameter();
                    parameter.ParameterName = keyValuePair.Key;
                    parameter.Value = keyValuePair.Value;
                    cmd.Parameters.Add(parameter);
                }
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }

            return dt;
        }

        //Query table - create connection
        public static DataTable ExecuteQuery(string sql, Dictionary<string, object> parameters)
        {
            using (IDbConnection conn = CreateConnection())
            {
                return ExecuteQuery(conn, sql, parameters);
            }
        }


    }
}
