using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MODEL;
using DAL;
using System.IO;
using DB;
using System.Text.RegularExpressions;


namespace BLL
{
    public class Filter
    {
        List<MyFileInfo> list;
        Regex reg1 = new Regex("[a-z]");
        Regex reg2 = new Regex("[a-z].*");
        public Filter()
        {

            list = getFileList();
        }

        public bool checkValid(His his)
        {
            string id = his.Vid;
            if (id==null|| id == "")
            {
                return true;
            }
            id = id.ToLower();
            bool flag = true;
            if (Tool.IsNum(id))
            {
                foreach (MyFileInfo info in list)
                {
                    if (info.FileName.Contains(id) || info.Directory.Contains(id))
                    {
                        flag = false;
                        break;
                    }
                }
        
            }
            else
            {
                id = reg2.Match(id).Value;
                string letter = "";
                string number = "";
                bool isEndofLetter = false;
                for (int i = 0; i < id.Length; i++)                        //修改   对于出现KIDM235A  KIDM235B
                    if (reg1.IsMatch(id[i].ToString()))
                    {
                        if (isEndofLetter)
                            break;
                        else
                            letter += id[i];
                    }
                    else
                    {
                        number += id[i];
                        isEndofLetter = true;
                    }

                string[] searchStr = { letter, number };


                Regex r = new Regex("[^a-z]" + searchStr[0] + @"(\s){0,3}[-_]?(\s){0,3}(0){0,3}" + searchStr[1] + "[^0-9]|^" + searchStr[0] + @"(\s){0,3}[-_]?(\s){0,3}(0){0,3}" + searchStr[1] + "$|[^a-z]" + searchStr[0] + @"(\s){0,3}[-_]?(\s){0,3}(0){0,3}" + searchStr[1] + "$|^" + searchStr[0] + @"(\s){0,3}[-_]?(\s){0,3}(0){0,3}" + searchStr[1] + "[^0-9]");
                for (int i = 0; i < list.Count; i++)
                {

                    flag = true;

                    string fileName = Path.GetFileNameWithoutExtension(list[i].FileName.ToLower());
                    string directoryName = list[i].Directory.ToLower();
                    string extension = list[i].Extension;
                    double len = list[i].Length;
                    //if (((fileName.Contains(searchStr[0]) && fileName.Contains(searchStr[1]) && checkDistance(fileName, searchStr[0], searchStr[1]) || directoryName.Contains(searchStr[0]) && directoryName.Contains(searchStr[1]) && checkDistance(directoryName, searchStr[0], searchStr[1])) && !fileName.Contains("incomplete")) && (len > 400||extension.ToLower()==".mds"))
                    //{
                    //    flag = false;
                    //    break;

                    //}

                    if ((len*1.7>his.Size || extension.ToLower() == ".mds"&&his.Size<5000) && (r.IsMatch(fileName) || r.IsMatch(directoryName)) && !fileName.Contains("incomplete"))
                    {
                        flag = false;
                        break;

                    }


                }

                }

                if (flag)
                {
                    flag = checkHis(his);
                }




                if (flag)
                {
                    DBHelper.insertHis(his);
                }
            
            return flag;
        }

        bool checkHis(His his)
        {
            if (DBHelper.searchHis(his) > 0)
                return false;
            else
                return true;
        }

        public List<MyFileInfo> getFileList()
        {
            return FileDAL.selectMyFileInfo("");
        }
        //public bool checkDistance(string s, string s0, string s1)
        //{
        //    s = s.ToLower();
        //    int dis = s.IndexOf(s1) - s.IndexOf(s0) - s0.Length;
        //    if (dis < 5 && dis >= 0)
        //        return true;
        //    else
        //        return false;
        //}

        //bool checkDistance(string s, string s0, string s1)
        //{

        //    Regex r = new Regex("[^a-z]" + s0 + @"(\s){0,3}[-_]?(\s){0,3}(0){0,3}" + s1 + "[^0-9]|^" + s0 + @"(\s){0,3}[-_]?(\s){0,3}(0){0,3}" + s1 + "$|[^a-z]" + s0 + @"(\s){0,3}[-_](\s){0,3}(0){0,3}" + s1 + "$|^" + s0 + @"(\s){0,3}[-_]?(\s){0,3}(0){0,3}" + s1 + "[^0-9]");
        //    return r.IsMatch(s);

        //}
    }
}
