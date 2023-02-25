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
    public partial class TourePackageView : Form
    {
        public TourePackageView()
        {
            InitializeComponent();
        }

        private void TourePackageView_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(CreateConnection.ConnectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TourePackages", con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    this.dataGridView1.DataSource = dt;

                }
            }
        }
    }
}
