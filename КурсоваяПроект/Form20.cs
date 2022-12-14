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
    public partial class Form20 : Form
    {
        public Form20()
        {
            InitializeComponent();
        }

        public void update()
        {
            Program.student.update_words_table(listBox1);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Student.Show();
        }

        private void Form20_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && listBox1.SelectedIndex != 0)
            {

                Program.student.subscribe(listBox1.SelectedIndex);
                update();

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
