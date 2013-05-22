using System;
using System.Collections.Generic;
using System.Text;

namespace Lxt2.Communication.Framework
{
    public class InnerPackage
    {
        IPackage package;
        /// <summary>
        /// 数据包
        /// </summary>
        public IPackage Package
        {
            get { return package; }
            set { package = value; }
        }
        DateTime sendTime = System.DateTime.Now;
        /// <summary>
        /// 上次发送时间
        /// </summary>
        public DateTime SendTime
        {
            get { return sendTime; }
            set { sendTime = value; }
        }

        long sendTimes = 1;
        /// <summary>
        /// 发送次数
        /// </summary>
        public long SendTimes
        {
            get { return sendTimes; }
            set { sendTimes = value; }
        }
    }
}
