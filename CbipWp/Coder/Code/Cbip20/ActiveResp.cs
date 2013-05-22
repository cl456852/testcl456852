using System;
using System.Collections.Generic;
using System.Text;
using Lxt2.Communication.Framework;

namespace Lxt2.Cbip.Api.Code.Cbip20
{
   public class ActiveResp:AbsPackage
    {
       public ActiveResp()
        {
            this.CommandID = AbsPackage.CBIP_ACTIVE_RESP;
        }
       public override byte[] GetBody()
        {
            return null;
        }

       public override void SetBody(byte[] body)
        {

        }

       public override IPackage Clone()
       {
           return (IPackage)this.MemberwiseClone();
       }

       public override IPackage GetInstance()
       {
           return new ActiveResp();
       }

       public override string ToString()
       {
           StringBuilder str = new StringBuilder();
           str.Append(this.GetType().Name);
           str.Append("    TotalLength:");
           str.Append(this.TotalLength);
           str.Append(" CommandID:");
           str.Append(this.CommandID);
           str.Append(" SequenceID:");
           str.Append(this.SequenceID);
           str.Append(" CommandStatus:");
           str.Append(this.CommandStatus);
           return str.ToString();
       }
    }
}
