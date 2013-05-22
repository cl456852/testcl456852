using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Communication.Framework;
using Lxt2.Cbip.Api.Code.API;
using Lxt2.Cbip.Api.API;
using Lxt2.Cbip.Api.Code.Cbip20;

namespace Lxt2.Cbip.Api.Handle.Cbip20
{
    public class ContentMmsHandle : AbsHandle
    {
        public ContentMmsHandle()
        {
            this.Package = new ContentMms();
        }


        public override void DealMessage(ISession client, IPackage package)
        {
            
        }
    }
}
