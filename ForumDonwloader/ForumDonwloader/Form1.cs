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
using RarbgDownloader;

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
            IListPageDownloader lpd = Config.Factory.createlstDl();
            string url = textBox1.Text;
            int start =Convert.ToInt32( textBox2.Text);
            int end = Convert.ToInt32(textBox3.Text);
            string path = textBox4.Text;
            IListPageDownloader lstDl = Config.Factory.createlstDl();
            WaitCallback callBack;
            callBack = new WaitCallback(lstDl.Download);

          
            for (int i = start; i <= end; i++)
            {
                ThreadPool.QueueUserWorkItem(lpd.Download,new AsynObj(path,url));
            }
        }

      
    }
}
