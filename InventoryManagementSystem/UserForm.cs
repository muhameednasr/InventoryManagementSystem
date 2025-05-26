using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace InventoryManagementSystem
{
    public partial class UserForm : Form
    {
        SqlConnection con = new SqlConnection("data source=. ; initial catalog = dbIMS ; integrated security = true");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public UserForm()
        {
            InitializeComponent();
            LoadUser();
        }
        public void LoadUser()
        {
            int i = 0;
            DGVUser.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbUser", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                DGVUser.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void customerButtons1_Click(object sender, EventArgs e)
        {
            UserModuleForm userModule = new UserModuleForm();
            userModule.btnSave.Enabled = true;
            userModule.btnUpdate.Enabled = false;
            userModule.ShowDialog();
            LoadUser();

        }

        private void DGVUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = DGVUser.Columns[e.ColumnIndex].Name;
            if (colName =="Edit")
            {
                UserModuleForm userModule = new UserModuleForm();
                userModule.TxtUserName.Text = DGVUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                userModule.TxtFullName.Text = DGVUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                userModule.TxtPass.Text = DGVUser.Rows[e.RowIndex].Cells[3].Value.ToString();
                userModule.TxtPhone.Text = DGVUser.Rows[e.RowIndex].Cells[4].Value.ToString();

                userModule.btnSave.Enabled=false;
                userModule.btnUpdate.Enabled=true;
                userModule.TxtUserName.Enabled = false;
                userModule.ShowDialog();

            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this user?","Delete Record ", MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    con.Open();
                    cm= new SqlCommand("DELETE FROM tbUser WHERE username LIKE '" + DGVUser.Rows[e.RowIndex].Cells[1].Value.ToString()+"'",con); 
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been deleted successfully!");
                }
            }
            LoadUser();
        }
    }
}
