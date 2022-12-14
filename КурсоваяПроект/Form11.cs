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
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }
        public void update()
        {
            Program.admin.group_info_add(checkedListBox1);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Edit_group.Show();
        }

        private void Form11_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.admin.group_info_add_done(checkedListBox1, Program.Edit_group.Get_listbox());
            this.Hide();
            Program.Edit_group.Show();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
