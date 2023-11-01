using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using auditLibrary;
using System.Collections.Generic;

namespace UnitTestProject1
{

    [TestClass]
    public class UnitTest1
    {
        //private static string sqlstr = @"Data Source=ADCLG1;Initial Catalog=Audit_db_ig;Integrated Security=True";
        private static string sqlstr = @"Data Source=MSI\SQLEXPRESS;Initial Catalog = auditDb; Integrated Security = True";
        private audit auditDB = audit.GetInstance(sqlstr);

        [TestMethod]
        public void TestingGettingEmployeesName()
        {
            string actualEmployeeName = "Иванов Иван Григорьевич";
            string execute = auditDB.GetEmployeeInfo(1).FullName;

            Assert.AreEqual(actualEmployeeName, execute);
        }

        [TestMethod]
        public void TestingDeleteFunction()
        {
            int idInsertedExpected = 9;
            bool delete = auditDB.DeleteEmployee(idInsertedExpected);
            Assert.IsTrue(delete);
        }

        [TestMethod]
        public void TestingGettingAllEmployees()
        {
            List<EmployeeWithCategory> employeesExpected = new List<EmployeeWithCategory>();
            employeesExpected.Add(new EmployeeWithCategory
            {
                EmployeeID = 1,
                FullName = "Иванов Иван Григорьевич",
                CategoryName = "Категория 1",
                DateOfBirth = DateTime.Parse("1980-05-15"),
                PassportNumber = "1230234678",
                PhoneNumber = "+7(715)775-00-06",
                HourlyRate = 60.00M
            });

            employeesExpected.Add(new EmployeeWithCategory {
                EmployeeID = 2,
                FullName = "Петров Петр Олегович",
                PassportNumber = "4091456673",
                DateOfBirth = DateTime.Parse("1990-08-20"),
                PhoneNumber = "+7(394)470-92-37",
                CategoryName = "Категория 2",
                HourlyRate = 40.00M });
            employeesExpected.Add(new EmployeeWithCategory {
                EmployeeID = 3,
                FullName = "Сидорова Анна Дмитриевна",
                PassportNumber = "9012567389",
                DateOfBirth = DateTime.Parse("1985-02-10"),
                PhoneNumber = "+7(745)868-50-50",
                CategoryName = "Категория 1",
                HourlyRate = 60.00M });

            List<EmployeeWithCategory> employeesActual = auditDB.GetAllEmployeesWithCategory();
            CollectionAssert.AreEqual(employeesExpected,employeesActual);
        }

        [TestMethod]
        public void TestingGettingNoneEnterprise()
        {
            Enterprise nameEnterpriseActual = auditDB.GetEnterpriseById(10);
            Assert.IsNull(nameEnterpriseActual);
        }

        [TestMethod]
        public void TestingInsertNewEmployee()
        {
            int idInsertedExpected = 10;
            Employee employee = new Employee
            {
                FullName = "Богомазов Виталий Абрамович",
                PassportNumber = "3901241454",
                PhoneNumber = "+7(871)958-34-12",
                DateOfBirth = DateTime.Parse("1996-12-29"),
                CategoryID =1
            };
            int actual = auditDB.AddEmployee(employee);
            Assert.AreEqual(idInsertedExpected, actual);
        }

        

        [TestMethod]
        public void TestingCalculatePayment()
        {
            decimal expectedPayment = 270.0000M;
            decimal actualPayment = auditDB.GenerateEmployeeReport(3).TotalPayment;
            Assert.AreEqual(expectedPayment, actualPayment);
        }

        [TestMethod]
        public void TestingUpdateDB()
        {
            EmployeeCategory updateEmployeeCategory = new EmployeeCategory
            {
                CategoryID = 1,
                CategoryName = "Категория 1",
                HourlyRate = 60.0M
            };
            bool updateExpectd = auditDB.UpdateEmployeeCategory(updateEmployeeCategory);
            Assert.IsTrue(updateExpectd);
        }
        [TestMethod]
        public void testingUpdate1()
        {
            EmployeeCategory updateEmployeeCategory = new EmployeeCategory
            {
                CategoryID = 15,
                CategoryName = "Категория 1",
                HourlyRate = 60.0M
            };
            bool updateExpectd = auditDB.UpdateEmployeeCategory(updateEmployeeCategory);
            Assert.IsTrue(updateExpectd);
        }
        [TestMethod]
        public void testingDeleteEnterprises()
        {
            int idInsertedExpected = 12;
            bool delete = auditDB.DeleteEnterprise(idInsertedExpected);
            Assert.IsTrue(delete);
        }
        [TestMethod]
        public void testingDeleteEmployee()
        {
            int idInsertedExpected = 11;
            bool delete = auditDB.DeleteEmployee(idInsertedExpected);
            Assert.IsTrue(delete);
        }
    }
}
