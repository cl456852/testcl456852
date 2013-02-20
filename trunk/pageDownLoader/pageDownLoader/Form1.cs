﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;

namespace pageDownLoader
{
    public partial class Form1 : Form
    {
        int year=2011;
        //int month=1;
        public Form1()
        {
            InitializeComponent();
        }
        //http://javjunkies.com/main/2012/01-01-3/
        private void button1_Click(object sender, EventArgs e)
        {
            Donwloader downloader = new Donwloader();

            for (int month = 2; month <= 12; month++)
            {
                for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
                {
                    string monthStr = month < 10 ? "0" + month : month + "";
                    string url = "http://javjunkies.com/main/";
                    url += year + "/" + monthStr + "-" + (i < 10 ? "0" + i : i + "");
                    string html = downloader.GetHtml(url);
                    MatchCollection mc = downloader.findUrls(html);
                    ArrayList htmls = new ArrayList();
                    Html h = new Html();
                    h.Content = html;
                    h.Name = url.Replace('/', '_').Replace(":", "^");
                    htmls.Add(h);
                    foreach (Match m in mc)
                    {
                        Html ht = new Html();
                        ht.Content = downloader.GetHtml(m.Value);
                        ht.Name = m.Value.Replace('/', '_').Replace(":", "^");
                        htmls.Add(ht);

                    }
                    foreach (Html ht in htmls)
                    {
                        downloader.SaveFile(ht.Content, ht.Name + ".htm");
                    }

                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
