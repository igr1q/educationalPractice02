using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auditLibrary
{
    public class JobWithDetails
    {
        public int JobID { get; set; }
        public string JobName { get; set; }
        public DateTime JobDate { get; set; }
        public string JobTypeName { get; set; }
        public string JobStatusName { get; set; }
        public decimal HoursWorked { get; set; }
        public string EnterpriseName { get; set; }
        public string EmployeeName { get; set; }
        public string CategoryName { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            JobWithDetails other = (JobWithDetails)obj;

            return this.JobID == other.JobID
                && this.EmployeeName == other.EmployeeName
                && this.JobName == other.JobName
                && this.EnterpriseName == other.EnterpriseName
                && this.JobDate == other.JobDate
                && this.JobTypeName == other.JobTypeName
                && this.JobStatusName == other.JobStatusName
                && this.HoursWorked == other.HoursWorked
                && this.CategoryName == other.CategoryName;
        }

        public override int GetHashCode()
        {
            return JobID.GetHashCode()  ^ JobName.GetHashCode()  ^ JobDate.GetHashCode() ^ HoursWorked.GetHashCode() ^ CategoryName.GetHashCode()^ EmployeeName.GetHashCode()^ EnterpriseName.GetHashCode()^ JobTypeName.GetHashCode()^ JobStatusName.GetHashCode();
        }
    }
}
