using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auditLibrary
{
    public class EmployeeCategory
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public decimal HourlyRate { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            EmployeeCategory other = (EmployeeCategory)obj;

            return this.CategoryID == other.CategoryID
                && this.CategoryName == other.CategoryName
                && this.HourlyRate == other.HourlyRate;
        }

        public override int GetHashCode()
        {
            return CategoryID.GetHashCode() ^ CategoryName.GetHashCode() ^ HourlyRate.GetHashCode();
        }
    }
}
