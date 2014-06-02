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

namespace RarbgDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            //p.redail();
           // checkTime();
            appendFile("nigga", "d:\\test\\failList.txt");
            appendFile("nigga2", "d:\\test\\failList.txt");
            Console.ReadLine();
        }

        void test()
        {
            DlTool dt = new DlTool();
           // dt.downLoadFile("http://rarbg.com/download.php?id=sgxzclp&f=Layered-Nylons.13.05.06.Gracie.XXX.720p.WMV-GAGViD-[rarbg.com].torrent","d:\\a.torrent",);
        }

        void configTest()
        {
            NameValueCollection demo4 = ConfigurationManager.GetSection("InnerNetCollection2") as NameValueCollection;  
        }

        void redail()
        {
            Process p1 = new Process();
            p1.StartInfo.FileName = "cmd.exe";
            p1.StartInfo.Arguments = "/c " +"rasdial.exe  /disconnect";
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
            FileStream stream=null;
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


    }
}
