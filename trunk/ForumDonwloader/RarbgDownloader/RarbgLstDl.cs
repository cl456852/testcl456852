using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.interf;
using Framework.abs;
using System.IO;
using RarbgDownloader;
using System.Threading;

namespace RarbgDownloader
{
    public class RarbgLstDl : AbsLstDl
    {
        
        public override void Download(object obj)
        {
            AsynObj o = (AsynObj)obj;
            List<string> list = new List<string>();


            string content = tool.GetHtml(o.Url);
               list.Add(content  );
               if (o.Path != null)
               {
                   tool.SaveFile(content, Path.Combine(o.Path, o.Url.Replace('/', '_').Replace(":", "^").Replace("?", "wenhao")));
               
               }
               RarbgSgDl sgDl = new RarbgSgDl();
               AsynObj asynObj = new AsynObj();
               asynObj.Path = o.Path;
               asynObj.Content = content;
               ThreadPool.QueueUserWorkItem(sgDl.Download, asynObj);
            
            
        }

       
    }
}
