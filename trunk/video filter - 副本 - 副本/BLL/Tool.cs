using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace BLL
{
    public class Tool
    {
        public static bool IsNum(String str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsNumber(str, i))
                    return false;
            }
            return true;
        }

        public static void WriteFile(string path, string content)
        {
            FileStream fs1 = new FileStream(path, FileMode.Create);
            //实例化一个StreamWriter-->与fs相关联  
            StreamWriter sw1 = new StreamWriter(fs1);
            //开始写入  
            sw1.Write(content);
            //清空缓冲区  
            sw1.Flush();
            //关闭流  
            sw1.Close();
            fs1.Close();
        }

        public static string[] getId(string id)
        {
            Regex reg1 = new Regex("[a-z]");
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
            return searchStr;
        }
    }

}
