using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Cbip.Api.API.Impl;
using Lxt2.Cbip.Api.Code.API;
using Lxt2.Cbip.Api.Code.Cbip20;
using Lxt2.Cbip.Api.StaticInfo;
using Lxt2.Cbip.Api.API;
using Lxt2.Cbip.Api.Receiver.Cbip20;
using Lxt2.Communication.Framework.Util;
using System.Threading;
using Lxt2.Communication.Framework;
using System.Diagnostics;

namespace Lxt2.Cbip.Api
{
    public class CbipSender
    {
        protected Sender sender = new Sender();

        protected CBIPReceiver cbipRecv;

        protected ActiveSubmitSender submitSender;
        #region 字段封装
        
        string ip;
        /// <summary>
        /// 服务器IP
        /// </summary>
        public string Ip
        {
            get { return ip; }
            set
            {
                ip = value;
                sender.Ip = value;
                StaticHandle.Config.Ip = value;
            }
        }
        int port;
        /// <summary>
        /// 服务器连接端口
        /// </summary>
        public int Port
        {
            get { return port; }
            set
            {
                port = value;
                sender.Port = value;
                StaticHandle.Config.Port = value;
            }
        }
        int clientID;
        /// <summary>
        /// 客户端ID
        /// </summary>
        public int ClientID
        {
            get { return clientID; }
            set
            {
                clientID = value;
                StaticHandle.Config.ClientID = value;
            }
        }
        string userName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                StaticHandle.Config.UserName = value;
            }
        }
        string passWord;
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
            get { return passWord; }
            set
            {
                StaticHandle.Config.Password = value;
                passWord = value;
            }
        }

        IApiReceiver<AbsPackage> apiReceiver;
        /// <summary>
        /// 接收REPORT,DELIVER接口，用户实现
        /// </summary>
        public IApiReceiver<AbsPackage> ApiReceiver
        {
            get { return apiReceiver; }
            set
            {
                apiReceiver = value;
                StaticHandle.ApiReceiver = value;
            }
        }

        IRespReceiver respReceiver;
        /// <summary>
        ///接收RESPONSE接口，用户实现 
        /// </summary>
        public IRespReceiver RespReceiver
        {
            get { return respReceiver; }
            set
            {
                respReceiver = value;
                StaticHandle.RespReceiver = value;
            }
        }

        int qSize = 16;

        /// <summary>
        /// 滑动窗口大小（默认16）
        /// </summary>
        public int QSize
        {
            get { return qSize; }
            set
            {
                qSize = value;
                this.sender.QSize = value;
            }
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
            set
            {
                receiveBufferSize = value;
                sender.ReceiveBufferSize = value;
            }
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
            set
            {
                sendBufferSize = value;
                sender.SendBufferSize = value;
            }
        }

        int reSendTimes = 3;

        private int reSendOverTime = 60;

        /// <summary>
        /// 重发超时时间(60秒)
        /// </summary>
        public int ReSendOverTime
        {
            get { return reSendOverTime; }
            set
            {
                reSendOverTime = value;
                sender.ReSendOverTime = value;
            }
        }

        /// <summary>
        /// 重发次数（默认0次,不重发）
        /// </summary>
        public int ReSendTimes
        {
            get { return reSendTimes; }
            set
            {
                reSendTimes = value;
                sender.ReSendTimes = value;
            }
        }

        volatile int reLinkInterval = 60;

        /// <summary>
        /// 重连间隔（默认60秒）
        /// </summary>
        public int ReLinkInterval
        {
            get { return reLinkInterval; }
            set
            {
                reLinkInterval = value;
                sender.ReLinkInterval = value;
            }
        }
        volatile int idleTime = 30;

        /// <summary>
        /// 空闲时间(默认30秒)
        /// </summary>
        public int IdleTime
        {
            get { return idleTime; }
            set
            {
                idleTime = value;
                sender.IdleTime = value;
            }
        }

        volatile int listNum = 1;

        /// <summary>
        /// 连接数量（默认1）
        /// </summary>
        public int ListNum
        {
            get { return listNum; }
            set
            {
                listNum = value;
                sender.ListNum = value;
            }
        }

        int smsMessageLength = 1000;
        /// <summary>
        /// 短信内容长度限制
        /// </summary>
        public int SmsMessageLength
        {
            get { return smsMessageLength; }
            set
            {
                smsMessageLength = value;
                StaticHandle.SmsMessageLength = value;
            }
        }
        int mmsResoureceSize = 409600;
        /// <summary>
        /// 彩信资源大小限制
        /// </summary>
        public int MmsResoureceSize
        {
            get { return mmsResoureceSize; }
            set
            {
                mmsResoureceSize = value;
                StaticHandle.MmsResoureceSize = value;
            }
        }
        int maxGroupSize = 100;
        /// <summary>
        /// 组包个数限制
        /// </summary>
        public int MaxGroupSize
        {
            get { return maxGroupSize; }
            set
            {
                maxGroupSize = value;
                StaticHandle.MaxGroupSize = value;
            }
        }
        #endregion


        public CbipSender()
        {
            this.cbipRecv = new CBIPReceiver();
            sender.Receiver = cbipRecv;
            submitSender = new ActiveSubmitSender(sender);
            //sender.Open();
        }

        public CbipSender(string ip, int port, int clientID, string userName, string passWord, IApiReceiver<AbsPackage> apiReceiver, IRespReceiver respReceiver)
        {
            StaticHandle.Config.ClientID = clientID;
            StaticHandle.Config.UserName = userName;
            StaticHandle.Config.Password = passWord;
            StaticHandle.ApiReceiver = apiReceiver;
            StaticHandle.RespReceiver = respReceiver;

            this.cbipRecv = new CBIPReceiver();

            this.sender = new Sender();
            sender.Receiver = cbipRecv;
            this.sender.Ip = ip;
            this.sender.Port = port;

            this.Ip = ip;
            this.Port = port;
            this.ClientID = clientID;
            this.UserName = userName;
            this.PassWord = passWord;

            submitSender = new ActiveSubmitSender(sender);
            //sender.Open();
        }

        /// <summary>
        /// 发送SUBMIT 方法
        /// </summary>
        /// <param name="submit">短信或彩信SUBMIT</param>
        public void SendSubmit(IApiSubmit submit)
        {
            if (submit.GetType().Name == "Submit")
            {

                Interlocked.Increment(ref StaticHandle.smsCount);

                    //string log = "发送的数据（SMS）：" + StaticTool.ObjToString(submit);
                    string log = "发送的数据（SMS）：" + submit.ToString();
                    Debug.WriteLine("smsSendTaskList");
                
            }
            else
            {

                Interlocked.Increment(ref StaticHandle.mmsCount);

              
                    //string log = "发送的数据（MMS）：" + StaticTool.ObjToString(submit);
                    string log = "发送的数据（MMS）：" + submit.ToString();
                    Debug.WriteLine("mmsSendTaskList", log);
                
            }
            submitSender.SendSubmit(submit);

        }

        /// <summary>
        /// 发送彩信资源方法
        /// </summary>
        /// <param name="contentMms">彩信资源</param>
        /// <returns>彩信资源RESP 包含彩信资源ID</returns>
        public IApiSubmitResp SendMMSContent(ContentMms contentMms)
        {

            Interlocked.Increment(ref StaticHandle.resourseCount);

            
                string log = "发送的数据（彩信资源）：" + contentMms.ToString();

                Debug.WriteLine("mmsResource", log);
            
            return submitSender.SendMMSContent(contentMms);
        }

        /// <summary>
        /// 建立连接登陆
        /// </summary>
        public void Open()
        {
            string log = "服务器：Server =" + sender.Ip + ", Port=" + sender.Port;
            Debug.WriteLine("Debug", log);
            sender.Open();
        }

        /// <summary>
        /// 关闭所有连接
        /// </summary>
        public void Close()
        {
            StaticHandle.LoginTimes = 0;
            sender.Close();
        }

        /// <summary>
        /// 得到是否连接
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return sender.IsConnected();
        }
    }
}
