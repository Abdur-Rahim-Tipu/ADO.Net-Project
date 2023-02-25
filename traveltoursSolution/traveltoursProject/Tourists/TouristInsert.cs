using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace traveltoursProject.Tourists
{
    public partial class TouristInsert : Form
    {
        string pictureName = "";
        public TouristInsert()
        {
            InitializeComponent();
        }
        public TouristView FormFeature { get; set; }
        public int TouristId { get; set; }
        private void TouristInsert_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = GetTouristId().ToString();
            LoadComboBox();
        }
        private int GetTouristId()
        {
            using (SqlConnection con = new SqlConnection(CreateConnection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(Touristid), 0) FROM Tourists ", con))
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
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM PackageFeatures", con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    this.comboBox1.DataSource = dt;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                this.pictureName = this.openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(CreateConnection.ConnectionString))
            {
                con.Open();
                using (SqlTransaction tran = con.BeginTransaction())
                {

                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO Tourists 
                                            (TouristId,TouristName,TouristOcupation,Picture,FeatureId) VALUES
                                            (@i, @n, @o, @pic, @fi)", con, tran))
                    {
                        cmd.Parameters.AddWithValue("@i", int.Parse(textBox1.Text));
                        cmd.Parameters.AddWithValue("@n", textBox2.Text);
                        cmd.Parameters.AddWithValue("@o", textBox3.Text);
                        cmd.Parameters.AddWithValue("@fi", comboBox1.SelectedValue);
                        string ext = Path.GetExtension(this.pictureName);
                        string fileName = $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}{ext}";
                        string savePath = Path.Combine(Path.GetFullPath(@"..\..\Pictures"), fileName);
                        File.Copy(pictureName, savePath, true);
                        cmd.Parameters.AddWithValue("@pic", fileName);

                        try
                        {
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Data Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                tran.Commit();
                                this.textBox1.Clear();
                                this.textBox2.Clear();
                                this.textBox3.Clear();
                               
                                pictureBox1.Image = null;
                                pictureName = "";
                                con.Close();
                                this.textBox1.Text = GetTouristId().ToString();
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TouristInsert_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.FormFeature.LoadData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
