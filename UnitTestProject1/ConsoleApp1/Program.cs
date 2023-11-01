using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using auditLibrary;

namespace ConsoleApp1
{
    class Program
    {
        private static string sqlstr = @"Data Source=ADCLG1;Initial Catalog=Audit_db_ig;Integrated Security=True";
        private static audit auditDB = audit.GetInstance(sqlstr);
        static void Main(string[] args)
        {
            List<EmployeeWithCategory> employeesActual = auditDB.GetAllEmployeesWithCategory();
            foreach(EmployeeWithCategory i in employeesActual)
            {
                Console.WriteLine(i);
            }
        }
            
    }
}


