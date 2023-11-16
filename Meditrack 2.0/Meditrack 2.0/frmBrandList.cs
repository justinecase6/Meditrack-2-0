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
    public partial class frmBrandList : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        public frmBrandList()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            LoadRecords();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                FrmBrand frm = new FrmBrand(this);
                frm.lblID.Text = dataGridView1[1, e.RowIndex].Value.ToString();
                frm.txtBrand.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                frm.ShowDialog();
            }
            else if (colName == "Delete")
            {
                {
                    if (MessageBox.Show("Are you sure you want to delete this brand?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string brandID = dataGridView1[1, e.RowIndex].Value.ToString();
                        string brandName = dataGridView1[2, e.RowIndex].Value.ToString();

                        tblDeletedbrands(brandID, brandName);

                        cn.Open();
                        cm = new SqlCommand("Delete from tblbrand where id like '" + dataGridView1[1, e.RowIndex].Value.ToString() + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Brand has been successfully deleted.", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRecords();
                    }
                }
            }
        }
                
            
         private void tblDeletedbrands(string brandID, string brandName) {
            {
                cn.Open();
                cm = new SqlCommand("INSERT INTO DeletedBrands (BrandID, BrandName, DeletedDateTime) VALUES (@BrandID, @BrandName, GETDATE())", cn);
                cm.Parameters.AddWithValue("@BrandID", brandID);
                cm.Parameters.AddWithValue("@BrandName", brandName);
                cm.ExecuteNonQuery();
                cn.Close();
            }
        }
    

        public void LoadRecords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select * from tblBrand order by brand", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dr["id"].ToString(), dr ["brand"].ToString());
            }
            dr.Close();
            cn.Close();
            
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FrmBrand frm = new FrmBrand(this);
            frm.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
