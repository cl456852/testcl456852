﻿using System;
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
            while (true)
            {
                while (true)
                {
                    RouterTest(0);
                    Console.WriteLine("disconnect");
                    Thread.Sleep(5000);
                    if (checkRouterStatus(5))
                    {
                        Console.WriteLine("disconnect Check Successful");
                        break;
                    }
                    Console.WriteLine("disconnect Check Fail");

                }
                while (true)
                {
                    RouterTest(1);
                    Console.WriteLine("connect");
                    Thread.Sleep(10000);
                    if (checkRouterStatus(2))
                    {
                        Console.WriteLine("connect Check Successful");
                        break;
                    }
                    Console.WriteLine("connect Check Fail");
                }
            }
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


    }
}
