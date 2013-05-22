using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Communication.Framework.Util;
using Lxt2.Communication.Framework.Client;

namespace Lxt2.Communication.Framework
{
    /// <summary>
    /// 客户端接口定义
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Session 编号
        /// </summary>
        int Id
        {
            get;
            set;
        }
        Exception ConnException
        {
            get;
            set;
        }

        /// <summary>
        /// 请求建立连接
        /// </summary>
        void Connect();
        /// <summary>
        /// 确认连接成功连接
        /// </summary>
        /// <param name="isLongin"></param>
        void Connected(Boolean isLongin);
        /// <summary>
        /// 发送数据(生产者)
        /// </summary>
        /// <param name="package">发送的数据包</param>
        /// <param name="isControl">是否需要滑动窗口控制</param>
        /// <returns></returns>
        Boolean Send(IPackage package, Boolean isControl);
        /// <summary>
        /// 发送innerPackage对象，默认为受滑动窗口控制（用于超时重发）
        /// </summary>
        /// <param name="innerPackage"></param>
        /// <returns></returns>
        Boolean Send(InnerPackage innerPackage);
        /// <summary>
        /// 发送应答（消费者）
        /// </summary>
        /// <param name="key"></param>
        void Response(long key);
        /// <summary>
        /// 断开连接并释放所有资源
        /// </summary>
        void Close();
        /// <summary>
        /// 滑动窗口
        /// </summary>
        BlockHashtable BlockMap
        {
            get;
        }
        /// <summary>
        /// Session状态
        /// </summary>
        SessionState State
        {
            get;
        }
        /// 获取滑动窗口中的数据包
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// </summary>
        IPackage GetPackage(long key);
        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="key">参数键</param>
        /// <param name="value">参数值</param>
        void SetPara(String key, Object value);
        /// <summary>
        /// 通过键获取参数值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Object GetPara(String key);
    }
}
