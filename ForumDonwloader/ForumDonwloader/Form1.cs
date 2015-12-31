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
using Framework.tool;
using System.Collections.Specialized;
using System.Configuration;
using System.Collections;
using System.IO;
using RarbgDownloader;
using Sis001Downloader;

namespace ForumDonwloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string url=null;
        string path;
        int start;
        int end;

        private void button1_Click(object sender, EventArgs e)
        {

            init();

            DlConfig.storage.Clear();
            DirectoryInfo dinfo = new DirectoryInfo(textBox4.Text);
            FileInfo[] finfo= dinfo.GetFiles();
            foreach (FileInfo f in finfo)
            {
                if (f.Name.Contains("_torrent_")&&f.Name.EndsWith(".htm"))
                    DlConfig.storage.Add(f.Name.Replace("http^__rarbg.to_torrent_", "").Replace(".htm", ""));
            }
            IListPageDownloader lpd = Config.Factory.createlstDl();
            for (int i = start; i <= end; i++)
            {
                ThreadPool.QueueUserWorkItem(lpd.Download,new AsynObj(path,string.Format( url,i)));
            }
        }

        void init()
        {
            path = textBox4.Text;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            System.Net.ServicePointManager.DefaultConnectionLimit = 200;
            url= textBox1.Text;
            start= Convert.ToInt32(textBox2.Text);
            end= Convert.ToInt32(textBox3.Text);
            Console.WriteLine("SETMAXTHREADS " + ThreadPool.SetMaxThreads(17, 17));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            init();
            IListPageDownloader lpd = new Sis001LstDl();
            for (int i = start; i <= end; i++)
            {
                ThreadPool.QueueUserWorkItem(lpd.Download, new AsynObj(path, string.Format(url, i)));
            }
            
        }

      
    }
}
