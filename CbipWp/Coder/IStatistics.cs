using System;
using System.Collections.Generic;
using System.Text;

namespace Lxt2.Cbip.Api.API
{
    public interface IStatistics
    {
        /// <summary>
        /// 得到短信发送量
        /// </summary>
        /// <returns></returns>
        int GetSubmitSMSCount();
        /// <summary>
        /// 得到彩信发送量
        /// </summary>
        /// <returns></returns>
        int GetSubmitMMSCount();
        /// <summary>
        /// 得到接收短信RESPONSE量
        /// </summary>
        /// <returns></returns>
        int GetRespSMSCount();
        /// <summary>
        /// 得到彩信RESPONSE量
        /// </summary>
        /// <returns></returns>
        int GetRespMMSCount();
        /// <summary>
        /// 得到短信REPORT量
        /// </summary>
        /// <returns></returns>
        int GetReportSMSCount();
        /// <summary>
        /// 得到彩信REPORT量
        /// </summary>
        /// <returns></returns>
        int GetReportMMSCount();
        /// <summary>
        /// 得到彩信资源包发送量
        /// </summary>
        /// <returns></returns>
        int GetResourseCount();
        /// <summary>
        /// 得到上行短信量
        /// </summary>
        /// <returns></returns>
        int GetDeliverCount();


    }
}
