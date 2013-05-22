using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Cbip.Api.Code.API;
using Lxt2.Communication.Framework;
using Lxt2.Communication.Framework.Util;
using System.Reflection;

namespace Lxt2.Cbip.Api.Code.Cbip20
{
    public class SubmitResp : AbsPackage, IApiSubmitResp
    {
        long _sysSeq;    //利信通唯一的序列号，定长8个字节；在status=0时产生，status!=0时此字段填0
        int _status;    //状态，定长4个字节: 0: 正确; 其它：失败
        long _clientReceiveTime;//记录接收时间

        /// <summary>
        /// 记录接收时间
        /// </summary>
        public long ClientReceiveTime
        {
            get { return _clientReceiveTime; }
            set { _clientReceiveTime = value; }
        }

        #region 封装字段
        /// <summary>
        /// 利信通唯一的序列号，定长8个字节；
        /// 在status=0时产生，status!=0时此字段填0
        /// </summary>
        public long SysSeq
        {
            get { return _sysSeq; }
            set { _sysSeq = value; }
        }
        
        /// <summary>
        /// 状态，定长4个字节: 
        /// 0: 正确; 其它：失败
        /// </summary>
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        #endregion
        public SubmitResp()
        {
            this.CommandID = AbsPackage.CBIP_SUBMIT_RESPONSE;
        }

        public override byte[] GetBody()
        {
            this.BuffBody.PutLong(this._sysSeq);
            this.BuffBody.PutInt(this._status);
            return this.BuffBody.Buff;
        }

        public override void SetBody(byte[] body)
        {
            this.BuffBody.Buff = body;
            this._sysSeq = this.BuffBody.GetLong();
            this._status = this.BuffBody.GetInt();
            this.ClientReceiveTime = ByteHelper.GetMilliSec();
        }

        public override IPackage Clone()
        {
            return (IPackage)this.MemberwiseClone();
        }

        public override IPackage GetInstance()
        {
            return new SubmitResp();
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append(this.GetType().Name);
            str.Append("    ClientReceiveTime:");
            str.Append(this.ClientReceiveTime);
            str.Append(" SysSeq:");
            str.Append(this.SysSeq);
            str.Append(" Status:");
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
