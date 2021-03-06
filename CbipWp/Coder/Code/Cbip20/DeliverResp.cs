﻿using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Communication.Framework;

namespace Lxt2.Cbip.Api.Code.Cbip20
{
    public class DeliverResp : AbsPackage
    {
        public DeliverResp()
        {
            this.CommandID = AbsPackage.CBIP_DELIVER_RESP;
        }

        int status;
        /// <summary>
        /// 状态: 0: 正确; 1: 失败
        /// </summary>
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public override byte[] GetBody()
        {
            this.BuffBody.PutInt(this.Status);
            return this.BuffBody.Buff;
        }

        public override void SetBody(byte[] body)
        {

        }

        public override IPackage Clone()
        {
            return (IPackage)this.MemberwiseClone();
        }

        public override IPackage GetInstance()
        {
            return new DeliverResp();
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
