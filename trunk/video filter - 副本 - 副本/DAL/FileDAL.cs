using System;
using System.Collections;
using System.Text;
using System.Linq;
using MODEL;
using DB;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace DAL
{
    public class FileDAL
    {

        public static int cdNo;
        public static List<MyFileInfo> Insert(ArrayList list)
        {
            List<MyFileInfo> duplicateList = new List<MyFileInfo>();
            duplicateList.Clear();

     
            string sql1 = "insert cd values (getdate())";
            string sql2 = "delete from cd where cdid=(select max(cdID) from [cd])";
            DBHelper.ExecuteSql(sql1);
            foreach (MyFileInfo fi in list)
            {
                //if(Validate(fi))
                    //if (!Check(fi))
                    //{
                        
                    //    //sql = "insert into files values('" + fi.FileName + "','1" + fi.Extension + "','" + fi.DirectoryName + "','" + fi.DirectoryName + "'," + fi.Length / 1024 / 1024 + ",'" + fi.LastAccessTime + "','" + fi.LastWriteTime + "')";
                        
                        

                        
                        
                    //}
                    //else
                if(Check(fi))
                    {
                        duplicateList.Add(fi);
                        //DBHelper.ExecuteSql(sql2);
                    }
                    cdNo = DBHelper.ExecuteInsert_SP("dbo.sp_InsertCD", fi);

            }
            return duplicateList;

        }

        private static bool Check(MyFileInfo fi)
        {
            bool flag=false;
            string sql = "select * from files where [fileName] = '" + fi.FileName + "' and [length]= "+fi.Length;
            //string sql = "select * from files where [fileName] = '" + fi.FileName + "' or [mark]='" + fi.Mark+"'";
            SqlDataReader sdr = DBHelper.SearchSql(sql);
            if (sdr.Read())
                flag = true;
            sdr.Close();
            sdr.Dispose();
            DBHelper.conn.Close();
            return flag;
        }

        public static int Update(MyFileInfo fi)
        {
            int res;
            string sql = "update files set fileName='"+fi.FileName+"',extension='"+fi.Extension+"', directoryName='"+fi.DirectoryName+"',directory='"+fi.Directory+"',length="+fi.Length+",lastAccessTime='"+fi.LastAccessTime+"',lastWriteTime='"+fi.LastWriteTime+"',mark='"+fi.Mark+"' where fileId="+fi.FileId;
            Console.WriteLine(sql);
            res= DBHelper.ExecuteSql(sql);
            return res;
        }
        //public static bool Validate(MyFileInfo fi)
        //{
        //    if (fi.Directory.Name != fi.Directory.Root.Name)
        //        return false;
        //    else
        //        return true;
        //}

        //public static bool Check(MyFileInfo fi)
        //{
        //    bool flag=true;
        //    string name=fi.Name.Substring(0,fi.Name.LastIndexOf("."));
        //    string directory=fi.Directory.Name;
        //    Regex   r1=new   Regex("^[0-9]+$");   
        //    Match   m1=r1.Match(name);
        //    Console.WriteLine(fi.Extension);

           
        //    if (!(name.Length < 4 && m1.Success))
        //    {
        //        if (name.Length > 7)
        //            name = name.Substring(name.Length - 6);
        //        else if (name.Length > 6)
        //            name = name.Substring(name.Length - 5);


        //        if (directory.Length > 7)
        //            directory = directory.Substring(name.Length - 6);
        //        else if (directory.Length > 6)
        //            directory = directory.Substring(name.Length - 5);
        //        string sql = "select * from files where [fileName] like '%" + name + "%' or [directoryName] like '%"+directory+"%' or [fileName] like '%"+directory+"%' or [directoryName] like '%"+name+"'";
                
        //        Console.WriteLine(sql);
        //        if (DBHelper.ExecuteSql(sql) <= 0)
        //            flag = false;
                
        //    }
        //    return flag;
        //}

        public static List<MyFileInfo> selectMyFileInfo(string sortBy)
        {
            string sql="";
            if (sortBy != "")
            {
                sql = "select * from [cdfile],[files] where [cdfile].fileId=[files].fileId order by " + sortBy;
            
            if(sortBy=="FileId")
                
                 sql = "select * from [cdfile],[files] where [cdfile].fileId=[files].fileId order by files."+sortBy;
            }
            else
                sql = "select * from [cdfile],[files] where [cdfile].fileId=[files].fileId and [files].length>=400";
            List<MyFileInfo> MyFileInfoList=new List<MyFileInfo>();
           
            SqlDataReader sdr= DBHelper.SearchSql(sql);
            while (sdr.Read())
            {
                
                //FileInfo fileInfo=new FileInfo(sdr["directory"].ToString());
                
                MyFileInfo myFileInfo = new MyFileInfo();
                myFileInfo.Directory = sdr["directory"].ToString();
                myFileInfo.DirectoryName = sdr["directoryName"].ToString();
                myFileInfo.FileName = sdr["fileName"].ToString();
                myFileInfo.Extension = sdr["extension"].ToString();
                myFileInfo.LastAccessTime=sdr["lastAccessTime"].ToString();
                myFileInfo.LastWriteTime = sdr["lastWriteTime"].ToString();
                myFileInfo.FileId =Convert.ToInt32( sdr["fileId"]);
                myFileInfo.Length = Convert.ToDouble(sdr["length"]);
                myFileInfo.Mark = sdr["mark"].ToString();
                myFileInfo.CdId =Convert.ToInt32( sdr["cdId"]);
                MyFileInfoList.Add(myFileInfo);

            }
            sdr.Close();
            sdr.Dispose();
            DBHelper.conn.Close();
            //DBHelper.conn.Dispose();
            return MyFileInfoList;
        }

        public static int getMaxCDID()
        {
            int maxcd;
            string sql = "select max(cdID) from [cd]";
            SqlDataReader sdr= DBHelper.SearchSql(sql);
            sdr.Read();
            maxcd = (int)sdr[0];
            sdr.Close();
            sdr.Dispose();
            DBHelper.conn.Close();
            return maxcd;
        }


    }
}
