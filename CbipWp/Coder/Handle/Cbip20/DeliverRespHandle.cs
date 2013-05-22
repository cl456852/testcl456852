using Lxt2.Cbip.Api.Code.Cbip20;
using Lxt2.Communication.Framework;

namespace Lxt2.Cbip.Api.Handle.Cbip20
{
    class DeliverRespHandle : AbsHandle
    {
        public DeliverRespHandle()
        {
            this.Package = new DeliverResp();
        }
        public override void DealMessage(ISession client, IPackage package)
        {

        }
        
    }
}
