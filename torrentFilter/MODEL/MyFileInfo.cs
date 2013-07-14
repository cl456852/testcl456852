using System;
using System.Collections;
using System.Text;

using System.IO;


namespace MODEL
{
    public class MyFileInfo
    {

        int fileId;

        public int FileId
        {
            get { return fileId; }
            set { fileId = value; }
        }
        int cdId;

        public int CdId
        {
        get { return cdId; }
            set { cdId = value; }
        }
        string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        string directory;

        public string Directory
        {
            get { return directory; }
            set { directory = value; }
        }
        string directoryName;

        public string DirectoryName
        {
            get { return directoryName; }
            set { directoryName = value; }
        }
        double length;

        public double Length
        {
            get { return length; }
            set { length = value; }
        }


        private string lastAccessTime;

        public string LastAccessTime
        {
            get { return lastAccessTime; }
            set { lastAccessTime = value; }
        }

        private string lastWriteTime;

        public string LastWriteTime
        {
            get { return lastWriteTime; }
            set { lastWriteTime = value; }
        }


        string extension;

        public string Extension
        {
            get { return extension; }
            set { extension = value; }
        }

        string mark;

        public string Mark
        {
            get { return mark; }
            set { mark = value; }
        }




    }
}
