using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lxt2.Cbip.Api.API;
using Lxt2.Cbip.Api.Code.Cbip20;

namespace CbipWp
{
    public class ApiReceiver : IApiReceiver<AbsPackage>
    {

        #region IApiReceiver<AbsPackage> 成员

        public void Receive(AbsPackage data)
        {
            //throw new NotImplementedException();
            if (data.GetType() == typeof(Deliver))
            {
                Deliver delive = (Deliver)data;
                Console.WriteLine("上行：" + delive.MessageContent);
            }
            if (data.GetType() == typeof(Report))
            {
                Report report = (Report)data;
                //Console.WriteLine("状态报告："+report.DestMobile);
            }

        }

        #endregion
    }
}
