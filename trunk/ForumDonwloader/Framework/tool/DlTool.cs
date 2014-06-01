using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Net.Sockets;

namespace Framework.tool
{
    public class DlTool
    {

        Cookie lastVisit = new Cookie("LastVisit", "1401625036", "/", "rarbg.com");

        public string GetHtml(string url,bool useProxy)
        {
            string str = string.Empty;
            bool success=false;
            while (!success)
            {
                HttpWebRequest request = null;
                HttpWebResponse response = null;
                Console.WriteLine(url);
                StreamReader streamReader = null;
               
                try
                {
                    CookieContainer cookieContainer = new CookieContainer();


                    Cookie __utma = new Cookie("__utma", "211336342.1333136546.1369105449.1369109171.1369112684.3", "/", "rarbg.com");
                    Cookie __utmb = new Cookie("__utmb", "211336342.5.10.1369112684", "/", "rarbg.com");
                    Cookie __utmc = new Cookie("__utmc", "211336342", "/", "rarbg.com");
                    Cookie __utmz = new Cookie("__utmz", "211336342.1369105449.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/", "rarbg.com");
                    Cookie bSbTZF2j = new Cookie("bSbTZF2j", "6BdPQ9qs", "/", "rarbg.com");
                    cookieContainer.Add(lastVisit);
                    cookieContainer.Add(__utma);
                    cookieContainer.Add(__utmb);
                    cookieContainer.Add(__utmc);
                    cookieContainer.Add(__utmz);
                    cookieContainer.Add(bSbTZF2j);
                    request = (HttpWebRequest)WebRequest.Create(url);
                    request.CookieContainer = cookieContainer;
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36";
                    request.Timeout = 15000;
                    request.KeepAlive = false;
                    request.Referer = "http://rarbg.com/torrent/j1kx3ny";
             
                    if (useProxy)
                    {

                        WebProxy proxy = new WebProxy("127.0.0.1", 8087);
                        request.Proxy = proxy;
                    }
                    response = (HttpWebResponse)request.GetResponse();
                    if (response.Cookies["LastVisit"] != null)
                        lastVisit = response.Cookies["LastVisit"];
                    Stream streamReceive = response.GetResponseStream();
                    Encoding encoding = Encoding.GetEncoding("GB2312");
                    streamReader = new StreamReader(streamReceive, encoding);
                    str = streamReader.ReadToEnd();
                    success = true;
                }

                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message + ":无法恢复:" + url);
                }
                finally
                {
                    if (request != null)
                        request.Abort();
                    if (streamReader != null)
                        streamReader.Close();
                    if (response != null)
                        response.Close();
                    Thread.Sleep(1000);



                }
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

        public void AppendFile(string content,string fileName)
        {
            StreamWriter sw = File.AppendText(fileName);
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();   
        }

        public void downLoadFile(string url,string name,bool useProxy)
        {
            bool success = false;
            while (!success)
            {
                Console.WriteLine(url);
                HttpWebResponse response = null;
                FileStream fstream = null;
                HttpWebRequest request = null;
                try
                {
                    CookieContainer cookieContainer = new CookieContainer();


                    Cookie __utma = new Cookie("__utma", "211336342.667375280.1401465651.1401590512.1401621110.14", "/", "rarbg.com");
                    Cookie __utmb = new Cookie("__utmb", "211336342.42.10.1401621110", "/", "rarbg.com");
                    Cookie __utmc = new Cookie("__utmc", "211336342", "/", "rarbg.com");
                    Cookie __utmz = new Cookie("__utmz", "211336342.1401465651.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/", "rarbg.com");
                    Cookie bSbTZF2j = new Cookie("bSbTZF2j", "6BdPQ9qs", "/", "rarbg.com");
                    cookieContainer.Add(lastVisit);
                    cookieContainer.Add(__utma);
                    cookieContainer.Add(__utmb);
                    cookieContainer.Add(__utmc);
                    cookieContainer.Add(__utmz);
                    cookieContainer.Add(bSbTZF2j);
                    request = (HttpWebRequest)WebRequest.Create(url);
                    request.CookieContainer = cookieContainer;
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36";
                    request.Timeout = 15000;
                    request.KeepAlive = false;
                    request.Referer = "http://rarbg.com/torrent/j1kx3ny";
                    if (useProxy)
                    {
                        WebProxy proxy = new WebProxy("127.0.0.1", 8087);
                        request.Proxy = proxy;
                    }
                    response = (HttpWebResponse)request.GetResponse();
                    if (response.Cookies["LastVisit"] != null)
                        lastVisit = response.Cookies["LastVisit"];
                    Stream streamReceive = response.GetResponseStream();
                    string path = Path.GetDirectoryName(name);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    fstream = new FileStream(name, FileMode.Create);
                    streamReceive.CopyTo(fstream);

                    success = true;

                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + ":无法恢复:" + url);


                }
                finally
                {
                    if (request != null)
                        request.Abort();
                    if (fstream != null)
                        fstream.Close();
                    if (response != null)
                        response.Close();
                    Thread.Sleep(1000);
                }
            }

        }
    }
}
