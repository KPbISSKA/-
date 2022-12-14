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
    public partial class Form16 : Form
    {
        public Form16()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.ModulesTable.update();
            Program.ModulesTable.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Trim();
            if (Program.teacher.check_module(textBox1.Text, false))
            {
                this.Hide();
                Program.teacher.add_module(textBox1.Text);
                Program.ModulesTable.update();
                Program.ModulesTable.Show();
            }
            else
            {
                MessageBox.Show("ОШИБКА ДОБАВЛЕНИЯ\n");
            }
        }

        private void Form16_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
