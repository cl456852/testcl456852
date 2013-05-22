using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Cbip.Api.Code.Cbip20;
using Lxt2.Cbip.Api.Code.API;

using Lxt2.Cbip.Api.StaticInfo;
using Lxt2.Communication.Framework;

namespace Lxt2.Cbip.Api.API.Impl
{
    public class ActiveSubmitSender:AbsApiImpl,IActiveSubmitSender
    {
        public ActiveSubmitSender(Sender sender)
        {
            this.Sender = sender;
        }
        
        #region IActiveSubmitSender 成员

        public IApiSubmitResp SendMMSContent(ContentMms contentMMS)
        {
            ContentMmsResp resp = new ContentMmsResp();
            StaticHandle.MmsResource = contentMMS;
            Sender.Send(contentMMS,false);
            bool signalled = StaticHandle.MMSContentEvent.WaitOne(20000);//模拟同步阻塞
            if (!signalled)//如果超时返回错误RESP
            {
                
                resp.Status = 1;
                resp.SysResourceID = "0";
                return resp;
            }
            resp.Status = StaticHandle.Status;
            resp.SysResourceID = StaticHandle.ResourceID;
            return resp;
            
        }

        public void SendSubmit(IApiSubmit submit)
        {
            Sender.Send(submit,true);
        }

        //public void SendSubmit(IApiSubmit submit)
        //{
        //    Submit sms = null;
        //    SubmitMMS mms = null;
        //    if (submit.GetType() == typeof(Submit))
        //    {
        //        sms = (Submit)submit;
        //        sms.ClientSubmitTime = ByteHelper.GetMilliSec();
        //        Sender.Send(sms);
        //        return;
        //    }
        //    else if (submit.GetType() == typeof(SubmitMMS))
        //    {
        //        mms = (SubmitMMS)submit;
        //        mms.ClientSubmitTime = ByteHelper.GetMilliSec();
        //        Sender.Send(mms, true);
        //    }
        //}


        #endregion
    }
}
