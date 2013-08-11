using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Configuration;

namespace RarbgDownloader
{
    public class DlConfig
    {
        public static IDictionary demo4 = ConfigurationManager.GetSection("genres") as IDictionary;

        public static List<string> storage = new List<string>();
    }
}
