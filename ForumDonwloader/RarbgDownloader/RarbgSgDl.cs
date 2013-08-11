using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.abs;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;

namespace RarbgDownloader
{
    public class RarbgSgDl : AbsSgDl
    {
        Regex regex = new Regex("href=\"/torrent/.*?\"");
        Regex torrentRegex = new Regex(@"download.php\?id=.*?\.torrent");
        Regex genresRegex = new Regex(@"Genres.*</a>");
        Regex genresRegex1 = new Regex("search=.*?\"");
        //http://rarbg.com/torrent/u1xt7vg
        public override void Download(object obj)
        {
            AsynObj o = (AsynObj)obj;

                MatchCollection mc = regex.Matches(o.Content);
                foreach (Match m in mc)
                {
                    //href="/torrent/yirda45"
                    if (!m.Value.Contains("#comments") && !DlConfig.storage.Contains(m.Value.Replace("href=\"/torrent/", "").Replace("\"","")))
                    {
                        ThreadPool.QueueUserWorkItem(work, new AsynObj(o.Path, "http://rarbg.com" + m.Value.Replace("href=", "").Replace("\"", "")));
                    }
                    else
                        Console.WriteLine(m.Value);
                }
            
          
        }

        private void work(object obj)
        {
            string genreStr="";
            MatchCollection genresMatches;
            AsynObj o = (AsynObj)obj;
            string content = dt.GetHtml(o.Url);
            string url = "http://rarbg.com/" + torrentRegex.Match(content).Value;
            if (obj != null)
                dt.SaveFile(content, Path.Combine(o.Path, o.Url.Replace('/', '_').Replace(":", "^").Replace("?", "wenhao"))+".htm");
            Match genres = genresRegex.Match(content);
            if (genres!=null&& genres.Value != "")
            {
                genresMatches = genresRegex1.Matches(genres.Value);
                foreach (Match m in genresMatches)
                    genreStr += m.Value.Replace("search=", "").Replace("\"", "").ToLower().Replace("+", " ") + ",";
            }
            if (check1( url.Substring(url.LastIndexOf('=') + 1).ToLower()))
            {
                dt.downLoadFile(url, Path.Combine(o.Path, genreStr + "$$" + url.Substring(url.LastIndexOf('=') + 1)));
                return;
            }
            if (!check2(url.Substring(url.LastIndexOf('=') + 1).ToLower()))
            {
                dt.downLoadFile(url, Path.Combine(o.Path, "notok", genreStr + "$$" + url.Substring(url.LastIndexOf('=') + 1)));
                return;
            }
            if (genres != null && genres.Value != "")
            {
                if( check(genreStr.Substring(0, genreStr.Length - 1)))
                    dt.downLoadFile(url, Path.Combine(o.Path,genreStr+"$$"+ url.Substring(url.LastIndexOf('=') + 1)));
                else
                    dt.downLoadFile(url, Path.Combine(o.Path, "notok", genreStr + "$$" + url.Substring(url.LastIndexOf('=') + 1)));
            }
            else
                dt.downLoadFile(url, Path.Combine(o.Path,"unknown", url.Substring(url.LastIndexOf('=') + 1)));
        }

        private bool check2(string name)
        {
            string[] okname = DlConfig.demo4["notokname"].ToString().Split(',');
            foreach (string s in okname)
                if (name.Contains(s))
                    return false;
            return true;
        }

        private bool check1(string name)
        {
            string[] okname = DlConfig.demo4["okname"].ToString().Split(',');
            foreach (string s in okname)
                if (name.Contains(s))
                    return true;
            return false;
        }

        private bool check(string genreStr)
        {
            bool hasKeyword = false;
            string[] notoks=DlConfig.demo4["notok"].ToString().Split(',');
            string[] oks = DlConfig.demo4["ok"].ToString().Split(',');
            string[] genres = genreStr.Split(',');
            foreach (string s in genres)
                if (notoks.Contains(s))
                {
                    return false;
                }
            foreach (string s in genres)
                if (oks.Contains(s))
                {
                    return true;
                }
            foreach(string s in genres)
                if (DlConfig.demo4.Contains(s))
                {
                    hasKeyword = true;
                    string[] strs = DlConfig.demo4[s].ToString().Split(',');
                    foreach (string s1 in genres)
                        if (strs.Contains(s1))
                        {
                            hasKeyword = false;
                            break;
                        }
                    if (hasKeyword)
                        break;
                }
            return hasKeyword;
        }
    }
}
