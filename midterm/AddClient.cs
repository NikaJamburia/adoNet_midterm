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
    public partial class AddClient : Form
    {
        string conn_str = @"Data Source=.\SQLEXPRESS; initial catalog=Bank; Integrated Security=True;";
        public AddClient()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(nameBox.Text) || string.IsNullOrEmpty(lastNameBox.Text ) || string.IsNullOrEmpty(idnBox.Text))
                {
                    throw new Exception("Sheavset yvela veli!!!");
                }

                DateTime date = DateTime.Today;
                string sqlDate = date.ToString("yyyy-MM-dd");

                string query = "INSERT INTO klienti(saxeli, gvari, piradi_nomeri, registraciis_tarigi) VALUES(@firstname, @lastname, @idn, @date)";
                using (SqlConnection conn = new SqlConnection(conn_str))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand(query, conn);
                    comm.Parameters.AddWithValue("@firstname", nameBox.Text);
                    comm.Parameters.AddWithValue("@lastname", lastNameBox.Text);
                    comm.Parameters.AddWithValue("@idn", idnBox.Text);
                    comm.Parameters.AddWithValue("@date", sqlDate);

                    comm.ExecuteNonQuery();
                }

                MessageBox.Show("Client added!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                nameBox.Text = "";
                lastNameBox.Text = "";
                idnBox.Text = "";
            }
        }
    }
}
