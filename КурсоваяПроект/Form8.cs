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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        public void update()
        {
            Program.admin.update_groups_table(listBox1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Admin.Show();
        }

        private void Form8_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Create_group.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && listBox1.SelectedIndex != 0)
            {

                this.Hide();
                Program.Edit_group.update(listBox1.SelectedIndex);
                Program.Edit_group.Show();
            }
        }
    }
}
