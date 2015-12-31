using Framework.interf;
using Framework.tool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sis001Downloader
{
    class Sis001SgDl : ISinglePageDonwloader
    {
        Regex nameRegex = new Regex("color.*?</a>");
        Regex nameRegex1 = new Regex(">.*?</a>");
        Regex threadRegex = new Regex("href=\"thread.*?\"><");
        Regex noColorNameRegex=new Regex("html\">.*?</a>");
        Regex sizeRegex=new Regex("<td class=\"nums\">.*?/");
        public void Download(object obj)
        {
            AsynObj o = (AsynObj)obj;
            MatchCollection mc = threadRegex.Matches(o.Content);
            MatchCollection pathMc = nameRegex.Matches(o.Content);
            List<String> pathList = new List<string>();
            string[] threads1 = o.Content.Split(new string[] { "版块主题" }, StringSplitOptions.RemoveEmptyEntries);
            string[] threads = threads1[1].Split(new string[] {  "normalthread_", "pages_btns" }, StringSplitOptions.RemoveEmptyEntries);
            List<AsynObj> filterThreads = new List<AsynObj>();
            foreach(string thread in threads)
            {
                if(thread.Contains("新窗口打开"))
                {
                    string path;
                    if (thread.Contains("color:"))
                    {
                        path = Path.Combine(o.Path, nameRegex1.Match(nameRegex.Match(thread).Value).Value.Replace(">", "").Replace("</a", "").Replace('/', '_').Replace(":", "^").Replace("?", "wenhao")) ;
                    
                    }
                    else
                    {
                        path = Path.Combine(o.Path, nameRegex1.Match(noColorNameRegex.Match(thread).Value).Value.Replace(">", "").Replace("</a", "").Replace('/', '_').Replace(":", "^").Replace("?", "wenhao"));

                    }
                    double size=0;
                    try
                    {
                        string sizeStr = sizeRegex.Matches(thread)[1].Value.Replace("<td class=\"nums\">", "").Replace(" /", "").ToUpper();
                        string sizeStrWithoutUnit = sizeStr.Replace("GB", "").Replace("G", "").Replace("MB", "").Replace("M", "");
                        size = Convert.ToDouble(sizeStrWithoutUnit);
                        if (sizeStr.Contains("G"))
                            size = size * 1024;
                    }
                    catch
                    {
                        Console.WriteLine("Can not get Size:  " + thread);
                    }
                    path += " size^^^" + size+".htm";
                    string link = "http://sis001.com/bbs/" + threadRegex.Match(thread).Value.Replace("href=\"", "").Replace("\" title=\"新窗口打开\" target=\"_blank\"><", "");
                    ThreadPool.QueueUserWorkItem(new Sis001SgDl().work, new AsynObj(path, link));
                }
                    
            }
        }

        void work(Object obj)
        {
            AsynObj asycObj=(AsynObj)obj;
            string content= Sis001DlTool.GetHtml(asycObj.Url, true);
            DlTool.SaveFile(content, asycObj.Path);
        }
    }
}
