using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Communication.Framework;
using Lxt2.Communication.Framework.Util;

namespace Lxt2.Cbip.Api.Code.Cbip20
{
    public class Report : AbsPackage
    {
        public Report()
        {
            this.CommandID = AbsPackage.CBIP_REPORT;
        }

        long clientSeq;
        /// <summary>
        /// 用户提供的唯一序列号
        /// </summary>
        public long ClientSeq
        {
            get { return clientSeq; }
            set { clientSeq = value; }
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

        string destMobile;
        /// <summary>
        /// 目的手机号（必添字段）
        /// </summary>
        public string DestMobile
        {
            get { return destMobile; }
            set { destMobile = value; }
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

        byte gatewayID;
        /// <summary>
        /// 网关ID
        /// </summary>
        public byte GatewayID
        {
            get { return gatewayID; }
            set { gatewayID = value; }
        }

        int status;
        /// <summary>
        /// 状态   0 发送成功 1 利信通失败 2 运营商失败
        /// </summary>
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        string errorCode;
        /// <summary>
        /// 消息错误码（默认填写 ""）
        /// </summary>
        public string ErrorCode
        {
            get { return errorCode; }
            set { errorCode = value; }
        }

        byte pkTotal;
        /// <summary>
        /// 同一条短信的拆分总数
        /// </summary>
        public byte PkTotal
        {
            get { return pkTotal; }
            set { pkTotal = value; }
        }

        byte pkNumber;
        /// <summary>
        /// 同一条短信的拆分序号
        /// </summary>
        public byte PkNumber
        {
            get { return pkNumber; }
            set { pkNumber = value; }
        }

        byte messageFormat;
        /// <summary>
        /// 下发消息格式 0 ASCII串 3 短信写卡操作 4 二进制信息 8 UCS2编码 15 GBK编码 30 彩信 31 wappush 32 长短信 33 个性化彩信 34 个性化短信 41 加密短信 42 加密长短信 
        /// </summary>
        public byte MessageFormat
        {
            get { return messageFormat; }
            set { messageFormat = value; }
        }

        short contentLen;
        /// <summary>
        /// 短信拆分内容长度
        /// </summary>
        public short ContentLen
        {
            get { return contentLen; }
            set { contentLen = value; }
        }

        string content;
        /// <summary>
        /// 短信拆分内容
        /// </summary>
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        short customLen;
        /// <summary>
        /// 扩展字段长度
        /// </summary>
        public short CustomLen
        {
            get { return customLen; }
            set { customLen = value; }
        }

        string custom;
        /// <summary>
        /// 扩展字段内容（即Cbip_Submit中扩展字段内容）
        /// </summary>
        public string Custom
        {
            get { return custom; }
            set { custom = value; }
        }

        public override byte[] GetBody()
        {
            return null;
     
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


        public override void SetBody(byte[] body)
        {
            this.BuffBody.Buff = body;
            this.ClientReceiveTime = ByteHelper.GetMilliSec();
            this.ClientSeq = this.BuffBody.GetLong();
            this.SysSeq = this.BuffBody.GetLong();
            this.DestMobile = this.BuffBody.GetString(21);
            this.OperatorID = this.BuffBody.GetByte();
            this.GatewayID = this.BuffBody.GetByte();
            this.Status = this.BuffBody.GetInt();
            this.ErrorCode = this.BuffBody.GetString(8);
            this.PkTotal = this.BuffBody.GetByte();
            this.PkNumber = this.BuffBody.GetByte();
            this.MessageFormat = this.BuffBody.GetByte();
            this.Content = this.BuffBody.GetString(this.BuffBody.GetShort());
            this.Custom = this.BuffBody.GetString(this.BuffBody.GetShort());


        }

        public override IPackage Clone()
        {
            return (IPackage)this.MemberwiseClone();
        }

        public override IPackage GetInstance()
        {
            return new Report();
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append(this.GetType().Name);
            str.Append("    ClientSeq:");
            str.Append(this.ClientSeq);
            str.Append(" DestMobile:");
            str.Append(this.DestMobile);
            str.Append(" OperatorID:");
            str.Append(this.OperatorID);
            str.Append(" GatewayID:");
            str.Append(this.GatewayID);
            str.Append(" Status:");
            str.Append(this.Status);
            str.Append(" ErrorCode:");
            str.Append(this.ErrorCode);
            str.Append(" PkTotal:");
            str.Append(this.PkTotal);
            str.Append(" PkNumber:");
            str.Append(this.PkNumber);
            str.Append(" MessageFormat:");
            str.Append(this.MessageFormat);
            str.Append(" Content:");
            str.Append(this.Content);
            str.Append(" Custom:");
            str.Append(this.Custom);
            str.Append(" TotalLength:");
            str.Append(this.TotalLength);
            str.Append(" CommandID:");
            str.Append(this.CommandID);
            str.Append(" SequenceID:");
            str.Append(this.SequenceID);
            str.Append(" CommandStatus:");
            str.Append(this.CommandStatus);
            str.Append(" ClientReceiveTime:");
            str.Append(this.ClientReceiveTime);
            return str.ToString();
        }
    }
}
