using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Lxt2.Communication.Framework.Client;
using Lxt2.Communication.Framework.Receiver;

namespace Lxt2.Communication.Framework
{
    public class Sender:ISender
    {
        /// <summary>
        /// ip地址（默认127.0.0.1）
        /// </summary>
        String ip = "127.0.0.1";

        /// <summary>
        /// ip地址（默认127.0.0.1）
        /// </summary>
        public String Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        /// <summary>
        /// 端口号（默认1234）
        /// </summary>
        int port = 1234;

        /// <summary>
        /// 端口号（默认1234）
        /// </summary>
        public int Port
        {
            get { return port; }
            set { port = value; }
        }
        /// <summary>
        /// 滑动窗口大小（默认16）
        /// </summary>
        int qSize = 16;

        /// <summary>
        /// 滑动窗口大小（默认16）
        /// </summary>
        public int QSize
        {
            get { return qSize; }
            set { qSize = value; }
        }

        /// <summary>
        /// 接收缓存大小（默认8k）
        /// </summary>
        int receiveBufferSize = 8192;

        /// <summary>
        /// 接收缓存大小（默认8k）
        /// </summary>
        public int ReceiveBufferSize
        {
            get { return receiveBufferSize; }
            set { receiveBufferSize = value; }
        }

        /// <summary>
        /// 发送缓存大小（默认8k）
        /// </summary>
        int sendBufferSize = 8192;

        /// <summary>
        /// 发送缓存大小（默认8k）
        /// </summary>
        public int SendBufferSize
        {
            get { return sendBufferSize; }
            set { sendBufferSize = value; }
        }


        volatile int connectOverTime = 20;
        /// <summary>
        /// 连接超时时间（20秒）
        /// </summary>
        public int ConnectOverTime
        {
            get { return connectOverTime; }
            set { connectOverTime = value; }
        }

        volatile int reSendTimes = 3;

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
        /// 空闲时间(默认30秒)
        /// </summary>
        public int IdleTime
        {
            get { return idleTime; }
            set { idleTime = value; }
        }

        volatile int listNum = 1;

        /// <summary>
        /// 连接数量（默认1）
        /// </summary>
        public int ListNum
        {
            get { return listNum; }
            set { listNum = value; }
        }

        private List<Session> listSession = new List<Session>();

        /// <summary>
        /// session队列
        /// </summary>
        public List<Session> ListSession
        {
            get { return listSession; }
            set { listSession = value; }
        }

        private object o = new object();

        volatile int position = 0;


        private BaseReceiver receiver;

        /// <summary>
        /// 信息接收处理实体
        /// </summary>
        public BaseReceiver Receiver
        {
            get { return receiver; }
            set { receiver = value; }
        }

        #region ISender 成员
        /// <summary>
        /// 打开连接
        /// </summary>
        public void Open()
        {
            if (listSession.Count > 0)
            {
                throw new Exception("请先关闭Sender");
            }
            else
            {
                for (int i = 0; i < listNum; i++)
                {
                    try
                    {
                        BaseReceiver receiver = this.receiver.GetInstance();
                        receiver.IdleTime = this.idleTime;
                        receiver.ReLinkInterval = this.reLinkInterval;
                        receiver.ReSendTimes = this.reSendTimes;
                        receiver.ReSendOverTime = this.reSendOverTime;
                        Session _session = new Session();
                        _session.Id = i;
                        _session.Ip = this.Ip;
                        _session.Port = this.port;
                        _session.Receiver = receiver;
                        _session.QSize = qSize;
                        _session.SendBufferSize = sendBufferSize;
                        _session.ReceiveBufferSize = receiveBufferSize;
                        _session.ConnectOverTime = connectOverTime;
                        _session.Connect();
                        this.listSession.Add(_session);
                    }
                    catch (Exception e)
                    {
                        this.Close();
                        Debug.WriteLine(e);
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        ///  发送包（默认为受滑动窗口控制）
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public Boolean Send(IPackage package)
        {
            return this.Send(package, true);
        }

        /// <summary>
        /// 发送包
        /// </summary>
        /// <param name="package">数据包</param>
        /// <param name="isControl">是否受滑动窗口控制</param>
        /// <returns></returns>
        public Boolean Send(IPackage package, Boolean isControl)
        {
            try
            {
                return this.SelectSession().Send(package, isControl);
            }
            catch (Exception e)
            {
                Debug.WriteLine( e);
                throw e;
            }
        }

        /// <summary>
        /// 关闭连接(并清理所有资源，包括重连、重发和空闲检测)
        /// </summary>
        public void Close()
        {
            foreach (Session item in listSession)
            {
                try
                {
                    item.Close();
                    item.Receiver.Dispose();
                }
                catch (Exception e)
                {
                    Debug.WriteLine( e);
                    throw e;
                }
            }
            listSession.Clear();
        }

        /// <summary>
        /// 得到连接状态
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            for (int i = 0; i < listSession.Count; i++)
                if (listSession[i].State == SessionState.Connected)
                    return true;
            return false;
        }
        #endregion

        /// <summary>
        /// 选择可用Session
        /// </summary>
        /// <returns></returns>
        private Session SelectSession()
        {
            try
            {
                switch (this.listSession.Count)
                {
                    case 0:
                        throw new Exception("无可用的连接");
                    case 1:
                        return listSession[0];
                    default:
                        {
                            while (true)
                            {
                                for (int i = 0; i < listSession.Count; i++)
                                {
                                    int temp;
                                    lock (o)
                                    {
                                        temp = position < listSession.Count ? position : position - listSession.Count;
                                        position = temp + 1;
                                    }
                                    if (listSession[temp].IsReady())
                                    {
                                        return listSession[temp];
                                    }
                                }
                                Thread.Sleep(1);
                            }
                        }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine( e);
                throw e;
            }
        }
    }
}
