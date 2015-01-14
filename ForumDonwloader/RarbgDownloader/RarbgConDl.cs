using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;
using System.Text.RegularExpressions;
using System.IO;

namespace RarbgDownloader
{
    //http://rarbg.com/download.php?id=sgxzclp&f=Layered-Nylons.13.05.06.Gracie.XXX.720p.WMV-GAGViD-[rarbg.com].torrent
    public class RarbgConDl : AbsConDl
    {
        Regex regex=new Regex(@"download.php\?id=.*?\.torrent");
        Regex genresRegex = new Regex(@"Genres.*</a>");
        Regex genresRegex1 = new Regex("search=.*?\"");
        public override void Download(string content,string path)
        {
            MatchCollection genresMatches;
            Match genres = genresRegex.Match(content);
            if(genres.Value!="")
                genresMatches = genresRegex1.Matches(genres.Value);

            string url ="http://rarbg.com/"+ regex.Match(content).Value;
            //dt.downLoadFile(url,Path.Combine(path, url.Substring(url.LastIndexOf('=')+1)),DlConfig.useProxy);
        }
    }
}
