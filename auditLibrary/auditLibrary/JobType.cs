using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auditLibrary
{
    public class JobType
    {
        public int JobTypeID { get; set; }
        public string JobTypeName { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            JobType other = (JobType)obj;

            return this.JobTypeID == other.JobTypeID
                && this.JobTypeName == other.JobTypeName;
        }

        public override int GetHashCode()
        {
            return JobTypeID.GetHashCode() ^ JobTypeName.GetHashCode();
        }
    }
}
