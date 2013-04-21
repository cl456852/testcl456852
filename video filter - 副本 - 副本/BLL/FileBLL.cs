using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using System.Linq;
using System.IO;
using System.Collections;
using MODEL;
using System.Text.RegularExpressions;
using GetSize;

namespace BLL
{
    public class FileBLL
    {

    //    Analysis ana;
        _141javAnalysis ana;
        Filter filter;

        public FileBLL()
        {
            filter = new Filter();
           // ana = new Analysis();
            ana = new _141javAnalysis();
        }

        public List<MyFileInfo> getFileList()
        {
            return FileDAL.selectMyFileInfo("");
        }

        public void process(string directoryStr)
        {
            string resultHTML = "<html><body>";
            String[] path = Directory.GetFiles(directoryStr, "*", SearchOption.TopDirectoryOnly);
            foreach (String p in path)
            {
                Console.WriteLine(p);
                StreamReader sr = new StreamReader(p);
                string content = sr.ReadToEnd();

                sr.Close();
                
                ArrayList list= ana.alys(content,Path.Combine(directoryStr,"nIn1.htm"));
                foreach (His his in list)
                {
                    if (filter.checkValid(his))
                    {
                        resultHTML += his.Html;
                    }
                }

            }
            resultHTML += "</body></html>";
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

       
    }
}

