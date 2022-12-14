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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.UsersTable.update();
            Program.UsersTable.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Trim();
            textBox2.Text = textBox2.Text.Trim();
            textBox3.Text = textBox3.Text.Trim();
            textBox4.Text = textBox4.Text.Trim();
            if(comboBox1.Text.Length>0 && Program.admin.check_user(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, false))
            {
                this.Hide();
                Program.admin.add_user(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, comboBox1.Text);
                Program.UsersTable.update();
                Program.UsersTable.Show();
            }
            else
            {
                MessageBox.Show("ОШИБКА ДОБАВЛЕНИЯ\n");
            }
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
