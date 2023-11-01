using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auditLibrary
{
    public class Report
    {
        public string EmployeeName { get; set; }
        public List<Job> Jobs { get; set; }
        public decimal TotalPayment { get; set; }

        public decimal employeeCategory_HourlyRate { get; set; }

        public Report(string employeeName)
        {
            EmployeeName = employeeName;
            Jobs = new List<Job>();
            TotalPayment = 0;
        }

        public void CalculateTotalPayment()
        {
            TotalPayment = Jobs.Sum(job => job.HoursWorked * employeeCategory_HourlyRate);
        }

        public decimal CalculateTotalHours()
        {
            return Jobs.Sum(job => job.HoursWorked);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Report other = (Report)obj;

            return this.EmployeeName == other.EmployeeName
                && this.TotalPayment == other.TotalPayment;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
