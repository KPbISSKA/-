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
    public partial class Form23 : Form
    {
        public Form23()
        {
            InitializeComponent();
        }

        public void update()
        {
            Program.student.update_mymodules_table(listBox1);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Student.Show();
        }

        private void Form23_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && listBox1.SelectedIndex != 0)
            {
                this.Hide();
                Program.student.start_module(listBox1.SelectedIndex);
                Program.ModulesWords.update();
                Program.ModulesWords.Show();
            }

        }
    }
}
