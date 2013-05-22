using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Communication.Framework.Handle;
using Lxt2.Communication.Framework;

namespace Lxt2.Cbip.Api.Code.Cbip20
{
    public class Login : AbsPackage
    {

        //定长字符串，位数不足时左对齐，右补二进制的零
        private int clientID;        //合作方编号，必添，定长4个字节
        private string userName;     //登录用户名，必添，定长16个字节
        private string passWord;     //登录密码，必添，定长16个字节
        private byte loginType;      //登录类型，定长1个字节（0：上下行兼容 1： 只下行 2：只上行）
        private byte version;        //定长1个字节

        public Login()
        {
            this.CommandID = AbsPackage.CBIP_LOGIN;
        }

        #region 封装字段
        /// <summary>
        /// 合作方编号
        /// 必添，定长4个字节
        /// </summary>
        public int ClientID
        {
            get { return clientID; }
            set { clientID = value; }
        }

        /// <summary>
        /// 登录用户名
        /// 必添，定长16字节
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// 登录密码
        /// 必添，定长16字节
        /// </summary>
        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }


        /// <summary>
        /// 登录类型
        /// 定长1个字节（0：上下行兼容 1： 只下行 2：只上行）
        /// </summary>
        public byte LoginType
        {
            get { return loginType; }
            set { loginType = value; }
        }

        /// <summary>
        /// 双方协商的版本号,定长1个字节
        /// (高位4bit表示主版本号,低位4bit表示次版本号)，对于2.0的版本，高4bit为2，低4位为0
        /// </summary>
        public byte Version
        {
            get { return version; }
            set { version = value; }
        }
        #endregion

        public override byte[] GetBody()
        {
            BuffBody.PutInt(clientID);
            BuffBody.PutString(userName, 16);
            BuffBody.PutString(passWord, 16);
            BuffBody.PutByte(loginType);
            BuffBody.PutByte(version);
            return this.BuffBody.Buff;
        }

        public override void SetBody(byte[] body)
        {
            this.BuffBody.Buff = body;
            this.clientID = BuffBody.GetInt();
            this.userName = BuffBody.GetString(16);
            this.passWord = BuffBody.GetString(16);
            this.loginType = BuffBody.GetByte();
            this.version = BuffBody.GetByte();
        }

        public override IPackage Clone()
        {
            return (IPackage)this.MemberwiseClone();
        }

        public override IPackage GetInstance()
        {
            return new Login();
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
            str.Append(" LoginType:");
            str.Append(this.LoginType);
            str.Append(" Version:");
            str.Append(this.Version);
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
