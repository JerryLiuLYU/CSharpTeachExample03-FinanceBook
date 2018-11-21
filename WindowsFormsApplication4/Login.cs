using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string strUsername = textBoxUsername.Text;
            string strPassword = textBoxPassword.Text;
            if (IsValid(strUsername, strPassword))
            {
                this.Visible = false;
                Form2 frm = new Form2();
                frm.Show();

            }
            else
            {
                MessageBox.Show("用户名或者密码错误");
            }
        }

        private bool IsValid(string strUsername, string strPassword)
        {
            if (strPassword == "123")
            {
                return true;
            }
            return false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            textBoxUsername.Text = "";
            textBoxPassword.Text = "";
            textBoxUsername.Focus();
        }
    }
}
