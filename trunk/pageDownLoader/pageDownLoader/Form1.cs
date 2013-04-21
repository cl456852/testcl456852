using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading;

namespace pageDownLoader
{
    public partial class Form1 : Form
    {
        int year=2012;
        //int month=1;
        public Form1()
        {
            InitializeComponent();
        }
        //http://javjunkies.com/main/2012/01-01-3/
        private void button1_Click(object sender, EventArgs e)
        {
            Donwloader downloader = new Donwloader();

            for (int month = 2; month <= 12; month++)
            {
                for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
                {
                    string monthStr = month < 10 ? "0" + month : month + "";
                    string url = "http://javjunkies.com/main/";
                    url += year + "/" + monthStr + "-" + (i < 10 ? "0" + i : i + "");
                    string html = downloader.GetHtml(url);
                    MatchCollection mc = downloader.findUrls(html);
                    ArrayList htmls = new ArrayList();
                    Html h = new Html();
                    h.Content = html;
                    h.Name = url.Replace('/', '_').Replace(":", "^");
                    htmls.Add(h);
                    foreach (Match m in mc)
                    {
                        Html ht = new Html();
                        ht.Content = downloader.GetHtml(m.Value);
                        ht.Name = m.Value.Replace('/', '_').Replace(":", "^");
                        htmls.Add(ht);

                    }
                    foreach (Html ht in htmls)
                    {
                        downloader.SaveFile(ht.Content, ht.Name + ".htm");
                    }

                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Donwloader downloader = new Donwloader();
            for (int i = 1; i <= 52; i++)
            {
                string url = "http://www.18-jav.com/?paged=" + i;
                string html = downloader.GetHtml(url);
                string name = url.Replace('/', '_').Replace(":", "^").Replace("?","wenhao");
                downloader.SaveFile(html,@"D:\我的资料库\Documents\pagenew\page\18Jav20130411"+"\\"+ name + ".htm");
            }
        }

        //http://hellojav.com/Daily.php?date=2012-6-22
        //http://hellojav.com/Daily.php?date=2012-6-28&start=4
        private void button3_Click(object sender, EventArgs e)
        {
            Regex r;
            Donwloader downloader = new Donwloader();
            for (int month = 1; month <= 3; month++)
            {
                for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
                {
                    r = new Regex(@"/Daily.php\?date=2013-"+month+"-"+i+@"&start=\d");
                    string url="http://hellojav.com/Daily.php?date=";
                    url += year + "-" + month + "-" + i;
                    string html = downloader.GetHtml(url);

                    MatchCollection mc = r.Matches(html);
                    ArrayList htmls = new ArrayList();
                    Html h = new Html();
                    h.Content = html;
                    h.Name = url.Replace('/', '_').Replace(":", "^").Replace("?","wenhao");
                    htmls.Add(h);
                    foreach (Match m in mc)
                    {
                        Html ht = new Html();
                        ht.Content = downloader.GetHtml("http://hellojav.com"+m.Value);
                        ht.Name = ("http://hellojav.com" + m.Value).Replace('/', '_').Replace(":", "^").Replace("?", "wenhao");
                        htmls.Add(ht);

                    }
                    foreach (Html ht in htmls)
                    {
                        downloader.SaveFile(ht.Content, "d:\\helloJav\\"+ht.Name + ".htm");
                    }
                }
            }
        }

        string _141javUrl="http://www.141jav.com/latest/";
        Donwloader downloader = new Donwloader();
        private void button4_Click(object sender, EventArgs e)
        {
            WaitCallback callBack;
            callBack = new WaitCallback(downLoad);

            bool flag = ThreadPool.SetMaxThreads(1, 1);
            for (int month = 3; month <= 3; month++)
            {
                for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
                {
                    ThreadPool.QueueUserWorkItem(callBack, _141javUrl + "2013-" + month + "-" + i + "/1");
                }
            }

           //downLoad("http://www.141jav.com/latest/2013-02-20/1");
        }
        Regex _141JavReg = new Regex("<a href=\"[0-9]*\">禄</a></div>");
        private void downLoad(object datetime)
        {
            string url=(string)datetime;
            string html = downloader.GetHtml( datetime.ToString());

            downloader.SaveFile(html, "d:\\141Jav\\" + url.Replace('/', '_').Replace(":", "^").Replace("?", "wenhao") + ".htm");
            string next = _141JavReg.Match(html).Value.Replace("<a href=\"", "").Replace("\">禄</a></div>", "");
            string nextUrl = url.Substring(0, url.LastIndexOf('/')+1)+next;
            if (next != "")
                downLoad(nextUrl);

        }


    }
}
