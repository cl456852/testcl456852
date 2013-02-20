using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MODEL;
namespace DB
{
    public class DBHelper
    {
        public static string connstr = "server=china-f8661e31d\\SQLEXPRESS;uid=sa;pwd=a;database=cd";
        //static string connstr = "server=MICROSOF-8335F8\\SQLEXPRESS;uid=sa;pwd=a;database=cd";
        public static SqlConnection conn = new SqlConnection(connstr);
        //static string connstr = "server=.;uid=sa;pwd=a;database=cd";
        public static int ExecuteSql(string sql)
        {
            int i = 0;
            //using (SqlConnection conn = new SqlConnection(connstr))
           // {
                Console.WriteLine(sql);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                i = cmd.ExecuteNonQuery();
                conn.Close();

                //conn.Dispose();
            //}
            
            return i;
        }
       
        public static SqlDataReader SearchSql(string sql)
        {
            conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader sda = cmd.ExecuteReader();

            //conn.Close();
            
            return sda;
        }

        public static int ExecuteInsert_SP(string spName, MyFileInfo fi)
        {
           // using (SqlConnection conn = new SqlConnection(connstr))
            //{
                SqlCommand objCommand = new SqlCommand(spName, conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.Add("@fileName", SqlDbType.VarChar, 300).Value=fi.FileName;
                objCommand.Parameters.Add(	"@extension", SqlDbType.VarChar,50 ).Value=fi.Extension;
	            objCommand.Parameters.Add("@directoryName" ,SqlDbType.VarChar,500).Value=fi.DirectoryName;
	            objCommand.Parameters.Add("@directory", SqlDbType.VarChar,500).Value=fi.Directory;
	            objCommand.Parameters.Add("@length" ,SqlDbType.Float).Value=fi.Length;
	            objCommand.Parameters.Add("@lastAccessTime" , SqlDbType.VarChar,50).Value=fi.LastAccessTime; 
	            objCommand.Parameters.Add("@lastWriteTime", SqlDbType.VarChar,50 ).Value=fi.LastWriteTime;
                objCommand.Parameters.Add("@mark", SqlDbType.Text).Value=fi.Mark;
                conn.Open();
                int i = objCommand.ExecuteNonQuery();
                conn.Close();

                //conn.Dispose();
                return i;
                
            //}
        }
    }
}
