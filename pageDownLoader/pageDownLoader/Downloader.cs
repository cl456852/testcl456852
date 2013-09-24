using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace pageDownLoader
{
    public class Downloader
    {
   
        public string GetHtml(string url)
        {
            Console.WriteLine(url);
            string str = string.Empty;
            try
            {
                CookieContainer cookieContainer = new CookieContainer();
                Cookie __cfduid = new Cookie("__cfduid", "dfa69ebb1c444a317361c9382e5a12c011370179159", "/", ".hellojav.com");
                Cookie __utma = new Cookie("__utma", "80647694.251620245.1370179180.1371975355.1372072541.8", "/", ".hellojav.com");
                Cookie __utmb = new Cookie("__utmb", "80647694.17.10.1372072541", "/", ".hellojav.com");
                Cookie __utmc = new Cookie("__cfduid", "80647694", "/", ".hellojav.com");
                Cookie __utmz = new Cookie("__utmz", "80647694.1370179180.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/", ".hellojav.com");
                Cookie splash505833 = new Cookie("splash-505833", "dfa69ebb1c444a317361c9382e5a12c011370179159", "/", ".hellojav.com");
                Cookie auth = new Cookie("auth", "true", "/", "www.hellojav.com");
                cookieContainer.Add(__cfduid);
                cookieContainer.Add(__utma);
                cookieContainer.Add(__utmb);
                cookieContainer.Add(__utmc);
                cookieContainer.Add(__utmz);
                cookieContainer.Add(splash505833);
                cookieContainer.Add(auth);
                WebProxy proxy = new WebProxy("http://127.0.0.1:8087/", true);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.CookieContainer = cookieContainer;
                request.Referer = "http://www.hellojav.com/include/file_downpage.php?idx=";
                //request.Proxy = proxy;
                request.Headers.Set("Pragma", "no-cache");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.93 Safari/537.36";
                WebResponse response = request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding("GB2312");
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                str = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("GET PATE ERROR  "+url);
                Console.WriteLine(ex.Message);
                if (!ex.Message.Contains("404"))
                    str= GetHtml(url);
            }
            return str;
        }

        public MatchCollection findUrls(string html)
        {
            Regex r = new Regex(@"http://javjunkies.com/main/.*?/\d/");
            return r.Matches(html);
        }

        public void SaveFile(string content, string fileName)
        {
            string dir=Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
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

    }
}

