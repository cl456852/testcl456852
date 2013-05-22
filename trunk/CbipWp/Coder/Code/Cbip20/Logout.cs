using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Communication.Framework;

namespace Lxt2.Cbip.Api.Code.Cbip20
{
   public class Logout:AbsPackage
    {
        private int clientID;        //合作方编号，必添，定长4个字节
        private string userName;     //登录用户名，必添，定长16个字节
        private string passWord;     //登录密码，必添，定长16个字节

        public int ClientID
        {
            get { return clientID; }
            set { clientID = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        
        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }
      
       public Logout()
        {
            this.CommandID = AbsPackage.CBIP_LOGOUT;
            this.TotalLength = 56;
        }
       public override byte[] GetBody()
        {
            BuffBody.PutInt(ClientID);
            BuffBody.PutString(userName, 16);
            BuffBody.PutString(passWord, 16);
            return BuffBody.Buff;
        }

       public override void SetBody(byte[] body)
        {
            this.BuffBody.Buff = body;
            this.ClientID = BuffBody.GetInt();
            this.UserName = BuffBody.GetString(16);
            this.PassWord = BuffBody.GetString(16);

        }

       public override IPackage Clone()
       {
           return (IPackage)this.MemberwiseClone();
       }

       public override IPackage GetInstance()
       {
           return new Logout();
       }

       public override string ToString()
       {
           StringBuilder str = new StringBuilder();
           str.Append(this.GetType().Name);
           str.Append("    ClientID:");
           str.Append(this.ClientID);
           str.Append(" UserName:");
           str.Append(this.UserName);
           str.Append(" PassWord:");
           str.Append(this.PassWord);
           str.Append(" TotalLength:");
           str.Append(this.TotalLength);
           str.Append(" CommandID:");
           str.Append(this.CommandID);
           str.Append(" SequenceID:");
           str.Append(this.SequenceID);
           str.Append(" CommandStatus:");
           str.Append(this.CommandStatus);
           return str.ToString();
       }
       
    }
}
