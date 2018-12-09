using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{

    
    public partial class Form2 : Form
    {
        // private List<InOutType> _data = new List<InOutType>();

        public event Action<string> AddSuccess;

        private int _listId = 0;

        public Form2()
        {
            InitializeComponent();
        }
        public Form2(int listId):this()
        {
            _listId = listId;
        }

        private void radioButtonIn_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonIn.Checked)
            {
                //comboBoxInOutClass.DataSource = _data.Find(p=>p.Name==radioButtonIn.Text).InOutClasses;
                //comboBoxInOutClass.DisplayMember = "Name";


                //foreach (var item in _data)
                //{
                //    if (item.Name == "收入")
                //    {
                //        comboBoxCategory.Items.Clear();
                //        foreach (var cls in item.InOutClasses)
                //        {
                //            comboBoxCategory.Items.Add(cls);
                //            comboBoxCategory.DisplayMember = "Name";
                //        }
                //    }
                //}
                LoadDataForComboBoxCategory(false);
            }

        }

        private void radioButtonOut_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOut.Checked)
            {
                //comboBoxInOutClass.DataSource = _data.Find(p => p.Name == radioButtonOut.Text).InOutClasses;
                //comboBoxInOutClass.DisplayMember = "Name";
                //foreach (var item in _data)
                //{
                //    if (item.Name == radioButtonOut.Text)
                //    {
                //        comboBoxCategory.Items.Clear();
                //        foreach (var cls in item.InOutClasses)
                //        {
                //            comboBoxCategory.Items.Add(cls);
                //            comboBoxCategory.DisplayMember = "Name";

                //        }
                //    }
                //}
                LoadDataForComboBoxCategory(true);

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

            //var t = ((InOutClass)(comboBoxCategory.SelectedItem)).Items;
            //listBoxItem.Items.Clear();
            //foreach (var item in t)
            //{
            //    listBoxItem.Items.Add(item);
            //}
            // MessageBox.Show(comboBoxCategory.SelectedValue.ToString());



        }

        private void Form2_Load(object sender, EventArgs e)
        {

            #region 模拟数据
            //InOutType t1 = new InOutType();
            //t1.Name = "收入";
            //InOutClass t1c1 = new InOutClass();
            //t1c1.Name = "工资";
            //t1c1.Items = new List<string>() { "月工资", "奖金" };
            //InOutClass t1c2 = new InOutClass();
            //t1c2.Name = "兼职";
            //t1c2.Items = new List<string>() { "程序员", "设计师" };

            //t1.InOutClasses.Add(t1c1);
            //t1.InOutClasses.Add(t1c2);

            //InOutType t2 = new InOutType();
            //t2.Name = "支出";
            //InOutClass t2c1 = new InOutClass();
            //t2c1.Name = "餐饮";
            //t2c1.Items = new List<string>() { "外出吃饭", "购买食材" };
            //InOutClass t2c2 = new InOutClass();
            //t2c2.Name = "交通";
            //t2c2.Items = new List<string>() { "飞机", "火车" };

            //t2.InOutClasses.Add(t2c1);
            //t2.InOutClasses.Add(t2c2);

            //_data.Add(t1);
            //_data.Add(t2); 
            #endregion
            LoadDataForComboBoxCategory();
            if (_listId != 0)
            {
                string strSql = string.Format("Select L.*,I.ItemName,I.CategoryId,C.CategoryName,C.IsPayOut from List as L join Item as I on L.ItemId=I.Id join Category as C on I.CategoryId = C.Id where L.Id = {0}", _listId);
               
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = @"Data Source=HOME-JERRY\SQLEXPRESS2014;Initial Catalog=Financing;Integrated Security=False;User ID=sa;Password=qwe123!@#";
                    conn.Open();
                    SqlDataAdapter adp = new SqlDataAdapter(strSql, conn);
                    adp.Fill(dt);
                }
                if (dt.Rows.Count==1)
                {
                    bool isPayOut = Convert.ToBoolean(dt.Rows[0]["IsPayOut"]);
                    if (isPayOut)
                    {
                        radioButtonOut.Checked = true;

                    }
                    else
                    {
                        radioButtonIn.Checked = true;
                    }
                }

                comboBoxCategory.SelectedValue = Convert.ToInt32(dt.Rows[0]["CategoryId"]);
                LoadDataForListBoxItem(Convert.ToInt32(comboBoxCategory.SelectedValue));
                listBoxItem.SelectedValue = Convert.ToInt32(dt.Rows[0]["ItemId"]);
                dateTimePickerDate.Value = Convert.ToDateTime(dt.Rows[0]["TradeDate"]);
                numericUpDownMoney.Value = Convert.ToDecimal(dt.Rows[0]["Amount"]);
                textBoxSummary.Text = Convert.ToString(dt.Rows[0]["Explain"]);



            }
        }

        private void LoadDataForComboBoxCategory(bool isOut = false)
        {
            string strSql = string.Format("Select * from Category where IsPayOut=0");
            if (isOut)
            {
                strSql = string.Format("Select * from Category where IsPayOut=1");
            }
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = @"Data Source=HOME-JERRY\SQLEXPRESS2014;Initial Catalog=Financing;Integrated Security=False;User ID=sa;Password=qwe123!@#";
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter(strSql, conn);
                adp.Fill(dt);
            }
            comboBoxCategory.DataSource = dt;
            comboBoxCategory.ValueMember = "Id";
            comboBoxCategory.DisplayMember = "CategoryName";
            LoadDataForListBoxItem(Convert.ToInt32(comboBoxCategory.SelectedValue));
        }
        private void LoadDataForListBoxItem(int categoryId)
        {
            string strSql = string.Format("Select * from Item where CategoryId={0}", categoryId);

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = @"Data Source=HOME-JERRY\SQLEXPRESS2014;Initial Catalog=Financing;Integrated Security=False;User ID=sa;Password=qwe123!@#";
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter(strSql, conn);
                adp.Fill(dt);
            }
            listBoxItem.DataSource = dt;
            listBoxItem.ValueMember = "Id";
            listBoxItem.DisplayMember = "ItemName";
            
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void GenerateSummary()
        {
            StringBuilder strSummary = new StringBuilder();
            strSummary.Append("要保存的信息为：\r\n");
            strSummary.AppendFormat("{0}-{1}\r\n", comboBoxCategory.Text, listBoxItem.Text);
            strSummary.AppendFormat("日期：{0}\r\n", dateTimePickerDate.Text);
            strSummary.AppendFormat("说明：{0}\r\n", textBoxRemark.Text);
            string strPeople = "";
            foreach (string item in checkedListBox1.CheckedItems)
            {
                strPeople += item + ",";
            }
            strPeople = strPeople.TrimEnd(',');
            strSummary.AppendFormat("收支人：{0}\r\n", strPeople);
            strSummary.AppendFormat("金额：{0}\r\n", numericUpDownMoney.Value);
            
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
            if (_listId != 0)
            {
                int itemId = Convert.ToInt32(listBoxItem.SelectedValue);
                decimal amount = numericUpDownMoney.Value;
                DateTime dateTime = dateTimePickerDate.Value;
                string remarks = textBoxSummary.Text;
                string strSql = string.Format("update List Set ItemId = {0}, Amount = {1},TradeDate = '{2}', Explain ='{3}' Where Id = {4};", itemId, amount, dateTime, remarks, _listId);

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = @"Data Source=HOME-JERRY\SQLEXPRESS2014;Initial Catalog=Financing;Integrated Security=False;User ID=sa;Password=qwe123!@#";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(strSql, conn);
                    cmd.ExecuteNonQuery();
                    AddSuccess(_listId.ToString());
                }
            }
            else
            {
                int itemId = Convert.ToInt32(listBoxItem.SelectedValue);
                decimal amount = numericUpDownMoney.Value;
                DateTime dateTime = dateTimePickerDate.Value;
                string remarks = textBoxSummary.Text;
                string strSql = string.Format("insert into List Values({0},{1},'{2}','{3}');SELECT SCOPE_IDENTITY();", itemId, amount, dateTime, remarks);

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = @"Data Source=HOME-JERRY\SQLEXPRESS2014;Initial Catalog=Financing;Integrated Security=False;User ID=sa;Password=qwe123!@#";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(strSql, conn);
                    // cmd.ExecuteNonQuery();
                    int newId = Convert.ToInt32(cmd.ExecuteScalar());
                    AddSuccess(newId.ToString());
                }
                //MessageBox.Show("新增成功");                
            }
            this.Close();


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
            // Application.Exit();
        }

        private void comboBoxCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadDataForListBoxItem(Convert.ToInt32(comboBoxCategory.SelectedValue));
        }
    }
}
