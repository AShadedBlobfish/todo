using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;

namespace todo
{
    public partial class Form1 : Form
    {
        public List<string> Lists = new List<string>(); // A List which will contain the names of all the todo lists the user has created
        List<string> Items = new List<string>(); // A List which will contain the items in the list which is currently being viewed
        List<bool> isComplete = new List<bool>();
        string listsPath = @"data\lists.txt";   // The path to the text file where these names are stored
        string itemsPath;  // The path to the folder which will contain each list's individual items
        public Form1()
        {
            InitializeComponent();
            // Checks if the dir "data" exists and creates it if it doesn't
            if (!Directory.Exists("data"))
            {
                Directory.CreateDirectory("data");
            }
            // Checks if the dir "data\lists" exists and creates it if it doesn't
            if (!Directory.Exists(@"data\lists"))
            {
                Directory.CreateDirectory(@"data\lists");
            }
            // Checks if the file lists.txt exists and creates it if it doesn't
            if (!File.Exists(listsPath))
            {
                File.WriteAllText(listsPath, "Warning: Modifying this file is not recommended. If this file is corrupted, the program will not work\n\n");
            }
            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
            string[] listsStrings = File.ReadAllLines(listsPath);   // Creates an array of strings from each line of lists.txt
            // Adds every line from line 4 onwards (Line 1 is a warning and lines 2 and 3 are empty) from "listsStrings" to the list "Lists"
            for (int i = 2; i < listsStrings.Length; i++)
            {
                Lists.Add(listsStrings[i]);
            }
            lists.DataSource = Lists;   // Sets the data source for the listbox items to the list "Lists"
            lists.SelectedIndex = -1;   // By default the first item is selected when the DataSource updates - this deselects it
            noListSelected();
        }

        private void noListSelected()
        {
            label1.Text = "Choose a list";
            label2.Text = "from the menu";
            label3.Text = "or create a new list";
            label4.Text = "to begin";
            label1.Show();
            label2.Show();
            label3.Show();
            label4.Show();
            label6.Hide();
            checkedListBox1.Hide();
            checkedListBox1.DataSource = null;
            foreach (int i in checkedListBox1.CheckedIndices)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            add.Hide();
            remove.Hide();
            edit.Hide();
            delete.Hide();
        }

        private void newList_Click(object sender, EventArgs e)
        {
            // Shows a dialog (Form2) for naming and creating a new list
            Form2 form2 = new Form2(Lists);
            form2.ShowDialog();
            Lists.Add(form2.name);  // Adds the new list to "Lists"
            // Needed to update the side panel
            lists.DataSource = null;
            lists.DataSource = Lists;
            updateListsFile();

            checkedListBox1.DataSource = null;

            lists.SelectedIndex = Lists.Count() - 1;    // Selects the last element of the listbox (The list that was just created)
        }

        private void lists_SelectedIndexChanged(object sender, EventArgs e)
        {
            Items.Clear();
            isComplete.Clear();
            if (lists.SelectedIndex == -1)
            {
                noListSelected();
            }
            else
            {
                label1.Hide();
                label2.Hide();
                label3.Hide();
                label4.Hide();

                label6.Text = lists.SelectedItem.ToString();
                label6.Show();
                checkedListBox1.Show();
                checkedListBox1.DataSource = null;
                add.Show();
                remove.Show();
                edit.Show();
                delete.Show();

                itemsPath = @"data\lists\" + lists.SelectedItem.ToString() + ".txt";
                if (!File.Exists(itemsPath))
                {
                    File.WriteAllText(itemsPath, "Warning: Modifying this file is not recommended. If this file is corrupted, the program will not work\n\n");
                }
                string[] itemStrings = File.ReadAllLines(itemsPath);
                for (int i = 2; i < itemStrings.Length; i++)
                {
                    string item = itemStrings[i];
                    Items.Add(item.Substring(0, item.Length-3));
                    if (item.Substring(item.Length-1) == "1")
                    {
                        isComplete.Add(true);
                    }
                    else
                    {
                        isComplete.Add(false);
                    }
                }
                checkedListBox1.DataSource = Items;
                for (int j = 0; j < isComplete.Count; j++)
                {
                    if (isComplete[j])
                    {
                        checkedListBox1.SetItemChecked(j, true);
                    }
                    else
                    {
                        checkedListBox1.SetItemChecked(j, false);
                    }
                }
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                isComplete[e.Index] = true;
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                isComplete[e.Index] = false;
            }
            updateItemsFile();
        }

        private void updateItemsFile()
        {
            string stringToWrite = "Warning: Modifying this file is not recommended. If this file is corrupted, the program will not work\n\n";
            for (int i = 0; i < Items.Count; i++)
            {
                int q;
                if (isComplete[i] == true)
                {
                    q = 1;
                }
                else
                {
                    q = 0;
                }
                stringToWrite += Items[i].ToString() + "= " + Convert.ToString(q) + "\n";
            }
            File.WriteAllText(itemsPath, stringToWrite);
        }

        private void updateListsFile()
        {
            string stringToWrite = "Warning: Modifying this file is not recommended. If this file is corrupted, the program will not work\n\n";
            foreach (string String in Lists)
            {
                stringToWrite += String + "\n";
            }
            File.WriteAllText(@"data\lists.txt", stringToWrite);
        }

        private void delete_Click(object sender, EventArgs e)
        {
            int items = Items.Count;
            Form3 form3 = new Form3(lists.SelectedItem.ToString(), items);
            var result = form3.ShowDialog();
            if (result == DialogResult.OK)
            {
                Lists.Remove(lists.SelectedItem.ToString());
                lists.DataSource = null;
                lists.DataSource = Lists;
                lists.SelectedIndex = -1;
                noListSelected();
                File.Delete(itemsPath);
                checkedListBox1.DataSource = null;
                checkedListBox1.DataSource = Items;
                updateListsFile();
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(0);
            form4.ShowDialog();
            if (form4.DialogResult == DialogResult.OK)
            {
                Items.Add(form4.name);
                isComplete.Add(false);
                checkedListBox1.DataSource = null;
                checkedListBox1.DataSource = Items;
                for (int j = 0; j < isComplete.Count; j++)
                {
                    if (isComplete[j])
                    {
                        checkedListBox1.SetItemChecked(j, true);
                    }
                    else
                    {
                        checkedListBox1.SetItemChecked(j, false);
                    }
                }
                updateItemsFile();
            }
        }

        private void remove_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedItem != null)
            {
                Items.RemoveAt(checkedListBox1.SelectedIndex);
                isComplete.RemoveAt(checkedListBox1.SelectedIndex);
                checkedListBox1.DataSource = null;
                checkedListBox1.DataSource = Items;
                for (int j = 0; j < isComplete.Count; j++)
                {
                    if (isComplete[j])
                    {
                        checkedListBox1.SetItemChecked(j, true);
                    }
                    else
                    {
                        checkedListBox1.SetItemChecked(j, false);
                    }
                }
                updateItemsFile();
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedItem != null)
            {
                Form4 form4 = new Form4(1);
                form4.ShowDialog();
                if (form4.DialogResult == DialogResult.OK)
                {
                    Items[checkedListBox1.SelectedIndex] = form4.name;
                    checkedListBox1.DataSource = null;
                    checkedListBox1.DataSource = Items;
                    for (int j = 0; j < isComplete.Count; j++)
                    {
                        if (isComplete[j])
                        {
                            checkedListBox1.SetItemChecked(j, true);
                        }
                        else
                        {
                            checkedListBox1.SetItemChecked(j, false);
                        }
                    }
                    updateItemsFile();
                }
            }
        }
    }
}
