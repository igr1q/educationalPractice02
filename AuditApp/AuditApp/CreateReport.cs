using auditLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuditApp
{
    public partial class CreateReport : Form
    {
        MainMenu mainMenu;
        public CreateReport(MainMenu mainMenu)
        {
            InitializeComponent();
            loadEmployees();
            this.mainMenu = mainMenu;
        }

        private void loadEmployees()
        {
            //сотрудники
            comboBox1.DataSource = audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).GetAllEmployeesWithCategory();
            comboBox1.DisplayMember = "FullName";
            comboBox1.ValueMember = "EmployeeID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainMenu.Show();
            this.Close();  
        }

        private void CreateReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainMenu.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Reports(this,int.Parse(comboBox1.SelectedValue.ToString())).ShowDialog();
        }
    }
}
