using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using System.Linq;
using System.IO;
using System.Collections;
using MODEL;
using System.Text.RegularExpressions;

namespace BLL
{
    public class FileBLL
    {
        public static List<MyFileInfo> InsertFiles(string directoryStr)
        {

            String[] path = Directory.GetFiles(directoryStr, "*", SearchOption.TopDirectoryOnly);
            ArrayList fis = new ArrayList();
            String lowerCase;
            //String[] extensions = { ".avi", ".wmv", ".rmvb", ".iso", ".rm", ".afs", ".flv", ".pdf",".mpg","mpeg" };
            String[] extensions = { ".avi", ".wmv", ".rmvb", ".iso", ".rm", ".afs", ".flv", ".pdf", ".vob", ".rar", ".mpg", ".mpeg", ".mds", ".jpg", ".bmp", ".jpeg", ".mkv", ".dat", ".tif", ".mp4", "zip", ".mov", ".mpe", ".dat", ".bik", ".asx", ".wvx", ".mpa", ".bt!", "m4v", ".nrg", ".ogm", ".mdf" };

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
                    myFileInfo.FileName = fileInfo.Name.Replace("'", "''");
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

            StreamReader sr = new StreamReader(@"D:\pages\02-2\25.htm");
            string content = sr.ReadToEnd();
            sr.Close();
            string content1 = content.Split(new string[] { "<div class=\"image\">", "</span></div>" }, StringSplitOptions.RemoveEmptyEntries)[1];
            Console.WriteLine(content1);



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
                    if (Convert.ToInt32(minMyFileInfo.GetType().GetProperty(sortBy).GetValue(minMyFileInfo, null)) > Convert.ToInt32(oldList[i + 1].GetType().GetProperty(sortBy).GetValue(oldList[i + 1], null)))
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
                    if (minMyFileInfo.GetType().GetProperty(sortBy).GetValue(minMyFileInfo, null).ToString().CompareTo(oldList[i + 1].GetType().GetProperty(sortBy).GetValue(oldList[i + 1], null).ToString()) > 0)
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

        static string[] hisList;

        public static void process(string directoryStr)
        {


            string resultHTML = "<html><body>";
            String[] path = Directory.GetFiles(directoryStr, "*", SearchOption.TopDirectoryOnly);
            foreach (String p in path)
            {

                StreamReader sr = new StreamReader(p);
                string content = sr.ReadToEnd();
                sr.Close();
                string[] content1 = content.Split(new string[] { "<div class=\"image\">", "</span></div>" }, StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine(content1);
                for (int i = 0; i < content1.Length; i++)
                {
                    if (content1[i].Contains("JavJ.php?k=") && checkValid(content1[i]))
                        resultHTML += content1[i];
                }

            }
            resultHTML += "</body></html>";
            resultHTML = resultHTML.Replace("width=100%", "").Replace("/main/JavJ.php?file=", "http://javjunkies.com/main/JavJ.php?file=");

            FileStream fs = new FileStream(Path.Combine(directoryStr, "result.htm"), FileMode.Create);
            //实例化一个StreamWriter-->与fs相关联  
            StreamWriter sw = new StreamWriter(fs);
            //开始写入  
            sw.Write(resultHTML);
            //清空缓冲区  
            sw.Flush();
            //关闭流  
            sw.Close();
            fs.Close();

        }
        static List<MyFileInfo> list;

        private static bool checkValid(string p)
        {
            Regex r2 = new Regex(@"Video Id:.*<br");
            string id = r2.Matches(p)[0].Value.Split(new string[] { "Video Id: ", "<br" }, StringSplitOptions.RemoveEmptyEntries)[0];
            id = id.ToLower();
            if (id.EndsWith("x"))
                id = id.Substring(0, id.Length - 1);
            string letter = "";
            string number = "";
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex("[a-z]");
            for (int i = 0; i < id.Length; i++)
                if (reg1.IsMatch(id[i].ToString()))
                    letter += id[i];
                else
                    number += id[i];
            if (list == null)
                list = FileBLL.getFileList();

            string[] searchStr = { letter, number };

            bool flag = true;

            for (int i = 0; i < list.Count; i++)
            {

                flag = true;

                if ((list[i].FileName.ToLower().Contains(searchStr[0]) && list[i].FileName.ToLower().Contains(searchStr[1]) && list[i].Length > 400 || list[i].Directory.ToLower().Contains(searchStr[0]) && list[i].Directory.ToLower().Contains(searchStr[1]) && list[i].Length > 400 && !list[i].FileName.ToLower().Contains("incomplete")))
                {
                    flag = false;
                    break;

                }





            }
            if (flag)
            {
                flag = checkHis(id);
            }


            if (flag)
            {
                saveHis(id);
            }
            return flag;
        }

        public static void saveHis(string his)
        {
            StreamWriter sw = File.AppendText("his.txt");
            sw.Write("," + his);
            sw.Flush();
            sw.Close();
        }

        public static string[] hisLsit;

        public static bool checkHis(string id)
        {


            StreamReader sr = new StreamReader("his.txt");
            hisList = sr.ReadToEnd().Split(',');
            sr.Close();


            if (hisList.Contains(id))
                return false;
            else
                return true;
        }
    }
}

