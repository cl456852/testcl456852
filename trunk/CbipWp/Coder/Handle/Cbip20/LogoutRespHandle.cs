using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Cbip.Api.Code.Cbip20;
using Lxt2.Communication.Framework;

namespace Lxt2.Cbip.Api.Handle.Cbip20
{
    class LogoutRespHandle : AbsHandle
    {
        public LogoutRespHandle()
        {
            this.Package = new LogoutResp();
        }
        public override void DealMessage(ISession client, IPackage package)
        {
            LogoutResp logoutResp = (LogoutResp)package;
            if (logoutResp.Status == 0)
            {
                if (client != null)
                {
                    client.Close();
                }
                Console.WriteLine("退出成功");
            }
            else
            {
                Console.WriteLine("退出失败");
            }
        }
    }
}
