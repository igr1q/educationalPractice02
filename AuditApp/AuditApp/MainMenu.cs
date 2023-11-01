using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using auditLibrary;

namespace AuditApp
{
    public partial class MainMenu : Form
    {
        Login login;
        public MainMenu(Login login)
        {
            InitializeComponent();
            this.login = login;
        }

        private void EmployeeBtn_Click(object sender, EventArgs e)
        {
            InfoTable infoTable = new InfoTable(this, "Employees");
            this.Hide();
            infoTable.Show();
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            login.Close();
        }

        private void EnterpriseBtn_Click(object sender, EventArgs e)
        {
            InfoTable infoTable = new InfoTable(this, "Enterprises");
            this.Hide();
            infoTable.Show();
        }

        private void JobsBtn_Click(object sender, EventArgs e)
        {
            InfoTable infoTable = new InfoTable(this, "Jobs");
            this.Hide();
            infoTable.Show();
        }

        private void ReportsBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            new CreateReport(this).Show();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex == 1)
            {
                LoadLog();
            }
        }

        private void LoadLog()
        {
            dataGridView1.DataSource = audit.GetInstance().GetLogEntries();
        }
    }
}
