using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Cbip.Api.Code.Cbip20;
using Lxt2.Communication.Framework;
using Lxt2.Cbip.Api.API;
using Lxt2.Cbip.Api.StaticInfo;
using Lxt2.Cbip.Api.Code.API;
using Lxt2.Communication.Framework.Util;

using System.Threading;
using System.Diagnostics;


namespace Lxt2.Cbip.Api.Handle.Cbip20
{
    class SubmitRespHandle : AbsHandle
    {
        public SubmitRespHandle()
        {
            this.Package = new SubmitResp();
            this.RespReceiver = StaticHandle.RespReceiver;
        }

        public override void DealMessage(ISession client, IPackage package)
        {
            

            SubmitResp resp = (SubmitResp)package;
          
                //string log = "接收的数据SMSResp：  " + ByteHelper.ObjToString(resp);
                string log = "接收的数据：  " + resp.ToString();
                Debug.WriteLine("Resp", log);
            

            try
            {
                IApiSubmit submit = (IApiSubmit)client.GetPackage(resp.GetKey());
                if (submit.GetType() == typeof(Submit))
                {
                    Interlocked.Increment(ref StaticHandle.respSMSCount);
                }
                else
                    Interlocked.Increment(ref StaticHandle.respMMSCount);
                client.Response(resp.GetKey());
                this.RespReceiver.Receive(submit, resp);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            
        }
    }
}
