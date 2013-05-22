using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Cbip.Api.Code.API;
using Lxt2.Cbip.Api.API;
using Lxt2.Communication.Framework;
using Lxt2.Cbip.Api.Code.Cbip20;
using Lxt2.Communication.Framework.Util;
using System.Diagnostics;

namespace Lxt2.Cbip.Api.Handle.Cbip20
{
    public class SubmitMMSRespHandle : AbsHandle
    {
        public SubmitMMSRespHandle()
        {
            this.Package = new SubmitMmsResp();
        }


        public override void DealMessage(ISession client, IPackage package)
        {
            SubmitMmsResp resp = (SubmitMmsResp)package;

            //判断是否需要记录日志
       
                string log = "接收的数据MMSResp：  " + resp.ToString();
                Debug.WriteLine("Resp", log);
            
            try
            {
                SubmitMMS mms = (SubmitMMS)client.GetPackage(resp.GetKey());
                client.Response(resp.GetKey());
                this.RespReceiver.Receive(mms, resp);
            }
            catch (Exception e)
            {  
                Debug.WriteLine( e);
            }
        }
    }
}
