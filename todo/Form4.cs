using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace todo
{
    public partial class Form4 : Form
    {
        public string name;
        Size size1 = new Size(250, 150);
        Size size2 = new Size(250, 170);
        public Form4(int mode)
        {
            InitializeComponent();
            if (mode == 1)
            {
                label1.Text = "Enter new name for item";
            }
            label2.Hide();
            this.Size = size1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            name = textBox1.Text;
            if (name != "")
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                label2.Show();
                this.Size = size2;
            }
        }
    }
}
