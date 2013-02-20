using System;
using System.Collections.Generic;
using System.Text;

namespace MODEL
{
    [Serializable]
    public class UserState
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
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
    }
}
