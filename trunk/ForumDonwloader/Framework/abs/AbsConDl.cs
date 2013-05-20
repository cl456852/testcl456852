using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.interf;
using Framework.tool;

namespace Framework
{
	abstract public class AbsConDl:IContentDownloader
	{
        protected DlTool dt = new DlTool();
        abstract public void Download(string content,string path);
        
    }
}
