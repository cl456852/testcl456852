using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.interf;
using System.Reflection;

namespace Framework
{
    public abstract class AbstractFactory
    {

        public abstract IContentDownloader createContentDownloader();

        public abstract IListPageDownloader createlstDl();

        public abstract ISinglePageDonwloader createSlDl();
    }
}
