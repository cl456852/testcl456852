using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Cbip.Api.Code.Cbip20;
using Lxt2.Communication.Framework;

namespace Lxt2.Cbip.Api.Handle.Cbip20
{
    class SubmitMMSHandle : AbsHandle
    {
        public SubmitMMSHandle()
        {
            this.Package = new SubmitMMS();
        }
        public override void DealMessage(ISession client, IPackage package)
        {

        }
    }
}
