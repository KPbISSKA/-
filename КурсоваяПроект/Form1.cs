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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.user.clear_user();
            string text = textBox1.Text;
            string[] words = text.Split(new string[] { " " } , StringSplitOptions.RemoveEmptyEntries);
            if (words.Length > 0)
            {
                Program.user.first_name = words[0];
                if (words.Length > 1)
                {
                    Program.user.second_name = words[1];
                    if (words.Length > 2)
                        Program.user.third_name = words[2];
                }
            }

            Program.user.password = textBox2.Text;
            Program.user.Autorization();

            if (Program.user.status == "active" )
            {
                switch (Program.user.role)
                {
                    case "admin":
                        this.Hide();
                        Program.admin = new admin(Program.user);
                        Program.Admin.Show();
                        break;
                    case "teacher":
                        this.Hide();
                        Program.teacher = new teacher(Program.user);
                        Program.Teacher.Show();
                        break;
                    case "student":
                        this.Hide();
                        Program.student = new student(Program.user);
                        Program.Student.Show();
                        break;
                    default:
                        break;
                }
            }
            else MessageBox.Show("В ДОСТУПЕ ОТКАЗАНО");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
