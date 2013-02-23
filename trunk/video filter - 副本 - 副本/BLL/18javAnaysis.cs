using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace BLL
{
    public class _18javAnaysis
    {
        Regex r = new Regex("Torrent Name: .*?<br />");
        Regex sizeRegex = new Regex("Size: .*?.&nbsp;");
        public ArrayList alys(string content)
        {
            ArrayList resList = new ArrayList();
            string[] contents = content.Split(new string[] { "<h3>" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in contents)
            {
                string id = r.Match(s).Value.Replace("Torrent Name: ","").Replace("<br />","").Replace("-","");


            }
            return resList;
        }
    }
}
