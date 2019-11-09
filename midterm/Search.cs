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
    public partial class Search : Form
    {
        string conn_str = @"Data Source=.\SQLEXPRESS; initial catalog=Bank; Integrated Security=True;";
        public Search()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dgvExtended.DataSource = null;
                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(conn_str))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = "SELECT dbo.klienti.*, dbo.sesxi.gacemis_tarigi, dbo.sesxi.odenoba, dbo.sesxi.procenti, dbo.sesxi.vali FROM dbo.sesxi INNER JOIN dbo.klienti ON dbo.sesxi.klienti_id = dbo.klienti.id";
                    comm.Connection = conn;
                    SqlDataReader dr = comm.ExecuteReader();
                    dt.Load(dr);
                }

                dgvExtended.DataSource = dt;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dgvExtended.DataSource = null;
                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(conn_str))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = "SELECT dbo.klienti.saxeli, dbo.klienti.gvari, dbo.anabari.tarigi AS gaxsnis_tarigi, dbo.anabari.odenoba, dbo.anabari.procenti FROM dbo.anabari INNER JOIN dbo.klienti ON dbo.anabari.klienti_id = dbo.klienti.id";
                    comm.Connection = conn;
                    SqlDataReader dr = comm.ExecuteReader();
                    dt.Load(dr);
                }

                dgvExtended.DataSource = dt;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime date_from = dateFrom.Value;
            DateTime date_to = dateTo.Value;

            string date_from_sql = date_from.ToString("yyyy-MM-dd");
            string date_to_sql = date_to.ToString("yyyy-MM-dd");

            try
            {
                dgvExtended.DataSource = null;
                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(conn_str))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = "SELECT * FROM klienti WHERE registraciis_tarigi > @datefrom AND registraciis_tarigi < @dateto";
                    comm.Parameters.AddWithValue("@datefrom", date_from_sql);
                    comm.Parameters.AddWithValue("@dateto", date_to_sql);
                    comm.Connection = conn;
                    SqlDataReader dr = comm.ExecuteReader();
                    dt.Load(dr);
                }

                dgvExtended.DataSource = dt;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(minVali.Text))
                {
                    throw new Exception("Sheiyvanet minimaluri davalianeba!!!");
                }

                dgvExtended.DataSource = null;
                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(conn_str))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = "SELECT dbo.klienti.*, dbo.sesxi.gacemis_tarigi, dbo.sesxi.odenoba, dbo.sesxi.procenti, dbo.sesxi.vali FROM dbo.sesxi INNER JOIN dbo.klienti ON dbo.sesxi.klienti_id = dbo.klienti.id WHERE dbo.sesxi.vali > @min ORDER BY dbo.sesxi.vali DESC";
                    comm.Parameters.AddWithValue("@min", minVali.Text);
                    comm.Connection = conn;
                    SqlDataReader dr = comm.ExecuteReader();
                    dt.Load(dr);
                }

                dgvExtended.DataSource = dt;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                dgvExtended.DataSource = null;
                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(conn_str))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = "SELECT dbo.klienti.saxeli, dbo.klienti.gvari, dbo.klienti.piradi_nomeri, dbo.klienti.registraciis_tarigi, dbo.anabari.tarigi AS gaxsnis_tarigi, dbo.anabari.odenoba AS tavdapirveli_tanxa, dbo.anabari.procenti, dbo.anabari.odenoba + DATEDIFF(month, dbo.klienti.registraciis_tarigi, CONVERT(date, GETDATE())) * (dbo.anabari.odenoba * dbo.anabari.procenti / 100) AS shegrovebuli_tanxa FROM dbo.anabari INNER JOIN dbo.klienti ON dbo.anabari.klienti_id = dbo.klienti.id";
                    comm.Connection = conn;
                    SqlDataReader dr = comm.ExecuteReader();
                    dt.Load(dr);
                }

                dgvExtended.DataSource = dt;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
