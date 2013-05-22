using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Cbip.Api.Code.Cbip20;
using Lxt2.Cbip.Api.Code.API;

namespace Lxt2.Cbip.Api.API
{
    /// <summary>
    /// 接收短信，彩信RESP，用户实现
    /// </summary>
    public interface IRespReceiver
    {
        /// <summary>
        /// 接收短信，彩信RESP，用户实现
        /// </summary>
        /// <param name="submit">发送的SUBMIT</param>
        /// <param name="resp">SUBMIT对应的RESPONSE</param>
        void Receive(IApiSubmit submit,IApiSubmitResp resp);
    }
}
