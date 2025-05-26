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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace InventoryManagementSystem
{
    public partial class CustomerForm : Form
    {
        SqlConnection con = new SqlConnection("data source=. ; initial catalog = dbIMS ; integrated security = true");
        //new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pc\Documents\dbIMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public CustomerForm()
        {
            InitializeComponent();
            LoadCustomer();
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string CcolName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (CcolName == "Edit")
            {
                CustomerModuleForm customerModule = new CustomerModuleForm();
                customerModule.txtCname.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                customerModule.txtCphone.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();

                customerModule.btnSave.Enabled = false;
                customerModule.btnUpdate.Enabled = true;
                customerModule.btnClear.Enabled = false;

                customerModule.ShowDialog();

                // تحقق إذا تم الضغط على زر Update
                if (customerModule.IsUpdateClicked)
                {
                    string cid = dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
                    cm = new SqlCommand("UPDATE tbCustomer SET cname = @cname, cphone = @cphone WHERE cid = @cid", con);
                    cm.Parameters.AddWithValue("@cname", customerModule.txtCname.Text);
                    cm.Parameters.AddWithValue("@cphone", customerModule.txtCphone.Text);
                    cm.Parameters.AddWithValue("@cid", cid);

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Customer has been UPDATED successfully!!");
                    customerModule.clear();
                }
            }

            else if (CcolName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this user?", "Delete Record ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbCustomer WHERE cid LIKE '" + dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been deleted successfully!");
                }
            }
            LoadCustomer();
        }
        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbCustomer", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CustomerModuleForm moduleform = new CustomerModuleForm();
            moduleform.btnSave.Enabled = true;
            moduleform.btnUpdate.Enabled = false;
            moduleform.ShowDialog();
            LoadCustomer();
        }
    }
}
