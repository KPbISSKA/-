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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Autorization.Show();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.WordsTable.update();
            Program.WordsTable.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.ModulesTable.update();
            Program.ModulesTable.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Statistics.update();
            Program.Statistics.Show();
        }
    }
}
