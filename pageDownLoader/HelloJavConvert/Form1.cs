using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using pageDownLoader;

namespace HelloJavConvert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Donwloader down = new Donwloader();
            Regex r = new Regex("idx=\\d*\"");
            Regex reg=new Regex("idx=.*?\"");
            string oringi = readFile(textBox1.Text);
            MatchCollection mc= r.Matches(oringi);
            foreach (Match m in mc)
            {
                string html= down.GetHtml(("http://www.hellojav.com/include/file_downpage.php?" + m.Value).Replace("\"",""));
                string newStr = reg.Match(html).Value;
                oringi.Replace(m.Value, newStr);
                
            }
            down.SaveFile(oringi, "d:\\res.htm");
        }

        string readFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            string res = sr.ReadToEnd();
            sr.Close();
            return res;
        }

        

    }
}
