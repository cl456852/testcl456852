using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Communication.Framework.Util;
using System.Threading;
using Lxt2.Cbip.Api.Code.Cbip20;
using Lxt2.Cbip.Api.Handle.Cbip20;
using Lxt2.Communication.Framework;
using Lxt2.Communication.Framework.Handle;
using Lxt2.Communication.Framework.Receiver;
using Lxt2.Cbip.Api.StaticInfo;
using Lxt2.Cbip.Api.API;
using System.Diagnostics;

namespace Lxt2.Cbip.Api.Receiver.Cbip20
{
    public class CBIPReceiver : BaseReceiver
    {
        public static int speed = 0;

        public static int count = 0;

        public static int resendCount = 0;

        public static int removeCount = 0;

        /// <summary>
        /// 侦测包发送计数
        /// </summary>
        public int ActiveCount
        {
            get
            {
                return Session.GetPara("ACTIVECOUNT") == null ? 0 : (int)Session.GetPara("ACTIVECOUNT"); ;
            }
            set
            {
                Session.SetPara("ACTIVECOUNT", value);
            }
        }

        public CBIPReceiver()
        {
            HandleList.Add(new SubmitRespHandle());
            HandleList.Add(new ReportHandle());
            HandleList.Add(new DeliverHandle());
            HandleList.Add(new LoginRespHandle());
            HandleList.Add(new ActiveRespHandle());
            HandleList.Add(new SubmitHandle());
            HandleList.Add(new LogoutRespHandle());
            HandleList.Add(new ContentMMSRespHandle());
            //HandleList.Add(new SubmitMMSHandle());
            //HandleList.Add(new SubmitMMSRespHandle());
            //HandleList.Add(new ActiveHandle());
            //HandleList.Add(new LoginHandle());
            //HandleList.Add(new LogoutHandle());
            //HandleList.Add(new DeliverRespHandle());
            //HandleList.Add(new ReportRespHandle());
        }

        public override void AfterConnect(ISession client)
        {
            try
            {
                Console.WriteLine("成功建立连接");
                Debug.WriteLine("成功建立连接");
                //client.Connected(true);

                //创建登录对象
                Login login = new Login();
                login.ClientID = StaticHandle.Config.ClientID;
                login.PassWord = StaticHandle.Config.Password;
                login.UserName = StaticHandle.Config.UserName;
                login.Version = StaticHandle.Config.Version;


                if (StaticHandle.LoginTimes > 0)
                {
                    if (Convert.ToBoolean(client.GetPara("isMo")))
                        login.LoginType = 0;
                    else
                    {
                        login.LoginType = 1;
                        client.SetPara("isMo", false);
                    }
                }
                else
                {
                    login.LoginType = 2;
                    client.SetPara("isMo",true);
                }
              
                StaticHandle.LoginTimes++;
                


                //发送登录信息
                if (client.Send(login, false))
                {
                    string log = "发送登录信息成功：Server =" + StaticHandle.Config.Ip + ", Port=" + StaticHandle.Config.Port + "，commandID = "+login.CommandID+" sequenceId = "+login.SequenceID+" commandStatus = "+login.CommandStatus+" ClientID = " + StaticHandle.Config.ClientID + " UserName = " + StaticHandle.Config.UserName + "	 PassWard = " + StaticHandle.Config.Password + " LoginType = " + login.LoginType + " Version = "+StaticHandle.Config.Version;
                    Debug.WriteLine( log);
                }
                else
                {
                    Console.WriteLine("发送登录信息失败");
                    Debug.WriteLine("发送登录信息失败");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                    // Debug.WriteLine(ex);
            }

        }

        public override void AfterDisConnect(ISession client)
        {
            Console.WriteLine("发送登录信息失败");
            Debug.WriteLine("发送登录信息失败");
        }

        public override void AfterSend(ISession client)
        {
            Interlocked.Increment(ref speed);
            Interlocked.Increment(ref count);
            //this.ActiveCount = 0;
        }
        /// <summary>
        /// 重新发送完成回调方法
        /// </summary>
        /// <param name="client"></param>
        public override void AfterReSend(ISession client, IPackage package)
        {
            
       
                string log = "重发数据：" + package.ToString();
                Debug.WriteLine("removeData", log);
            
            Console.WriteLine("重新发送完成");
            Interlocked.Increment(ref resendCount);
            
        }
        public override void AfterIdle(ISession client)
        {
            try
            {
                if (this.ActiveCount < 3)
                {
                    this.ActiveCount++;
                    Console.WriteLine("处于空闲(第" + this.ActiveCount + "次)");
                    //发送测试包
                    Active active = new Active();
                    client.Send(active, false);
                }
                else
                {
                    this.ActiveCount = 0;
                    client.Close();
                    string log = "已累计发送三次心跳包，服务器端没有响应，断开连接";
                    Debug.WriteLine("session", log);
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }
        }
        public override void AfterReceive(ISession client, IPackage package, ref long key)
        {
            this.ActiveCount = 0;
            
        }
        public override void AfterRemove(ISession client, IPackage package)
        {
            string log = "超时清理的数据：" + package.ToString();
       
                Debug.WriteLine("removeData", log);
                Console.WriteLine("超时清理完成");
            
            Interlocked.Increment(ref removeCount);
        }

        public override void AfterClose(ISession client, List<IPackage> removeList)
        {
          
                for (int i = 0; i < removeList.Count; i++)
                {
                    string log = "关闭连接时清理滑动窗口的数据：  " + removeList[i].ToString();
                    Debug.WriteLine("removeData", log);
                }
            
            Console.WriteLine("成功关闭连接");
        }

        public override BaseReceiver Clone()
        {
            return new CBIPReceiver();
        }

        public override BaseReceiver GetInstance()
        {
            return new CBIPReceiver();
        }
    }
}
