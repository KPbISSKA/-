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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Trim();
            textBox2.Text = textBox2.Text.Trim();
            if (Program.admin.check_group(textBox1.Text, textBox2.Text,false))
            {
                this.Hide();
                Program.admin.add_group(textBox1.Text, textBox2.Text);
                Program.GroupsTable.update();
                Program.GroupsTable.Show();
            }
            else
            {
                MessageBox.Show("ОШИБКА ДОБАВЛЕНИЯ\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.GroupsTable.update();
            Program.GroupsTable.Show();
        }

        private void Form9_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
