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
        int year=2013;
        //int month=1;
        public Form1()
        {
            InitializeComponent();
        }
        //http://javjunkies.com/main/2012/01-01-3/
        private void button1_Click(object sender, EventArgs e)
        {
            Downloader downloader = new Downloader();

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
            Downloader downloader = new Downloader();
            for (int i = 1; i <=167; i++)
            {
                string url = "http://www.18-jav.com/?paged=" + i;
                //string url = "http://www.18-jav.com/?s=xxx-av&paged=" + i;
                string html = downloader.GetHtml(url);
                string name = url.Replace('/', '_').Replace(":", "^").Replace("?","wenhao");
                downloader.SaveFile(html, @"d:\document\Documents\pagenew\page\18Javxxx-av" + "\\" + name + ".htm");
            }
        }

        //http://hellojav.com/Daily.php?date=2012-6-22
        //http://hellojav.com/Daily.php?date=2012-6-28&start=4
        private void button3_Click(object sender, EventArgs e)
        {
            Regex r;
            Downloader downloader = new Downloader();
            for (int month = 11; month <= 11; month++)
            {
                for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
                {
                    r = new Regex(@"/Daily.php\?date=2013-" + month + "-" + i + @"&start=\d?\d");
                    string url="http://www.hellojav.com/Daily.php?date=";
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
                        ht.Content = downloader.GetHtml("http://www.hellojav.com"+m.Value);
                        ht.Name = ("http://www.hellojav.com" + m.Value).Replace('/', '_').Replace(":", "^").Replace("?", "wenhao");
                        htmls.Add(ht);

                    }
                    foreach (Html ht in htmls)
                    {
                        downloader.SaveFile(ht.Content, "d:\\helloJav\\"+ht.Name + ".htm");
                    }
                }
            }
        }

        string _141javUrl = "http://www.141jav.com/month/2015-02/";
      
        private void button4_Click(object sender, EventArgs e)
        {
            WaitCallback callBack;
            callBack = new WaitCallback(downLoad);

            bool flag = ThreadPool.SetMaxThreads(1, 1);
            for (int month = 1; month <= 50; month++)
            {
               
                    ThreadPool.QueueUserWorkItem(callBack, _141javUrl + month);
                
            }
        }
        Regex _141JavReg = new Regex("<a href=\"[0-9]*\">禄</a></div>");
        Regex r = new Regex("view/.*?\"");
        private void downLoad(object datetime)
        {
            
            WaitCallback callBack;
            callBack = new WaitCallback(downloadSg);

            Downloader downloader = new Downloader();
            string url=(string)datetime;
            string html = downloader.getHtml141Jav(datetime.ToString());
            MatchCollection mc= r.Matches(html);
            foreach (Match m in mc)
            {
                ThreadPool.QueueUserWorkItem(callBack, "http://www.141jav.com/"+m.Value.Replace("\"",""));
            }
            downloader.SaveFile(html, "d:\\document\\Documents\\pagenew\\page\\141Jav\\" + url.Replace('/', '_').Replace(":", "^").Replace("?", "wenhao") + ".htm");
            //string next = _141JavReg.Match(html).Value.Replace("<a href=\"", "").Replace("\">禄</a></div>", "");
            //string nextUrl = url.Substring(0, url.LastIndexOf('/')+1)+next;
            //if (next != "")
            //    downLoad(nextUrl);

        }

        private void downloadSg(object url)
        {
            string urlString = (string)url;
            Downloader downloader = new Downloader();
            string html = downloader.getHtml141Jav((string)url);
            downloader.SaveFile(html, "d:\\document\\Documents\\pagenew\\page\\141Jav\\" + urlString.Replace('/', '_').Replace(":", "^").Replace("?", "wenhao") + ".htm");
        }
        //http://btdigg.org/search?q=fangyuanli&p=2
        private void button5_Click(object sender, EventArgs e)
        {
            Downloader downloader = new Downloader();
            for (int i = 0; i < 87; i++)
            {
                string url = "http://btdigg.org/search?q=kidm&p=" + i;
                string html = downloader.GetHtml(url);
                string name = url.Replace('/', '_').Replace(":", "^").Replace("?", "wenhao");
                downloader.SaveFile(html, @"D:\document\new\page\btdigg" + "\\" + name + ".htm");
            }
        }


    }
}
