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
    public partial class Form25 : Form
    {
        public Form25()
        {
            InitializeComponent();
        }

        public void update()
        {
            Program.student.update_module_words_table(listBox1);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.ModulesList.update();
            Program.ModulesList.Show();
        }

        private void Form25_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Training.Show();
            Program.Training.update();
        }
    }
}
