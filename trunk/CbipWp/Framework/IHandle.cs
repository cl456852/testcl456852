using System;
using System.Collections.Generic;
using System.Text;

namespace Lxt2.Communication.Framework.Handle
{
    public interface IHandle
    {
        /// <summary>
        /// 数据合法性验证
        /// </summary>
        /// <param name="package"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        Boolean CheckMessage(ref IPackage package,byte[] bytes);
        /// <summary>
        /// 数据包处理
        /// </summary>
        /// <param name="client"></param>
        /// <param name="packageBytes"></param>
        void DealMessage(ISession client, IPackage packageBytes);
    }
}
