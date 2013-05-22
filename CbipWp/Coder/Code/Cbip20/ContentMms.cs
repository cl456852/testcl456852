using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Communication.Framework.Util;
using Lxt2.Cbip.Api.Code.API;
using Lxt2.Communication.Framework;

namespace Lxt2.Cbip.Api.Code.Cbip20
{
    public class ContentMms : AbsPackage, IApiSubmit
    {
        public ContentMms()
        {
            this.CommandID = Convert.ToUInt32(AbsPackage.CBIP_CONTENT_MMS);
        }

        short messageKeepDate;
        /// <summary>
        /// 消息保留时长（单位：天），当值 <7 时，默认保留7天
        ///  资源文件个数；协议将包含messageFileSize个messageLength和messageContent
        /// </summary>
        public short MessageKeepDate
        {
            get { return messageKeepDate; }
            set { messageKeepDate = value; }
        }
        short messageFileSize;
        /// <summary>
        /// 资源文件个数；协议将包含messageFileSize个messageLength和messageContent
        /// </summary>
        private short MessageFileSize
        {
            get { return Convert.ToInt16( MessageContentResource.Count); }
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
        List<ResourceContent> resource = new List<ResourceContent>();
        /// <summary>
        /// 消息的内容（必添字段）
        /// </summary>
        private List<ResourceContent> MessageContentResource
        {
            get { return resource; }
            set { resource = value; }
        }

        
        string resourceID="";
        /// <summary>
        /// 客户彩信资源编号
        /// </summary>
        public string ResourceID
        {
            get { return resourceID; }
            set { resourceID = value; }
        }



        public override byte[] GetBody()
        {
            this.BuffBody.PutShort(MessageKeepDate);
            this.BuffBody.PutShort(this.MessageFileSize);
            byte[] messageContentResourceBytes=new byte[0];
            this.BuffBody.PutInt(getMessageLength(ref messageContentResourceBytes));
            this.BuffBody.Buff = ByteHelper.AddByte(BuffBody.Buff, messageContentResourceBytes);
            this.BuffBody.PutString(this.ResourceID, 30);
            return BuffBody.Buff;

            //int msgLength = getMessageLength(ref messageContentResourceBytes);

            //if (msgLength <= StaticInfo.StaticHandle.MmsResoureceSize)
            //{
            //    this.BuffBody.PutInt(getMessageLength(ref messageContentResourceBytes));
            //    this.BuffBody.Buff = ByteHelper.AddByte(BuffBody.Buff, messageContentResourceBytes);
            //    this.BuffBody.PutString(this.ResourceID, 30);
            //    return BuffBody.Buff;
            //}
            //else
            //{
            //    throw new Exception("彩信资源大小超过设置的最大限制：" + StaticInfo.StaticHandle.MaxGroupSize.ToString());
            //}
        }

        public override void SetBody(byte[] body)
        {
            throw new NotImplementedException();
        }


        private int getMessageLength(ref byte[] b)
        {
            ByteBuffer bf = new ByteBuffer();
            for (int i = 0; i < MessageFileSize; i++)
            {
                bf.PutShort(MessageContentResource[i].FileNameLength);
                bf.PutString(MessageContentResource[i].FileName, MessageContentResource[i].FileNameLength);
                bf.PutInt(MessageContentResource[i].ContentLength);
                bf.Buff = ByteHelper.AddByte(bf.Buff, MessageContentResource[i].FileContent);
            }
            b = bf.Buff;
            //return Convert.ToInt16(bf.Buff.Length);
            return bf.Buff.Length;
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

        public override IPackage Clone()
        {
            return (IPackage)this.MemberwiseClone();
        }

        public override IPackage GetInstance()
        {
            return new ContentMms();
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append(this.GetType().Name);
            str.Append("    MessageKeepDate:");
            str.Append(this.MessageKeepDate);
            str.Append(" MessageFileSize:");
            str.Append(this.MessageFileSize);
            str.Append(" MessageContentResource:");
            for (int i = 0; i < MessageFileSize; i++)
            {
                str.Append(" [FileNameLength:");
                str.Append(MessageContentResource[i].FileNameLength);
                str.Append(" FileName:");
                str.Append(MessageContentResource[i].FileName);
                str.Append(" ContentLength:");
                str.Append(MessageContentResource[i].ContentLength);
                str.Append("]");
            }
            str.Append(" ResourceID:");
            str.Append(this.ResourceID);
            
            return str.ToString();
        }
    }
}
