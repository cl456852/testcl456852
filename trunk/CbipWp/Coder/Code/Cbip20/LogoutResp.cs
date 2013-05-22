﻿using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Communication.Framework;

namespace Lxt2.Cbip.Api.Code.Cbip20
{
   public class LogoutResp:AbsPackage
    {
        int _status; //状态,定长4个字节

        #region 字段封装
        /// <summary>
        /// 状态,定长4个字节
        /// 0：正确
        /// 1：失败
        /// </summary>
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
        #endregion

       public LogoutResp()
        {
            this.CommandID = AbsPackage.CBIP_LOGOUT_RESP;
        }
      
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
           return new LogoutResp();
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
