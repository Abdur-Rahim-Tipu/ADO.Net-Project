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

namespace traveltoursProject.Reports
{
    public partial class SubRep : Form
    {
        public SubRep()
        {
            InitializeComponent();
        }

        private void SubRep_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(CreateConnection.ConnectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TourePackages", con))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "TourePackages");

                    da.SelectCommand.CommandText = "SELECT * FROM PackageFeatures";
                    da.Fill(ds, "PackageFeatures");
                    da.SelectCommand.CommandText = "SELECT * FROM Travelagents";
                    da.Fill(ds, "Travelagents");
                    CrystalReport3 cr = new CrystalReport3();
                    cr.SetDataSource(ds);
                    this.crystalReportViewer1.ReportSource = cr;
                    this.crystalReportViewer1.Refresh();

                }
            }
        }
    }
}
