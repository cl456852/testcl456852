﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.interf;
using Framework.abs;
using System.IO;
using RarbgDownloader;
using System.Threading;
using Framework.tool;

namespace RarbgDownloader
{
    public class RarbgLstDl : AbsLstDl
    {
        
        public override void Download(object obj)
        {
            AsynObj o = (AsynObj)obj;


            string content = DlTool.GetHtml(o.Url,DlConfig.useProxy);
            if (content != "")
            {
                string[] contents = content.Split(new string[] { "<td align=\"center\">" }, StringSplitOptions.RemoveEmptyEntries);
                content = contents[1];
                if (o.Path != null)
                {
                    DlTool.SaveFile(content, Path.Combine(o.Path, o.Url.Replace('/', '_').Replace(":", "^").Replace("?", "wenhao")) + ".htm");

                }
                RarbgSgDl sgDl = new RarbgSgDl();
                AsynObj asynObj = new AsynObj();
                asynObj.Path = o.Path;
                asynObj.Content = content;
                ThreadPool.QueueUserWorkItem(sgDl.Download, asynObj);
            }
            
            
        }

       
    }
}
