using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace КурсоваяПроект
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }
        public void update(int index)
        {
            Program.admin.group_info(index, textBox1, textBox2, listBox1);
        }
        public ListBox Get_listbox()
        {
            return listBox1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.GroupsTable.update();
            Program.GroupsTable.Show();
        }

        private void Form10_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Edit_group_add_info.update();
            Program.Edit_group_add_info.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && listBox1.SelectedIndex != 0)
            {
                Program.admin.group_info_delete_done(listBox1.SelectedIndex,listBox1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Trim();
            textBox2.Text = textBox2.Text.Trim();
            if (Program.admin.check_group(textBox1.Text, textBox2.Text,true))
            {
                this.Hide();
                Program.admin.edit_group(textBox1.Text, textBox2.Text);
                Program.GroupsTable.update();
                Program.GroupsTable.Show();
            }
            else
            {
                MessageBox.Show("ОШИБКА ИЗМЕНЕНИЯ\n");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.admin.delete_group();
            this.Hide();
            Program.GroupsTable.update();
            Program.GroupsTable.Show();
        }
    }
}
