using DVLD_BusinessLayer.Drivers;
using Project_DVLD_.Licenses;
using Project_DVLD_.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_
{
    public partial class frmDriversList : Form
    {
        
        public frmDriversList()
        {
            InitializeComponent();
            _RefreshDriversTable();
        }

        DataTable dtDrivers = clsDrivers.GetDriversList();

        private void frmDriversList_Load(object sender, EventArgs e)
        {

        }

        private void _RefreshDriversTable()
        {
            dgvDriversList.DataSource = dtDrivers;
            lbRecords.Text = dgvDriversList.Rows.Count.ToString() + " Driver(s)";
            dgvDriversList.Columns[0].Width = 100;
            dgvDriversList.Columns[1].Width = 100;
            dgvDriversList.Columns[2].Width = 150;
            dgvDriversList.Columns[3].Width = 300;
            dgvDriversList.Columns[4].Width = 180;
            dgvDriversList.Columns[5].Width = 100;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0)
                txtSearch.Visible = false;
            else
                txtSearch.Visible = true;
        }

        private void Filter()
        {
            string Filter = "";

            switch (cbFilterBy.Text)
            {
                case "None":
                    Filter = "";
                    break;

                case "Driver ID":
                    Filter = "DriverID";
                    break;
                case "Person ID":
                    Filter = "PersonID";
                    break;
                case "National No.":
                    Filter = "NationalNo";
                    break;
                case "Full Name":
                    Filter = "FullName";
                    break;
            }

            if(Filter == "" || txtSearch.Text == "")
            {
                dtDrivers.DefaultView.RowFilter = "";
                lbRecords.Text = dgvDriversList.Rows.Count.ToString() + " Driver(s)";
            }
            else if (Filter == "DriverID" || Filter == "PersonID")
            {
                dtDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", Filter, txtSearch.Text);
                lbRecords.Text = dgvDriversList.Rows.Count.ToString() + " Driver(s)";
            }
            else
            {
                dtDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", Filter, txtSearch.Text);
                lbRecords.Text = dgvDriversList.Rows.Count.ToString() + " Driver(s)";
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowPersonDetails((int)dgvDriversList.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            _RefreshDriversTable();
        }

        private void issueInternationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmIssueInternationalLicense();
            frm.ShowDialog();
            _RefreshDriversTable();
        }

        private void showDriverLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowLicenseHistory((int)dgvDriversList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshDriversTable();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
    }
}
