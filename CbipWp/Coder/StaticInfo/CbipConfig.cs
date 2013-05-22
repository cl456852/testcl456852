using System;
using System.Collections.Generic;
using System.Text;

namespace Lxt2.Cbip.Api.StaticInfo
{
    public class CbipConfig
    {
        int port;

        public int Port
        {
            get { return port; }
            set { port = value; }
        }
        string ip;

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }
        int clientID;

        public int ClientID
        {
            get { return clientID; }
            set { clientID = value; }
        }
        string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        byte loginType=1;

        public byte LoginType
        {
            get { return loginType; }
            set { loginType = value; }
        }
        byte version=2;

        public byte Version
        {
            get { return version; }
            set { version = value; }
        }
    }
}
