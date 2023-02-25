using CrystalDecisions.ReportAppServer;
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

namespace traveltoursProject.Reports
{
    public partial class FormWP : Form
    {
        public FormWP()
        {
            InitializeComponent();
        }

        private void FormWP_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(CreateConnection.ConnectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TouristR", con))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "TouristR");
                    ds.Tables["TouristR"].Columns.Add(new DataColumn("image", typeof(System.Byte[])));
                    for (var i = 0; i < ds.Tables["TouristR"].Rows.Count; i++)
                    {
                        ds.Tables["TouristR"].Rows[i]["image"] = File.ReadAllBytes(Path.Combine(Path.GetFullPath(@"..\..\Pictures"), ds.Tables["TouristR"].Rows[i]["Picture"].ToString()));
                    }
                    CrystalReport5 rpt = new CrystalReport5();
                    rpt.SetDataSource(ds);
                    this.crystalReportViewer1.ReportSource = rpt;
                    this.crystalReportViewer1.Refresh();
                }
            }
        }
    }
}
