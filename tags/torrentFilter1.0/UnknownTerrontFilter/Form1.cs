using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace UnknownTerrontFilter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String[] path = Directory.GetFiles(textBox1.Text, "*", SearchOption.TopDirectoryOnly);
            foreach (string p in path)
            {
                string prifix;
                string name = Path.GetFileName(p);
                if(name.Contains("%20"))
                {
                    prifix=name.Split(new string[]{ "%20"},StringSplitOptions.RemoveEmptyEntries)[0];
                }
                else
                {
                    prifix=name.Split('.')[0];
                }
                if (DB.DBHelper.checkUnknownTorrents(prifix+".") > 10)
                {
                    string newPath=Path.Combine( Path.GetDirectoryName(p),"filtered",Path.GetFileName(p));
                    if (!Directory.Exists(Path.GetDirectoryName( newPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(newPath));
                    }
                    File.Move(p, newPath);
                }
            }
        }
    }
}
