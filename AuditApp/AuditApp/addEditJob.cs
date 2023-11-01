using auditLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuditApp
{
    public partial class addEditJob : Form
    {
        InfoTable InfoTable;
        Job job;
        string action;
        int id;
        public addEditJob(InfoTable infoTable,string action)
        {
            InitializeComponent();
            InfoTable = infoTable;
            loadEmployee();
            loadEnterprises();
            loadTypeWork();
            loadStatusWork();
            this.action = action;
        }
        public addEditJob(InfoTable infoTable, string action,int id)
        {
            InitializeComponent();
            InfoTable = infoTable;
            loadEmployee();
            loadEnterprises();
            loadTypeWork();
            loadStatusWork();
            job = audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).GetJobById(id);
            dateTimePicker1.Value = job.JobDate;
            textBox5.Text = job.HoursWorked.ToString();
            comboBox1.SelectedValue = job.EmployeeID;
            comboBox3.SelectedValue = job.EnterpriseID;
            comboBox4.SelectedValue = job.JobTypeID;
            comboBox2.SelectedValue = job.JobStatusID;
            textBox1.Text = job.JobName;
            this.action = action;
            this.id = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            close();
        }

        private void loadEmployee()
        {
            //сотрудники
            comboBox1.DataSource = audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).GetAllEmployeesWithCategory();
            comboBox1.DisplayMember = "FullName";
            comboBox1.ValueMember = "EmployeeID";
        }
        
        private void loadEnterprises()
        {
            //Предприятия
            comboBox3.DataSource = audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).GetAllEnterprises();
            comboBox3.DisplayMember = "CompanyName";
            comboBox3.ValueMember = "EnterpriseID";
        }

        private void loadStatusWork()
        {
            //статус работы
            comboBox2.DataSource = audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).GetAllJobStatuses();
            comboBox2.DisplayMember = "JobStatusName";
            comboBox2.ValueMember = "JobStatusID";
        }

        private void loadTypeWork()
        {
            //Типы работы
            comboBox4.DataSource = audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).GetAllJobTypes();
            comboBox4.DisplayMember = "JobTypeName";
            comboBox4.ValueMember = "JobTypeID";
        }

        private void addEditJob_FormClosed(object sender, FormClosedEventArgs e)
        {
            InfoTable.setNeedDataUpdate = true;
            InfoTable.Show();
        }

        private void close()
        {
            InfoTable.setNeedDataUpdate = true;
            InfoTable.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (action)
            {
                case "add":
                    Job job = new Job
                    {
                        JobName = textBox1.Text,
                        JobDate = dateTimePicker1.Value,
                        JobStatusID = int.Parse(comboBox2.SelectedValue.ToString()),
                        JobTypeID = int.Parse(comboBox4.SelectedValue.ToString()),
                        EnterpriseID = int.Parse((comboBox3.SelectedValue.ToString())),
                        EmployeeID = int.Parse((comboBox1.SelectedValue.ToString())),
                        HoursWorked = decimal.Parse(textBox5.Text)
                    };
                    audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).AddJob(job);
                    break;
                case "edit":
                    Job jobUpdate = new Job
                    {
                        JobID = id,
                        JobName = textBox1.Text,
                        JobDate = dateTimePicker1.Value,
                        JobStatusID = int.Parse(comboBox2.SelectedValue.ToString()),
                        JobTypeID = int.Parse(comboBox4.SelectedValue.ToString()),
                        EnterpriseID = int.Parse((comboBox3.SelectedValue.ToString())),
                        EmployeeID = int.Parse((comboBox1.SelectedValue.ToString())),
                        HoursWorked = decimal.Parse(textBox5.Text)
                    };
                    audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).UpdateJob(jobUpdate);
                    break;
            }
            close();
        }
    }
}
