using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auditLibrary
{
    public class Job
    {
        public int JobID { get; set; }
        public int EmployeeID { get; set; }
        public string JobName { get; set; }
        public int EnterpriseID { get; set; }
        public DateTime JobDate { get; set; }
        public int JobTypeID { get; set; }
        public int JobStatusID { get; set; }
        public decimal HoursWorked { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Job other = (Job)obj;

            return this.JobID == other.JobID
                && this.EmployeeID == other.EmployeeID
                && this.JobName == other.JobName
                && this.EnterpriseID == other.EnterpriseID
                && this.JobDate == other.JobDate
                && this.JobTypeID == other.JobTypeID
                && this.JobStatusID == other.JobStatusID
                && this.HoursWorked == other.HoursWorked;
        }

        public override int GetHashCode()
        {
            return JobID.GetHashCode() ^ EmployeeID.GetHashCode() ^ JobName.GetHashCode() ^ EnterpriseID.GetHashCode() ^ JobDate.GetHashCode() ^ JobTypeID.GetHashCode() ^ JobStatusID.GetHashCode() ^ HoursWorked.GetHashCode();
        }
    }
}
