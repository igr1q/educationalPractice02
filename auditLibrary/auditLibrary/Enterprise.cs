using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auditLibrary
{
    public class Enterprise
    {
        public int EnterpriseID {get;set;}
        public string CompanyName { get;set;}

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Enterprise other = (Enterprise)obj;

            return this.EnterpriseID == other.EnterpriseID
                && this.CompanyName == other.CompanyName;
        }

        public override int GetHashCode()
        {
            return EnterpriseID.GetHashCode() ^ CompanyName.GetHashCode();
        }
    }
}
