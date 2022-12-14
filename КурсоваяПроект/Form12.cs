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
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }
        public void update()
        {
            Program.teacher.update_words_table(listBox1);
        }
        private void Form12_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Teacher.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Create_word.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && listBox1.SelectedIndex != 0)
            {
                this.Hide();
                Program.Edit_word.update(listBox1.SelectedIndex);
                Program.Edit_word.Show();
            }

        }

    }
}
