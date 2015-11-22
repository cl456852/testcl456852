using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.tool;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Net;

namespace RarbgDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            //p.redail();
            // checkTime();
            //appendFile("nigga", "d:\\test\\failList.txt");
            //appendFile("nigga2", "d:\\test\\failList.txt");
            //while (true)
            //{
            //    while (true)
            //    {
            //        RouterTest(0);
            //        Console.WriteLine("disconnect");
            //        Thread.Sleep(5000);
            //        if (checkRouterStatus(5))
            //        {
            //            Console.WriteLine("disconnect Check Successful");
            //            break;
            //        }
            //        Console.WriteLine("disconnect Check Fail");

            //    }
            //    w0hile (true)
            //    {
            //        RouterTest(1);
            //        Console.WriteLine("connect");
            //        Thread.Sleep(10000);
            //        if (checkRouterStatus(2))
            //        {
            //            Console.WriteLine("connect Check Successful");
            //            break;
            //        }
            //        Console.WriteLine("connect Check Fail");
            //    }
            //}

            checkRouterStatusNew(0);
            Console.ReadLine();
        }

        void test()
        {
            DlTool dt = new DlTool();
            // dt.downLoadFile("http://rarbg.to/download.php?id=sgxzclp&f=Layered-Nylons.13.05.06.Gracie.XXX.720p.WMV-GAGViD-[rarbg.to].torrent","d:\\a.torrent",);
        }

        void configTest()
        {
            NameValueCollection demo4 = ConfigurationManager.GetSection("InnerNetCollection2") as NameValueCollection;
        }

        void redail()
        {
            Process p1 = new Process();
            p1.StartInfo.FileName = "cmd.exe";
            p1.StartInfo.Arguments = "/c " + "rasdial.exe  /disconnect";
            p1.Start();
            p1.WaitForExit();
            Thread.Sleep(3000);
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c " + "rasdial.exe 宽带连接 300000162885 68819054";
            p.Start();
            p.WaitForExit();

        }
        static TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
        static bool isFisrtTimeConnectionReset = false;
        public static bool checkTime()
        {
            Console.WriteLine("开始检测时间");
            //isFisrtTimeConnectionReset = false;


            Thread.Sleep(4000);
            TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            if (!isFisrtTimeConnectionReset)
            {
                Console.WriteLine(ts.Seconds);
                if (ts.Seconds > 15)
                {
                    Console.WriteLine("时间>15s, 新的REST");
                    ts1 = ts2;
                    return false;
                }
                else
                {
                    Console.WriteLine("时间<15s, retry");
                    return true;
                }
            }
            else
            {
                Console.WriteLine("第一次REST");
                ts1 = ts2;
                return false;
            }
        }


        static object txtLock = new object();
        public static void appendFile(string content, string path)
        {
            FileStream stream = null;
            lock (txtLock)
            {
                if (!File.Exists(path))
                {
                    stream = File.Create(path);
                    stream.Close();
                }
                StreamWriter sw = File.AppendText(path);
                sw.WriteLine(content);
                sw.Flush();
                sw.Close();
            }
        }


        public static void RouterTest(int status)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://192.168.1.1/start_apply2.htm");

                request.Method = "get";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Credentials = CredentialCache.DefaultCredentials;

                //获得用户名密码的Base64编码
                string code = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "admin", "admin")));
                request.Method = "POST";
                //添加Authorization到HTTP头
                request.Headers.Add("Authorization", "Basic " + code);
                byte[] data = Encoding.ASCII.GetBytes("current_page=%2Findex.asp&next_page=%2Findex.asp&flag=Internet&action_mode=apply&action_script=restart_wan_if&action_wait=5&wan_enable=" + status + "&wans_dualwan=wan+none&wan_unit=0");
                request.ContentLength = data.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static bool checkRouterStatus(int status)
        {
            string code = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "admin", "admin")));

            HttpWebRequest request = null;
            HttpWebResponse response = null;

            StreamReader streamReader = null;
            request = (HttpWebRequest)WebRequest.Create("http://192.168.1.1/ajax_status.asp");
            request.Method = "POST";
            //添加Authorization到HTTP头
            request.Headers.Add("Authorization", "Basic " + code);
            byte[] data = Encoding.ASCII.GetBytes("current_page=%2Findex.asp&next_page=%2Findex.asp&flag=Internet&action_mode=apply&action_script=restart_wan_if&action_wait=5&wan_enable=" + status + "&wans_dualwan=wan+none&wan_unit=0");
            request.ContentLength = data.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);




            response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            Encoding encoding = Encoding.GetEncoding("GB2312");
            streamReader = new StreamReader(streamReceive, encoding);
            string str;
            str = streamReader.ReadToEnd();
            if (str.Contains("<wan>" + status + "</wan>"))
                return true;
            else
                return false;
        }

        public static void checkRouterStatusNew(int status)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.1.1:80/login.cgi");
            request.Method = "POST";
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] b = encoding.GetBytes("group_id=&action_mode=&action_script=&action_wait=5&current_page=Main_Login.asp&next_page=index.asp&login_authorization=YWRtaW46YWRtaW4%3D");
            request.Host = "192.168.1.1";
            request.Referer = "http://192.168.1.1/Main_Login.asp";

            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.89 Safari/537.36";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = 138;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(b, 0, b.Length);
            }  
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            String token = response.Headers["set-Cookie"].Split(new char[] { '=', ';' })[1];
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create("http://192.168.1.1/ajax_status.xml");
            request1.Method = "GET";
            CookieContainer container = new CookieContainer();
            container.Add(new Cookie("asus_token", token, "/", "192.168.1.1"));
            container.Add(new Cookie("dm_enable", "no", "/", "192.168.1.1"));
            container.Add(new Cookie("dm_install", "no", "/", "192.168.1.1"));
            container.Add(new Cookie("hwaddr", "40:16:7E:A3:02:C8", "/", "192.168.1.1"));
            container.Add(new Cookie("nwmapRefreshTime", "1446483367482", "/", "192.168.1.1"));
          //  container.Add(new Cookie("ouiClientList", "<8C:3A:E3:3D:F8:C6>LG Electronics<CC:FA:00:C7:B7:3B>LG Electronics<38:BC:1A:EF:5F:54>Meizu technology co.,ltd<BC:5F:F4:89:66:2D>ASRock Incorporation<44:2A:60:9A:7E:47>apple<D8:CB:8A:39:C7:E2>Micro-Star INTL CO., LTD.", "/", "192.168.1.1"));
            container.Add(new Cookie("traffic_warning_0", "2015.10:1", "/", "192.168.1.1"));
            container.Add(new Cookie("wireless_list", "<8C:3A:E3:3D:F8:C6>Yes<44:2A:60:9A:7E:47>Yes<CC:FA:00:C7:B7:3B>Yes<38:BC:1A:EF:5F:54>No", "/", "192.168.1.1"));
           // container.Add(new Cookie("asus_token", token, "/", "192.168.1.1"));
            request1.CookieContainer = container;
            request1.Host = "192.168.1.1";
            request1.Connection = "keep-alive";
            request1.Referer = "http://192.168.1.1/login.cgi";
            request1.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request1.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.89 Safari/537.36";
            request1.Headers.Add("Accept-Encoding", "gzip, deflate, sdch");
            request1.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8,en;q=0.6,es;q=0.4");
            request1.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            HttpWebResponse statusResp = (HttpWebResponse)request1.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(streamReceive, encoding);
            string str;
            str = streamReader.ReadToEnd();
        }


    }
}
