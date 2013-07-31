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
        public string GetHtml(string url)
        {
            Console.WriteLine(url);
            string str = string.Empty;
            try
            {
                Singl.mr.WaitOne();
                CookieContainer cookieContainer = new CookieContainer();
                Cookie LastVisit = new Cookie("LastVisit", "1369113747", "/", "rarbg.com");
                Cookie MarketGidStorage = new Cookie("MarketGidStorage", "%7B%220%22%3A%7B%22svspr%22%3A%22http%3A%2F%2Frarbg.com%2Fbot_check.php%22%2C%22svsds%22%3A9%2C%22TejndEEDj%22%3A%22MTM2OTEwNTQ3MjkzMTIxNTMxNDU%3D%22%7D%2C%22C2153%22%3A%7B%22page%22%3A5%2C%22time%22%3A1369113744213%2C%22mg_id%22%3A%2213894%22%2C%22mg_type%22%3A%22news%22%7D%7D", "/", "rarbg.com");
                Cookie __utma = new Cookie("__utma", "211336342.1333136546.1369105449.1369109171.1369112684.3", "/", "rarbg.com");
                Cookie __utmb = new Cookie("__utmb", "211336342.5.10.1369112684", "/", "rarbg.com");
                Cookie __utmc = new Cookie("__utmb", "211336342", "/", "rarbg.com");
                Cookie __utmz = new Cookie("__utmb", "211336342.1369105449.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/", "rarbg.com");
                Cookie bSbTZF2j = new Cookie("bSbTZF2j", "6BdPQ9qs", "/", "rarbg.com");
                cookieContainer.Add(LastVisit);
                cookieContainer.Add(__utma);
                cookieContainer.Add(__utmb);
                cookieContainer.Add(__utmc);
                cookieContainer.Add(__utmz);
                cookieContainer.Add(bSbTZF2j);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.CookieContainer = cookieContainer;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.31 (KHTML, like Gecko) Chrome/26.0.1410.64 Safari/537.31";
                request.Timeout = 30000;
                // request.Headers.Set("Pragma", "no-cache");
                WebResponse response = request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding("GB2312");
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                str = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.Status.ToString() == "ReceiveFailure")
                {
                    AppendFile(url, "d:\\test\\fail.txt");
                    Singl.mr.Reset();
                    Thread.Sleep(60000);
                    Singl.mr.Set();
                    return "";
                }
                else if (!ex.Message.Contains("404"))
                    str = GetHtml(url);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Thread.Sleep(200);
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

        public void downLoadFile(string url,string name)
        {
            try
            {
                Singl.mr.WaitOne();
                Console.WriteLine(url);
                WebClient myWebClient = new WebClient();
                myWebClient.DownloadFile(url, name);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.Status.ToString() == "ReceiveFailure")
                {
                    AppendFile(url, "d:\\test\\fail.txt");
                    Singl.mr.Reset();
                    Thread.Sleep(60000);
                    Singl.mr.Set();
                }
                else if (!ex.Message.Contains("404"))
                    downLoadFile(url, name);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Thread.Sleep(200);
            }

        }
    }
}
