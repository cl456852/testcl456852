using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using BLL;
using System.IO;

namespace BLL
{
    public class _18javAnaysis
    {
        Regex nIn1Reg = new Regex(@"Permanent Link to \d\d In 1|Permanent Link to \d In 1|Permanent Link to \d\d-In-1|Permanent Link to \d-In-1");
        Regex r = new Regex("Download <strong>.*?</strong>");
        Regex sizeRegex = new Regex("Size: .*?.<br />");
        Regex actressRegex = new Regex("Actress : .*?</p>");
        Regex imageRex = new Regex("http://www.18-jav.com/wp-content/.*?jpg");
        Regex torrentRex=new Regex("http://www.18-jav.com/./\\?download=.*?\"");
        public ArrayList alys(string content,string path)
        {
            ArrayList resList = new ArrayList();
            string[] contents = content.Split(new string[] { "<h3>" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in contents)
            {
                if (!s.Contains("Torrent Name"))
                    continue;
                if (nIn1Reg.IsMatch(s))
                {
                    if (!File.Exists(path))
                    {
                        File.Create(path).Close() ;
                    }
                    StreamWriter sw = File.AppendText(path);
                    sw.Write(s);
                    sw.Flush();
                    sw.Close();
                    continue;
                }
                His his = new His();
                his.OriginalHtml = s;
                his.Vid = r.Match(s).Value.Replace("Download <strong>", "").Replace("</strong>", "");
                if (his.Vid.Split('-').Length > 2 && Tool.IsNum( his.Vid.Split('-')[1]))   //对于xxx-av-1234的修改，只考虑两个-的情况
                {
                    his.Vid = his.Vid.Substring(0, his.Vid.LastIndexOf('-'));
                }
                his.Vid=his.Vid.Replace("-","");
                string size = sizeRegex.Match(s).Value.Replace("Size: ", "").Replace(".&nbsp;", "");
                string sizeDigit = size.Replace("Size:", "").Replace(".<br />", "").Replace("<br />","");
                try
                {
                    if (sizeDigit.EndsWith("GB"))
                    {
                        sizeDigit = sizeDigit.Replace("GB", "");
                        his.Size = Convert.ToDouble(sizeDigit) * 1024;

                    }
                    else
                        his.Size = Convert.ToDouble(sizeDigit.Replace("MB", "").Replace("KB", ""));
                }
                catch (Exception e)
                {
                    Console.WriteLine("CAN NOT FIND SIZE!!");
                    Console.WriteLine(e.Message);
                }
                his.Actress = actressRegex.Match(s).Value.Replace("Actress : ", "").Replace("</p>", "");
                getHtml(his, size);
                resList.Add(his);
            }
            return resList;
        }

       // <a href="http://javjunkies.com/main/JavJ.php?k=2313&file=9054f637431322f526f6c615f54616b697a6177612e4d4153303837"><img src="./result1_files/118mas087pl.jpg"></a><br>
//Rola_Takizawa<br>Vídeo Id: MAS087<br>Sìze: 3.78GB<br><u>Fíles in tørrent:</u> 20 fìles<br>MAS ⁰  ⁸  ⁷ .ISO<br>
        void getHtml(His his,string size)
        {
            string imgUrl =imageRex.Match( his.OriginalHtml).Value;
            string torrentUrl = torrentRex.Match(his.OriginalHtml).Value;
            his.Html="<a href=\""+torrentUrl+"><img src=\""+imgUrl+"\"/></a><br>"+his.Vid+"<br>"+size+"<br>"+"<br>"+his.Actress+"<br>";
            
        }


    }
}
