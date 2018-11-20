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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            (sender as Button).Left += 30;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                MessageBox.Show("我被选中了");
            }
            else
            {
                MessageBox.Show("我被取消了");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strResult = "";
            foreach ( var item in checkedListBox1.CheckedItems)
            {
                strResult += item.ToString();
            }
            MessageBox.Show(strResult);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strResult = "";
            foreach (var item in listBox1.SelectedItems)
            {
                strResult += item.ToString();
            }
            MessageBox.Show(strResult);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //读取数据库
            List<string> citys = new List<string>()
            {
                "上海","北京","深圳"
            };

            listBox1.DataSource = citys;
            comboBox1.DataSource = citys;
        }
    }
}
