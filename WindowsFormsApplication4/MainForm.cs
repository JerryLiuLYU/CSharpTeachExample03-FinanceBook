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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 添加收支ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About frm = new About();
            
            frm.ShowDialog();
        }

        private void 添加收支项目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItem frm = new AddItem();
            frm.MdiParent = this;
            frm.Show();
            //以下代码只允许该窗口打开一个
            //if (Application.OpenForms["AddItem"] == null)
            //{
            //    AddItem child = new AddItem();
            //    child.MdiParent = this;                
            //    child.Show();
            //}
            //else//有Name为FrmChildren的子船体，就直接show()
            //{
            //    Application.OpenForms["AddItem"].Show();
            //    Application.OpenForms["AddItem"].Focus();
            //}
        }
    }
}
