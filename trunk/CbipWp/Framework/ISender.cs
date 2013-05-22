using System;
using System.Collections.Generic;
using System.Text;

namespace Lxt2.Communication.Framework
{
    public interface ISender
    { 
        /// <summary>
        /// 请求建立连接
        /// </summary>
        void Open();
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        Boolean Send(IPackage package);
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="package"></param>
        /// <param name="isControl"></param>
        /// <returns></returns>
        Boolean Send(IPackage package, Boolean isControl);
        /// <summary>
        /// 断开连接并释放所有资源
        /// </summary>
        void Close();
    }
}
