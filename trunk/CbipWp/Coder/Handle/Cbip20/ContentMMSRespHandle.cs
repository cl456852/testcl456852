using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Communication.Framework;
using Lxt2.Cbip.Api.Code.API;
using Lxt2.Cbip.Api.Code.Cbip20;
using Lxt2.Cbip.Api.StaticInfo;
using Lxt2.Communication.Framework.Util;
using System.Diagnostics;

namespace Lxt2.Cbip.Api.Handle.Cbip20
{
    public class ContentMMSRespHandle : AbsHandle
    {
        public ContentMMSRespHandle()
        {
            this.Package = new ContentMmsResp();
        }
        public override void DealMessage(ISession client, IPackage package)
        {
            ContentMmsResp cmr = (ContentMmsResp)package;
      
                string log = "接收的数据MMSResourceResp：  " + cmr.ToString();
                Debug.WriteLine( log);
            
            if (StaticHandle.MmsResource.SequenceID == package.GetKey())
            {
                StaticHandle.ResourceID = cmr.SysResourceID;
                StaticHandle.Status = cmr.Status;
                StaticHandle.mmsContentEvent.Set();//使发送彩信资源线程继续
            }
        }
    }
}
