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

namespace traveltoursProject.Travelagent
{
    public partial class TravelagentView : Form
    {
        public TravelagentView()
        {
            InitializeComponent();
        }

        private void TravelagentView_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(CreateConnection.ConnectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Travelagents", con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    this.dataGridView1.DataSource = dt;

                }
            }
        }
    }
}
