using System;
using System.Collections.Generic;
using System.Text;

namespace Lxt2.Communication.Framework
{
    public interface ISubmit<T>:IPackage
    {
        T GetKey();
    }
}
