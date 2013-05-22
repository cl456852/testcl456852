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
    class DeliverHandle : AbsHandle
    {
        public DeliverHandle()
        {
            this.Package = new Deliver();
            this.Receiver = StaticHandle.ApiReceiver;
        }

        public override void DealMessage(ISession client, IPackage package)
        {
            string log = "接收的数据：  " + ByteHelper.ObjToString(package);
            Debug.WriteLine( log);
            Interlocked.Increment(ref StaticHandle.deliverCount);
            Deliver deliver = (Deliver)package;
            DeliverResp deliverResp = new DeliverResp();

            deliverResp.SequenceID = deliver.SequenceID;
            deliverResp.Status = 0;
            client.Send(deliverResp, false);
            this.Receiver.Receive(deliver);

            
        }
    }
}
