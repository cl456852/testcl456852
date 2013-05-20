using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.interf

{
    public interface IListPageDownloader
    {
        void Download(string url, string path);
    }
}
