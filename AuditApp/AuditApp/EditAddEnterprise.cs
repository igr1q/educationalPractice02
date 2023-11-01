using System;
using System.Configuration;
using System.Windows.Forms;
using auditLibrary;

namespace AuditApp
{
    public partial class EditAddEnterprise : Form
    {
        InfoTable InfoTable;
        string action;
        int id;
        public EditAddEnterprise(InfoTable infoTable, string action)
        {
            InitializeComponent();
            InfoTable = infoTable;
            this.action = action;
        }
        public EditAddEnterprise(InfoTable infoTable, string action, int id)
        {
            InitializeComponent();
            InfoTable = infoTable;
            this.action = action;
            this.id = id;
            textBox1.Text = audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).GetEnterpriseById(id).CompanyName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch(action)
            {
                case "add":
                    Enterprise enterprise = new Enterprise
                    {
                        CompanyName = textBox1.Text
                    };
                    audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).AddEnterprise(enterprise);
                    break;
                case "edit":
                    Enterprise enterpriseWithId = new Enterprise
                    {
                        EnterpriseID = id,
                        CompanyName = textBox1.Text
                    };
                    audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).UpdateEnterprise(enterpriseWithId);
                    break;
            }
            close();
        }

        private void close()
        {
            InfoTable.setNeedDataUpdate = true;
            InfoTable.Show();
            this.Close();
        }

        private void EditAddEnterprise_FormClosed(object sender, FormClosedEventArgs e)
        {
            InfoTable.setNeedDataUpdate = true;
            InfoTable.Show();
        }
    }
}
