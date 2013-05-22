using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Cbip.Api.StaticInfo;

namespace Lxt2.Cbip.Api.API.Impl
{
    public class Statistics : IStatistics
    {
        #region IStatistics 成员

        public int GetSubmitSMSCount()
        {
            return StaticHandle.SmsCount;
        }

        public int GetSubmitMMSCount()
        {
            return StaticHandle.MmsCount;
        }

        public int GetRespSMSCount()
        {
            return StaticHandle.RespSMSCount;
        }

        public int GetRespMMSCount()
        {
            return StaticHandle.RespMMSCount;
        }

        public int GetReportSMSCount()
        {
            return StaticHandle.ReportSMSCount;
        }

        public int GetReportMMSCount()
        {
            return StaticHandle.ReportMMSCount;
        }

        public int GetResourseCount()
        {
            return StaticHandle.ResourseCount;
        }

        public int GetDeliverCount()
        {
            return StaticHandle.DeliverCount;
        }

        #endregion
    }
}
