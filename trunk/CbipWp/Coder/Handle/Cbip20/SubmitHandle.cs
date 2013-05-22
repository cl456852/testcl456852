using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Cbip.Api.Code.Cbip20;
using Lxt2.Communication.Framework;

namespace Lxt2.Cbip.Api.Handle.Cbip20
{
    class SubmitHandle : AbsHandle
    {
        public SubmitHandle()
        {
            this.Package = new Submit();
        }
        public override void DealMessage(ISession client, IPackage package)
        {
            client.Response(package.GetKey());
        }
    }
}
