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
    public partial class Form17 : Form
    {
        public Form17()
        {
            InitializeComponent();
        }
        public void update(int index)
        {
            Program.teacher.module_info(index, textBox1, listBox1);
        }
        public ListBox Get_listbox()
        {
            return listBox1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.ModulesTable.update();
            Program.ModulesTable.Show();
        }

        private void Form17_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Edit_module_add_info.update();
            Program.Edit_module_add_info.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && listBox1.SelectedIndex != 0)
            {
                Program.teacher.module_info_delete_done(listBox1.SelectedIndex, listBox1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Trim();
            if (Program.teacher.check_module(textBox1.Text,  true))
            {
                this.Hide();
                Program.teacher.edit_module(textBox1.Text);
                Program.ModulesTable.update();
                Program.ModulesTable.Show();
            }
            else
            {
                MessageBox.Show("ОШИБКА ИЗМЕНЕНИЯ\n");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.teacher.delete_module();
            Program.ModulesTable.update();
            Program.ModulesTable.Show();
        }
    }
}
