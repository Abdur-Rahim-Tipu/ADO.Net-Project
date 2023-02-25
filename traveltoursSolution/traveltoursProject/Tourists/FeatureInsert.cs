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

namespace traveltoursProject.Tourists
{
    public partial class FeatureInsert : Form
    {
        public FeatureInsert()
        {
            InitializeComponent();
        }
        public TouristView FormFeature { get; set; }
        private void FeatureInsert_Load(object sender, EventArgs e)
        {
            this.textBox2.Text = GetOrderId().ToString();
            LoadComboBox();
        }
        private int GetOrderId()
        {
            using (SqlConnection con = new SqlConnection(CreateConnection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(FeatureId), 0) FROM PackageFeatures", con))
                {
                    con.Open();
                    int id = (int)cmd.ExecuteScalar();
                    con.Close();
                    return id + 1;
                }
            }
        }
        private void LoadComboBox()
        {
            using (SqlConnection con = new SqlConnection(CreateConnection.ConnectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TourePackages", con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    this.comboBox1.DataSource = dt;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(CreateConnection.ConnectionString))
            {
                con.Open();
                using (SqlTransaction tran = con.BeginTransaction())
                {

                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO PackageFeatures 
                                            (FeatureId, HotelBooking, PackageId ) VALUES
                                            (@fi, @hb, @pi)", con, tran))
                    {
                        cmd.Parameters.AddWithValue("@fi", int.Parse(textBox2.Text));
                        cmd.Parameters.AddWithValue("@hb", textBox1.Text);
                        cmd.Parameters.AddWithValue("@pi", comboBox1.SelectedValue);





                        try
                        {
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Data Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                tran.Commit();
                                this.textBox1.Clear();
                                this.textBox2.Clear();
                                con.Close();
                                this.textBox2.Text = GetOrderId().ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tran.Rollback();
                        }
                        finally
                        {
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }
                        }

                    }
                }

            }
        }

        private void FeatureInsert_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.FormFeature.LoadData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

