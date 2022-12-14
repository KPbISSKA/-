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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Autorization.Show();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Dictionary.update();
            Program.Dictionary.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.MyDictionary.update();
            Program.MyDictionary.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.ModulesList.update();
            Program.ModulesList.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.student.training();
            Program.Training.Show();
            Program.Training.update();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Student_Statistics.update();
            Program.Student_Statistics.Show();
        }
    }
}
