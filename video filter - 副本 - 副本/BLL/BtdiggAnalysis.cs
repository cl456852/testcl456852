﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;

namespace BLL
{
    class BtdiggAnalysis
    {
        Regex nIn1Reg = new Regex(@"Permanent Link to \d\d In 1|Permanent Link to \d In 1|Permanent Link to \d\d-In-1|Permanent Link to \d-In-1");
        Regex r = new Regex("[a-z]+[- _]+[0-9]+");
        Regex sizeRegexGB = new Regex("大小:</span><span class=\"attr_val\">.*&nbsp;GB</span>");
        Regex sizeRegexMB = new Regex("大小:</span><span class=\"attr_val\">.*&nbsp;MB</span>");
        Regex actressRegex = new Regex("Actress : .*?</p>");
        Regex imageRex = new Regex("http://www.18-jav.com/wp-content/.*?jpg");
        Regex torrentRex = new Regex("http://www.18-jav.com/./\\?download=.*?\"");
        string invalid;
        public ArrayList alys(string content, string path,string vid)
        {
            ArrayList resList = new ArrayList();
            content = content.Replace("<b>", "").Replace("</b>", "");
            string[] contents = content.Split(new string[] { "class=\"idx\"" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in contents)
            {
                His his = new His();
                his.OriginalHtml = s;
                if (s.Contains("DOCTYPE HTML PUBLIC"))
                    continue;
                his.Vid = s.Split(new string[] { "\">", "</a></td></tr></tbody></table><table class=" }, StringSplitOptions.RemoveEmptyEntries)[3];

                MatchCollection match = r.Matches(his.Vid.ToLower());
                if (match.Count != 1)
                {
                    invalid += his.OriginalHtml + "/r/n";
                    continue;
                }
                his.Vid = match[0].Value.Replace(" ","").Replace("-","").Replace("_","");
                
                MatchCollection sizeMatch = sizeRegexGB.Matches(his.OriginalHtml);
                if (sizeMatch.Count > 0)
                {
                    his.Size = Convert.ToDouble(sizeMatch[0].Value.Replace("&nbsp;GB</span>", "").Replace("大小:</span><span class=\"attr_val\">", "")) * 1024;
                }
                if (sizeMatch.Count == 0)
                {
                    sizeMatch = sizeRegexMB.Matches(his.OriginalHtml);
                    his.Size = Convert.ToDouble(sizeMatch[0].Value.Replace("&nbsp;MB</span>", "").Replace("大小:</span><span class=\"attr_val\">", ""));
                }
                getHtml(his, his.Size.ToString());
                resList.Add(his);
            }
            Tool.WriteFile(path, invalid);
            return resList;
        }

        void getHtml(His his, string size)
        {

            his.Html = his.OriginalHtml;

        }
    }
}
