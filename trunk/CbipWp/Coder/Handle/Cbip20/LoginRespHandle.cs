using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Cbip.Api.Code.Cbip20;
using Lxt2.Communication.Framework;
using Lxt2.Cbip.Api.API;
using System.Threading;

using System.Diagnostics;

namespace Lxt2.Cbip.Api.Handle.Cbip20
{
    class LoginRespHandle : AbsHandle
    {
        public LoginRespHandle()
        {
            this.Package = new LoginResp();
        }

        public override void DealMessage(ISession client, IPackage package)
        {
            try
            {
              LoginResp loginResp = (LoginResp)package;

                if (loginResp.Status == 0)
                {
                    client.Connected(true);
                    string log = "客户端ClientID = " + StaticInfo.StaticHandle.Config.ClientID + " 登录成功：--Server =" + StaticInfo.StaticHandle.Config.Ip + ", Port=" + StaticInfo.StaticHandle.Config.Port + " ，commandID = " + loginResp.CommandID + " sequenceId = " + loginResp.SequenceID + " commandStatus = " + loginResp.CommandStatus + " Status = " + loginResp.Status;
                    Debug.WriteLine(log);
                }
                else
                {
                    /// 状态,定长4个字节
                    /// 0：正确
                    /// 1：消息结构错
                    /// 2：非法源地址
                    /// 3：认证错
                    /// 4：版本错
                    /// 5： 登录协议错
                    /// 6：其他错误
                    if (loginResp.Status == 1)
                    {
                        client.ConnException = new Exception("鉴权失败原因：" + loginResp.Status + "消息结构错");
                    }
                    else if (loginResp.Status == 2)
                    {
                        client.ConnException = new Exception("鉴权失败原因：" + loginResp.Status + "非法源地址");
                    }
                    else if (loginResp.Status == 3)
                    {
                        client.ConnException = new Exception("鉴权失败原因：" + loginResp.Status + "认证错");
                    }
                    else if (loginResp.Status == 4)
                    {
                        client.ConnException = new Exception("鉴权失败原因：" + loginResp.Status + "版本错");
                    }
                    else if (loginResp.Status == 5)
                    {
                        client.ConnException = new Exception("鉴权失败原因：" + loginResp.Status + "登录协议错");
                    }
                    else if (loginResp.Status == 6)
                    {
                        client.ConnException = new Exception("鉴权失败原因：" + loginResp.Status + "其他错误");
                    }
                    client.Connected(false);
                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
