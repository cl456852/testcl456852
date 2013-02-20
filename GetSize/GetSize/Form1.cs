using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using DB;
using System.Data.SqlClient;
using System.Collections;

namespace GetSize
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(@"D:\我的资料库\Documents\pagenew\software\video filter - 副本\UI1\bin\Debug\his.txt");
            string his = sr.ReadToEnd();
            sr.Close();
            string[] hiss = his.Split(',');
            string html="";
            Regex regexVid = new Regex("VID:.*");
            Regex regex = new Regex("Size:.*");
            Regex regexActress = new Regex("Actress:.*");
            Regex regexFileCount = new Regex("Files:.*");
            Regex regexFiles = new Regex("List:.*?<");
            string size="";
            double sizeDouble;
            int fileCount=0;
            string actress="";
            string files="";
            foreach (string s in hiss)
            {
                if (s != "")
                {
                    if (DBHelper.getCount(s) == 0)
                    {
                        //http://www.141jav.com/search/jufd097/
                        html = GetHtml("http://www.141jav.com/search/" + s);
                        string[] subHtmls = html.Split(new string[] { "<h3><p>" }, StringSplitOptions.RemoveEmptyEntries);
                        bool isFind = false;
                        foreach (string subHtml in subHtmls)
                        {
                            if (subHtml.Contains("VID:") && regexVid.Match(subHtml).Value.ToLower().Contains(s))  //如果符合查找条件
                            {
                                isFind = true;
                                size = regex.Match(subHtml).Value;
                                if (size.EndsWith("GB"))
                                {
                                    size = size.Replace("Size:", "").Replace("GB", "");
                                    sizeDouble = Convert.ToDouble(size) * 1024;

                                }
                                else
                                    sizeDouble = Convert.ToDouble(size.Replace("Size:", "").Replace("MB", "").Replace("KB", ""));
                                actress = regexActress.Match(subHtml).Value.Replace("Actress: ", "");
                                fileCount = Convert.ToInt32(regexFileCount.Match(subHtml).Value.Replace("Files: ", ""));
                                files = regexFiles.Match(subHtml).Value.Replace("List: ", "").Replace("<", "").Replace("'","''");



                                DBHelper.insertHis(s, sizeDouble, actress, fileCount, files);
                            }

                        }
                        if (!isFind)
                        {
                            DBHelper.insertHis(s, 0, actress, fileCount, files);

                        }


                    }
                }
            }

        }



        public string GetHtml(string url)
        {
            Console.WriteLine(url);
            string str = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Timeout = 30000;
                request.Headers.Set("Pragma", "no-cache");
                WebResponse response = request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding("GB2312");
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                str = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("GET PATE ERROR  " + url);
                Console.WriteLine(ex.Message);
                if (!ex.Message.Contains("404"))
                    str = GetHtml(url);
            }
            return str;
        }

       private void button2_Click(object sender, EventArgs e)
        {
            string letter = "";
            string number = "";
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex("[a-z]");
            bool isEndofLetter = false;
            StreamReader sr = new StreamReader("his.txt");
            string content = sr.ReadToEnd();
            string[] list = content.Split(',');
            foreach (string id in list)
            {
                if (id != "")
                {
                    
                        for (int i = 0; i < id.Length; i++)                        //修改   对于出现KIDM235A  KIDM235B
                            if (reg1.IsMatch(id[i].ToString()))
                            {
                                if (isEndofLetter)
                                    break;
                                else
                                    letter += id[i];
                            }
                            else
                            {
                                number += id[i];
                                isEndofLetter = true;
                            }
                        if (!File.Exists("d:\\dictionaryList\\"+letter+number+".htm"))
                        {
                            string url = "http://www.141jav.com/search/" + letter + number;
                            SaveFile(GetHtml(url), "d:\\dictionaryList\\" + id + ".htm");
                        }
                }
                letter="";
                number="";
                isEndofLetter = false;
            }
        }

       public void SaveFile(string content, string fileName)
       {
           //实例化一个文件流--->与写入文件相关联
           FileStream fs = new FileStream( fileName, FileMode.Create);
           //实例化一个StreamWriter-->与fs相关联
           StreamWriter sw = new StreamWriter(fs);
           //开始写入
           sw.Write(content);
           //清空缓冲区
           sw.Flush();
           //关闭流
           sw.Close();
           fs.Close();
       }

       private void button3_Click(object sender, EventArgs e)
       {
           Analysis aly = new Analysis();
           string[] path = Directory.GetFiles("D:\\html2", "*", SearchOption.TopDirectoryOnly);
           foreach (string p in path)
           {
               ArrayList list= aly.alys(p);
               foreach (His his in list)
               {
                   DBHelper.UpdateInfo(his);
               }

           }

       }

       private void button4_Click(object sender, EventArgs e)
       {
           StreamReader sr = new StreamReader(@"D:\我的资料库\Documents\pagenew\software\video filter - 副本\UI1\bin\Debug\his.txt");
           string his = sr.ReadToEnd();
           sr.Close();
           string[] hiss = his.Split(',');
           foreach (string id in hiss)
           {
               if (id != "")
               {
                   string url = "http://javjunkies.com/main/?s=" + id;
                   SaveFile(GetHtml(url), "d:\\html2\\" + id+".htm");
               }
           }
       }

       private void button5_Click(object sender, EventArgs e)
       {
           Filter filter = new Filter();
           ArrayList list = DBHelper.getList();
           foreach (His his in list)
           {
               DBHelper.getDiffSizeList(his);
        
           }
           foreach (KeyValuePair<string, string> entry in DBHelper.dic)
           {
               if (filter.checkValid(entry.Key.ToString()))
               {
                   StreamWriter sw = File.AppendText("his.txt");
                   sw.Write(entry.Key.ToString()+",");
                   sw.Flush();
                   sw.Close();
               }
           }
       }

       private void button6_Click(object sender, EventArgs e)
       {
           StreamReader sr = new StreamReader(@"his - 副本.txt");
           string his = sr.ReadToEnd();
           sr.Close();
           string[] oringinal = his.Split(',');
           StreamReader sr1 = new StreamReader(@"his.txt");
           string his1 = sr1.ReadToEnd();
           sr1.Close();
           string[] newString = his1.Split(',');
           foreach (string s in oringinal)
               if (!newString.Contains(s))
                   Console.WriteLine(s);
       }

       private void button7_Click(object sender, EventArgs e)
       {
           string[] path = Directory.GetFiles("D:\\dictionaryList", "*", SearchOption.TopDirectoryOnly);
           string res = "";
           foreach(string p in path)
           {
                StreamReader sr = new StreamReader(p);
                string his = sr.ReadToEnd();
                res+= his.Split(new string[] { "<div role=\"main\">", " <div class=\"side-panel\">" },StringSplitOptions.RemoveEmptyEntries)[1];
                sr.Close();
           }
           SaveFile(res, "D:\\dictionaryList\\result.htm");


       }


    }
}
