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
    public partial class Form19 : Form
    {
        public Form19()
        {
            InitializeComponent();
        }
        public void update()
        {
            listBox1.Items.Clear();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stat_type = comboBox1.SelectedItem.ToString();
            Program.teacher.statistics(stat_type, listBox1);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Teacher.Show();
        }

        private void Form19_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
