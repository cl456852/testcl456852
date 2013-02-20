using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MODEL;
using DAL;
using System.IO;

namespace GetSize
{
    public class Filter
    {
        bool checkHis = false;
        List<MyFileInfo> list;
        public Filter()
        {
            
            list= getFileList();
        }

        public bool checkValid(string id)
        {
            if (id == "")
            {
                return false;
            }
            id = id.ToLower();
            if (id.EndsWith("x"))
                id = id.Substring(0, id.Length - 1);
            bool flag = true;
            if (id != "")
            {
                string letter = "";
                string number = "";
                System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex("[a-z]");
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



                for (int i = 0; i < list.Count; i++)
                {

                    flag = true;

                    string fileName = Path.GetFileNameWithoutExtension(list[i].FileName.ToLower());
                    string directoryName = list[i].Directory.ToLower();
                    double len = list[i].Length;
                    if (((fileName.Contains(searchStr[0]) && fileName.Contains(searchStr[1]) && checkDistance(fileName, searchStr[0], searchStr[1]) || directoryName.Contains(searchStr[0]) && directoryName.Contains(searchStr[1]) && checkDistance(directoryName, searchStr[0], searchStr[1])) && !fileName.Contains("incomplete")) && len > 400)
                    {
                        flag = false;
                        break;

                    }





                }
                if (checkHis)
                {
                    if (flag)
                    {
                        //    flag = checkHis(id);
                    }
                }
            }


            if (flag)
            {

            }
            return flag;
        }

        public List<MyFileInfo> getFileList()
        {
            return FileDAL.selectMyFileInfo("");
        }
        private bool checkDistance(string s, string s0, string s1)
        {
            s = s.ToLower();
            int dis = s.IndexOf(s1) - s.IndexOf(s0) - s0.Length;
            if (dis < 5 && dis >= 0)
                return true;
            else
                return false;
        }


    }
}
