using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Lxt2.Communication.Framework;


namespace Lxt2.Communication.Framework.Handle
{
    public abstract class BaseHandle:IHandle
    {

    

        IPackage package;

        public IPackage Package
        {
            get { return package; }
            set { package = value; }
        }

        public Boolean CheckMessage(ref IPackage pack,byte[] bytes)
        {
            try
            {
                return this.package.CheckMessage(ref pack,bytes);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }
        public abstract void DealMessage(ISession client, IPackage package);
    }
}
