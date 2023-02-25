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

namespace traveltoursProject.TourePackage
{
    public partial class TourePackageUpdate : Form
    {
        public TourePackageUpdate()
        {
            InitializeComponent();
        }

        private void TourePackageUpdate_Load(object sender, EventArgs e)
        {
            LoadComboBox();
        }
        private void LoadComboBox()
        {
            using (SqlConnection con = new SqlConnection(CreateConnection.ConnectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TourePackages ", con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    this.comboBox1.DataSource = dt;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(CreateConnection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM TourePackages  WHERE PackageId=@i", con))
                {
                    cmd.Parameters.AddWithValue("@i", comboBox1.SelectedValue);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        textBox2.Text = dr.GetString(1);
                        textBox3.Text = dr.GetString(2);
                        textBox4.Text = dr.GetDecimal(3).ToString("0.00");
                        checkBox1.Checked = dr.GetBoolean(4);
                        dateTimePicker1.Value = dr.GetDateTime(5);
                    }
                    con.Close();
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
                    using (SqlCommand cmd = new SqlCommand(@"UPDATE TourePackages  
                                            SET PackageName=@n , PackageCatagory=@pc, CostPer=@c, AvailablePackage=@a, ToureTime= @tt WHERE PackageId =@i", con, tran))
                    {
                        cmd.Parameters.AddWithValue("@i", comboBox1.SelectedValue);
                        cmd.Parameters.AddWithValue("@n", textBox2.Text);
                        cmd.Parameters.AddWithValue("@pc", textBox3.Text);
                        cmd.Parameters.AddWithValue("@c", textBox4.Text);
                        cmd.Parameters.AddWithValue("@a", checkBox1.Checked);
                        cmd.Parameters.AddWithValue("@tt", dateTimePicker1.Value);

                        try
                        {
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Data Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                tran.Commit();
                                this.textBox2.Clear();
                                this.textBox3.Clear();
                                this.textBox4.Clear();
                                this.checkBox1.Checked=false;
                                this.dateTimePicker1.Value=DateTime.Now;
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
                        con.Close();
                    }
                }
            }
        }
    }
}
