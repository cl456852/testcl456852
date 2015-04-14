using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using BencodeLibrary;

namespace MoveFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ArrayList list;
        ArrayList moveList;
        ArrayList moveList1;
        
        private void Insert_Click(object sender, EventArgs e)
        {
            moveList1 = new ArrayList();
            moveList = new ArrayList();
           list = new ArrayList();
            totalLength = 0;
            list.Add("D:\\THUNDER20150108");
            list.Add("D:\\QQDownload");
            list.Add("D:\\QQDownload1");
            //list.Add("D:\\VuzeDownloads1");
            //list.Add("D:\\新建文件夹");
            process();
            moveFile();
            //test();
        }

        void test()
        {
            DirectoryInfo TheFolder = new DirectoryInfo("D:\\新建文件夹 (2)\\新建文件夹");
            Directory.Move(TheFolder.FullName, "d:\\abcd");
            Console.WriteLine(TheFolder.Root.FullName);
        }
        double totalLength = 0;
        private void process()
        {
            
            foreach (string s in list)
            {
                process(s);
            }
            label1.Text = totalLength.ToString();
            dataGridView1.DataSource = moveList;
            dataGridView1.Refresh();
        }

        private void process(string folderPath)
        {
             
            
            DirectoryInfo TheFolder = new DirectoryInfo(folderPath);
            foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories("*",SearchOption.TopDirectoryOnly))
            {
                int torrentNumber = 0;
                bool hasIncomplete=false;
                bool hasBigFile=false;
                FileInfo[] fileInfos = NextFolder.GetFiles("*", SearchOption.AllDirectories);
                double folderLength = 0;
                foreach(FileInfo f in fileInfos)
                {

                    if (f.Length / 1024 / 1024 > 60 && (f.Extension == ".bt!" || f.Extension == ".!ut" || f.Extension == ".bc!" || f.Extension == ".az!" || f.Extension == ".td" || f.Extension == ".tdl"))
                    {
                        hasIncomplete = true;
                        continue;
                    }
                    else if (f.Length / 1024 / 1024 > 60)
                    {
                        hasBigFile = true;
                    }
                    if (f.Extension.ToLower() == ".torrent")
                    {
                        torrentNumber++;
                    }
                    folderLength += f.Length/1024/1024;
                }
                if (!hasIncomplete && hasBigFile)
                {
                    if (torrentNumber > 1)
                    {
                        moveList1.Add(new MyFileInfo(NextFolder.FullName));
                    }
                    else
                        moveList.Add(new MyFileInfo ( NextFolder.FullName));
                    totalLength += folderLength;

                }
              


            }
           
         
        }

        private void moveFile()
        {
            foreach (MyFileInfo myfileInfo in moveList)
            {
                string newDir="";
                bool hasTorrent=false;
                string torrentName="";
                string folder = myfileInfo.Path;
                DirectoryInfo TheFolder = new DirectoryInfo(folder);
                foreach (FileInfo file in TheFolder.GetFiles())
                {
                    if (file.Extension == ".torrent")
                    {
                        hasTorrent = true;
                        torrentName = getName(file.FullName);
                    }
                }
                if(hasTorrent)
                    newDir = folder[0] + ":\\abcd\\finish\\" + torrentName;
                else
                    newDir = folder[0] + ":\\abcd\\finish\\" + new DirectoryInfo(folder).Name;
                try
                {
               
                    Directory.Move(folder, newDir);
                }
                catch(Exception e)
                {

                    MessageBox.Show("old:"+folder+"       new:"+newDir +"     "+e.Message);
                }

            }
            foreach (MyFileInfo myfileInfo in moveList1)
            {
                string folder = myfileInfo.Path;
                string newDir = folder[0] + ":\\abcd\\muti-torrents\\" + new DirectoryInfo(folder).Name;
                try
                {

                    Directory.Move(folder, newDir);
                }
                catch (Exception e)
                {

                    MessageBox.Show("old:" + folder + "       new:" + newDir + "     " + e.Message);
                }

            }
        }

        private string getName(string torrentPath)
        {
            BDict torrentFile = BencodingUtils.DecodeFile(torrentPath) as BDict;

            return ((BString)((torrentFile["info"] as BDict)["name"])).Value;
        }
    }


    public class MyFileInfo
    {

        public MyFileInfo(string path)
        {
            this.Path = path;
        }
        string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }
    }
}
