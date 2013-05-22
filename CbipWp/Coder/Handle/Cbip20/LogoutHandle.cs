using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Cbip.Api.Code.Cbip20;
using Lxt2.Communication.Framework;
using Lxt2.Cbip.Api.API;

namespace Lxt2.Cbip.Api.Handle.Cbip20
{
    class LogoutHandle : AbsHandle
    {
        public LogoutHandle()
        {
            this.Package = new Logout();
        }
        
        public override void DealMessage(ISession client, IPackage package)
        {
            
        }
    }
}
