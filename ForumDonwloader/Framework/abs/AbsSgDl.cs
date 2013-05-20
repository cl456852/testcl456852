using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.interf;
using Framework.tool;

namespace Framework.abs
{
    abstract public class AbsSgDl:ISinglePageDonwloader
    {
       protected DlTool dt = new DlTool();

       abstract public void Download(object obj);
       
    }
}
