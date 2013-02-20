using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace DB
{
    public class DBHelper
    {
        static string connstr = "server=.;uid=sa;pwd=;database=db";
        public static int ExecuteSql(string sql)
        {
            int i = 0;
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        public static SqlDataReader SearchSql(string sql)
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}
