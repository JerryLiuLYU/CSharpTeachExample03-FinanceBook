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
    public partial class AdoTest : Form
    {
        public AdoTest()
        {
            InitializeComponent();
        }

        private void AdoTest_Load(object sender, EventArgs e)
        {
            LoadDataForComboBoxCategory();

        }

        private void radioButtonIn_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonIn.Checked)
            {
                LoadDataForComboBoxCategory(false);
            }
            else
            {
                LoadDataForComboBoxCategory(true);
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
            LoadDataForComboBoxItem(Convert.ToInt32(comboBoxCategory.SelectedValue));
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            // MessageBox.Show(comboBoxCategory.SelectedValue.ToString());
        }

        private void comboBoxCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // MessageBox.Show(comboBoxCategory.SelectedValue.ToString());
            LoadDataForComboBoxItem( Convert.ToInt32(comboBoxCategory.SelectedValue));
        }

        private void LoadDataForComboBoxItem(int categoryId)
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
            comboBoxItem.DataSource = dt;
            comboBoxItem.ValueMember = "Id";
            comboBoxItem.DisplayMember = "ItemName";
            LoadDataForGridView(Convert.ToInt32(comboBoxItem.SelectedValue));
        }
        private void LoadDataForGridView(int itemId)
        {
            string strSql = string.Format("Select * from List where ItemId={0}", itemId);

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = @"Data Source=HOME-JERRY\SQLEXPRESS2014;Initial Catalog=Financing;Integrated Security=False;User ID=sa;Password=qwe123!@#";
                conn.Open();
                SqlDataAdapter adp = new SqlDataAdapter(strSql, conn);
                adp.Fill(dt);
            }
            dataGridViewList.DataSource = dt;            
        }

        private void comboBoxItem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadDataForGridView(Convert.ToInt32(comboBoxItem.SelectedValue));
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.AddSuccess += On_AddSuccess;
            frm.ShowDialog();
        }
        private void On_UpdateSuccess(string newId)
        {
            MessageBox.Show("更新成功" + newId);
            LoadDataForGridView(Convert.ToInt32(comboBoxItem.SelectedValue));
        }
        private void On_AddSuccess(string newId)
        {
            MessageBox.Show("新增成功"+ newId);
            LoadDataForGridView(Convert.ToInt32(comboBoxItem.SelectedValue));
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count == 1)
            {
                int listId = Convert.ToInt32(dataGridViewList.SelectedRows[0].Cells["Id"].Value);

                string strSql = string.Format("delete from List Where id={0}", listId);

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = @"Data Source=HOME-JERRY\SQLEXPRESS2014;Initial Catalog=Financing;Integrated Security=False;User ID=sa;Password=qwe123!@#";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(strSql, conn);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("删除成功");
                LoadDataForGridView(Convert.ToInt32(comboBoxItem.SelectedValue));
            }
            else
            {
                MessageBox.Show("请首先选择需要删除的行！");
            }
            
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count == 1)
            {
                int listId = Convert.ToInt32(dataGridViewList.SelectedRows[0].Cells["Id"].Value);
                Form2 frm = new Form2(listId);
                frm.AddSuccess += On_UpdateSuccess;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("请首先选择需要更新的行！");
            }
        }
    }
}
