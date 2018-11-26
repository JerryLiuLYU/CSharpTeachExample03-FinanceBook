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

    
    public partial class Form2 : Form
    {
        private List<InOutType> _data = new List<InOutType>();

        public Form2()
        {
            InitializeComponent();
        }

        private void radioButtonIn_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonIn.Checked)
            {               
                //comboBoxInOutClass.DataSource = _data.Find(p=>p.Name==radioButtonIn.Text).InOutClasses;
                //comboBoxInOutClass.DisplayMember = "Name";


                foreach (var item in _data)
                {
                    if (item.Name == "收入")
                    {
                        comboBoxInOutClass.Items.Clear();
                        foreach (var cls in item.InOutClasses)
                        {
                            comboBoxInOutClass.Items.Add(cls);
                            comboBoxInOutClass.DisplayMember = "Name";
                        }
                    }
                }
            }

        }

        private void radioButtonOut_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOut.Checked)
            {
                //comboBoxInOutClass.DataSource = _data.Find(p => p.Name == radioButtonOut.Text).InOutClasses;
                //comboBoxInOutClass.DisplayMember = "Name";
                foreach (var item in _data)
                {
                    if (item.Name == radioButtonOut.Text)
                    {
                        comboBoxInOutClass.Items.Clear();
                        foreach (var cls in item.InOutClasses)
                        {
                            comboBoxInOutClass.Items.Add(cls);
                            comboBoxInOutClass.DisplayMember = "Name";

                        }
                    }
                }

            }
        }

        private void comboBoxInOutClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var t = comboBoxInOutClass.DataSource as List<InOutClass>;
            //if (t.Find(p => p.Name == comboBoxInOutClass.Text) == null)
            //{
            //    return;
            //}
            //listBoxItem.DataSource = t.Find(p => p.Name == comboBoxInOutClass.Text).Items;
            //listBoxItem.SelectedIndex = -1;

            var t = ((InOutClass)(comboBoxInOutClass.SelectedItem)).Items;
            listBoxItem.Items.Clear();
            foreach (var item in t)
            {
                listBoxItem.Items.Add(item);
            }




        }

        private void Form2_Load(object sender, EventArgs e)
        {

            #region 模拟数据
            InOutType t1 = new InOutType();
            t1.Name = "收入";
            InOutClass t1c1 = new InOutClass();
            t1c1.Name = "工资";
            t1c1.Items = new List<string>() { "月工资", "奖金" };
            InOutClass t1c2 = new InOutClass();
            t1c2.Name = "兼职";
            t1c2.Items = new List<string>() { "程序员", "设计师" };

            t1.InOutClasses.Add(t1c1);
            t1.InOutClasses.Add(t1c2);

            InOutType t2 = new InOutType();
            t2.Name = "支出";
            InOutClass t2c1 = new InOutClass();
            t2c1.Name = "餐饮";
            t2c1.Items = new List<string>() { "外出吃饭", "购买食材" };
            InOutClass t2c2 = new InOutClass();
            t2c2.Name = "交通";
            t2c2.Items = new List<string>() { "飞机", "火车" };

            t2.InOutClasses.Add(t2c1);
            t2.InOutClasses.Add(t2c2);

            _data.Add(t1);
            _data.Add(t2); 
            #endregion
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void GenerateSummary()
        {
            StringBuilder strSummary = new StringBuilder();
            strSummary.Append("要保存的信息为：\r\n");
            strSummary.AppendFormat("{0}-{1}\r\n", comboBoxInOutClass.Text, listBoxItem.SelectedItem);
            strSummary.AppendFormat("日期：{0}\r\n", dateTimePickerDate.Text);
            strSummary.AppendFormat("说明：{0}\r\n", textBoxRemark.Text);
            string strPeople = "";
            foreach (string item in checkedListBox1.CheckedItems)
            {
                strPeople += item + ",";
            }
            strPeople = strPeople.TrimEnd(',');
            strSummary.AppendFormat("收支人：{0}\r\n", strPeople);
            strSummary.AppendFormat("金额：{0}\r\n", numericUpDownMoney.Text);
            
            textBoxSummary.Text = strSummary.ToString();
        }

        private void listBoxItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateSummary();
        }

        private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
        {
            GenerateSummary();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDownMoney_ValueChanged(object sender, EventArgs e)
        {
            GenerateSummary();
        }

        private void textBoxRemark_TextChanged(object sender, EventArgs e)
        {
            GenerateSummary();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateSummary();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
