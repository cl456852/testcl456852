using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Framework;
using Framework.interf;

namespace ForumDonwloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IListPageDownloader lstDl = Config.Factory.createlstDl();
            WaitCallback callBack;
            callBack = new WaitCallback(downLoad);

            bool flag = ThreadPool.SetMaxThreads(1, 1);
            for (int month = 3; month <= 3; month++)
            {
                for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
                {
                    ThreadPool.QueueUserWorkItem(callBack, _141javUrl + "2013-" + month + "-" + i + "/1");
                }
            }
        }

        private void Download()
        {

        }
    }
}
