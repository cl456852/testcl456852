using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace BLL
{
    public class UnknowHtmlGernerator
    {
        public void process(string path)
        {
            Regex posterRex = new Regex("Poster:<br></td>.*</td></tr>");
            Regex descRex = new Regex("Description:</td>.*<div");
            Regex torrentRex = new Regex("Torrent:</td>.*</tr>");
            DirectoryInfo theFolder = new DirectoryInfo(path);
            string result="";
            foreach (FileInfo file in theFolder.GetFiles())
            {
                StreamReader sr = new StreamReader(file.FullName);
                string content = sr.ReadToEnd();
                string poster = posterRex.Match(content).Value.Replace("//","http://");
                string desc = descRex.Match(content.Replace("\r\n","")).Value.Replace("<div","");
                string torrent = torrentRex.Match(content).Value.Replace("/download.php?", "http://rarbg.com/download.php?");
                result += poster + "<br>\r\n" + desc + "<br>\r\n" + torrent + "<br>\r\n";
            }
            File.WriteAllText(Path.Combine(path,"result.htm"),result);
        }
    }
}
