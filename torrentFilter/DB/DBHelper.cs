using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MODEL;
using GetSize;
namespace DB
{
    public class DBHelper
    {
        static string seachHisSql = "select * from his1 where LOWER(vid)=LOWER('{0}') and ABS((size-{1})/{1})<0.05 ";
        static string insertHisSql = "insert into his1 values('{0}',{1},'{2}',{3},'{4}',getdate())";
        static string checkTorrentSql = "select count(*) from his where LOWER([file])='{0}' and size> 104857600";
        static string insertTorrentSql = "insert into his values('{0}',{1},'{2}',getdate(),'{3}','{4}')";
        public static string connstr = @"server=localhost;uid=sa;pwd=a;database=cd";
        static string checkFilesSql = "select count(*) from files where filename='{0}' and length>60";

        static string checkUnknownTorrentsSql = "select count(*) from files where directory like '{0}%'";
        //static string connstr = "server=MICROSOF-8335F8\\SQLEXPRESS;uid=sa;pwd=a;database=cd";
        public static SqlConnection conn = new SqlConnection(connstr);
        static string checkFilesSql1 = "  select * from his where ";
        //static string connstr = "server=.;uid=sa;pwd=a;database=cd";
       // static string filterString = "_avc_hd,_avc,_hd,720p,1080p,480p,_,480,720,1080,2160p,[,],.,2000,4000,8000,12000,6000,1500, ,";
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

        public static int searchHis(His his)
        {
            int res;
            string sql="";
            if (his.Size > 0)
            {

                sql = string.Format(seachHisSql, his.Vid, his.Size);
            }
            else
                return 0;
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand sc = new SqlCommand(sql, conn);
                res = Convert.ToInt32(sc.ExecuteScalar());
            }
            return res;
        }

        public static void insertHis(His his)
        {
            if (his.Actress.Length > 250)
                his.Actress = his.Actress.Substring(0, 248);
            string sql = string.Format(insertHisSql, his.Vid, his.Size,his.Actress.Replace("'","''"), his.FileCount, his.Files.Replace("'","''"));
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand sc = new SqlCommand(sql, conn);
                sc.ExecuteNonQuery();
            }

        }

        public static int checkTorrent(HisTorrent his)
        {
            int res = 0;
            string sql = string.Format(checkTorrentSql, his.File.Replace("'","''").ToLower());
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand sc = new SqlCommand(sql, conn);
                res = Convert.ToInt32( sc.ExecuteScalar());
            }
            return res;
        }

        public static void insertTorrent(HisTorrent his)
        {
            string sql = string.Format(insertTorrentSql, his.File.Replace("'","''"), his.Size, his.Path.Replace("'","''"), his.Ext,his.Md5 );
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand sc = new SqlCommand(sql, conn);
                sc.ExecuteNonQuery();
            }
        }

        public static int checkFiles(HisTorrent his,string filter)
        {
            int res = 0;
            string sql = string.Format(checkFilesSql, his.File.Replace("'", "''")+ his.Ext);
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand sc = new SqlCommand(sql, conn);
                res = Convert.ToInt32(sc.ExecuteScalar());
            }
            if (res > 0)
                Console.WriteLine(his.Path + "  already downloaded");
            return res;
        }

        public static int checkUnknownTorrents(string s)
        {
            int res = 0;
            string sql = string.Format(checkUnknownTorrentsSql,s);
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand sc = new SqlCommand(sql, conn);
                res = Convert.ToInt32(sc.ExecuteScalar());
            }
            return res;
        }

        public static Dictionary<string, HisTorrent> getList(string filterStr)
        {
            string[] strs = filterStr.Split(',');

            string filterStr1 = " REPLACE(LOWER([file]),'" + strs[0] + "','') ";
            for (int i = 1; i < strs.Length; i++)
            {
         
                filterStr1 = "REPLACE(" + filterStr1 + ",'" + strs[i] + "','') ";
            }
            Dictionary<string, HisTorrent> dic = new Dictionary<string, HisTorrent>();

            string sql = "select " + filterStr1 + " as [file] ,createtime,[path],[size],ext,md5 from his where size>100*1024*1024 and DATEDIFF(M,createtime,GETDATE())<=500 order by createtime";
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand sc = new SqlCommand(sql, conn);
                SqlDataReader reader = sc.ExecuteReader();
                while (reader.Read())
                {
                    HisTorrent t = new HisTorrent();
                    t.File = reader["file"].ToString();
                    t.CreateTime = Convert.ToDateTime(reader["createtime"]);
                    t.Ext = reader["ext"].ToString();
                    t.Path = reader["path"].ToString();
                    t.Size = Convert.ToInt64(reader["size"].ToString());
                    t.Md5 = reader["md5"].ToString();
                    try
                    {
                        dic.Add(t.File, t);
                    }
                    catch (ArgumentException e)
                    {
                        dic.Remove(t.File);
                        dic.Add(t.File, t);
                    }
                }
                return dic;

            }
        }

        public static Dictionary<string, HisTorrent> getFileList(string filterStr)
        {
            Dictionary<string, HisTorrent> dic = new Dictionary<string, HisTorrent>();
            string sql = "select fileName from files where length>70";
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand sc = new SqlCommand(sql, conn);
                SqlDataReader reader = sc.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        string fileName=filterName(reader[0].ToString(),filterStr);
                        dic.Add(fileName, new HisTorrent());
                    }
                    catch (ArgumentException e)
                    {
                    }
                }
                return dic;

            }
        }

        static string filterName(string fileName, string filterString)
        {
            fileName = fileName.ToLower();
            if (fileName.LastIndexOf('.') > 0)
            {
                fileName = fileName.Substring(0, fileName.LastIndexOf('.'));
            }
            string[] strs = filterString.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in strs)
            {
                fileName = fileName.Replace(s, "");
            }
            return fileName;
        }
        
    }
}
