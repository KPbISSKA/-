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
    public partial class Form24 : Form
    {
        public Form24()
        {
            InitializeComponent();
        }

        public void update()
        {
            if (Program.student.check_list())
            {
                label2.Visible = false;
                button2.Visible = false;
                button4.Visible = false;
                Program.student.next_stage(label1,label2);
            }
            else
            {
                this.Hide();
                Program.Student.Show();
            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Student.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Visible = true;
            button2.Visible = true;
            button4.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.student.result(false);
            update();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Program.student.result(true);
            update();
        }

        private void Form24_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
