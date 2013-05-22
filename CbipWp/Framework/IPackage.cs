using System;
using System.Collections.Generic;
using System.Text;

namespace Lxt2.Communication.Framework
{
    public interface IPackage
    {
        /// <summary>
        /// 获取头数据
        /// </summary>
        /// <returns></returns>
        byte[] GetHead();
        /// <summary>
        /// 设置头数据
        /// </summary>
        /// <param name="package"></param>
        void SetHead(byte[] package);
        /// <summary>
        /// 获取Body数据
        /// </summary>
        /// <returns></returns>
        byte[] GetBody();
        /// <summary>
        /// 设置body数据
        /// </summary>
        /// <param name="package"></param>
        void SetBody(byte[] package);
        /// <summary>
        /// 获取package数据
        /// </summary>
        /// <returns></returns>
        byte[] GetPackage();
        /// <summary>
        /// 设置package数据
        /// </summary>
        /// <param name="package"></param>
        void SetPackage(byte[] package);
        /// <summary>
        /// 数据验证
        /// </summary>
        /// <param name="package"></param>
        /// <param name="packageBytes"></param>
        /// <returns></returns>
        Boolean CheckMessage(ref IPackage package, byte[] packageBytes);
        /// <summary>
        /// 获取数据key信息
        /// </summary>
        /// <returns></returns>
        long GetKey();
        /// <summary>
        /// 深度克隆当前实例
        /// </summary>
        /// <returns></returns>
        IPackage Clone();
        /// <summary>
        /// 创建新实例
        /// </summary>
        /// <returns></returns>
        IPackage GetInstance();
    }
}
