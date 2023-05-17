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
    public partial class Form2 : Form
    {
        public string name;
        Size size1 = new Size(250, 150);
        Size size2 = new Size(250, 170);
        Size sizea1 = new Size(50, 110);
        Size sizea2 = new Size(49, 110);
        List<string> Lists = new List<string>();
        public Form2(List<string> lists)
        {
            InitializeComponent();
            this.Size = size1;
            label2.Hide();
            Lists = lists;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            name = textBox1.Text;
            if (name != "" && !Lists.Contains(name))
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else if (name == "")
            {
                this.Size = size2;
                label2.Text = "List name cannot be empty";
                label2.Size = sizea1;
                label2.Show();
            }
            else
            {
                this.Size = size2;
                label2.Text = "That name is already in use";
                label2.Size = sizea2;
                label2.Show();
            }
        }
    }
}
