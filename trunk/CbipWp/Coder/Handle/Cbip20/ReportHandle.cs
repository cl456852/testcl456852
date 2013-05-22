using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Cbip.Api.Code.Cbip20;
using Lxt2.Communication.Framework;
using Lxt2.Cbip.Api.API;
using Lxt2.Cbip.Api.StaticInfo;
using Lxt2.Communication.Framework.Util;

using System.Threading;
using System.Diagnostics;

namespace Lxt2.Cbip.Api.Handle.Cbip20
{
    class ReportHandle : AbsHandle
    {
        public ReportHandle()
        {
            this.Package = new Report();
            this.Receiver = StaticHandle.ApiReceiver;
        }

        public override void DealMessage(ISession client, IPackage package)
        {
            Report report = (Report)package;
            int mf=report.MessageFormat;
            if (mf == 30 || mf == 33)
            {
                Interlocked.Increment(ref StaticHandle.reportMMSCount);
            }
            else
                Interlocked.Increment(ref StaticHandle.reportSMSCount);
            //判断是否需要记录日志
      
                string log = "接收的数据Report：  " + report.ToString();
                Debug.WriteLine( log);
            
            ReportResp resp = new ReportResp();
            resp.SequenceID = report.SequenceID;
            resp.Status = 0;
            client.Send(resp, false);
            Receiver.Receive(report);
            
        }
    }
}
