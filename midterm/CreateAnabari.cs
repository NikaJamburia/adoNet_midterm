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

namespace midterm
{
    public partial class CreateAnabari : Form
    {
        string conn_str = @"Data Source=.\SQLEXPRESS; initial catalog=Bank; Integrated Security=True;";

        private int k_id;
        private string k_name;
        private string k_lastname;

        public CreateAnabari(int k_id, string k_name, string k_lastname)
        {
            this.k_id = k_id;
            this.k_name = k_name;
            this.k_lastname = k_lastname;
            InitializeComponent();

            clientBox.ReadOnly = true;
            clientBox.Text = this.k_name + " " + this.k_lastname;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(percentBox.Text))
                {
                    throw new Exception("Sheavset yvela veli!!!");
                }

                DateTime date = DateTime.Today;
                string sqlDate = date.ToString("yyyy-MM-dd");

                string query = "INSERT INTO anabari(klienti_id, tarigi, odenoba, procenti) VALUES(@k_id, @date, @odenoba, @percent)";
                using (SqlConnection conn = new SqlConnection(conn_str))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand(query, conn);
                    comm.Parameters.AddWithValue("@k_id", k_id);
                    comm.Parameters.AddWithValue("@date", sqlDate);
                    comm.Parameters.AddWithValue("@odenoba", tanxaBox.Text);
                    comm.Parameters.AddWithValue("@percent", percentBox.Text);

                    comm.ExecuteNonQuery();
                }

                MessageBox.Show("Anabari warmatebit gaixsna!", "warmateba!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
