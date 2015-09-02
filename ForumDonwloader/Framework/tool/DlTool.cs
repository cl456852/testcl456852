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
                    Config1.mre.WaitOne();
                    CookieContainer cookieContainer = new CookieContainer();
                    Cookie _7fAY799j = new Cookie("7fAY799j", "VtdTzG69", "/", "rarbg.to");
                    Cookie lastVisit = new Cookie("LastVisit", Config1.getLastVisit(), "/", "rarbg.to");
                    Cookie __utma = new Cookie("__utma", "9515318.860353583.1429342721.1434283337.1435333589.21", "/", ".rarbg.to");
                    Cookie __utmb = new Cookie("__utmb", "9515318.19.10.1435333589", "/", ".rarbg.to");
                    Cookie __utmc = new Cookie("__utmc", "9515318", "/", ".rarbg.to");
                    Cookie __utmz = new Cookie("__utmz", "211336342.1407912383.29.2.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided)", "/", ".rarbg.to");
                    Cookie __utmt = new Cookie("__utmt", "1", "/", ".rarbg.to");
                    cookieContainer.Add(_7fAY799j);
                    cookieContainer.Add(lastVisit);
                   // cookieContainer.Add(bSbTZF2j);
                    cookieContainer.Add(__utma);
                    cookieContainer.Add(__utmb);
                    cookieContainer.Add(__utmc);
                    cookieContainer.Add(__utmz);
                    cookieContainer.Add(__utmt);
                    request = (HttpWebRequest)WebRequest.Create(url);
                    request.CookieContainer = cookieContainer;
                    request.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.153 Safari/537.36";
                    request.Timeout = 15000;
                    request.KeepAlive = false;
                    request.Referer = "";
                  //  request.SendChunked = true;
                    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                 //   request.TransferEncoding = "gzip,deflate,sdch";

                    if (useProxy)
                    {

                        WebProxy proxy = new WebProxy("10.10.8.1", 3128);
                        request.Proxy = proxy;
                    }
                    response = (HttpWebResponse)request.GetResponse();
                    if (response.Cookies["LastVisit"] != null)
                        Config1.setLastVisit( response.Cookies["LastVisit"].ToString());
                    Stream streamReceive = response.GetResponseStream();
                    Encoding encoding = Encoding.GetEncoding("GB2312");
                    streamReader = new StreamReader(streamReceive, encoding);
                    str = streamReader.ReadToEnd();
                    success = true;
                }

                catch (Exception ex)
                {
                    
                    Console.WriteLine(ex.Message + "  " + url);
                    if (ex.Message.Contains("接收时发生错误")&&!Config1.checkTime())
                    {
                        Config1.appendFile(url,"d:\\test\\failList.txt");
                        success = true;
                    }
                    //Config1.Check();
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
            fileName = fileName.Replace("%20", "").Replace("%2C", "").Replace("%22","");
            //实例化一个文件流--->与写入文件相关联
            FileStream fs = new FileStream(fileName, FileMode.Create);
            //实例化一个StreamWriter-->与fs相关联
            StreamWriter sw = new StreamWriter(fs,Encoding.UTF8);
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
            
        public void downLoadFile(string url,string name,bool useProxy,string content)
        {

            bool success = false;
            while (!success)
            {
                Console.WriteLine(url);
                HttpWebResponse response = null;
                FileStream fstream = null;
                HttpWebRequest request = null;
                Stream stream=null;
                StreamReader reader = null;
                Stream streamReceive=null;
                try
                {
                    Config1.mre.WaitOne();
                    CookieContainer cookieContainer = new CookieContainer();
                    Cookie _7fAY799j = new Cookie("7fAY799j", "VtdTzG69", "/", "rarbg.to");
                    Cookie lastVisit = new Cookie("LastVisit", Config1.getLastVisit(), "/", "rarbg.to");
                    Cookie __utma = new Cookie("__utma", "9515318.860353583.1429342721.1434283337.1435333589.21", "/", ".rarbg.to");
                    Cookie __utmb = new Cookie("__utmb", "9515318.19.10.1435333589", "/", ".rarbg.to");
                    Cookie __utmc = new Cookie("__utmc", "9515318", "/", ".rarbg.to");
                    Cookie __utmz = new Cookie("__utmz", "211336342.1407912383.29.2.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided)", "/", ".rarbg.to");
                    Cookie __utmt = new Cookie("__utmt", "1", "/", ".rarbg.to");
                    cookieContainer.Add(_7fAY799j);
                    cookieContainer.Add(lastVisit);
                   // cookieContainer.Add(bSbTZF2j);
                    cookieContainer.Add(__utma);
                    cookieContainer.Add(__utmb);
                    cookieContainer.Add(__utmc);
                    cookieContainer.Add(__utmz);
                    cookieContainer.Add(__utmt);
                    request = (HttpWebRequest)WebRequest.Create(url);
                    request.CookieContainer = cookieContainer;
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36";
                    request.Timeout = 15000;
                    request.KeepAlive = false;
                    request.Referer = "http://rarbg.com/torrent/j1kx3ny";
                    if (useProxy)
                    {
                        WebProxy proxy = new WebProxy("10.10.8.1", 3128);
                        request.Proxy = proxy;
                    }
                    response = (HttpWebResponse)request.GetResponse();
                    if (response.Cookies["LastVisit"] != null)
                        Config1.setLastVisit(response.Cookies["LastVisit"].ToString());
                    streamReceive = response.GetResponseStream();
                    string path = Path.GetDirectoryName(name);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    if (File.Exists(name))
                    {
                        name =Path.Combine( Path.GetDirectoryName(name),"duplicateName", Path.GetFileNameWithoutExtension(name) + "(" + System.Guid.NewGuid().ToString().Substring(0,4) + ").torrent");
                        Console.WriteLine("duplicate filename: " + name);
                    }
                    stream = new MemoryStream();
                    streamReceive.CopyTo(stream);
                     reader = new StreamReader(stream);
                    stream.Position = 0;
                    string fileContent = reader.ReadToEnd();
                    if (fileContent.Contains("We are sorry but this is pure flooding"))
                    {
                        Config1.Flooding();
                        continue;
                    }
                    stream.Position = 0;
                    fstream = new FileStream(name, FileMode.Create);
                    stream.CopyTo(fstream);
                    SaveFile(content, name+".htm");
                    success = true;

                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "  " + url);
                    if (ex.Message.Contains("接收时发生错误") && !Config1.checkTime() || ex.Message.Contains("不支持给定路径的格式") || ex.Message.Contains("指定的路径或文件名太长"))
                    {
                        Config1.appendFile(url, "d:\\test\\failList.txt");
                        success = true;
                    }
                    //Config1.Check();

                }
                finally
                {
                    if (request != null)
                        request.Abort();
                    if (fstream != null)
                        fstream.Close();
                    if (response != null)
                        response.Close();
                    if (stream != null)
                        stream.Close();
                    if (reader != null)
                        reader.Close();
                    if (streamReceive != null)
                        streamReceive.Close();
                    Thread.Sleep(1000);
                }
            }

        }
    }
}
