using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Communication.Framework.Util;
using Lxt2.Cbip.Api.Code.API;
using Lxt2.Communication.Framework;

namespace Lxt2.Cbip.Api.Code.Cbip20
{
    public class SubmitMMS : AbsPackage, IApiSubmit
    {
        public SubmitMMS()
        {
            this.CommandID = AbsPackage.CBIP_SUBMIT_MMS;
        }

        long clientSeq;
        /// <summary>
        /// 客户编号
        /// </summary>
        public long ClientSeq
        {
            get { return clientSeq; }
            set { clientSeq = value; }
        }

        string srcNumber="";
        /// <summary>
        /// 下发的源号码（默认填写 ""）
        /// </summary>
        public string SrcNumber
        {
            get { return srcNumber; }
            set { srcNumber = value; }
        }
        byte messagePriority;
        /// <summary>
        /// 消息的优先级      0 最低 --- 15 最高
        /// </summary>
        public byte MessagePriority
        {
            get { return messagePriority; }
            set { messagePriority = value; }
        }
        byte reportType;
        /// <summary>
        /// 是否需要状态报告  0 不需要 1 需要 2 其它
        /// </summary>
        public byte ReportType
        {
            get { return reportType; }
            set { reportType = value; }
        }
        byte messageFormat;
        /// <summary>
        /// "消息格式 30：普通彩信33：个性化彩信"
        /// </summary>
        public byte MessageFormat
        {
            get { return messageFormat; }
            set { messageFormat = value; }
        }

        long overTime;
        /// <summary>
        /// 过期时间戳(1970年1月1日0点0分0秒0毫秒为起点的时间值，精确到毫秒)
        /// </summary>
        public long OverTime
        {
            get { return overTime; }
            set { overTime = value; }
        }
        long sendTime;
        /// <summary>
        /// 定时发送时间(1970年1月1日0点0分0秒0毫秒为起点的时间值，精确到毫秒)
        /// </summary>
        public long SendTime
        {
            get { return sendTime; }
            set { sendTime = value; }
        }

        string linkID="";
        /// <summary>
        /// 行临时订购关系（默认填写 ""）
        /// </summary>
        public string LinkID
        {
            get { return linkID; }
            set { linkID = value; }
        }


        int sendGroupID;
        /// <summary>
        /// 下发批次（默认填写 0）（保留字段，暂无意义）
        /// </summary>
        public int SendGroupID
        {
            get { return sendGroupID; }
            set { sendGroupID = value; }
        }
        int productID;
        /// <summary>
        /// 通道组ID（产品编号）
        /// </summary>
        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }
        byte messageType;
        /// <summary>
        /// "消息的下发类型    0 免费下发 1 按条下发 2 包月下发 
        ///          3 订阅请求 4 取消请求 5 包月扣费"
        /// </summary>
        public byte MessageType
        {
            get { return messageType; }
            set { messageType = value; }
        }
        /// <summary>
        /// 接收信息的用户数量(小于100)（必添字段）
        /// </summary>
        public short DestMobileCount
        {
            get { return (short)DestMobile.Count; }
        }
        List<Individual> destMobile = new List<Individual>();
        /// <summary>
        /// 手机号码和个性化内容
        /// </summary>
        private List<Individual> DestMobile
        {
            get { return destMobile; }
            //set { destMobile = value; }
        }
        


        short subjectLength;

        private short SubjectLength
        {
            get { return ByteHelper.GetEncodedLength(this.SubjectContent); }
        }

        string subjectContent;
        /// <summary>
        /// 彩信主题内容
        /// </summary>
        public string SubjectContent
        {
            get { return subjectContent; }
            set { subjectContent = value; }
        }

        byte messageFileSize;
        /// <summary>
        /// "彩信资源文件的个数
        ///当填写0时，表示messageContent里填的是资源ID(Otect String)
        ///当填写>0时，表示彩信资源文件的个数"

        /// </summary>
        private byte MessageFileSize
        {
            get { return messageContentResource.Count > 0 ? Convert.ToByte(messageContentResource.Count) : Convert.ToByte(0); }
            set { messageFileSize = value; }
        }

        int messageLength;
        /// <summary>
        /// 消息的长度（必添字段）
        /// </summary>
        public int MessageLength
        {
            get { return messageLength; }
            set { messageLength = value; }
        }

        string messageContentString = "";
        /// <summary>
        /// "消息的内容
        ///消息内容类型为0时填写，MessageContentString类型为String，内容是彩信资源ID
        /// </summary>
        public string MessageContentString
        {
            get { return messageContentString; }
            set { messageContentString = value; }
        }

        List<ResourceContent>  messageContentResource=new List<ResourceContent>();
        /// <summary>
        /// 消息的内容（必添字段）
        ///消息内容类型为大于0时填写，messageContent类型为彩信内容
        /// </summary>
        private List<ResourceContent> MessageContentResource
        {
            get { return messageContentResource; }
            set { messageContentResource = value;}
        }


        short customLen;
        /// <summary>
        /// 扩展字段长度
        /// </summary>
        private short CustomLen
        {
            get { return ByteHelper.GetEncodedLength(this.Custom); }
        }
        string custom;
        /// <summary>
        /// 用户自定义信息
        /// </summary>
        public string Custom
        {
            get { return custom; }
            set { custom = value; }
        }

        long clientSubmitTime;

        public long ClientSubmitTime
        {
            get { return clientSubmitTime; }
            set { clientSubmitTime = value; }
        }

        public override byte[] GetBody()
        {
            this.ClientSubmitTime = ByteHelper.GetMilliSec();
            BuffBody.PutLong(this.ClientSeq);
            BuffBody.PutString(this.SrcNumber,21);
            BuffBody.PutByte(this.MessagePriority);
            BuffBody.PutByte(this.ReportType);
            BuffBody.PutByte(this.MessageFormat);
            BuffBody.PutLong(this.OverTime);
            BuffBody.PutLong(this.SendTime);
            BuffBody.PutString(this.LinkID, 20);
            BuffBody.PutInt(this.SendGroupID);
            BuffBody.PutInt(this.ProductID);
            BuffBody.PutByte(this.MessageType);
            BuffBody.PutShort(this.DestMobileCount);
            for (int i = 0; i < DestMobileCount; i++)
            {
                BuffBody.PutString(this.DestMobile[i].DestMobile, 21);
                BuffBody.PutInt(this.DestMobile[i].IndividualLength);
                BuffBody.PutString(this.DestMobile[i].IndividualContents,DestMobile[i].IndividualLength);
            }
            BuffBody.PutShort(this.SubjectLength);
            BuffBody.PutString(this.SubjectContent, this.SubjectContent.Length);

            BuffBody.PutByte(this.MessageFileSize);
            byte[] MessageContentResourceBytes=new byte[0];
            BuffBody.PutInt(getMessageLength( ref MessageContentResourceBytes));
            BuffBody.Buff=ByteHelper.AddByte(BuffBody.Buff,MessageContentResourceBytes);

            BuffBody.PutShort(CustomLen);
            BuffBody.PutString(Custom, CustomLen);

            return this.BuffBody.Buff;
        }

        public override void SetBody(byte[] body)
        {
            //this.BuffBody.Buff = body;
            //this.ClientSeq = BuffBody.getLong();
            //this.SrcNumber = BuffBody.getString(21);
            //this.MessagePriority = BuffBody.getByte();
            //this.ReportType = BuffBody.getByte();
            //this.MessageFormat = BuffBody.getByte();
            //this.OverTime = BuffBody.getLong();
            //this.SendTime = BuffBody.getLong();
            //this.LinkID = BuffBody.getString(20);
            //this.SendGroupID = BuffBody.getInt();
            //this.ProductID = BuffBody.getInt();
            //this.MessageType = BuffBody.getByte();
            //int destMobileCount = BuffBody.getShort();

            //this.DestMobile = new Individual[DestMobileCount];
            //for (int i = 0; i < destMobileCount; i++)
            //{
            //    DestMobile[i].DestMobile = BuffBody.getString(21);
            //    DestMobile[i].IndividualContents=BuffBody.getString( BuffBody.getInt());
            //}

            //this.SubjectContent = BuffBody.getString(BuffBody.getShort());
            //this.MessageFileSize = BuffBody.getByte();
            //this.MessageLength = BuffBody.getInt();

            //if (this.MessageFileSize == 0)
            //{

            //}


            throw new NotImplementedException();
        }




        private int getMessageLength( ref byte[] b)
        {
            ByteBuffer bf = new ByteBuffer();
            if (MessageFileSize == 0)
            {
                int length= Convert.ToInt16(ByteHelper.GetEncodedLength(this.MessageContentString));
                bf.PutString(this.MessageContentString,length);
                b = bf.Buff;
                return Convert.ToInt16( length);
            }
            else
            {
                
                for (int i = 0; i < MessageFileSize; i++)
                {
                    bf.PutShort(MessageContentResource[i].FileNameLength);
                    bf.PutString(MessageContentResource[i].FileName, MessageContentResource[i].FileNameLength);
                    bf.PutInt(MessageContentResource[i].ContentLength);
                    bf.Buff = ByteHelper.AddByte(bf.Buff, MessageContentResource[i].FileContent);
                }
                
                
            }
            b = bf.Buff;
            return Convert.ToInt32(bf.Buff.Length);
        }

        public override IPackage Clone()
        {
            return (IPackage)this.MemberwiseClone();
        }

        public override IPackage GetInstance()
        {
            return new SubmitMMS();
        }

        /// <summary>
        /// 添加彩信资源
        /// </summary>
        /// <param name="resource"></param>
        public void AddMessageContentResource(ResourceContent resource)
        {
            this.MessageContentResource.Add(resource);
            this.MessageLength += resource.ContentLength;
            if (this.MessageLength > StaticInfo.StaticHandle.MmsResoureceSize)
            {
                throw new Exception("彩信资源大小超过设置的最大限制：" + StaticInfo.StaticHandle.MaxGroupSize.ToString());
                
            }

        }

        public List<ResourceContent> GetMessageContentResource()
        {
            return this.MessageContentResource;
        }
        /// <summary>
        /// 添加DestMobile
        /// </summary>
        /// <param name="resource"></param>
        public void AddDestMobile(Individual idv)
        {
            this.DestMobile.Add(idv);
            if (this.DestMobileCount > StaticInfo.StaticHandle.MaxGroupSize)
            {
                throw new Exception("组包发送个数超过设置的最大限制：" + StaticInfo.StaticHandle.MaxGroupSize.ToString());

            }

        }

        public List<Individual> GetDestMobile()
        {
            return this.DestMobile;
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
            for (int i = 0; i < DestMobileCount; i++)
            {
                str.Append(this.DestMobile[i].ToString());
            }

            str.Append("]  MessageLength:");
            str.Append(this.MessageLength);
            str.Append(" SubjectLength:");
            str.Append(this.SubjectLength);
            str.Append(" SubjectContent:");
            str.Append(this.SubjectContent);
            str.Append(" MessageFileSize:");
            str.Append(this.MessageFileSize);
            str.Append(" MessageContentResource:[");
            if (this.MessageFileSize > 0)
            {
                for (int i = 0; i < MessageFileSize; i++)
                {
                    str.Append(MessageContentResource[i].ToString());
                }
            }
            else
            {
                str.Append(" MessageContentString:");
                str.Append(this.MessageContentString);
            }
            str.Append("] CustomLen:");
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


           
            BuffBody.PutShort(this.SubjectLength);
            BuffBody.PutString(this.SubjectContent, this.SubjectContent.Length);

            BuffBody.PutByte(this.MessageFileSize);
            byte[] MessageContentResourceBytes = new byte[0];
            BuffBody.PutInt(getMessageLength(ref MessageContentResourceBytes));
            BuffBody.Buff = ByteHelper.AddByte(BuffBody.Buff, MessageContentResourceBytes);

            BuffBody.PutShort(CustomLen);
            BuffBody.PutString(Custom, CustomLen);
        }
    }
}
