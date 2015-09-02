using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace Framework.tool
{
    public class Config1
    {
        static object txtLock = new object();
        public static void appendFile(string content,string path)
        {

            lock (txtLock)
            {
                try
                {
                    FileStream stream;
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
                catch (Exception e)
                {
                    Console.WriteLine("FileApprendError:  "+e.Message + content + ":" + path);
                }
            }
        }


        static bool isFisrtTimeConnectionReset = true;
        static TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);

        public static bool checkTime()
        {
            Console.WriteLine("开始检测时间");
            TimeSpan ts2= new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            if (!isFisrtTimeConnectionReset)
            {
                if (ts.TotalSeconds > 30)
                {
                    Console.WriteLine("时间>30s, 新的REST");
                    ts1 = ts2;
                    return false;
                }
                else
                {
                    Console.WriteLine("时间<30s, retry");
                    return true;
                }
            }
            else
            {
                Console.WriteLine("第一次REST");
                isFisrtTimeConnectionReset = false;
                ts1 = ts2;
                return false;
            }
        }
        public DateTime time=new DateTime();

        static object lock1 = new object();
        public static string failList="";

        public static void setFailList(string s)
        {
            lock (lock1)
            {
                failList += s;
            }
        }

        static object ob = new object();
        public static ManualResetEvent mre=new ManualResetEvent(true);

        public static void Check()
        {
            
            Monitor.TryEnter(ob);
                mre.Reset();
                if (!checkConnection())
                    redail();
                mre.Set();
                Monitor.Exit(ob);
        }

        public static void Flooding()
        {

            if (Monitor.TryEnter(ob))
            {
                mre.Reset();
                redailRouter(0);
                Thread.Sleep(20000);
                redailRouter(1);
                mre.Set();
                Monitor.Exit(ob);
            }
             
        }

        static void redailRouter(int param)
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
            byte[] data = Encoding.ASCII.GetBytes("current_page=%2Findex.asp&next_page=%2Findex.asp&flag=Internet&action_mode=apply&action_script=restart_wan_if&action_wait=5&wan_enable="+param+"&wans_dualwan=wan+none&wan_unit=0");
            request.ContentLength = data.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Close();
        }

        static void redail()
        {
            Console.WriteLine("StartRedail");
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
            Console.WriteLine("EndRedail");
            Thread.Sleep(10000);
            mre.Set();

        }

        static bool checkConnection()
        {
            string url = "http://rarbg.com/index5.php";
            bool success=false;
            string str;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Console.WriteLine("CHECKCONNECT:" + "http://rarbg.com/index5.php");
            StreamReader streamReader = null;

            try
            {
                CookieContainer cookieContainer = new CookieContainer();

                Cookie lastVisit = new Cookie("LastVisit", Config1.getLastVisit(), "/", "rarbg.com");
                Cookie __utma = new Cookie("__utma", "211336342.1333136546.1369105449.1369109171.1369112684.3", "/", "rarbg.com");
                Cookie __utmb = new Cookie("__utmb", "211336342.5.10.1369112684", "/", "rarbg.com");
                Cookie __utmc = new Cookie("__utmc", "211336342", "/", "rarbg.com");
                Cookie __utmz = new Cookie("__utmz", "211336342.1369105449.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/", "rarbg.com");
                Cookie bSbTZF2j = new Cookie("bSbTZF2j", "6BdPQ9qs", "/", "rarbg.com");
                cookieContainer.Add(lastVisit);
                cookieContainer.Add(bSbTZF2j);
                cookieContainer.Add(__utma);
                cookieContainer.Add(__utmb);
                cookieContainer.Add(__utmc);
                cookieContainer.Add(__utmz);

                request = (HttpWebRequest)WebRequest.Create(url);
                request.CookieContainer = cookieContainer;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36";
                request.Timeout = 15000;
                request.KeepAlive = false;
                request.Referer = "http://rarbg.com/torrent/j1kx3ny";

   
                response = (HttpWebResponse)request.GetResponse();
                if (response.Cookies["LastVisit"] != null)
                    Config1.setLastVisit(response.Cookies["LastVisit"].ToString());
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding("GB2312");
                streamReader = new StreamReader(streamReceive, encoding);
                str = streamReader.ReadToEnd();
                success = true;
                Console.WriteLine("检测成功");
            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.Message + ":检测失败:" + url);
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
            return success;
        }


        static object o=new object();
        private static string lastVisit = "1374857595";

        public static void setLastVisit(string s)
        {
            lock (o)
            {
                lastVisit = s;
            }
        }
        public static string getLastVisit()
        {
            return lastVisit;
        }




    }
}
