using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Communication.Framework.Util;
using Lxt2.Communication.Framework;

namespace Lxt2.Cbip.Api.Code.Cbip20
{
    public class Deliver : AbsPackage
    {
        public Deliver()
        {
            this.CommandID = AbsPackage.CBIP_DELIVER;
        }

        long sysSeq;
        /// <summary>
        /// 利信通提供的唯一序列号
        /// </summary>
        public long SysSeq
        {
            get { return sysSeq; }
            set { sysSeq = value; }
        }

        string srcMobile;
        /// <summary>
        /// 源手机号（必添字段）
        /// </summary>
        public string SrcMobile
        {
            get { return srcMobile; }
            set { srcMobile = value; }
        }

        string destNumber;
        /// <summary>
        /// 目的号码（默认填写 ""）
        /// </summary>
        public string DestNumber
        {
            get { return destNumber; }
            set { destNumber = value; }
        }

        byte messageFormat;
        /// <summary>
        /// 消息的编码格式 0 ASCII串 3 短信写卡操作 4 二进制信息 8 UCS2编码 15 GBK编码 30 彩信 
        /// </summary>
        public byte MessageFormat
        {
            get { return messageFormat; }
            set { messageFormat = value; }
        }

        string linkID;
        /// <summary>
        /// 临时订购关系（默认填写 ""）
        /// </summary>
        public string LinkID
        {
            get { return linkID; }
            set { linkID = value; }
        }

        byte operatorID;
        /// <summary>
        /// 运营商编号  1 移动 2 联通 3 电信
        /// </summary>
        public byte OperatorID
        {
            get { return operatorID; }
            set { operatorID = value; }
        }
        /// <summary>
        /// 网关编号
        /// </summary>
        byte geteWayID;
        /// <summary>
        /// 消息的长度（必添字段）
        /// </summary>
        public byte GeteWayID
        {
            get { return geteWayID; }
            set { geteWayID = value; }
        }

        short messageLength;
        /// <summary>
        /// 消息的内容（必添字段）
        /// </summary>
        private short MessageLength
        {
            get { return ByteHelper.GetEncodedLength(this.MessageContent); }
        }

        string messageContent;
        /// <summary>
        /// 
        /// </summary>
        public string MessageContent
        {
            get { return messageContent; }
            set { messageContent = value; }
        }

        long _clientReceiveTime;//记录接收时间
        /// <summary>
        /// 记录接收时间
        /// </summary>
        public long ClientReceiveTime
        {
            get { return _clientReceiveTime; }
            set { _clientReceiveTime = value; }
        }

        public override byte[] GetBody()
        {
            return null;
        }

        public override void SetBody(byte[] body)
        {
            this.BuffBody.Buff = body;
            this.ClientReceiveTime = ByteHelper.GetMilliSec();
            this.SysSeq = this.BuffBody.GetLong();
            this.SrcMobile = this.BuffBody.GetString(21);
            this.DestNumber = this.BuffBody.GetString(21);
            this.MessageFormat = this.BuffBody.GetByte();
            this.LinkID = this.BuffBody.GetString(20);
            this.OperatorID = this.BuffBody.GetByte();
            this.GeteWayID = this.BuffBody.GetByte();
            this.MessageContent= this.BuffBody.GetStringByFmt( this.BuffBody.GetShort(),this.MessageFormat);
        }

        public override IPackage Clone()
        {
            return (IPackage)this.MemberwiseClone();
        }

        public override IPackage GetInstance()
        {
            return new Deliver();
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append(this.GetType().Name);
            str.Append("    ClientReceiveTime:");
            str.Append(this.ClientReceiveTime);
            str.Append(" sysSeq:");
            str.Append(this.sysSeq);
            str.Append(" SrcMobile:");
            str.Append(this.SrcMobile);
            str.Append(" DestNumber:");
            str.Append(this.DestNumber);
            str.Append(" MessageFormat:");
            str.Append(this.MessageFormat);
            str.Append(" LinkID:");
            str.Append(this.LinkID);
            str.Append(" OperatorID:");
            str.Append(this.OperatorID);
            str.Append(" GeteWayID:");
            str.Append(this.GeteWayID);
            str.Append(" MessageContent:");
            str.Append(this.MessageContent);
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
