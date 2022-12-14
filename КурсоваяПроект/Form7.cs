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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        
        public void update(int index)
        {
            Program.admin.user_info(index, textBox1, textBox2, textBox3, textBox4, checkedListBox1);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.UsersTable.update();
            Program.UsersTable.Show();
        }

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Trim();
            textBox2.Text = textBox2.Text.Trim();
            textBox3.Text = textBox3.Text.Trim();
            textBox4.Text = textBox4.Text.Trim();
            if (Program.admin.check_user(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, true))
            {
                Program.admin.edit_user(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, checkedListBox1);
                this.Hide();
                Program.UsersTable.update();
                Program.UsersTable.Show();
            }
            else
            {
                MessageBox.Show("ОШИБКА ИЗМЕНЕНИЯ\n");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.admin.delete_user();
            this.Hide();
            Program.UsersTable.update();
            Program.UsersTable.Show();
        }
    }
}
