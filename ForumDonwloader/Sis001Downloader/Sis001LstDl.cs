using Framework.abs;
using Framework.tool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sis001Downloader
{
    public class Sis001LstDl : AbsLstDl
    {
        public override void Download(object obj)
        {
            AsynObj o = (AsynObj)obj;


            string content = Sis001DlTool.GetHtml(o.Url, true);
            if (content != "")
            {
                //string[] contents = content.Split(new string[] { "<td align=\"center\">" }, StringSplitOptions.RemoveEmptyEntries);
                //content = contents[1];
                if (o.Path != null)
                {
                    DlTool.SaveFile(content, Path.Combine(o.Path, o.Url.Replace('/', '_').Replace(":", "^").Replace("?", "wenhao")) + ".htm");

                }
                Sis001SgDl sgDl = new Sis001SgDl();
                AsynObj asynObj = new AsynObj();
                asynObj.Path = o.Path;
                asynObj.Content = content;
                ThreadPool.QueueUserWorkItem(sgDl.Download, asynObj);
            }


        }
    }
}
