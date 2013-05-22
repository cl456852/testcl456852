using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lxt2.Cbip.Api.API;
using Lxt2.Cbip.Api.Code.API;
using Lxt2.Cbip.Api.Code.Cbip20;

namespace CbipWp
{
    public class RespReceiver : IRespReceiver
    {
        #region IRespReceiver 成员

        public void Receive(IApiSubmit submit, IApiSubmitResp resp)
        {
            SubmitResp s = (SubmitResp)resp;
            Debug.WriteLine(s);
        }

        #endregion
    }
}
