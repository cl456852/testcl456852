using System;
using System.Collections.Generic;
using System.Text;

namespace MODEL
{
    [Serializable]
    public class User
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
        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private string tel;

        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private UserState user_state;

        public UserState User_state
        {
            get { return user_state; }
            set { user_state = value; }
        }
    }
}
