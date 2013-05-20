using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.interf

{
    public interface IListPageDownloader
    {
        List<string> Download(string url, int start, int end, string path);
    }
}
