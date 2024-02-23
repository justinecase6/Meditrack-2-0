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
    public partial class FrmCategoryList : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        public FrmCategoryList()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadCategory()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblCategory order by category", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i,dr[0].ToString(),dr[1].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FrmCategory frm = new FrmCategory(this);
            frm.btnsave.Enabled = true;
            frm.btnUpdate.Enabled = false;
            frm.ShowDialog();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if(colName == "Edit")
            {
                FrmCategory frm = new FrmCategory(this);
                frm.txtCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.lblID.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.btnsave.Enabled = false;
                frm.btnUpdate.Enabled = true;
                frm.ShowDialog();  
            } else if(colName=="Delete")
            {
                if(MessageBox.Show("Are you sure you want to delete this category?","Delete Category",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("delete from tblcategory where id like '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery(); 
                    cn.Close();
                    MessageBox.Show("Record has been successfully deleted!");
                    LoadCategory();
                }
            }
        }

        public void LoadCategoryMeds()
        {
            int i = 0;
            dataGridView2.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblCategoryMeds order by category", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView2.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            cn.Close();
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView2.Columns[e.ColumnIndex].Name;
            if (colName == "colEdit")
            {
                frmCategoryMeds frm = new frmCategoryMeds(this);
                frm.txtCategory.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.lblID.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.btnsave.Enabled = false;
                frm.btnUpdate.Enabled = true;
                frm.ShowDialog();
            }
            else if (colName == "colDelete")
            {
                if (MessageBox.Show("Are you sure you want to delete this category?", "Delete Category", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("delete from tblcategoryMeds where id like '" + dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully deleted!");
                    LoadCategoryMeds();
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmCategoryMeds frm = new frmCategoryMeds(this);
            frm.btnsave.Enabled = true;
            frm.btnUpdate.Enabled = false;
            frm.ShowDialog();
            //this.Dispose();
        }
    }
}
