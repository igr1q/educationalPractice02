using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auditLibrary
{
    public class EmployeeWithCategory
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public string PassportNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string CategoryName { get; set; }
        public decimal HourlyRate { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            EmployeeWithCategory other = (EmployeeWithCategory)obj;

            return this.EmployeeID == other.EmployeeID
                && this.FullName == other.FullName
                && this.PassportNumber == other.PassportNumber
                && this.DateOfBirth == other.DateOfBirth
                && this.PhoneNumber == other.PhoneNumber
                && this.CategoryName == other.CategoryName
                && this.HourlyRate == other.HourlyRate;
        }

        public override int GetHashCode()
        {
            return EmployeeID.GetHashCode() ^ FullName.GetHashCode() ^ PassportNumber.GetHashCode() ^ DateOfBirth.GetHashCode() ^ PhoneNumber.GetHashCode() ^ CategoryName.GetHashCode() ^ HourlyRate.GetHashCode();
        }
    }
}
