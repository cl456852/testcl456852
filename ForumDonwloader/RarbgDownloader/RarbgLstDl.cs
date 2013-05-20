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
        public override void Download(string url,string path)
        {
            List<string> list = new List<string>();


            string content = tool.GetHtml(url);
               list.Add(content  );
               if (path != null)
               {
                   tool.SaveFile(content, Path.Combine(path, url.Replace('/', '_').Replace(":", "^").Replace("?", "wenhao")));
               }
            
            
        }

       
    }
}
