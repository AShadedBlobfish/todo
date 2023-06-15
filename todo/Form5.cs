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
    public partial class Form5 : Form
    {
        public List<string> Items;
        public Form5(List<string> items, string listName)
        {
            InitializeComponent();
            label1.Text = "Items in " + listName;
            Items = items;
            listBox1.DataSource = Items;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && listBox1.SelectedIndex != 0)
            {
                string String = Items[listBox1.SelectedIndex - 1];
                Items[listBox1.SelectedIndex - 1] = listBox1.SelectedItem.ToString();
                Items[listBox1.SelectedIndex] = String;
                listBox1.DataSource = Items;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && listBox1.SelectedIndex != Items.Count)
            {
                string String = Items[listBox1.SelectedIndex + 1];  // Todo - Fix
                Items[listBox1.SelectedIndex + 1] = listBox1.SelectedItem.ToString();
                Items[listBox1.SelectedIndex] = String;
                listBox1.DataSource = Items;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }
    }
}
