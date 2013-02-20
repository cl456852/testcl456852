using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MODEL;


namespace UI1
{
    public partial class Form2 : Form
    {
        public Form2(List<MyFileInfo> mfi)
        {
            InitializeComponent();
            dataGridView1.DataSource = mfi;
        }
    }
}
