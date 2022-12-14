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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        public void update()
        {
            Program.admin.update_users_table(listBox1);
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Admin.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Create_user.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && listBox1.SelectedIndex != 0) 
            {

                this.Hide();
                Program.Edit_user.update(listBox1.SelectedIndex);
                Program.Edit_user.Show();
            }

        }
    }
}
