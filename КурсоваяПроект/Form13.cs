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
    public partial class Form13 : Form
    {
        public Form13()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.WordsTable.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Trim();
            textBox2.Text = textBox2.Text.Trim();
            if (Program.teacher.check_word(textBox1.Text, textBox2.Text, false))
            {
                this.Hide();
                Program.teacher.add_word(textBox1.Text, textBox2.Text);
                Program.WordsTable.update();
                Program.WordsTable.Show();
            }
            else
            {
                MessageBox.Show("ОШИБКА ДОБАВЛЕНИЯ\n");
            }
        }

        private void Form13_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
