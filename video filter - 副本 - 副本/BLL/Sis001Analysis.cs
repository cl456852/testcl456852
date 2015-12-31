using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BLL
{
    class Sis001Analysis
    {
        Regex idRegex = new Regex("[A-Z]{1,}-[0-9]{1,}");
        Regex sizeRegex=new Regex("size\\^\\^\\^.*");
        Regex imgRegex=new Regex("<img src=\".*?\"");
        Regex torrentLinkRegex=new Regex("attachment.php.*?\"");
        Regex reg1 = new Regex("[A-Z]");
        public ArrayList alys(string content, string path, string vid)
        {
            ArrayList resList = new ArrayList();
            MatchCollection mc =idRegex.Matches( Path.GetFileNameWithoutExtension(path.ToUpper()));
            if(mc.Count!=1)
            {
                String unknownPath = Path.Combine(Path.GetDirectoryName(path), "sisUnknown");
                if (!Directory.Exists(unknownPath))
                    Directory.CreateDirectory(unknownPath);
                File.Move(path,Path.Combine(unknownPath,Path.GetFileNameWithoutExtension(path))+".htm");
                return resList;
            }
            His his = new His();
            his.Vid = mc[0].Value.Replace("-","");
            his.Size = Convert.ToDouble(sizeRegex.Match(path).Value.Replace("size^^^", "").Replace(".htm",""));
            his.Html = content.Split(new string[] { "count_add_one", "下载次数:" }, StringSplitOptions.RemoveEmptyEntries)[1];
            string torrentLink ="http://sis001.com/bbs/"+ torrentLinkRegex.Match(his.Html).Value;
            
            MatchCollection imgMc = imgRegex.Matches(his.Html);
            his.Html = "";
            foreach(Match match in imgMc)
            {
                if(!match.Value.Contains("torrent.gif"))
                {
                    his.Html += "<a href=\"" + torrentLink + "\">" + match.Value + "/></a><br>";
                }
            }

                    string letter = "";
                string number = "";
                bool isEndofLetter = false;
            foreach(char c in his.Vid)
            {
                    if (reg1.IsMatch(c.ToString()))
                    {
                        if (isEndofLetter)
                            break;
                        else
                            letter += c;
                    }
                    else
                    {
                        number +=c;
                        isEndofLetter = true;
                    }
            }
            his.Html += "<a href=\"https://www.google.com.tw/search?um=1&newwindow=1&safe=off&hl=zh-CN&biw=1362&bih=839&dpr=1&ie=UTF-8&tbm=isch&source=og&sa=N&tab=wi&ei=QKr6U8KMKtOWaqbigogK&q=" + his.Vid + "\"/>" + his.Vid + "</a><br>";
            his.Html += his.Size + "<br>";
            his.Html += "<a href=\"http://btdigg.org/search?info_hash=&q=" + letter + "+" + number + "\">" + his.Vid + "</a><br><br><br>\n";
            resList.Add(his);
            return resList;
        }
    }
}
