using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Lxt2.Communication.Framework.Util;
using Lxt2.Communication.Framework.Receiver;

using System.Collections.Generic;
using System.Diagnostics;

namespace Lxt2.Communication.Framework.Client
{
    /// <summary>
    /// 异步非阻塞客户端
    /// </summary>
    public class Session : ISession
    {

        int id = 0;

        /// <summary>
        /// Session 编号
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        volatile String ip = "127.0.0.1";

        /// <summary>
        /// ip地址
        /// </summary>
        public String Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        volatile int port = 1234;

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        volatile int qSize = 16;

        /// <summary>
        /// 滑动窗口大小
        /// </summary>
        public int QSize
        {
            get { return qSize; }
            set { qSize = value; }
        }

        volatile int receiveBufferSize = 8192;

        /// <summary>
        /// 接收缓存大小
        /// </summary>
        public int ReceiveBufferSize
        {
            get { return receiveBufferSize; }
            set { receiveBufferSize = value; }
        }

        volatile int sendBufferSize = 8192;

        /// <summary>
        /// 发送缓存大小
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

        volatile int closeOverTime = 60;

        /// <summary>
        /// 断连超时时间（20秒）
        /// </summary>
        public int CloseOverTime
        {
            get { return closeOverTime; }
            set { closeOverTime = value; }
        }

        volatile int workerThreads = 64;

        /// <summary>
        /// 线程池中辅助线程的最大数目
        /// </summary>
        public int WorkerThreads
        {
            get { return workerThreads; }
            set { workerThreads = value; }
        }

        volatile int completionPortThreads = 64;

        /// <summary>
        /// 线程池中异步 I/O 线程的最大数目。
        /// </summary>
        public int CompletionPortThreads
        {
            get { return completionPortThreads; }
            set { completionPortThreads = value; }
        }

        SessionState state = SessionState.Closed;


        object o = new object();

        object link = new object();

        /// <summary>
        /// Session State
        /// </summary>
        public SessionState State
        {
            get
            {
                lock (o)
                {
                    return state;
                }
            }
            set
            {
                lock (o)
                {
                    state = value;
                }
            }
        }


        /// <summary>
        /// Socket
        /// </summary>
        Socket client;

        ManualResetEvent connectDone =
            new ManualResetEvent(false);


        ManualResetEvent sendDone =
            new ManualResetEvent(false);

        public ManualResetEvent ConnectDone
        {
            get { return connectDone; }
            set { connectDone = value; }
        }
        /// <summary>
        /// 滑动窗口
        /// </summary>
        BlockHashtable blockMap;

        public BlockHashtable BlockMap
        {
            get { return blockMap; }
        }

        BaseReceiver receiver;

        private Exception connException;

        public Exception ConnException
        {
            get { return connException; }
            set { connException = value; }
        }

        public BaseReceiver Receiver
        {
            get { return receiver; }
            set { receiver = value; }
        }

        /// <summary>
        /// 用于注入参数
        /// </summary>
        private Dictionary<String, Object> parameter = new Dictionary<string, Object>();

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="key">参数键</param>
        /// <param name="value">参数值</param>
        public void SetPara(String key, Object value)
        {
            lock (parameter)
            {
                if (this.parameter.ContainsKey(key))
                {
                    this.parameter.Remove(key);
                }
                this.parameter.Add(key, value);
            }
        }
        /// <summary>
        /// 通过键获取参数值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Object GetPara(String key)
        {
            lock (parameter)
            {
                Object obj;
                this.parameter.TryGetValue(key, out obj);
                return obj;
            }
        }

        /// <summary>
        /// 建立连接
        /// </summary>
        public void Connect()
        {
            try
            {
                lock (link)
                {
                    if (this.state == SessionState.Closed)
                    {
                        this.connectDone.Reset();
                        connException = null;
                        Debug.WriteLine(this.GetHashCode() + "(1)开始物理连接");
                        this.state = SessionState.Connecting;
                        if (this.workerThreads != 0 || this.completionPortThreads != 0)
                        {
                            ThreadPool.SetMaxThreads(this.workerThreads, this.completionPortThreads);
                        }
                        blockMap = new BlockHashtable(this.qSize);
                        
                        client = new Socket(AddressFamily.InterNetwork,
                            SocketType.Stream, ProtocolType.Tcp);
                        // The socket will linger for 10 seconds after 
                        // Socket.Close is called.
                       // client.LingerState = new LingerOption(true, 10);
                        client.ReceiveBufferSize = ReceiveBufferSize;
                        client.SendBufferSize = SendBufferSize;
                        SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();

                        //将消息内容转化为发送的byte[]格式 
                       // byte[] buffer = Encoding.UTF8.GetBytes(Message.Text);
                        //将发送内容的信息存放进Socket异步事件参数中
                        //socketEventArg.SetBuffer(buffer, 0, buffer.Length);
                        //注册Socket完成事件
                        string host = this.Ip;
                        int port = this.Port;
                        socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(ConnectCallback);

                        DnsEndPoint hostEntry = new DnsEndPoint(host, port);
                        //设置Socket异步事件参数的Socket远程终结点
                        socketEventArg.RemoteEndPoint = hostEntry;
                        //将定义好的Socket对象赋值给Socket异步事件参数的运行实例属性
                        socketEventArg.UserToken = client;
                        //byte[] buffer = new byte[500];
                        //socketEventArg.SetBuffer(buffer, 0, buffer.Length);
                        client.ConnectAsync(socketEventArg);
                        Debug.WriteLine("session", this.GetHashCode() + "(--)连接中");
                       // if (!connectDone.WaitOne(1000 * this.connectOverTime))
                        if (!connectDone.WaitOne())
                        {
                            Debug.WriteLine("session", this.GetHashCode() + "(--)连接超时");
                            throw new Exception(this.GetHashCode() + " 连接超时 " + this.connectOverTime);
                        }
                        else
                        {
                            if (this.connException != null)
                            {
                                Debug.WriteLine("session", this.GetHashCode() + "(--)连接异常:" + connException.Message);
                                throw connException;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                this.connectDone.Set();
                this.Close();
                Debug.WriteLine( e);
                throw e;
            }
        }
        /// <summary>
        /// 连接后回调方法
        /// </summary>
        /// <param name="ar"></param>
        private void ConnectCallback(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                Socket _client = (Socket)e.UserToken;
                Receive();
                Receiver.ConnetCallBack(this);
            }
            catch (Exception ex)
            {
                connException = ex;
                this.connectDone.Set();
                this.Close();
                Debug.WriteLine( e);
                System.Console.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// 异步发送
        /// </summary>
        /// <param name="package">数据包</param>
        /// <param name="isControl">是否受滑动窗口控制</param>
        /// <returns></returns>
        public Boolean Send(IPackage package, Boolean isControl)
        {
            try
            {
                if (this.State == SessionState.Connected || this.State == SessionState.Connecting)
                {
                   
                    byte[] b = package.GetPackage();
                    if (isControl)
                    {
                        InnerPackage innerPackage = new InnerPackage();
                        innerPackage.Package = package;
                        blockMap.Add(package.GetKey(), innerPackage);
                    }

               
            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            
            //将发送内容的信息存放进Socket异步事件参数中
            socketEventArg.SetBuffer(b, 0, b.Length);
            //注册Socket完成事件
            socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(SendCallback);
            //将定义好的Socket对象赋值给Socket异步事件参数的运行实例属性
            socketEventArg.UserToken = client;
            client.SendAsync(socketEventArg);
 
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                this.Close();
                Debug.WriteLine( e);
                throw e;
            }
        }



        /// <summary>
        /// 滑动窗口数据回收
        /// </summary>
        /// <param name="key"></param>
        public void Response(long key)
        {
            try
            {
                blockMap.Remove(key);
            }
            catch (Exception e)
            {
                Debug.WriteLine( e);
                throw e;
            }
        }

        private void SendCallback(object sender, SocketAsyncEventArgs e)
        {
            try
            {
               
                Receiver.SendCallBack(this);
            }
            catch (Exception ex)
            {
                this.Close();
                Debug.WriteLine( ex);
                throw ex;
            }
            finally
            {
                this.sendDone.Set();
            }
        }
        private void Receive()
        {
            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            byte[] bt = new byte[500];
            socketEventArg.SetBuffer(bt,0,bt.Length);
            socketEventArg.Completed += ReceiveCallback;
            socketEventArg.UserToken = client;
            //socketEventArg.Buffer = new byte[500];
            client.ReceiveAsync(socketEventArg);
        }

        private void ReceiveCallback(object sender,SocketAsyncEventArgs e)
        {
            try
            {
                
                Socket handler = (Socket)e.UserToken;
                
                
                
                if (e.BytesTransferred > 0)
                {
                    byte[] readbuffer = new byte[e.BytesTransferred];
                    Array.Copy(e.Buffer, 0, readbuffer, 0, e.BytesTransferred);
                    long key = 0;
                    Receiver.ReceiveCallBack(this, readbuffer, ref key);
                    if (key != 0)
                    {
                        this.Response(key);
                    }
                }
                
             SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            socketEventArg.Completed += ReceiveCallback;
            socketEventArg.UserToken = client;
            socketEventArg.SetBuffer(new byte[500], 0, 500);
            handler.ReceiveAsync(socketEventArg);
            
            }
            catch (Exception ex)
            {
                this.Close();
                Debug.WriteLine( ex);
            }

        }
        /// <summary>
        /// 完成连接
        /// </summary>
        /// <param name="isLogin">是否已登录成功</param>
        public void Connected(Boolean isLogin)
        {
            if (this.client.Connected)
            {
                if (isLogin)
                {
                    this.State = SessionState.Connected;
                }
            }
            else
                this.State = SessionState.Closed;
            connectDone.Set();
        }

        /// <summary>
        /// 彻底关闭连接
        /// </summary>
        public void Close()
        {
            try
            {
                lock (link)
                {
                    if (this.State != SessionState.Closed)
                    {
                        Debug.WriteLine("session", this.GetHashCode() + "(5)开始关闭连接");
                        this.State = SessionState.Closed;
                        if (this.client.Connected)
                        {
                            Debug.WriteLine("session", this.GetHashCode() + "(6)开始断开物理连接");
                            this.client.Close();
                            Debug.WriteLine("session", this.GetHashCode() + "(7)物理连接断开完成");
                        }
                        Debug.WriteLine("session", this.GetHashCode() + "(8)触发关闭连接回调方法");
                        this.receiver.CloseCallBack(this);
                        Debug.WriteLine("session", this.GetHashCode() + "(9)关闭连接回调方法完成");
                        Debug.WriteLine("session", this.GetHashCode() + "(10)关闭连接完成");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine( e);
                throw e;
            }
        }

        /// <summary>
        /// 获取滑动窗口中的数据包
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IPackage GetPackage(long key)
        {
            return this.blockMap.Get(key);
        }

        /// <summary>
        /// 判断当前session是否可用
        /// </summary>
        /// <returns></returns>
        public Boolean IsReady()
        {
            if (this.state == SessionState.Connected & !this.blockMap.IsBlock)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool Send(InnerPackage innerPackage)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Session 状态枚举
    /// </summary>
    public enum SessionState
    {
        /// <summary>
        /// 连接中
        /// </summary>
        Connecting = 1,
        /// <summary>
        /// 已建立连接
        /// </summary>
        Connected = 2,
        /// <summary>
        /// 已关闭连接
        /// </summary>
        DisConnected = 3,
        /// <summary>
        /// 已完全关闭
        /// </summary>
        Closed = 4
    }
}
