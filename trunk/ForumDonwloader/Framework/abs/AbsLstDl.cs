using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.interf;
using Framework.tool;

namespace Framework.abs
{
    abstract public class AbsLstDl:IListPageDownloader
	{
        protected DlTool tool = new DlTool();

        abstract public List<string> Download(string url, int start, int end, string path);
        
    }
}
