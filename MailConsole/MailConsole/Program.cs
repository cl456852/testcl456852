using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using MasterSoft.WinUI;
using System.Threading;

namespace MailConsole
{
    class Program
    {
        static void Main(string[] args)
        {
           // DoGetHostAddresses("PC-201210302211");
            //GetIP();
         //   ThunderMonitor tm=new ThunderMonitor();
            Program p = new Program();
           // ProcessMoitor processMonitor = new ProcessMoitor();
           // Thread thread = new Thread(processMonitor.start);
           // thread.Start();
           // Thread thread1 = new Thread(tm.start);
           // thread1.Start();
            while (true)
            {
                p.sendMail();
                Thread.Sleep(30* 1000);
            }
        }

        void sendMail()
        {
            Mail mail = new Mail();
            mail.ReceiverAddess = "cl4568521@gmail.com";
            mail.Subject = "MyIP";
            mail.Body = (GetIP());
            try
            {
                mail.Send();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void DoGetHostAddresses(string hostname)
        {
            IPAddress[] ips;

            ips = Dns.GetHostAddresses(hostname);

            Console.WriteLine("GetHostAddresses({0}) returns:", hostname);

            foreach (IPAddress ip in ips)
            {
                Console.WriteLine("    {0}", ip);
            }
        }

        //获取本机的公网IP
        private static string GetIP()
        {
            string tempip = "";
            try
            {
                WebRequest wr = WebRequest.Create("http://1111.ip138.com/ic.asp");
                Stream s = wr.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.Default);
                string all = sr.ReadToEnd(); //读取网站的数据

                int start = all.IndexOf("[") + 1;
                int end = all.IndexOf("]", start);
                tempip = DateTime.Now + "   " + all.Substring(start, end - start);
                Console.WriteLine(tempip);
                sr.Close();
                s.Close();
            }
            catch
            {
            }
            return  tempip;
        }
    }
}
