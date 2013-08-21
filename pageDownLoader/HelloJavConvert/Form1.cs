using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using pageDownLoader;
using System.Net;
using System.Diagnostics;
using System.Threading;

namespace HelloJavConvert
{
    public partial class Form1 : Form
    {
        List<string> fileList = new List<string>();
        string ksf = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            initFileList();
            getKsf();
            Downloader down = new Downloader();
            Regex r = new Regex("idx=\\d*\"");
           // Regex reg=new Regex("idx=.*?\"");
            Regex reg = new Regex("mkln.*?http");
            string oringi = readFile(textBox1.Text);
            MatchCollection mc= r.Matches(oringi);
            foreach (Match m in mc)
            {

                string html= getHtmlPost("http://www.hellojav.com/include/file_downpage.php",m.Value.Replace("\"",""));
                string newStr = reg.Match(html).Value;
                string[] strs = newStr.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string idx ="idx="+ strs[0].Replace("mkln('", "").Replace("'","").Replace("\"","").Trim().Replace("=","%3d");
                if (!fileList.Contains(idx + ".torrent"))
                {
                    string tvy = "tvy=" + strs[1].Replace("'", "").Replace("\"", "").Trim();
                    
                    getFile("http://www.hellojav.com/include/file_down.php?" + idx + "&" + "ksf=" + ksf + "&" + tvy, idx);
                }
                oringi = oringi.Replace("http://hellojav.com/include/file_down.php?"+m.Value, idx.Replace("%3d","").Replace("idx=","") + ".torrent/");
                
            }
            down.SaveFile(oringi, "d:\\res.htm");
        }

        string readFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            string res = sr.ReadToEnd();
            sr.Close();
            return res;
        }

        string getHtmlPost(string url,string postInfo)
        {
            Console.WriteLine(url);
            Console.WriteLine(postInfo);
            CookieContainer cookieContainer = new CookieContainer();
            Cookie __cfduid = new Cookie("__cfduid", "dfa69ebb1c444a317361c9382e5a12c011370179159", "/", ".hellojav.com");
            Cookie __utma = new Cookie("__utma", "80647694.251620245.1370179180.1373181718.1373184350.16", "/", ".hellojav.com");
            Cookie __utmb = new Cookie("__utmb", "80647694.7.10.1373184350", "/", ".hellojav.com");
            Cookie __utmc = new Cookie("__cfduid", "80647694", "/", ".hellojav.com");
            Cookie __utmz = new Cookie("__utmz", "80647694.1370179180.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/", ".hellojav.com");
            Cookie auth = new Cookie("auth", "true", "/", "www.hellojav.com");
            Cookie prPop117853 = new Cookie("prPop117853", "1%7CSun%2C%2007%20Jul%202013%2014%3A05%3A56%20GMT", "/", "www.hellojav.com");
            cookieContainer.Add(__cfduid);
            cookieContainer.Add(__utma);
            cookieContainer.Add(__utmb);
            cookieContainer.Add(__utmc);
            cookieContainer.Add(__utmz);
            cookieContainer.Add(prPop117853);
            cookieContainer.Add(auth);

            string str=string.Empty;
            try
            {
                byte[] data = Encoding.Default.GetBytes(postInfo + "&ksf="+ksf);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.hellojav.com/include/file_downpage.php");
               // WebProxy proxy = new WebProxy("127.0.0.1", 8087);
                //request.Proxy = proxy;
                request.ContentType = "application/x-www-form-urlencoded";
                //request.Connection = "keep-alive";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.CookieContainer = cookieContainer;
                request.Host = "www.hellojav.com";
                request.Headers.Set("Origin", "http://www.hellojav.com");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.93 Safari/537.36";
                request.Referer = "http://www.hellojav.com/Search.php?search%5B%5D=MDS744";
                request.Method = "POST";

                request.ContentLength = data.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
              
                

                WebResponse response = request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding("GB2312");
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                str = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("GET PATE ERROR  " + url);
                Console.WriteLine(postInfo);
                Console.WriteLine(ex.Message);
                if (!ex.Message.Contains("404"))
                    str = getHtmlPost(url,postInfo);
            }
            return str;
        }

        string getFile(string url, string id)
        {
            Console.WriteLine(url);
            CookieContainer cookieContainer = new CookieContainer();
            Cookie __cfduid = new Cookie("__cfduid", "dfa69ebb1c444a317361c9382e5a12c011370179159", "/", ".hellojav.com");
            Cookie __utma = new Cookie("__utma", "80647694.251620245.1370179180.1373181718.1373184350.16", "/", ".hellojav.com");
            Cookie __utmb = new Cookie("__utmb", "80647694.7.10.1373184350", "/", ".hellojav.com");
            Cookie __utmc = new Cookie("__cfduid", "80647694", "/", ".hellojav.com");
            Cookie __utmz = new Cookie("__utmz", "80647694.1370179180.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", "/", ".hellojav.com");
            Cookie auth = new Cookie("auth", "true", "/", "www.hellojav.com");
            Cookie prPop117853 = new Cookie("prPop117853", "1%7CSun%2C%2007%20Jul%202013%2014%3A05%3A56%20GMT", "/", "www.hellojav.com");
            cookieContainer.Add(__cfduid);
            cookieContainer.Add(__utma);
            cookieContainer.Add(__utmb);
            cookieContainer.Add(__utmc);
            cookieContainer.Add(__utmz);
            cookieContainer.Add(prPop117853);
            cookieContainer.Add(auth);

            string str = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                // WebProxy proxy = new WebProxy("127.0.0.1", 8087);
                //request.Proxy = proxy;
                request.ContentType = "application/x-www-form-urlencoded";
                //request.Connection = "keep-alive";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.CookieContainer = cookieContainer;
                request.Host = "www.hellojav.com";
                request.Headers.Set("Origin", "http://www.hellojav.com");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.93 Safari/537.36";
                request.Referer = "http://b6290f6a.linkbucks.com/url/http://b6290f6a.linkbucks.com/url/http://b6290f6a.linkbucks.com/url/http://www.hellojav.com/include/file_down.php?idx=MjY4Nzg=";


                WebResponse response = request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                MemoryStream mStream = new MemoryStream();
                streamReceive.CopyTo(mStream);
                mStream.Position = 0;
                Encoding encoding = Encoding.GetEncoding("GB2312");
                StreamReader streamReader = new StreamReader(mStream, encoding);
                str = streamReader.ReadToEnd();
                if (str.Contains("You have exceeded"))
                {
                    reConnect();
                    Thread.Sleep(7000);
                    getKsf();
                    getFile(url, id);
                    return str;
                }
                mStream.Position = 0;
                string path= Path.GetDirectoryName(textBox1.Text);
                FileStream fstream = new FileStream(Path.Combine(path, id.Replace("%3d", "").Replace("idx=", "") + ".torrent"), FileMode.Create);
                mStream.CopyTo(fstream);
                fstream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("GET PATE ERROR  " + url);
                Console.WriteLine(ex.Message);
                if (!ex.Message.Contains("404"))
                    str = getHtmlPost(url,id);
            }
            return str;
        }

        void reConnect()
        {
            ProcessStartInfo start = new ProcessStartInfo("rasdial");//设置运行的命令行文件问ping.exe文件，这个文件系统会自己找到
            //如果是其它exe文件，则有可能需要指定详细路径，如运行winRar.exe
            start.Arguments = "宽带连接 /disconnect";//设置命令参数
            start.CreateNoWindow = true;//不显示dos命令行窗口
            start.RedirectStandardOutput = true;//
            start.RedirectStandardInput = true;//
            start.UseShellExecute = false;//是否指定操作系统外壳进程启动程序
            Process p = Process.Start(start);
            Console.WriteLine("disconnected");
            Thread.Sleep(5000);
            ProcessStartInfo start1 = new ProcessStartInfo("rasdial");//设置运行的命令行文件问ping.exe文件，这个文件系统会自己找到
            //如果是其它exe文件，则有可能需要指定详细路径，如运行winRar.exe
            start1.Arguments = "宽带连接 300000162885 68819054";//设置命令参数
            start1.UseShellExecute = false;//是否指定操作系统外壳进程启动程序
            Process p1 = Process.Start(start1);
            Console.WriteLine("connected");
        }

        private void getKsf()
        {
            byte[] b = Encoding.Default.GetBytes(GetIP());
            ksf = Convert.ToBase64String(b);

        }

        private string GetIP()
        {
            string tempip = "";
            try
            {
                WebRequest wr = WebRequest.Create("http://iframe.ip138.com/ic.asp");
                Stream s = wr.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.Default);
                string all = sr.ReadToEnd(); //读取网站的数据

                int start = all.IndexOf("[") + 1;
                int end = all.IndexOf("]", start);
                tempip = all.Substring(start, end - start);
                Console.WriteLine(tempip);
                sr.Close();
                s.Close();
            }
            catch
            {
            }
            return tempip;
        }

        private void initFileList()
        {
            string path=textBox1.Text.Substring(0,textBox1.Text.LastIndexOf("\\"));
            String[] paths = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
            foreach (string p in paths)
            {
                fileList.Add(Path.GetFileName(p));
            }
        }

    }
}
