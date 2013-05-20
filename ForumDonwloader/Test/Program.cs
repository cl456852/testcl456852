using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.tool;

namespace RarbgDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.test();
        }

        void test()
        {
            DlTool dt = new DlTool();
            dt.downLoadFile("http://rarbg.com/download.php?id=sgxzclp&f=Layered-Nylons.13.05.06.Gracie.XXX.720p.WMV-GAGViD-[rarbg.com].torrent","d:\\a.torrent");
        }

    }
}
