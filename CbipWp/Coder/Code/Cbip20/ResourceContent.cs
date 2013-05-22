using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Communication.Framework.Util;
using Lxt2.Communication.Framework;

namespace Lxt2.Cbip.Api.Code.Cbip20
{
    public class ResourceContent
    {
        short fileNameLength;
        /// <summary>
        /// 文件名长度
        /// </summary>
        public short FileNameLength
        {
            get { return ByteHelper.GetEncodedLength(this.FileName); }
        }
        string fileName;
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        int contentLength;
        /// <summary>
        /// 文件内容长度
        /// </summary>
        public int ContentLength
        {
            get { return FileContent.Length; }
        }
        byte[] fileContent;
        /// <summary>
        /// 文件内容
        /// </summary>
        public byte[] FileContent
        {
            get { return fileContent; }
            set { fileContent = value; }
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append(" {FileNameLength:");
            str.Append(this.FileNameLength);
            str.Append(" FileName:");
            str.Append(this.FileName);
            str.Append(" ContentLength:");
            str.Append(this.ContentLength);
            str.Append("}");
            return str.ToString();

        }
    }
}
