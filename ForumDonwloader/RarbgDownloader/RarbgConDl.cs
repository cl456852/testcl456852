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
        public override void Download(string content,string path)
        {
            string url ="http://rarbg.com/"+ regex.Match(content).Value;
            dt.downLoadFile(url,Path.Combine(path, url.Substring(url.LastIndexOf('=')+1)));
        }
    }
}
