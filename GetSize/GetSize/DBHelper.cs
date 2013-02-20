using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using GetSize;
using System.IO;
namespace DB
{
    public class DBHelper
    {
        public static string connstr = @"server=localhost\SQLEXPRESS;uid=sa;pwd=a;database=cd";
        public static string count = "  select * from his where vid='{0}'";

        public static string update = " update his set size={0}, fileCount={1},actress='{2}',files='{3}' where vid='{4}'";
        //static string connstr = "server=MICROSOF-8335F8\\SQLEXPRESS;uid=sa;pwd=a;database=cd";
        public static SqlConnection conn = new SqlConnection(connstr);
        //static string connstr = "server=.;uid=sa;pwd=a;database=cd";

        static string insertHisSql = "insert into his values('{0}',{1},'{2}',{3},'{4}')";
        static string select0size = "select * from his where size=0";

        static string selectVid = "select * from his1";

        static string selectDiffSize = "select * from his1 where vid like '{0}%' and size!={1}";

        public static Dictionary<string, string> dic = new Dictionary<string, string>();
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

        public static void insertHis(string vid,double size, string actress,int fileCount,string files )
        {
            string sql= string.Format(insertHisSql,vid,size,actress,fileCount,files);
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand sc = new SqlCommand(sql, conn);
                sc.ExecuteNonQuery();
                conn.Close();
             }
            
        }

        public static int getCount(string vid)
        {
            int res = 0;
            string sql = string.Format(count, vid);
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand sc = new SqlCommand(sql, conn);
                res= Convert.ToInt32( sc.ExecuteScalar());
                conn.Close();
            }
            return res;
        }

        public static ArrayList getSize0IDs()
        {
            ArrayList list = new ArrayList();
            SqlDataReader sdr = null;
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand sc = new SqlCommand(select0size, conn);

                sdr = sc.ExecuteReader();
                while (sdr.Read())
                {
                    list.Add(sdr["vid"].ToString());
                }
            }
         
            return list;

        }

        public static void UpdateInfo(His his)
        {
            string sql = string.Format(update, his.Size, his.FileCount, his.Actress, his.Files,his.Vid);
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand sc = new SqlCommand(sql, conn);
                sc.ExecuteNonQuery();
              
            }
        }

        public static ArrayList getList()
        {
            ArrayList list = new ArrayList();
            SqlDataReader sdr = null;
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand sc = new SqlCommand(selectVid, conn);

                sdr = sc.ExecuteReader();
                while (sdr.Read())
                {
                    His his =new His();
                    his.Vid=sdr["vid"].ToString();
                    his.Size=Convert.ToDouble( sdr["size"].ToString());
                    list.Add(his);
                }
            }

            return list;

        }

        public static void getDiffSizeList(His his)
        {
            string sql = string.Format(selectDiffSize, his.Vid, his.Size);
            ArrayList list = new ArrayList();
            SqlDataReader sdr = null;
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand sc = new SqlCommand(sql, conn);

                sdr = sc.ExecuteReader();
                while (sdr.Read())
                {
                    His his1 = new His();
                    his1.Vid = sdr["vid"].ToString();
                    his1.Size = Convert.ToDouble(sdr["size"].ToString());
                    if (!dic.ContainsKey(his1.Vid))
                    {
                        dic.Add(his1.Vid, "");
                        StreamWriter sw = File.AppendText("dictionaryList.txt");
                        sw.Write(his1.Vid + ",");
                        sw.Flush();
                        sw.Close();
                    }
                    Console.WriteLine(his1.Vid);
                }
            }
        }

        //public static int ExecuteInsert_SP(string spName, MyFileInfo fi)
        //{
        //   // using (SqlConnection conn = new SqlConnection(connstr))
        //    //{
        //        SqlCommand objCommand = new SqlCommand(spName, conn);
        //        objCommand.CommandType = CommandType.StoredProcedure;
        //        objCommand.Parameters.Add("@fileName", SqlDbType.VarChar, 300).Value=fi.FileName;
        //        objCommand.Parameters.Add(	"@extension", SqlDbType.VarChar,50 ).Value=fi.Extension;
        //        objCommand.Parameters.Add("@directoryName" ,SqlDbType.VarChar,500).Value=fi.DirectoryName;
        //        objCommand.Parameters.Add("@directory", SqlDbType.VarChar,500).Value=fi.Directory;
        //        objCommand.Parameters.Add("@length" ,SqlDbType.Float).Value=fi.Length;
        //        objCommand.Parameters.Add("@lastAccessTime" , SqlDbType.VarChar,50).Value=fi.LastAccessTime; 
        //        objCommand.Parameters.Add("@lastWriteTime", SqlDbType.VarChar,50 ).Value=fi.LastWriteTime;
        //        objCommand.Parameters.Add("@mark", SqlDbType.Text).Value=fi.Mark;
        //        conn.Open();
        //        int i = objCommand.ExecuteNonQuery();
        //        conn.Close();

        //        //conn.Dispose();
        //        return i;
                
        //    //}
        //}



    }
}
