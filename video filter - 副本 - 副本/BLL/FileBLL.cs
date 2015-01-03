using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using System.Linq;
using System.IO;
using System.Collections;
using MODEL;
using System.Text.RegularExpressions;
using BLL;

namespace BLL
{
    public class FileBLL
    {

    //    Analysis ana;
        //_18javAnaysis ana;
        //HelloJavAnalysis ana;
        _141javAnalysisNew ana;
        Filter filter;

        public FileBLL()
        {
            filter = new Filter();
           // ana = new Analysis();
            ana = new _141javAnalysisNew();
          //  ana = new _18javAnaysis();
        }

        public List<MyFileInfo> getFileList()
        {
            return FileDAL.selectMyFileInfo("");
        }

        public void process(string directoryStr)
        {
            string invalidHTML="<html><body>";
            ArrayList htmlList = new ArrayList();
            ArrayList hisList = new ArrayList();
            string resultHTML = "<html><body>";
            String[] path = Directory.GetFiles(directoryStr, "*", SearchOption.TopDirectoryOnly);
            foreach (String p in path)
            {
                Console.WriteLine(p);
                StreamReader sr = new StreamReader(p);
                string content = sr.ReadToEnd();

                sr.Close();
                string[] strs = Path.GetFileNameWithoutExtension(p).Split('_');
                string vid="";
                if (strs.Length >= 1)
                    vid = strs[1];
                ArrayList list= ana.alys(content,Path.Combine(directoryStr,"nIn1.htm"),vid);
                foreach (His his in list)
                {
                    if (filter.checkValid(his))
                    {
                        hisList.Add(his);
                    }
                    else
                        invalidHTML += his.Html;
                }

            }
            hisList.Sort();
            foreach (His his in hisList)
            {
                resultHTML += his.Html;
            }
            resultHTML += "</body></html>";
            invalidHTML += "</body></html>";
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

            FileStream fs1 = new FileStream(Path.Combine(directoryStr, "invalid.htm"), FileMode.Create);
            //实例化一个StreamWriter-->与fs相关联  
            StreamWriter sw1 = new StreamWriter(fs1);
            //开始写入  
            sw1.Write(invalidHTML);
            //清空缓冲区  
            sw1.Flush();
            //关闭流  
            sw1.Close();
            fs1.Close();


        }

       
    }
}

