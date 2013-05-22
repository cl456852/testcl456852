using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Cbip.Api.Code.Cbip20;

namespace Lxt2.Cbip.Api.API
{
    /// <summary>
    /// 接收协议对象，用户实现
    /// </summary>
    /// <typeparam name="Package"></typeparam>
    public interface IApiReceiver<Package>
    {
        /// <summary>
        /// 用于接收DELIVER或REPORT，由用户实现，接收到数据后自行处理
        /// </summary>
        /// <param name="data">REPORT或DELIVER</param>
        void Receive(Package data);
    }
}
