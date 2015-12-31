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


        abstract public void Download(object obj);
        
    }
}
