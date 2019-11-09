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
    public partial class Form1 : Form
    {
        string conn_str = @"Data Source=.\SQLEXPRESS; initial catalog=Bank; Integrated Security=True;";
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                dgv.DataSource = null;
                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(conn_str))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = "SELECT * FROM klienti";
                    comm.Connection = conn;
                    SqlDataReader dr = comm.ExecuteReader();
                    dt.Load(dr);
                }

                dgv.DataSource = dt;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddClient add = new AddClient();
            add.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Jer airchiet klienti");
            }
            else if(dgv.SelectedRows.Count > 1)
            {
                MessageBox.Show("Airchiet mxolod erti klienti!");
            }
            else
            {
                DataGridViewRow row = dgv.SelectedRows[0];
                createSesxi sesxi = new createSesxi((int)(row.Cells["id"].Value), row.Cells["saxeli"].Value.ToString(), row.Cells["gvari"].Value.ToString());
                sesxi.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Jer airchiet klienti");
            }
            else if (dgv.SelectedRows.Count > 1)
            {
                MessageBox.Show("Airchiet mxolod erti klienti!");
            }
            else
            {
                int count = 0;
                DataGridViewRow row = dgv.SelectedRows[0];
                try
                {
                    DataTable dt = new DataTable();

                    using (SqlConnection conn = new SqlConnection(conn_str))
                    {
                        conn.Open();
                        SqlCommand comm = new SqlCommand();
                        comm.CommandText = "SELECT id FROM anabari WHERE klienti_id = @id";
                        comm.Parameters.AddWithValue("@id", (int)row.Cells["id"].Value);
                        comm.Connection = conn;
                        SqlDataReader dr = comm.ExecuteReader();

                        
                        while (dr.Read())
                        {
                            count++;
                        }

                    }
                    

                    
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                if (count == 0)
                {
                    CreateAnabari anabari = new CreateAnabari((int)(row.Cells["id"].Value), row.Cells["saxeli"].Value.ToString(), row.Cells["gvari"].Value.ToString());
                    anabari.Show();
                }
                else
                {
                    MessageBox.Show("Am klients ukve gaxsnili aqvs anabari");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Search s = new Search();
            s.Show();
        }
    }
}
