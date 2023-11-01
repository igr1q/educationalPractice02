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
using auditLibrary;

namespace AuditApp
{
    public partial class InfoTable : Form
    {
        string info;
        MainMenu mainMenu;
        private bool needDataUpdate = true;
        public InfoTable(MainMenu menu,string info)
        {
            InitializeComponent();
            this.info = info;
            mainMenu = menu;
        }


        private void LoadData()
        {
            switch (info)
            {
                case "Employees":
                    List<EmployeeWithCategory> employees = audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).GetAllEmployeesWithCategory();
                    dataGridView1.DataSource = employees;
                    break;

                case "Enterprises":
                    List<Enterprise> enterprises = audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).GetAllEnterprises();
                    dataGridView1.DataSource = enterprises;
                    break;

                case "Jobs":
                    List<JobWithDetails> jobs = audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).GetAllJobsWithDetails();
                    dataGridView1.DataSource = jobs;
                    break;
            }
            needDataUpdate = false;
        }

        private void back_Click(object sender, EventArgs e)
        {
            mainMenu.Show();
            this.Close();
        }

        private void InfoTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainMenu.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                int columnIndex = 0; 


                object value = dataGridView1.Rows[e.RowIndex].Cells[columnIndex].Value;

                if (value != null)
                {

                    textBoxId.Text = value.ToString();
                }
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            switch (info)
            {
                case "Employees":
                    new EditAddEmploee(this, "add").Show();
                    this.Hide();
                    break;

                case "Enterprises":
                    new EditAddEnterprise(this, "add").Show();
                    this.Hide();
                    break;

                case "Jobs":
                    new addEditJob(this, "add").Show();
                    this.Hide();
                    break;
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            if(textBoxId.Text !="")
            {
                switch (info)
                {
                    case "Employees":
                        new EditAddEmploee(this, "edit", int.Parse(textBoxId.Text)).Show();
                        this.Hide();
                        break;

                    case "Enterprises":
                        new EditAddEnterprise(this, "edit", int.Parse(textBoxId.Text)).Show();
                        this.Hide();
                        break;

                    case "Jobs":
                        new addEditJob(this, "edit", int.Parse(textBoxId.Text)).Show();
                        this.Hide();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Выберите строку, для получения id и реадктирования записи");
            }
        }

        private void InfoTable_Activated(object sender, EventArgs e)
        {
            if(needDataUpdate)
            {
                LoadData();
            }
        }

        public bool setNeedDataUpdate
        {
            get { return needDataUpdate; } set {  needDataUpdate = value; }
        }
    }
}
