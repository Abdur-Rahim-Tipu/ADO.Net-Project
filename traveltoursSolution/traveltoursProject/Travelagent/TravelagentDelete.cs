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

namespace traveltoursProject.Travelagent
{
    public partial class TravelagentDelete : Form
    {
        public TravelagentDelete()
        {
            InitializeComponent();
        }

        private void TravelagentDelete_Load(object sender, EventArgs e)
        {
            LoadComboBox();
        }
        private void LoadComboBox()
        {
            using (SqlConnection con = new SqlConnection(CreateConnection.ConnectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Travelagents", con))
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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Travelagents WHERE AgentId=@i", con))
                {
                    cmd.Parameters.AddWithValue("@i", comboBox1.SelectedValue);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        textBox2.Text = dr.GetString(1);
                    }
                    con.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are your sure to delete?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(CreateConnection.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE Travelagents WHERE AgentId=@i", con))
                    {
                        cmd.Parameters.AddWithValue("@i", comboBox1.SelectedValue);
                        con.Open();
                        try
                        {
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadComboBox();
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Data delete failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            if (con.State == ConnectionState.Open)
                            {
                                con.Open();
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
