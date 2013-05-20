using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.abs;
using System.Text.RegularExpressions;
using System.IO;

namespace RarbgDownloader
{
    public class RarbgSgDl : AbsSgDl
    {
        List<string> singlePageList = new List<string>();
        Regex regex = new Regex("href=\"/torrent/.*?\"");
        //http://rarbg.com/torrent/u1xt7vg
        public override List<string> Download(List<string> stringList, string path)
        {
            List<string> list = new List<string>();
            foreach (string listPage in stringList)
            {
                MatchCollection mc = regex.Matches(listPage);
                foreach (Match m in mc)
                {
                    if (!m.Value.Contains("#comments"))
                    {
                        string content = dt.GetHtml("http://rarbg.com/" + m.Value);
                        singlePageList.Add(content);
                        if (path != null)
                            dt.SaveFile(content, Path.Combine(path, m.Value.Replace('/', '_').Replace(":", "^").Replace("?", "wenhao")));
                    }
                }
            }
            return singlePageList;
        }
    }
}
