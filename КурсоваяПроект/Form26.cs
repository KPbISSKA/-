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
    public partial class Form26 : Form
    {
        public Form26()
        {
            InitializeComponent();
        }

        public void update()
        {
            Program.student.statistics(listBox1);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Student.Show();
        }

        private void Form26_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
