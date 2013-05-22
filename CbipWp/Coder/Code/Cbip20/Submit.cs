using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Communication.Framework.Util;
using Lxt2.Cbip.Api.Code.API;
using Lxt2.Communication.Framework;

namespace Lxt2.Cbip.Api.Code.Cbip20
{
    public class Submit : AbsPackage, IApiSubmit
    {

        long _clientSeq;         //用户提供的唯一序列号，定长8个字节
        string _srcNumber="";      //下发的源号码（默认填写 ""），定长21个字节
        byte _messagePriority;   //消息的优先级，定长1个字节，0 最低 --- 15 最高
        short _reportType;        //是否需要状态报告，定长2个字节，0：不需要；1：需要； 2：其它
        byte _messageFormat;     //下发消息格式，定长1个字节
        long _overTime;          //过期时间戳，定长8个字节
        long _sendTime;          //定时发送时间戳，定长8个字节
        string _linkID="";         //下行临时订购关系（默认填写 ""），定长20个字节
        int _sendGroupID;       //下发批次，（默认填写 0），定长4个字节（保留字段，暂无意义）
        int _productID;         //产品编号（必添字段，默认填写 0）定长4个字节
        byte _messageType = 0;      //消息的下发类型，定长1个字节 
        short _destMobileCount;   //接收信息的用户数量(小于100)（必添字段），定长2个字节
        String[] _destMobile;     //目的手机号（必添字段）,长度：destMobileCount * 21
        short _messageLength;     //消息的长度（必添字段），定长2个字节
        string _messageContent=""; //消息的内容（必添字段），变长
        short _signLen;           //短信签名长度（默认填写0），定长2个字节
        string _sign="";           //短信签名，变长
        short _customLen;         //扩展字段长度，定长2个字节
        string _custom="";         //扩展字段内容（即Cbip_Submit中扩展字段内容），变长
        long _clientSubmitTime; //下行数据记录提交时间(数据入滑动窗口时间)

        

        #region 封装字段
        /// <summary>
        /// 用户提供的唯一序列号，定长8个字节
        /// </summary>
        public long ClientSeq
        {
            get { return _clientSeq; }
            set { _clientSeq = value; }
        }
        
        /// <summary>
        /// 下发的源号码（默认填写 ""），定长21个字节
        /// </summary>
        public string SrcNumber
        {
            get { return _srcNumber; }
            set { _srcNumber = value; }
        }
        
        /// <summary>
        /// 消息的优先级，定长1个字节
        /// 0 最低 --- 15 最高
        /// </summary>
        public byte MessagePriority
        {
            get { return _messagePriority; }
            set { _messagePriority = value; }
        }
        
        /// <summary>
        /// 是否需要状态报告，定长2个字节
        /// 0：不需要；1：需要； 2：其它
        /// </summary>
        public short ReportType
        {
            get { return _reportType; }
            set { _reportType = value; }
        }
        
        /// <summary>
        /// 下发消息格式，定长一个字节，
        /// 0 ASCII串 3 短信写卡操作 4 二进制信息 8 UCS2编码 15 GBK编码 
        /// 31 wappush 32 长短信  34 个性化短信 41 加密短信 42 加密长短信
        /// </summary>
        public byte MessageFormat
        {
            get { return _messageFormat; }
            set { _messageFormat = value; }
        }
        
        /// <summary>
        /// 过期时间戳定长8个字节
        /// (1970年1月1日0点0分0秒0毫秒为起点的时间值，精确到毫秒)(如不填写默认使用当前时间+1天作为过期时间戳)
        /// </summary>
        public long OverTime
        {
            get { return _overTime; }
            set { _overTime = value; }
        }
        
        /// <summary>
        /// 定时发送时间戳，定长8个字节
        /// (1970年1月1日0点0分0秒0毫秒为起点的时间值，精确到毫秒,定时时间不能晚于当前时间+1天)（如不填写默认为立即发送）
        /// </summary>
        public long SendTime
        {
            get { return _sendTime; }
            set { _sendTime = value; }
        }
        
        /// <summary>
        /// 下行临时订购关系（默认填写 ""），定长20个字节
        /// </summary>
        public string LinkID
        {
            get { return _linkID; }
            set { _linkID = value; }
        }
        
        /// <summary>
        /// 下发批次，（默认填写 0），定长4个字节（保留字段，暂无意义）
        /// </summary>
        public int SendGroupID
        {
            get { return _sendGroupID; }
            set { _sendGroupID = value; }
        }
        
        /// <summary>
        /// 产品编号（必添字段，默认填写 0）定长4个字节
        /// </summary>
        public int ProductID
        {
            get { return _productID; }
            set { _productID = value; }
        }
        
        /// <summary>
        /// 消息的下发类型，定长1个字节    
        /// 0 免费下发 1 按条下发 2 包月下发        3 订阅请求 4 取消请求 5 包月扣费（保留字段，暂无意义）
        /// </summary>
        public byte MessageType
        {
            get { return _messageType; }
            set { _messageType = value; }
        }
        
        /// <summary>
        /// 接收信息的用户数量(小于100)（必添字段），定长2个字节
        /// </summary>
        private short DestMobileCount
        {
            get { return (short)this.DestMobile.Length; }
            //set { _destMobileCount = value; }
        }
        
        /// <summary>
        /// 目的手机号（必添字段）,长度：destMobileCount * 21
        /// </summary>
        public String[] DestMobile
        {
            get { return _destMobile; }
            set 
            {
                //判断组包个数是否超过设置的最大值
                if (value.Length <= StaticInfo.StaticHandle.MaxGroupSize)
                {
                    _destMobile = value; 
                }
                else
                {
                    throw new Exception("组包发送个数超过设置的最大限制：" +StaticInfo.StaticHandle.MaxGroupSize.ToString());
                    
                }
                
            }
        }
        
        /// <summary>
        /// 消息的长度（必添字段），定长2个字节
        /// </summary>
        public  short MessageLength
        {
            get { return ByteHelper.GetEncodedLength(this._messageContent); }
        }
        
        /// <summary>
        /// 消息的内容（必添字段），变长
        /// </summary>
        public string MessageContent
        {
            get { return _messageContent; }
            set {
                short len = ByteHelper.GetEncodedLength(value);
                if (len <= StaticInfo.StaticHandle.SmsMessageLength)
                {
                    _messageContent = value;
                }
                else
                {
                    throw new Exception("短信内容长度超过设置的最大限制：" + StaticInfo.StaticHandle.SmsMessageLength.ToString());
                }
            }
        }
        /// <summary>
        /// 短信签名长度（默认填写0），定长2个字节
        /// </summary>
        public  short SignLen
        {
            get { return ByteHelper.GetEncodedLength(this._sign); }
            
        }
        /// <summary>
        /// 短信签名，变长
        /// </summary>
        public string Sign
        {
            get { return _sign; }
            set { _sign = value; }
        }
        
        /// <summary>
        /// 扩展字段长度，定长2个字节
        /// </summary>
        public short CustomLen
        {
            get { return ByteHelper.GetEncodedLength(this.Custom); }
        }
        
        /// <summary>
        /// 扩展字段内容（即Cbip_Submit中扩展字段内容），变长
        /// </summary>
        public string Custom
        {
            get { return _custom; }
            set { _custom = value; }
        }

        /// <summary>
        /// 下行数据记录提交时间(数据入滑动窗口时间)
        /// </summary>
        public long ClientSubmitTime
        {
            get { return _clientSubmitTime; }
            set { _clientSubmitTime = value; }
        }

        #endregion

        public Submit()
        {
            this.CommandID = AbsPackage.CBIP_SUBMIT;
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <returns></returns>
        public override byte[] GetBody()
        {
            this.ClientSubmitTime = ByteHelper.GetMilliSec();
            this.BuffBody.PutLong(_clientSeq);//4
            this.BuffBody.PutString(_srcNumber, 21);//25
            this.BuffBody.PutByte(_messagePriority);//26
            this.BuffBody.PutShort(_reportType);
            this.BuffBody.PutByte(_messageFormat);
            this.BuffBody.PutLong(_overTime);
            this.BuffBody.PutLong(_sendTime);
            this.BuffBody.PutString(_linkID, 20);
            this.BuffBody.PutInt(_sendGroupID);
            this.BuffBody.PutInt(_productID);
            this.BuffBody.PutByte(_messageType);
            this.BuffBody.PutShort(DestMobileCount);

            for (int i = 0; i < _destMobile.Length; i++)
            {
                this.BuffBody.PutString(_destMobile[i], 21);
            }
                
            this.BuffBody.PutShort(MessageLength);
            this.BuffBody.PutString(_messageContent, MessageLength);
            this.BuffBody.PutShort(SignLen);
            this.BuffBody.PutString(_sign, SignLen);
            this.BuffBody.PutShort(CustomLen);
            this.BuffBody.PutString(_custom, CustomLen);


            return this.BuffBody.Buff;
            
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="body"></param>
        public override void SetBody(byte[] body)
        {
            this.BuffBody.Buff = body;
            this._clientSeq = this.BuffBody.GetLong();
            this._srcNumber = this.BuffBody.GetString(21);
            this._messagePriority = this.BuffBody.GetByte();
            this._reportType = this.BuffBody.GetShort();
            this._messageFormat = this.BuffBody.GetByte();
            this._overTime = this.BuffBody.GetLong();
            this._sendTime = this.BuffBody.GetLong();
            this._linkID = this.BuffBody.GetString(20);
            this._sendGroupID = this.BuffBody.GetInt();
            this._productID = this.BuffBody.GetInt();
            this._messageType = this.BuffBody.GetByte();
            this._destMobileCount = this.BuffBody.GetShort();
            
            String[] tempMobile = new String[this._destMobileCount];
            for (int i = 0; i < this._destMobileCount; i++)
            {
                tempMobile[i] = this.BuffBody.GetString(21);
            }

            this._destMobile = tempMobile;

            this._messageLength = this.BuffBody.GetShort();
            this._messageContent = this.BuffBody.GetString(this._messageLength);
            this._signLen = this.BuffBody.GetShort();
            this._sign = this.BuffBody.GetString(this._signLen);
            this._customLen = this.BuffBody.GetShort();
            this._custom = this.BuffBody.GetString(_customLen);

        }

        public override IPackage Clone()
        {
            return (IPackage)this.MemberwiseClone();
        }

        public override IPackage GetInstance()
        {
            return new Submit();
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append(this.GetType().Name);
            str.Append("   ClientSeq:");
            str.Append(this.ClientSeq);
            str.Append(" SrcNumber:");
            str.Append(this.SrcNumber);
            str.Append(" MessagePriority:");
            str.Append(this.MessagePriority);
            str.Append(" ReportType:");
            str.Append(this.ReportType);
            str.Append(" MessageFormat:");
            str.Append(this.MessageFormat);
            str.Append(" OverTime:");
            str.Append(this.OverTime);
            str.Append(" SendTime:");
            str.Append(this.SendTime);
            str.Append(" LinkID:");
            str.Append(this.LinkID);
            str.Append(" SendGroupID:");
            str.Append(this.SendGroupID);
            str.Append(" ProductID:");
            str.Append(this.ProductID);
            str.Append(" MessageType:");
            str.Append(this.MessageType);
            str.Append(" DestMobileCount:");
            str.Append(this.DestMobileCount);
            str.Append(" DestMobile:[");
            str.Append(DestMobile.ToString());
            str.Append("]  MessageLength:");
            str.Append(this.MessageLength);
            str.Append(" MessageContent:");
            str.Append(this.MessageContent);
            str.Append(" SignLen:");
            str.Append(this.SignLen);
            str.Append(" Sign:");
            str.Append(this.Sign);
            str.Append(" CustomLen:");
            str.Append(this.CustomLen);
            str.Append(" Custom:");
            str.Append(this.Custom);
            str.Append(" ClientSubmitTime:");
            str.Append(this.ClientSubmitTime);
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
