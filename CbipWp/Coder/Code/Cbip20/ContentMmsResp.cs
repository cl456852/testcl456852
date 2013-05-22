using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Cbip.Api.Code.API;
using Lxt2.Communication.Framework;

namespace Lxt2.Cbip.Api.Code.Cbip20
{
    public class ContentMmsResp : AbsPackage, IApiSubmitResp
    {
        public ContentMmsResp()
        {
            this.CommandID = AbsPackage.CBIP_CONTENT_MMS_RESPONSE;
        }

        string sysResourceID;
        /// <summary>
        /// 系统生成的资源ID
        /// </summary>
        public string SysResourceID
        {
            get { return sysResourceID; }
            set { sysResourceID = value; }
        }

        int status;
        /// <summary>
        /// 状态: 0: 正确; 其它：失败
        /// </summary>
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public override byte[] GetBody()
        {
            //throw new NotImplementedException();
            this.BuffBody.PutGuidString(SysResourceID, 30);
            this.BuffBody.PutInt(Status);
            return this.BuffBody.Buff;
        }

        public override void SetBody(byte[] body)
        {
            this.BuffBody.Buff = body;
            
            this.SysResourceID =BuffBody.GetGuidString(30);
            this.Status = BuffBody.GetInt();

        }

        public override IPackage Clone()
        {
            return (IPackage)this.MemberwiseClone();
        }

        public override IPackage GetInstance()
        {
            return new ContentMmsResp();
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append(this.GetType().Name);
            str.Append("    Status:");
            str.Append(this.Status);
            str.Append(" SysResourceID:");
            str.Append(this.SysResourceID);
            str.Append(" TotalLength:");
            str.Append(this.TotalLength);
            str.Append(" CommandID:");
            str.Append(this.CommandID);
            str.Append(" SequenceID:");
            str.Append(this.SequenceID);
            str.Append(" CommandStatus:");
            str.Append(this.CommandStatus);
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
