using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Cbip.Api.Code.Cbip20;
using Lxt2.Cbip.Api.Code.API;

namespace Lxt2.Cbip.Api.API
{
    /// <summary>
    /// 被动发送，用户实现，由API调用获取数据以发送
    /// </summary>
    public interface IPassiveSubmitSender
    {
        /// <summary>
        /// 被动发送，用户实现，由API调用获取数据以发送
        /// </summary>
        /// <returns>IApiSubmit对象，彩信会短信</returns>
        IApiSubmit GetSubmit();
    }
}
