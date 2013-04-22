using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using GetSize;

namespace BLL
{
    class HelloJavAnalysis
    {
        Regex r=new Regex("<h1>[\\s\\S]*?</h1>");
        Regex imageRex = new Regex("none\" src=\".*\"");
        Regex torrentRex=new Regex("popupPos\\(.*,");
        public ArrayList alys(string content, string path)
        {
            ArrayList resList = new ArrayList();
            string[] contents = content.Split(new string[] { "bevel clear" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in contents)
            {
                if (!s.Contains("left center mgb10 desc radius"))
                    continue;
                His his = new His();
                his.OriginalHtml = s;
                string info = r.Match(s).Value;
                string[] infos=info.Split(new string[]{"<h1>","<br />","</h1>"},StringSplitOptions.RemoveEmptyEntries);
                his.Actress=infos[0];
                string[] infos1=infos[1].Split(',');
                his.Id=infos1[0];
                string size=infos1[2].Trim();
                his.Vid = r.Match(s).Value.Replace("<br />VID:", "").Trim();
                //if (his.Vid.Split('-').Length > 2)
                //{
                //    his.Vid = his.Vid.Substring(0, his.Vid.LastIndexOf('-'));
                //}
                his.Vid = his.Vid.Replace("-", "");
                string sizeDigit = size.Replace("Size:", "").Replace(".<br />", "").Replace("<br />", "");
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
                
                getHtml(his, size);
                resList.Add(his);
            }
            return resList;
        }

        //http://hellojav.com/include/file_down.php?idx=19631
        void getHtml(His his, string size)
        {
            string imgUrl = imageRex.Match(his.OriginalHtml).Value;
            string torrentUrl = torrentRex.Match(his.OriginalHtml).Value.Replace("popupPos(","").Replace(",","");
            his.Html = "<a href=\"" + torrentUrl + "><img src=\"" + imgUrl + "\"/></a><br>" + his.Vid + "<br>" + size + "<br>" + "<br>" + his.Actress + "<br>";

        }
    }
}
