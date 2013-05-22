using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Communication.Framework.Util;
using System.Threading;
using Lxt2.Communication.Framework.Handle;
using Lxt2.Communication.Framework;
using Lxt2.Communication.Framework.Client;
using System.Collections;
using System.Diagnostics;

namespace Lxt2.Communication.Framework.Receiver
{
    public abstract class BaseReceiver : IReceiver
    {
        volatile int reSendTimes = 0;

        private int reSendOverTime = 60;

        /// <summary>
        /// 重发超时时间(60秒)
        /// </summary>
        public int ReSendOverTime
        {
            get { return reSendOverTime; }
            set { reSendOverTime = value; }
        }

        /// <summary>
        /// 重发次数（默认0次,不重发）
        /// </summary>
        public int ReSendTimes
        {
            get { return reSendTimes; }
            set { reSendTimes = value; }
        }

        volatile int reLinkInterval = 60;

        /// <summary>
        /// 重连间隔（默认60秒）
        /// </summary>
        public int ReLinkInterval
        {
            get { return reLinkInterval; }
            set { reLinkInterval = value; }
        }
        volatile int idleTime = 30;

        /// <summary>
        /// 空闲时间(默认60秒)
        /// </summary>
        public int IdleTime
        {
            get { return idleTime; }
            set { idleTime = value; }
        }

        /// <summary>
        /// 重连定时器
        /// </summary>
        Timer reConnTimer;

        /// <summary>
        /// 重发定时器
        /// </summary>
        Timer reSendTimer;

        /// <summary>
        /// 空闲定时器
        /// </summary>
        Timer idleTimer;

        private List<IHandle> handleList = new List<IHandle>();

        /// <summary>
        /// 信息处理Handle列表
        /// </summary>
        protected List<IHandle> HandleList
        {
            get { return handleList; }
            set { handleList = value; }
        }

        /// <summary>
        /// 最后读写时间
        /// </summary>
        private DateTime lastTime = System.DateTime.Now;

        byte[] bytes = new byte[0];

        int packageLen = 4;

        /// <summary>
        /// 包长所占字节数（默认4）
        /// </summary>
        public int PackageLen
        {
            get { return packageLen; }
            set { packageLen = value; }
        }

        ISession session;

        public ISession Session
        {
            get { return session; }
            set { session = value; }
        }

        internal void ConnetCallBack(ISession client)
        {
            this.session = client;
            Debug.WriteLine("session", session.GetHashCode() + "(2)物理连接成功");
            reConnTimer = new System.Threading.Timer(new TimerCallback(ReConnect), null, 1000 * ReLinkInterval, 1000 * ReLinkInterval);
            reSendTimer = new System.Threading.Timer(new TimerCallback(ReSend), null, 1000 * ReSendOverTime, 1000 * ReSendOverTime);
            idleTimer = new System.Threading.Timer(new TimerCallback(CheckIdel), null, 0, 1000);
            Debug.WriteLine("session", session.GetHashCode() + "(3)触发连接回调方法");
            this.AfterConnect(client);
            Debug.WriteLine("session", session.GetHashCode() + "(4)连接回调方法执行完成");
        }

        internal void DisConnetCallBack(ISession client)
        {

            this.AfterDisConnect(client);
        }

        internal void CloseCallBack(ISession client)
        {
            List<IPackage> removeList = client.BlockMap.RemoveAll();
            this.AfterClose(client, removeList);
        }

        internal void SendCallBack(ISession client)
        {
            lastTime = System.DateTime.Now;
            this.AfterSend(client);
        }

        internal void ReceiveCallBack(ISession client, byte[] bytes, ref long key)
        {
            lastTime = System.DateTime.Now;
            ByteHelper.PutByteArr(ref this.bytes, this.bytes.Length, bytes);
            while (true)
            {
                if (this.bytes.Length > packageLen)
                {
                    int len = ByteHelper.GetInt(ByteHelper.GetByteArr(this.bytes, 0, packageLen), 0);
                    if (this.bytes.Length >= len)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(DealMessage), ByteHelper.GetByteArr(this.bytes, 0, len));
                        ByteHelper.PutByteArr(ref this.bytes, 0, ByteHelper.GetByteArr(this.bytes, len, this.bytes.Length - len));
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public void DealMessage(object bytes)
        {
            IPackage temp = null;
            foreach (IHandle handle in handleList)
            {
                if (handle.CheckMessage(ref temp, (byte[])bytes))
                {
                    handle.DealMessage(this.session, (IPackage)temp);
                    break;
                }
            }
            //未找到处理handle时关闭session
            if (temp == null)
            {
                Debug.WriteLine( new Exception("未找到处理handle"));
            }
            else
            {
                long key = 0;
                this.AfterReceive(this.session, temp, ref key);
            }
        }

        #region IReceiver 成员
        /// <summary>
        /// 建立连接完成回调方法
        /// </summary>
        /// <param name="client"></param>
        public abstract void AfterConnect(ISession client);

        /// <summary>
        /// 断开连接完成回调方法
        /// </summary>
        /// <param name="client"></param>
        public abstract void AfterDisConnect(ISession client);


        /// <summary>
        /// 关闭Session完成回调方法
        /// </summary>
        /// <param name="client"></param>
        public abstract void AfterClose(ISession client, List<IPackage> removeList);

        /// <summary>
        /// 发送完成回调方法
        /// </summary>
        /// <param name="client"></param>
        public abstract void AfterSend(ISession client);

        /// <summary>
        /// 重新发送完成回调方法
        /// </summary>
        /// <param name="client"></param>
        public abstract void AfterReSend(ISession client, IPackage package);

        /// <summary>
        /// 空闲回调方法
        /// </summary>
        /// <param name="client"></param>
        public abstract void AfterIdle(ISession client);

        /// <summary>
        /// 滑动窗口超时清理回调方法
        /// </summary>
        /// <param name="client"></param>
        public abstract void AfterRemove(ISession client, IPackage package);

        /// <summary>
        /// 接收数据完成回调方法
        /// </summary>
        /// <param name="client"></param>
        public abstract void AfterReceive(ISession client, IPackage package, ref long key);


        /// <summary>
        /// 空闲检查方法
        /// </summary>
        /// <param name="state"></param>
        public void CheckIdel(Object state)
        {
            try
            {
                //Print();
                if (session.State == SessionState.Connected)
                {
                    if (lastTime.AddSeconds(idleTime) <= System.DateTime.Now)
                    {
                        lastTime = System.DateTime.Now;
                        Debug.WriteLine("session", this.session.GetHashCode() + "(100)开始空闲处理");
                        this.AfterIdle(this.session);
                        Debug.WriteLine("session", this.session.GetHashCode() + "(100)空闲处理完成");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine( e);
            }
        }

        /// <summary>
        /// 数据超时重发
        /// </summary>
        /// <param name="state"></param>
        private void ReSend(Object state)
        {
            try
            {

                if (this.session.State == SessionState.Connected)
                {
                    List<InnerPackage> removeKeys = new List<InnerPackage>();
                    List<InnerPackage> resendKeys = new List<InnerPackage>();
                    lock (session.BlockMap)
                    {
                        //遍历滑动窗口，找到过期和重发的数据
                        foreach (KeyValuePair<long,object> de in session.BlockMap)
                        {
                            try
                            {
                                InnerPackage temp = (InnerPackage)de.Value;
                                if (this.ReSendTimes == 0 || temp.SendTimes > this.ReSendTimes)
                                {
                                    removeKeys.Add(temp);
                                }
                                else
                                {
                                    if (temp.SendTime.AddSeconds(this.ReSendOverTime) <= System.DateTime.Now)
                                    {
                                        resendKeys.Add(temp);
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine( e);
                            }
                        }
                        //移除过期数据
                        foreach (InnerPackage item in removeKeys)
                        {
                            try
                            {
                                Debug.WriteLine("session", this.session.GetHashCode() + "(200)移除：key=" + item.Package.GetKey() + " time=" + item.SendTime + " times=" + item.SendTimes);
                                this.session.BlockMap.Remove(item.Package.GetKey());
                                this.AfterRemove(this.session, item.Package);
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine( e);
                            }
                        }
                        //重发超时数据
                        foreach (InnerPackage item in resendKeys)
                        {
                            try
                            {
                                Debug.WriteLine("session", session.GetHashCode() + "(200)重发：key=" + item.Package.GetKey() + " time=" + item.SendTime + " times=" + item.SendTimes);
                                item.SendTime = System.DateTime.Now;
                                item.SendTimes++;
                                this.session.Send(item.Package, false);
                                this.AfterReSend(this.session, item.Package);
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine(e);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine( e);
            }

        }

        /// <summary>
        /// 断开重连
        /// </summary>
        /// <param name="state"></param>
        private void ReConnect(Object state)
        {
            try
            {
                if (this.session.State == SessionState.Closed)
                {
                    Debug.WriteLine("session", session.GetHashCode() + "(0)重连开始");
                    this.session.Connect();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine( e);
            }
        }

        #endregion

        /// <summary>
        /// 资源清理
        /// </summary>
        public void Dispose()
        {
            this.idleTimer.Dispose();
            this.reConnTimer.Dispose();
            this.reSendTimer.Dispose();
        }

        private class Receiver
        {
            IHandle handle = null;

            public IHandle Handle
            {
                get { return handle; }
                set { handle = value; }
            }
            ISession client = null;
            public Receiver(ISession client, IHandle handle)
            {
                this.handle = handle;
                this.client = client;
            }

            public ISession Client
            {
                get { return client; }
                set { client = value; }
            }
            public void DealMessage(Object package)
            {
                try
                {
                    this.handle.DealMessage(this.client, (IPackage)package);
                }
                catch (Exception e)
                {
                    Debug.WriteLine( e);
                }
            }
        }

        public abstract BaseReceiver Clone();

        public abstract BaseReceiver GetInstance();

        public void Print()
        {
            System.Console.WriteLine("size:" + this.session.BlockMap.Count);
            System.Console.WriteLine("lenth:" + this.bytes.Length);
        }
    }
}
