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

namespace traveltoursProject.Tourists
{
    public partial class TouristView : Form
    {
        public TouristView()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        DataSet ds = new DataSet();
        BindingSource BSPF = new BindingSource();
        BindingSource DST = new BindingSource();

        private void TouristView_Load(object sender, EventArgs e)
        {
            LoadData();
            AddRelations();
            BindControls();
        }

        private void BindControls()
        {
            BSPF.DataSource = ds;
            BSPF.DataMember = "PackageFeatures";
            DST.DataSource = BSPF;
            DST.DataMember = "FK_T_PF";
            lblid.DataBindings.Add(new Binding("Text", BSPF, "FeatureId"));
            lblHB.DataBindings.Add(new Binding("Text", BSPF, "HotelBooking"));
            lblpck.DataBindings.Add(new Binding("Text", BSPF, "PackageId"));
            this.dataGridView1.DataSource = DST;
        }

        public void LoadData()
        {

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables["PackageFeatures"].Rows.Count > 0)
                {
                    ds.Tables["PackageFeatures"].Rows.Clear();
                }
                if (ds.Tables["Tourists"].Rows.Count > 0)
                {
                    ds.Tables["Tourists"].Rows.Clear();
                }
            }
            using (SqlConnection con = new SqlConnection(CreateConnection.ConnectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(@"SELECT *
                                                                FROM PackageFeatures", con))
                {

                    da.Fill(ds, "PackageFeatures");
                    da.SelectCommand.CommandText = @"SELECT *
                                                    FROM Tourists";
                    da.Fill(ds, "Tourists");
                    ds.Tables["Tourists"].Columns.Add(new DataColumn("image", typeof(System.Byte[])));
                    for (var i = 0; i < ds.Tables["Tourists"].Rows.Count; i++)
                    {
                        ds.Tables["Tourists"].Rows[i]["image"] = File.ReadAllBytes(Path.Combine(Path.GetFullPath(@"..\..\Pictures"), ds.Tables["Tourists"].Rows[i]["Picture"].ToString()));
                    }
                    

                }
            }
        }

        private void AddRelations()
        {
            ds.Tables["PackageFeatures"].PrimaryKey = new DataColumn[] { ds.Tables["PackageFeatures"].Columns["FeatureId"] };
            DataRelation rel = new DataRelation("FK_T_PF",
                ds.Tables["PackageFeatures"].Columns["FeatureId"],
                ds.Tables["Tourists"].Columns["TouristId"]);
            ds.Relations.Add(rel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BSPF.MoveFirst();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BSPF.MoveLast();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (BSPF.Position > 0)
            {
                BSPF.MovePrevious();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (BSPF.Position < BSPF.Count - 1)
            {
                BSPF.MoveNext();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new FeatureInsert { FormFeature = this }.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new TouristInsert { FormFeature = this }.ShowDialog();
        }
    }
}
