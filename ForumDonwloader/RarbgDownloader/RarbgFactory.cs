using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;

namespace RarbgDownloader
{
    class RarbgFactory : AbstractFactory
    {


        public override Framework.interf.IListPageDownloader createlstDl()
        {
            return new RarbgLstDl();
        }

        public override Framework.interf.ISinglePageDonwloader createSlDl()
        {
            return new RarbgSgDl();
        }
    }
}
