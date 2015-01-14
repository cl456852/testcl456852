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
using System.Collections.Specialized;
using System.Configuration;
using System.Collections;
using System.IO;

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

            string path = textBox4.Text;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            System.Net.ServicePointManager.DefaultConnectionLimit = 200;

            DlConfig.storage.Clear();
            DirectoryInfo dinfo = new DirectoryInfo(textBox4.Text);
            FileInfo[] finfo= dinfo.GetFiles();
            foreach (FileInfo f in finfo)
            {
                if (f.Name.Contains("_torrent_")&&f.Name.EndsWith(".htm"))
                    DlConfig.storage.Add(f.Name.Replace("http^__rarbg.com_torrent_", "").Replace(".htm", ""));
            }
            IListPageDownloader lpd = Config.Factory.createlstDl();
            string url = textBox1.Text;
            int start =Convert.ToInt32( textBox2.Text);
            int end = Convert.ToInt32(textBox3.Text);
            
            IListPageDownloader lstDl = Config.Factory.createlstDl();
            Console.WriteLine("SETMAXTHREADS "+ ThreadPool.SetMaxThreads(17,17));

           
            for (int i = start; i <= end; i++)
            {
                ThreadPool.QueueUserWorkItem(lpd.Download,new AsynObj(path,string.Format( url,i)));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (RarbgDownloader.DlConfig.useProxy)
                RarbgDownloader.DlConfig.useProxy = false;
            else
                RarbgDownloader.DlConfig.useProxy =true;
        }

      
    }
}
