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
                //d6f378f8186966bdafb043a16dad43edb1401883196292
                //GA1.2.893403694.1401883248
                //a056e8cef0f53a4c1da34ce4ce69383e9692659c-1402136201-1800
                CookieContainer cookieContainer = new CookieContainer();
                Cookie __cfduid = new Cookie("__cfduid", "d6f378f8186966bdafb043a16dad43edb1401883196292", "/", ".hellojav.com");
                Cookie __utma = new Cookie("_ga", "GA1.2.893403694.1401883248", "/", ".hellojav.com");
                Cookie __utmb = new Cookie("cf_clearance", "d7bff3f36aedc2fabd63e4beeaaeccdd15b147c0-1402134125-1800", "/", ".hellojav.com");
              

                Cookie auth = new Cookie("auth", "true", "/", "www.hellojav.com");
                cookieContainer.Add(__cfduid);
                cookieContainer.Add(__utma);
                cookieContainer.Add(__utmb);
                cookieContainer.Add(auth);
                WebProxy proxy = new WebProxy("http://127.0.0.1:8087/", true);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.CookieContainer = cookieContainer;
                request.Referer = "http://www.hellojav.com/include/file_downpage.php?idx=";
                request.Host = "www.hellojav.com";
                WebHeaderCollection myWebHeaderCollection = request.Headers;
                myWebHeaderCollection.Add("Accept-Language", "zh-CN,zh;q=0.8,en;q=0.6,es;q=0.4");
               // request.Proxy = proxy;
                request.Headers.Set("Pragma", "no-cache");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.114 Safari/537.36";
                WebResponse response = request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding("utf-8");
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
        CookieContainer container;

        public string getHtml141Jav(string url)
        {
            Console.WriteLine(url);
            string str = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                if (container == null)
                {
                    container = new CookieContainer();
                    Cookie __cfduid = new Cookie("__atuvc", "5%7C12", "/", "www.141jav.com");
                    Cookie __utma = new Cookie("__atuvs", "550e67d59d71e67c003", "/", "www.141jav.com");
                }
                request.CookieContainer = container;

                //cookieContainer.Add(__cfduid);
             //   cookieContainer.Add(__utma);
                WebProxy proxy = new WebProxy("http://10.10.3.6:3128/", true);
                
    
                request.Host = "www.141jav.com";
                WebHeaderCollection myWebHeaderCollection = request.Headers;
                myWebHeaderCollection.Add("Accept-Language", "zh-CN,zh;q=0.8,en;q=0.6,es;q=0.4");
                //  request.Proxy = proxy;
                request.Headers.Set("Pragma", "no-cache");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.114 Safari/537.36";
                WebResponse response = request.GetResponse();
                
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding("utf-8");
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                str = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("GET PATE ERROR  " + url);
                Console.WriteLine(ex.Message);
                //if (!ex.Message.Contains("404"))
                str = getHtml141Jav(url);
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

