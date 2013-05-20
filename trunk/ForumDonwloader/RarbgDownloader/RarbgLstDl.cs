using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.interf;
using Framework.abs;
using System.IO;

namespace RarbgDownloader
{
    public class RarbgLstDl : AbsLstDl
    {
        public override List<string> Download(string url, int start, int end,string path)
        {
            List<string> list = new List<string>();
            for (int i = start; i <= end; i++)
            {
                string urlStr = string.Format(url, i);
                string content = tool.GetHtml(urlStr);
               list.Add(content  );
               if (path != null)
               {
                   tool.SaveFile(content, Path.Combine(path, urlStr.Replace('/', '_').Replace(":", "^").Replace("?", "wenhao")));
               }
            }
            return list;
        }

       
    }
}
