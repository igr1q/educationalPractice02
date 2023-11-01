using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auditLibrary
{
    public class JobStatus
    {
        public int JobStatusID { get; set; }
        public string JobStatusName { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            JobStatus other = (JobStatus)obj;

            return this.JobStatusID == other.JobStatusID
                && this.JobStatusName == other.JobStatusName;
        }

        public override int GetHashCode()
        {
            return JobStatusID.GetHashCode() ^ JobStatusName.GetHashCode();
        }
    }
}
