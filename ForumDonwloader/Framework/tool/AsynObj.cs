using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.interf;

namespace Framework.tool
{
    public class AsynObj
    {
        public AsynObj()
        {

        }
        public AsynObj(string path, string url)
        {
            Path = path;
            Url = url;
        }
        string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        string htmlName;

        public string HtmlName
        {
            get { return htmlName; }
            set { htmlName = value; }
        }

        string singleContent;

        public string SingleContent
        {
            get { return singleContent; }
            set { singleContent = value; }
        }
    }
}
