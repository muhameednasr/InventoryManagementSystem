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
    public partial class CustomerModuleForm : Form
    {
        SqlConnection con = new SqlConnection("data source=. ; initial catalog = dbIMS ; integrated security = true");
        SqlCommand cm = new SqlCommand();

        public CustomerModuleForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are yo sure you want to save this Customer?", "saving record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbCustomer(cname,cphone)VALUES(@cname,@cphone)", con);
                    cm.Parameters.AddWithValue("@cname", txtCname.Text);
                    cm.Parameters.AddWithValue("@cphone", txtCphone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Customer has been saved successfully.");
                    clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void clear()
        {
            txtCname.Clear();
            txtCphone.Clear();
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void CustomerModuleForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

        private void txtCname_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCphone_TextChanged(object sender, EventArgs e)
        {

        }

        public bool IsUpdateClicked = false;

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            IsUpdateClicked = true;
            this.Close();
        }

    }
}
