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
    public partial class Test : Form
    {
        public Test()
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

        private void button4_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int i = 0; i < 100; i+=20)
            {
                var now = new DateTime(2018, 4, 12, 23, 7, 3);
                
                string day = now.Day.ToString("00");
                string month = now.Month.ToString("00");
                string year = now.Year.ToString("00");
                string hour = now.Hour.ToString("00");
                string minute = now.Minute.ToString("00");
                string second = now.Second.ToString("00");
                string SerialNumber = "CK" + year + month + day + hour + minute + second + rnd.Next(10, 99).ToString();
                richTextBox1.Text += SerialNumber + "\r\n";
            }
            // console.log(SerialNumber)
            // return SerialNumber;
        }
    }
}
