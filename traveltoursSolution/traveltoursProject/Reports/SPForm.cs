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
using traveltoursProject.Reports;

namespace traveltoursProject
{
    public partial class SPForm : Form
    {
        public SPForm()
        {
            InitializeComponent();
        }

        private void SPForm_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(CreateConnection.ConnectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TourePackages", con))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "TourePackages");
                   CrystalReport1 cr = new CrystalReport1();
                    cr.SetDataSource(ds);
                    this.crystalReportViewer1.ReportSource = cr;
                    this.crystalReportViewer1.Refresh();

                }
            }
        }
    }
}
