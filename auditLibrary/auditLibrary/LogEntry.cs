using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auditLibrary
{
    public class LogEntry
    {
        public int LogID { get; set; }
        public string Username { get; set; }
        public DateTime LoginTime { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
