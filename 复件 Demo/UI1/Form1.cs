﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MODEL;
using BLL;
using DAL;
using DB;
using System.IO;
namespace UI1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<MyFileInfo> list = new List<MyFileInfo>();

        public void refresh()
        {
            list = FileBLL.getFileList();
            dataGridView1.DataSource = list;
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 20;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //refresh();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            MyFileInfo myFileInfo = new MyFileInfo();
            try
            {
                
                int row = e.RowIndex;
                myFileInfo.FileId = Convert.ToInt32(dataGridView1["FileId", row].Value);
                myFileInfo.FileName = dataGridView1["FileName", row].Value.ToString();
                myFileInfo.Directory = dataGridView1["Directory", row].Value.ToString();
                myFileInfo.DirectoryName = dataGridView1["DirectoryName", row].Value.ToString();
                myFileInfo.Extension = dataGridView1["Extension", row].Value.ToString();
                myFileInfo.LastAccessTime = dataGridView1["LastAccessTime", row].Value.ToString();
                myFileInfo.LastWriteTime = dataGridView1["LastWriteTime", row].Value.ToString();
                myFileInfo.Length = Convert.ToInt32(dataGridView1["length", row].Value);
                myFileInfo.Mark = dataGridView1["mark", row].Value.ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            if (FileDAL.Update(myFileInfo) > 0)
                //MessageBox.Show("Seccess");
                ;
            else
                MessageBox.Show("Fail");
        }

        private void Insert_Click(object sender, EventArgs e)
        {
    
            List<MyFileInfo> duplicateList = new List<MyFileInfo>();
            duplicateList= FileBLL.InsertFiles(textBox1.Text.Replace("\\","\\\\"));
            if (duplicateList.Capacity > 0)
            {

                Form2 f2 = new Form2(duplicateList);
                foreach (MyFileInfo myFileInfo in duplicateList)
                {
                    if (myFileInfo.Length > 70)
                        moveFile(myFileInfo);
                }
                f2.Show();
            }
            else
                MessageBox.Show("seccuss CDID: "+FileDAL.getMaxCDID());
            refresh();
        }

        private void moveFile(MyFileInfo myFileInfo)
        {
            if(!Directory.Exists(myFileInfo.DirectoryName[0] + ":\\duplicate\\"))
                Directory.CreateDirectory(myFileInfo.DirectoryName[0] + ":\\duplicate\\");
            string newPath = Path.Combine(myFileInfo.DirectoryName[0] + ":\\duplicate\\", myFileInfo.FileName);
            while (File.Exists(newPath))
                newPath += "1";
           
            File.Move(Path.Combine(myFileInfo.DirectoryName, myFileInfo.FileName), newPath);
            
        
                
        }
        private void button1_Click(object sender, EventArgs e)
        {

            using (OpenFolderDialog openFolderDlg = new OpenFolderDialog())
            {
                if (openFolderDlg.ShowDialog() == DialogResult.OK)
                {
                    this.textBox1.Text = openFolderDlg.Path;
                    Console.WriteLine(this.textBox1.Text.Replace("\\","\\\\"));
                }
            }
        }
        string dataClicked="" ;
        bool flag = false;
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            List<MyFileInfo> sortList = new List<MyFileInfo>();
            sortList = (List<MyFileInfo>)dataGridView1.DataSource;
            string dataclick = dataGridView1.Columns[e.ColumnIndex].DataPropertyName;
            //List<MyFileInfo> listNow;
            //if (dataClicked == dataGridView1.Columns[e.ColumnIndex].DataPropertyName)
            //{
                
            //    listNow= (List<MyFileInfo>)dataGridView1.DataSource;
            //    listNow.Reverse();
            //    dataGridView1.DataSource = listNow;
            //}
            //else
            //    dataGridView1.DataSource = FileDAL.selectMyFileInfo(dataGridView1.Columns[e.ColumnIndex].DataPropertyName);
            //dataClicked = dataGridView1.Columns[e.ColumnIndex].DataPropertyName;
            //dataGridView1.Refresh();
            //dataGridView1.Columns[e.ColumnIndex].HeaderCell.Style.
            if (flag = true && dataclick == dataClicked)
            {
                sortList.Reverse();

            }
            else
            {
                sortList = FileBLL.Sort(list, dataclick);

            }
            dataGridView1.DataSource = sortList;
            dataGridView1.Refresh();
            flag = true;
            dataClicked = dataclick;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Contains("`"))
            {
                textBox2.Text = "";
                DBHelper.connstr = this.textBox3.Text;
                refresh();
            }
            string[] searchStr = textBox2.Text.ToLower().Split(' ');
            bool flag = true;
            List<MyFileInfo> newList=new List<MyFileInfo>();
            for (int i = 0; i < list.Count; i++)
            {

                flag = true;
                for (int j = 0; j < searchStr.Length; j++)
                {
                    if (!(list[i].Directory.ToLower().Contains(searchStr[j]) || list[i].FileName.Substring(0, list[i].FileName.LastIndexOf('.') >= 0 ? list[i].FileName.LastIndexOf('.') : list[i].FileName.Length).ToLower().Contains(searchStr[j]) || list[i].Mark.ToLower().Contains(searchStr[j])))
                    {
                        flag = false;
                        break;
                        
                    }
                    
                    
                }
                if (flag)
                    newList.Add(list[i]);
                
            }
            this.dataGridView1.DataSource = newList;
            //list = newList;
            dataGridView1.Refresh();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DBHelper.connstr = this.textBox3.Text;
            refresh();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            textBox2.Focus();
        }
        
        

        //private void SortRows(DataGridViewColumn sortColumn, bool orderToggle)
        //{
        //    if (sortColumn == null)
        //        return;

        //    //清除前面的排序
        //    if (sortColumn.SortMode == DataGridViewColumnSortMode.Programmatic &&
        //        dataGridView1.SortedColumn != null &&
        //        !dataGridView1.SortedColumn.Equals(sortColumn))
        //    {
        //        dataGridView1.SortedColumn.HeaderCell.SortGlyphDirection =
        //            SortOrder.None;
        //    }

        //    //设定排序的方向（升序、降序）
        //    ListSortDirection sortDirection;
        //    if (orderToggle)
        //    {
        //        sortDirection =
        //            dataGridView1.SortOrder == SortOrder.Descending ?
        //            ListSortDirection.Ascending : ListSortDirection.Descending;
        //    }
        //    else
        //    {
        //        sortDirection =
        //            dataGridView1.SortOrder == SortOrder.Descending ?
        //            ListSortDirection.Descending : ListSortDirection.Ascending;
        //    }
        //    SortOrder sortOrder =
        //        sortDirection == ListSortDirection.Ascending ?
        //        SortOrder.Ascending : SortOrder.Descending;

        //    //进行排序
        //    dataGridView1.Sort(sortColumn, sortDirection);

        //    if (sortColumn.SortMode == DataGridViewColumnSortMode.Programmatic)
        //    {
        //        //变更排序图标
        //        sortColumn.HeaderCell.SortGlyphDirection = sortOrder;
        //    }
        //}


    }
}
