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
namespace Meditrack_2._0
{
    public partial class FrmBrand : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        frmBrandList frmlist;
        public FrmBrand(frmBrandList flist)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            frmlist = flist;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FrmBrand_Load(object sender, EventArgs e)
        {

        }
        private void Clear()
        {
            btnsave.Enabled = true;
            btnUpdate.Enabled = false;
            txtBrand.Clear();
            txtBrand.Focus();

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBrand.Text))
                {
                    MessageBox.Show("Please fill out the brand field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cn.Open();
                cm = new SqlCommand("SELECT COUNT(*) FROM tblbrand WHERE Brand = @brand", cn);
                cm.Parameters.AddWithValue("@brand", txtBrand.Text);
                int count = Convert.ToInt32(cm.ExecuteScalar());
                cn.Close();

                // Check if the brand already exists
                if (count > 0)
                {
                    MessageBox.Show("Brand with the same name already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to save this brand?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tblbrand(Brand) VALUES (@brand)", cn);
                        cm.Parameters.AddWithValue("@brand", txtBrand.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Record has been successfully saved.");
                        Clear();
                        frmlist.LoadRecords();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBrand.Text))
                {
                    MessageBox.Show("Please fill out the brand field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; 
                }
                if (MessageBox.Show("Are you sure you want to update this brand?","Update Record",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("update tblbrand set brand = @brand where id like '" + lblID.Text + "'", cn);
                    cm.Parameters.AddWithValue("@brand", txtBrand.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Brand has been successfully updated.");
                    Clear();
                    frmlist.LoadRecords();
                    this.Dispose();
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btncancel_Click(object sender, EventArgs e)
        {
            txtBrand.Text = string.Empty;
        }
    }
}
