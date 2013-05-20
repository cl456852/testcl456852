using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework.interf
{
    public interface ISinglePageDonwloader
    {
        List<string> Download(List<string> stringList, string path);
    }
}
