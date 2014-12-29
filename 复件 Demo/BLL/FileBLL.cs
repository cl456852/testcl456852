using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using System.Linq;
using System.IO;
using System.Collections;
using MODEL;

namespace BLL
{
    public class FileBLL
    {
        public static List<MyFileInfo> InsertFiles(string directoryStr)
        {
      
            String[] path = Directory.GetFiles(directoryStr, "*", SearchOption.AllDirectories);
            ArrayList fis = new ArrayList();
            String lowerCase;
            //String[] extensions = { ".avi", ".wmv", ".rmvb", ".iso", ".rm", ".afs", ".flv", ".pdf",".mpg","mpeg" };
            String[] extensions = { ".avi", ".wmv", ".rmvb", ".iso", ".rm", ".afs", ".flv", ".pdf", ".vob", ".rar", ".mpg", ".mpeg", ".mds", ".jpg", ".bmp", ".jpeg", ".mkv", ".dat", ".tif", ".mp4", "zip", ".mov", ".mpe", ".dat", ".bik", ".asx", ".wvx", ".mpa", ".bt!", ".m4v", ".divx", ".asf", ".nrg", ".ogm", ".mdf", ".md0", ".md1", ".md2", ".md3", ".md4", ".m4v", ".ogv", ".exe", ".rar", ".msi", ".7z", ".r00" };

            foreach (String p in path)
            {
                FileInfo fileInfo = new FileInfo(p);
                
                //Console.WriteLine(f);
                //Console.WriteLine(f.LastIndexOf("."));
                string sub = fileInfo.Extension;
                lowerCase = sub.ToLower();
                Console.WriteLine(fileInfo.Extension);

                //if (sub == "avi" || sub=="wmv"||sub=="rmvb"||sub=="iso"||sub=="rm"||sub=="afs"||sub=="flv"||sub.Contains("md")||sub=="pdf")
                if (extensions.Contains(lowerCase))
                {
                    //Console.WriteLine(f.Substring(f.LastIndexOf(".")));
                    //Console.WriteLine(p);
                    //listStr += p + "\n";
                    //fis.Add(myFileInfo);
                    MyFileInfo myFileInfo = new MyFileInfo();
                    myFileInfo.FileName = fileInfo.Name.Replace("'","''");
                    myFileInfo.Directory = fileInfo.Directory.Name;
                    myFileInfo.DirectoryName = fileInfo.DirectoryName;
                    myFileInfo.Extension = fileInfo.Extension;
                    myFileInfo.LastAccessTime = fileInfo.LastAccessTime.ToString();
                    myFileInfo.LastWriteTime = fileInfo.LastWriteTime.ToString();
                    myFileInfo.Length = (double)((int)(fileInfo.Length / 1024.0 / 1024.0 * 100)) / 100;
                       
                    myFileInfo.Mark = "";
                    fis.Add(myFileInfo);
                }
            }

            return FileDAL.Insert(fis);
        }

        public static List<MyFileInfo> getFileList()
        {
            return FileDAL.selectMyFileInfo("");
        }

        public static List<MyFileInfo> Sort(List<MyFileInfo> oldList, string sortBy)
        {
            List<MyFileInfo> newList = new List<MyFileInfo>();
             if (oldList[0].GetType().GetProperty(sortBy).GetValue(oldList[0], null).GetType() == typeof(string))
                newList = StringSort(oldList, sortBy);
            else
                newList = IntSort(oldList, sortBy);
            return newList;
        }

        private static List<MyFileInfo> IntSort(List<MyFileInfo> oldList, string sortBy)
        {
            List<MyFileInfo> newList = new List<MyFileInfo>();
            MyFileInfo minMyFileInfo = oldList[0];
            while (oldList.Count > 1)
            {
                for (int i = 0; i < oldList.Count - 1; i++)
                {
                    //Console.WriteLine("for");
                    if (Convert.ToInt32(minMyFileInfo.GetType().GetProperty(sortBy).GetValue(minMyFileInfo, null)) > Convert.ToInt32(oldList[i+1].GetType().GetProperty(sortBy).GetValue(oldList[i+1], null)))
                    {
                        minMyFileInfo = oldList[i + 1];
                       
                    }
                    
                }
                newList.Add(minMyFileInfo);
                oldList.Remove(minMyFileInfo);
                minMyFileInfo = oldList[0];

            }
            newList.Add(oldList[0]);
            return newList;
        }

        private static List<MyFileInfo> StringSort(List<MyFileInfo> oldList, string sortBy)
        {
            List<MyFileInfo> newList = new List<MyFileInfo>();
            MyFileInfo minMyFileInfo = oldList[0];
            while (oldList.Count > 1)
            {
                for (int i = 0; i < oldList.Count - 1; i++)
                {
                    //Console.WriteLine("for");
                    if (minMyFileInfo.GetType().GetProperty(sortBy).GetValue(minMyFileInfo, null).ToString().CompareTo(oldList[i+1].GetType().GetProperty(sortBy).GetValue(oldList[i+1], null).ToString())>0)
                    {
                        minMyFileInfo = oldList[i + 1];
                        
                    }
      
                }
                newList.Add(minMyFileInfo);
                oldList.Remove(minMyFileInfo);
                minMyFileInfo = oldList[0];
            }
            newList.Add(oldList[0]);

            return newList;
        }

    }


}