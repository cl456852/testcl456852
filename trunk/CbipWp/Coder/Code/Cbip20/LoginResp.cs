using System.Text;
using Lxt2.Communication.Framework;


namespace Lxt2.Cbip.Api.Code.Cbip20
{
    public class LoginResp : AbsPackage
    {
        int _status; //状态,定长4个字节
        
        public LoginResp()
        {
            this.CommandID = AbsPackage.CBIP_LOGIN_RESP;
        }

        #region 字段封装
        /// <summary>
        /// 状态,定长4个字节
        /// 0：正确
        /// 1：消息结构错
        /// 2：非法源地址
        /// 3：认证错
        /// 4：版本错
        /// 5： 登录协议错
        /// 6：其他错误
        /// </summary>
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
        #endregion
        public override byte[] GetBody()
        {
            BuffBody.PutInt(this._status);
            return BuffBody.Buff;
        }

        public override void SetBody(byte[] body)
        {
            this.BuffBody.Buff = body;
            this._status = this.BuffBody.GetInt();
        }

        public override IPackage Clone()
        {
            return (IPackage)this.MemberwiseClone();
        }

        public override IPackage GetInstance()
        {
            return new LoginResp();
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append(this.GetType().Name);
            str.Append("    Status:");
            str.Append(this.Status);
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
