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
        
        private void Insert_Click(object sender, EventArgs e)
        {
            moveList = new ArrayList();
           list = new ArrayList();
            totalLength = 0;
            list.Add("D:\\Downloads");
            list.Add("D:\\我的资料库\\Downloads");
            list.Add("F:\\Downloads");
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
                bool hasIncomplete=false;
                bool hasBigFile=false;
                FileInfo[] fileInfos = NextFolder.GetFiles("*", SearchOption.AllDirectories);
                double folderLength = 0;
                foreach(FileInfo f in fileInfos)
                {

                    if (f.Length / 1024 / 1024 > 70 && (f.Extension == ".bt!" || f.Extension == ".!ut" || f.Extension == ".bc!"||f.Extension==".az!"))
                    {
                        hasIncomplete = true;
                        continue;
                    }
                    else if (f.Length / 1024 / 1024 > 70)
                    {
                        hasBigFile = true;
                    }
                    folderLength += f.Length/1024/1024;
                }
                if (!hasIncomplete && hasBigFile)
                {
                    moveList.Add(new MyFileInfo ( NextFolder.FullName));
                    totalLength += folderLength;
                }


            }
           
         
        }

        private void moveFile()
        {
            foreach (MyFileInfo myfileInfo in moveList)
            {
                string folder = myfileInfo.Path;
                string newDir = folder[0] + ":\\abcd\\"+new DirectoryInfo(folder).Name;
                try
                {
               
                    Directory.Move(folder, newDir);
                }
                catch(Exception e)
                {

                    MessageBox.Show("old:"+folder+"       new:"+newDir +"     "+e.Message);
                }

            }
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
