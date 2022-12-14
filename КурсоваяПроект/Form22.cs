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
    public partial class Form22 : Form
    {
        public Form22()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.MyDictionary.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Trim();
            textBox2.Text = textBox2.Text.Trim();
            if (Program.student.check_word(textBox1.Text, textBox2.Text))
            {
                this.Hide();
                Program.student.add_word(textBox1.Text, textBox2.Text);
                Program.MyDictionary.update();
                Program.MyDictionary.Show();
            }
            else MessageBox.Show("ОШИБКА ДОБАВЛЕНИЯ");
        }
    }
}
