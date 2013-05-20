using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Framework.tool
{
    public class DlTool
    {
        public string GetHtml(string url)
        {
            Console.WriteLine(url);
            string str = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Timeout = 30000;
                request.Headers.Set("Pragma", "no-cache");
                WebResponse response = request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding("GB2312");
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                str = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("GET PATE ERROR  " + url);
                Console.WriteLine(ex.Message);
                if (!ex.Message.Contains("404"))
                    str = GetHtml(url);
            }
            return str;
        }

        public void SaveFile(string content, string fileName)
        {
            //实例化一个文件流--->与写入文件相关联
            FileStream fs = new FileStream(fileName, FileMode.Create);
            //实例化一个StreamWriter-->与fs相关联
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(content);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }

        public void downLoadFile(string url,string name)
        {
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFile(url, name);
        }
    }
}
