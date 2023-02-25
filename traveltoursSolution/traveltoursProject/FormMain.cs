using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using traveltoursProject.Reports;
using traveltoursProject.TourePackage;
using traveltoursProject.Tourists;
using traveltoursProject.Travelagent;

namespace traveltoursProject
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TravelagentView { MdiParent= this }.Show();
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TravelagentInsert { MdiParent = this }.Show();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TravelagentUpdate { MdiParent = this }.Show();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TravelagentDelete { MdiParent = this }.Show();
        }

        private void viewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new TourePackageView { MdiParent = this }.Show();
        }

        private void insertToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new TourePackageInsert { MdiParent = this }.Show();
        }

        private void updateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new TourePackageUpdate { MdiParent = this }.Show();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new TourePackageDelete { MdiParent = this }.Show();
        }

        private void touristsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TouristView { MdiParent = this }.Show();
        }

        private void singleReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SPForm { MdiParent = this }.Show();
        }

        private void groupReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new GPForm { MdiParent = this }.Show();
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void subReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SubRep { MdiParent = this}.Show();
        }

        private void reportWithPicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormWP { MdiParent = this }.Show();
        }
    }
}
