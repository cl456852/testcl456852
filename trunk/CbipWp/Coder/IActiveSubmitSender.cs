using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Cbip.Api.Code.Cbip20;
using Lxt2.Cbip.Api.Code.API;

namespace Lxt2.Cbip.Api.API
{
    /// <summary>
    /// 主动发送接口，用于发送短彩信和彩信资源
    /// </summary>
    public interface IActiveSubmitSender
    {
        /// <summary>
        /// 发送彩信资源
        /// </summary>
        /// <param name="contentMMS">彩信资源对象</param>
        /// <returns>彩信资源响应包，包含资源ID</returns>
        IApiSubmitResp SendMMSContent(ContentMms contentMMS);
        /// <summary>
        /// 发送SUBMIT对象，包括彩信和短信
        /// </summary>
        /// <param name="submit">SUBMIT对象</param>
        void SendSubmit(IApiSubmit submit);
    }
}
