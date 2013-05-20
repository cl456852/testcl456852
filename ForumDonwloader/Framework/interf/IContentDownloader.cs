using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.interf
{
    public interface IContentDownloader
    {
        void Download(string content,string path);
    }
}
