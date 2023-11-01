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
using auditLibrary;

namespace AuditApp
{
    public partial class EditAddEmploee : Form
    {
        InfoTable InfoTable;
        string action;
        int id; 
        public EditAddEmploee(InfoTable infoTable, string action)
        {
            InitializeComponent();
            InfoTable = infoTable;
            this.action = action;
            loadCategory();
        }

        public EditAddEmploee(InfoTable infoTable, string action, int id)
        {
            InitializeComponent();
            InfoTable = infoTable;
            loadCategory();
            this.id = id;
            this.action = action;
            Employee employee = audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).GetEmployeeInfo(id);
            textBox1.Text = employee.FullName;
            comboBox1.SelectedValue = employee.CategoryID;
            textBox2.Text = employee.PassportNumber;
            maskedTextBox1.Text = employee.PhoneNumber;
            dateTimePicker1.Value = employee.DateOfBirth;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch(action)
            {
                case "add":
                    Employee employee = new Employee
                    {
                        FullName = textBox1.Text,
                        CategoryID = int.Parse(comboBox1.SelectedValue.ToString()),
                        DateOfBirth = dateTimePicker1.Value,
                        PassportNumber = textBox2.Text,
                        PhoneNumber = maskedTextBox1.Text
                    };
                    audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).AddEmployee(employee);
                    break;
                case "edit":
                    Employee employeeUpdate = new Employee
                    {
                        EmployeeID = id,
                        FullName = textBox1.Text,
                        CategoryID = int.Parse(comboBox1.SelectedValue.ToString()),
                        DateOfBirth = dateTimePicker1.Value,
                        PassportNumber = textBox2.Text,
                        PhoneNumber = maskedTextBox1.Text
                    };
                    audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).UpdateEmployee(employeeUpdate);
                    break;
            }
            close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            close();
        }

        private void close()
        {
            InfoTable.setNeedDataUpdate = true;
            InfoTable.Show();
            this.Close();
        }

        private void EditAddEmploee_FormClosed(object sender, FormClosedEventArgs e)
        {
            InfoTable.Show();
        }

        private void loadCategory()
        {
            comboBox1.DataSource = audit.GetInstance(ConfigurationManager.ConnectionStrings["HomeConnectionString"].ConnectionString).GetAllEmployeeCategories();
            comboBox1.ValueMember = "CategoryID";
            comboBox1.DisplayMember = "CategoryName";
        }
    }
}
