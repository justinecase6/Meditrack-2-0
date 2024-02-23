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
    public partial class frmQty : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        private String pcode;
        private double price;
        private String transno;
        //string stitle = "MediTrack";
        frmPOS fpos;
        public frmQty(frmPOS frmpos)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            fpos = frmpos;
        }

        private void frmQty_Load(object sender, EventArgs e)
        {

        }

        public void ProductDetails(String pcode, double price, String transno)
        {
            this.pcode = pcode;
            this.price = price;
            this.transno = transno;
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) && (txtQty.Text != String.Empty))
            {
                cn.Open();
                cm = new SqlCommand("insert into tblCart (transno, pcode, price, qty, sdate)values(@transno, @pcode, @price, @qty, @sdate)", cn);
                cm.Parameters.AddWithValue("@transno", transno);
                cm.Parameters.AddWithValue("@pcode", pcode);
                cm.Parameters.AddWithValue("@price", price);
                cm.Parameters.AddWithValue("qty", int.Parse(txtQty.Text));
                cm.Parameters.AddWithValue("sdate", DateTime.Now);
                cm.ExecuteNonQuery();
                cn.Close();

                fpos.txtSearch.Clear();
                fpos.txtSearch.Focus();
                fpos.LoadCart();
                this.Dispose();
            }
        }
    }
}
