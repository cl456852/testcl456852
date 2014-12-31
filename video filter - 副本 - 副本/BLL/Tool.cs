using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
    }

}
