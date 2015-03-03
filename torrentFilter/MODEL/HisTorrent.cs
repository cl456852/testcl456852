using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MODEL
{
    public class HisTorrent
    {
        int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        string file;

        public string File
        {
            get { return file; }
            set { file = value; }
        }
        double size;

        public double Size
        {
            get { return size; }
            set { size = value; }
        }
        string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        DateTime createTime;

        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        string ext;

        public string Ext
        {
            get { return ext; }
            set { ext = value; }
        }

        string md5;

        public string Md5
        {
            get { return md5; }
            set { md5 = value; }
        }

        string filteredFileName;

        public string FilteredFileName
        {
            get 
            {
                string s=path.Substring(path.LastIndexOf('$'));
            }
        }
    }
}
