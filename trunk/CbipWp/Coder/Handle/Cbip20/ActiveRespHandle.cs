using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Lxt2.Cbip.Api.Code.Cbip20;
using Lxt2.Communication.Framework;
using Lxt2.Cbip.Api.StaticInfo;

namespace Lxt2.Cbip.Api.Handle.Cbip20
{
    class ActiveRespHandle : AbsHandle
    {
        public ActiveRespHandle()
        {
            this.Package = new ActiveResp();
        }

        public override void DealMessage(ISession client, IPackage package)
        {
            ActiveResp resp = (ActiveResp)package;
            if (resp == null)
            {
                Console.WriteLine("发送测试包失败");
            }
            else
            {
                int activeCount = client.GetPara("ACTIVECOUNT") == null ? 0 : (int)client.GetPara("ACTIVECOUNT");
                Console.WriteLine("发送第" + activeCount + "次心跳包成功");
                //线程安全计数
                //Interlocked.Decrement(ref StaticHandle.activeCount);
                //Interlocked.Exchange(ref StaticHandle.activeCount, 0);
                client.SetPara("ACTIVECOUNT", 0);

            }
        
        }
    }
}
