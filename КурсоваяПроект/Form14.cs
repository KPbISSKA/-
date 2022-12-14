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
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
        }
        public void update(int index)
        {
            Program.teacher.word_info(index, textBox1, textBox2);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.WordsTable.update();
            Program.WordsTable.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Trim();
            textBox2.Text = textBox2.Text.Trim();
            if (Program.teacher.check_word(textBox1.Text, textBox2.Text, true))
            {
                Program.teacher.edit_word(textBox1.Text, textBox2.Text);
                this.Hide();
                Program.WordsTable.update();
                Program.WordsTable.Show();
            }
            else
            {
                MessageBox.Show("ОШИБКА ИЗМЕНЕНИЯ\n");
            }
        }

        private void Form14_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.teacher.delete_word();
            this.Hide();
            Program.WordsTable.update();
            Program.WordsTable.Show();
        }
    }
}
