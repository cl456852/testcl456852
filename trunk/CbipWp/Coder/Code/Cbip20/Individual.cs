using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Communication.Framework.Util;

namespace Lxt2.Cbip.Api.Code.Cbip20
{
    public class Individual
    {
        string destMobile;
        /// <summary>
        /// 目标手机号码
        /// </summary>
        public string DestMobile
        {
            get { return destMobile; }
            set { destMobile = value; }
        }
        int individualLength;
        /// <summary>
        /// "msgFmt=30时，此字段为0 msgFmt=33时，为个性化彩信内容的总长度"

        /// </summary>
        public int IndividualLength
        {
            get { return ByteHelper.GetEncodedLength(this.IndividualContents); }
        }
        string individualContents="";
        /// <summary>
        /// "msgFmt=30时，此字段不存在 msgFmt=33时，为个性化内容,多个内容之间使用 “|” 进行分隔"

        /// </summary>
        public string IndividualContents
        {
            get { return individualContents; }
            set { individualContents = value; }
        }

        public override  string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("DestMobile:");
            str.Append(this.DestMobile);
            str.Append(" IndividualLength:");
            str.Append(this.IndividualLength);
            str.Append(" IndividualContents:");
            str.Append(this.IndividualContents);
            return str.ToString();
        }

    }
}
