using System;
using System.Collections.Generic;
using System.Text;

namespace Lxt2.Communication.Framework
{
    public interface IReceiver
    {
        /// <summary>
        /// session
        /// </summary>
        ISession Session
        {
            get;
            set;
        }
        /// <summary>
        /// 完成连接后处理方法
        /// </summary>
        /// <param name="client"></param>
        void AfterConnect(ISession client);
        /// <summary>
        /// 断开连接完成回调方法
        /// </summary>
        /// <param name="client"></param>
        void AfterDisConnect(ISession client);
        /// <summary>
        /// 断开连接完成回调方法
        /// </summary>
        /// <param name="client"></param>
        void AfterClose(ISession client, List<IPackage> removeList);
        /// <summary>
        /// 发送完成回调方法
        /// </summary>
        /// <param name="client"></param>
        void AfterSend(ISession client);
        /// <summary>
        /// 接收数据完成回调方法
        /// </summary>
        /// <param name="client"></param>
        /// <param name="package"></param>
        /// <param name="key"></param>
        void AfterReceive(ISession client, IPackage package, ref long key);
        /// <summary>
        /// 空闲回调方法
        /// </summary>
        /// <param name="client"></param>
        void AfterIdle(ISession client);
        /// <summary>
        /// 发送超时清理回调方法
        /// </summary>
        /// <param name="client"></param>
        /// <param name="package"></param>
        void AfterRemove(ISession client,IPackage package);
        /// <summary>
        /// 资源清理
        /// </summary>
        void Dispose();
    }
}
