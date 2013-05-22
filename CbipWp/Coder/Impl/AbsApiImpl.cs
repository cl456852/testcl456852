using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Cbip.Api.Code.Cbip20;
using Lxt2.Communication.Framework;

namespace Lxt2.Cbip.Api.API.Impl
{
    public abstract class AbsApiImpl
    {
        Sender sender;

        public Sender Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        IApiReceiver<AbsPackage> receiver;
        /// <summary>
        /// 接收协议对象，用户实现
        /// </summary>
        public IApiReceiver<AbsPackage> Receiver
        {
            get { return receiver; }
            set { receiver = value; }
        }

        IRespReceiver respReceiver;
        /// <summary>
        /// 接收短信，彩信RESP，用户实现
        /// </summary>
        public IRespReceiver RespReceiver
        {
            get { return respReceiver; }
            set { respReceiver = value; }
        }
    }
}
