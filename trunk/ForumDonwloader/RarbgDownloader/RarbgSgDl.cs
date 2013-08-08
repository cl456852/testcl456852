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
        List<string> singlePageList = new List<string>();
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
                    if (!m.Value.Contains("#comments"))
                    {
                        ThreadPool.QueueUserWorkItem(work, new AsynObj(o.Path, "http://rarbg.com"+m.Value.Replace("href=","").Replace("\"","")));
                    }
                }
            
          
        }

        private void work(object obj)
        {
            MatchCollection genresMatches;
            AsynObj o = (AsynObj)obj;
            string content = dt.GetHtml(o.Url);
            singlePageList.Add(content);
            if (obj != null)
                dt.SaveFile(content, Path.Combine(o.Path, o.Url.Replace('/', '_').Replace(":", "^").Replace("?", "wenhao"))+".htm");
            Match genres = genresRegex.Match(content);
            if (genres.Value != "")
                genresMatches = genresRegex1.Matches(genres.Value);
            string url = "http://rarbg.com/" + torrentRegex.Match(content).Value;
            dt.downLoadFile(url, Path.Combine(o.Path, url.Substring(url.LastIndexOf('=') + 1)));
        }

        private bool check(string genreStr)
        {
            string[] genres = genreStr.Split(',');

        }
    }
}
