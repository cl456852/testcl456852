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
    }
}
