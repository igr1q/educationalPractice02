using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auditLibrary
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public string PassportNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public int CategoryID { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Employee other = (Employee)obj;

            return this.EmployeeID == other.EmployeeID
                && this.FullName == other.FullName
                && this.PassportNumber == other.PassportNumber
                && this.DateOfBirth == other.DateOfBirth
                && this.PhoneNumber == other.PhoneNumber
                && this.CategoryID == other.CategoryID;
        }

        public override int GetHashCode()
        {
            return EmployeeID.GetHashCode() ^ FullName.GetHashCode() ^ PassportNumber.GetHashCode() ^ DateOfBirth.GetHashCode() ^ PhoneNumber.GetHashCode() ^ CategoryID.GetHashCode();
        }
    }
}
