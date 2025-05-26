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
    public partial class UserModuleForm : Form
    {
        SqlConnection con = new SqlConnection("data source=. ; initial catalog = dbIMS ; integrated security = true");
        SqlCommand cm = new SqlCommand();
        public UserModuleForm()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtPass.Text != txtRepass.Text)
                {
                    MessageBox.Show("Password is not matching!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if(MessageBox.Show("Are yo sure you want to save this user?","saving record",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbUser(username,fullname,password,phone)VALUES(@username,@fullname,@password,@phone)", con);
                    cm.Parameters.AddWithValue("@username",TxtUserName.Text);
                    cm.Parameters.AddWithValue("@fullname", TxtFullName.Text);
                    cm.Parameters.AddWithValue("@password", TxtPass.Text);
                    cm.Parameters.AddWithValue("@phone", TxtPhone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User has been saved successfully.");
                    clear();
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        public void clear()
        {
            TxtUserName.Clear();
            TxtFullName.Clear();
            TxtPass.Clear();
            txtRepass.Clear();
            TxtPhone.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtPass.Text != txtRepass.Text)
                {
                    MessageBox.Show("Password is not matching!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are yo sure you want to Update this user?", " Updatingrecord", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE  tbUser SET fullname=@fullname,password=@password,phone=@phone WHERE username LIKE '"+TxtUserName.Text+"'", con);
                    
                    cm.Parameters.AddWithValue("@fullname", TxtFullName.Text);
                    cm.Parameters.AddWithValue("@password", TxtPass.Text);
                    cm.Parameters.AddWithValue("@phone", TxtPhone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User has been Updated successfully.");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void UserModuleForm_Load(object sender, EventArgs e)
        {

        }
    }
}
