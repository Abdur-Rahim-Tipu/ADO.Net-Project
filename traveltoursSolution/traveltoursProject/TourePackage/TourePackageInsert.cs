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
    public partial class TourePackageInsert : Form
    {
        public TourePackageInsert()
        {
            InitializeComponent();
        }

        private void TourePackageInsert_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = GetPackegeID().ToString();
        }
        private object GetPackegeID()
        {
            using (SqlConnection con = new SqlConnection(CreateConnection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(PackageId), 0) FROM TourePackages", con))
                {
                    con.Open();
                    int id = (int)cmd.ExecuteScalar();
                    con.Close();
                    return id + 1;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(CreateConnection.ConnectionString))
            {
                con.Open();
                using (SqlTransaction tran = con.BeginTransaction())
                {

                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO TourePackages 
                                            (PackageId, PackageName, PackageCatagory, CostPer, AvailablePackage, ToureTime) VALUES
                                            (@i, @n, @pc, @c, @a, @tt)", con, tran))
                    {
                        cmd.Parameters.AddWithValue("@i", int.Parse(textBox1.Text));
                        cmd.Parameters.AddWithValue("@n", textBox2.Text);
                        cmd.Parameters.AddWithValue("@pc", textBox3.Text);
                        cmd.Parameters.AddWithValue("@c", textBox4.Text);
                        cmd.Parameters.AddWithValue("@a", checkBox1.Checked=true);
                        cmd.Parameters.AddWithValue("@tt", dateTimePicker1.Value);
                        try
                        {
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Data Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                tran.Commit();
                                this.textBox1.Clear();
                                this.textBox2.Clear();
                                this.textBox3.Clear();
                                this.textBox4.Clear();
                                this.checkBox1.Checked=false;
                                this.dateTimePicker1.Value=DateTime.Now;
                                this.textBox1.Text = GetPackegeID().ToString();
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
