using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace auditLibrary
{
    public class audit
    {
        private audit() { }              // Запрет на создание ещё одного экземпляра класса.
        private static audit Instance;   // Единственный экземпляр Database для работы с БД.
        private string sqlConnectionString;
        private delegate object Handler(SqlCommand cmd); // Делагат для определения функции, которая принимает SqlCommand
        public bool successfulLogin { get; set; }

        public static audit GetInstance(string sqlconnectionString = null)
        {
            // Если экзепляр Instance не существует, инициализировать новый.
            if (Instance == null)
            {
                Instance = new audit();
            }

            if (sqlconnectionString != null) Instance.sqlConnectionString = sqlconnectionString;

            return Instance;
        }

        public bool AuthenticateAdmin(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "SELECT COUNT(*) FROM Admins WHERE Username = @Username AND Password = @Password";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    int adminCount = (int)command.ExecuteScalar();

                    if (adminCount > 0)
                    {
                        // Вход успешен, записываем лог входа
                        int adminID = GetAdminIDByUsername(username);
                        successfulLogin = true;
                        LogAdminLogin(adminID,username, successfulLogin);
                        return true;
                    }
                    else
                    {
                        int adminID = GetAdminIDByUsername(username);
                        successfulLogin = false;
                        LogAdminLogin(adminID, username, successfulLogin);
                    }
                }
                connection.Close();
            }
            return false;
        }


        private int GetAdminIDByUsername(string username)
        {
            int adminID;
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "SELECT AdminID FROM Admins WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    adminID = (int)command.ExecuteScalar();

                }
                connection.Close();
            }
            return adminID;
        }


        private void LogAdminLogin(int adminID, string username, bool successfulLogin)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "INSERT INTO AdminLoginLogs (AdminID, LoginTime, LoginUsername, SuccessfulLogin) " +
                             "VALUES (@AdminID, GETDATE(), @LoginUsername, @SuccessfulLogin)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@AdminID", adminID);
                    command.Parameters.AddWithValue("@LoginUsername", username);
                    command.Parameters.AddWithValue("@SuccessfulLogin", successfulLogin);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }

        }

        public Employee GetEmployeeInfo(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Employee
                            {
                                EmployeeID = (int)reader["EmployeeID"],
                                FullName = (string)reader["FullName"],
                                PassportNumber = (string)reader["PassportNumber"],
                                DateOfBirth = (DateTime)reader["DateOfBirth"],
                                PhoneNumber = (string)reader["PhoneNumber"],
                                CategoryID = (int)reader["CategoryID"]
                            };
                        }
                    }
                }
                connection.Close();
            }
            return null;
        }

        public EmployeeCategory GetCategoryInfo(int categoryId)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM EmployeeCategories WHERE CategoryID = @CategoryID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CategoryID", categoryId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new EmployeeCategory
                            {
                                CategoryID = (int)reader["CategoryID"],
                                CategoryName = (string)reader["CategoryName"],
                                HourlyRate = (decimal)reader["HourlyRate"]
                            };
                        }
                    }
                }
                connection.Close();
            }
            return null;
        }

        public JobType GetJobTypeById(int jobTypeId)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM JobTypes WHERE JobTypeID = @JobTypeID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@JobTypeID", jobTypeId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new JobType
                            {
                                JobTypeID = (int)reader["JobTypeID"],
                                JobTypeName = (string)reader["JobTypeName"]
                            };
                        }
                    }
                }
                connection.Close();
            }
            return null;
        }

        public Job GetJobById(int jobId)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Jobs WHERE JobID = @JobID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@JobID", jobId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Job
                            {
                                JobID = (int)reader["JobID"],
                                EmployeeID = (int)reader["EmployeeID"],
                                JobName = (string)reader["JobName"],
                                EnterpriseID = (int)reader["EnterpriseID"],
                                JobDate = (DateTime)reader["JobDate"],
                                JobTypeID = (int)reader["JobTypeID"],
                                JobStatusID = (int)reader["JobStatusID"],
                                HoursWorked = (decimal)reader["HoursWorked"]
                            };
                        }
                    }
                }
                connection.Close();
            }
            return null;
        }
        public List<Job> GetFinishedJobsForEmployee(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Jobs " +
                             "WHERE EmployeeID = @EmployeeID " +
                             "AND (JobTypeID <> 1 OR (JobTypeID = 1 AND JobStatusID = 1))";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    List<Job> jobs = new List<Job>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            jobs.Add(new Job
                            {
                                JobID = (int)reader["JobID"],
                                EmployeeID = (int)reader["EmployeeID"],
                                JobName = (string)reader["JobName"],
                                EnterpriseID = (int)reader["EnterpriseID"],
                                JobDate = (DateTime)reader["JobDate"],
                                JobTypeID = (int)reader["JobTypeID"],
                                JobStatusID = (int)reader["JobStatusID"],
                                HoursWorked = (decimal)reader["HoursWorked"]
                            });
                        }
                    }
                    connection.Close();
                    return jobs;
                }
            }
        }
        public List<JobWithDetails> GetAllJobsWithDetails()
        {
            List<JobWithDetails> jobsWithDetails = new List<JobWithDetails>();
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "SELECT J.JobID, J.JobName, J.JobDate, E1.CompanyName AS EnterpriseName, JT.JobTypeName, JS.JobStatusName, J.HoursWorked, E2.FullName AS EmployeeName, EC.CategoryName " +
                         "FROM Jobs J " +
                         "INNER JOIN Enterprises E1 ON J.EnterpriseID = E1.EnterpriseID " +
                         "INNER JOIN JobTypes JT ON J.JobTypeID = JT.JobTypeID " +
                         "INNER JOIN JobStatuses JS ON J.JobStatusID = JS.JobStatusID " +
                         "INNER JOIN Employees E2 ON J.EmployeeID = E2.EmployeeID " +
                         "INNER JOIN EmployeeCategories EC ON E2.CategoryID = EC.CategoryID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            jobsWithDetails.Add(new JobWithDetails
                            {
                                JobID = (int)reader["JobID"],
                                JobName = (string)reader["JobName"],
                                EnterpriseName = (string)reader["EnterpriseName"],
                                JobTypeName = (string)reader["JobTypeName"],
                                JobStatusName = (string)reader["JobStatusName"],
                                HoursWorked = (decimal)reader["HoursWorked"],
                                EmployeeName = (string)reader["EmployeeName"],
                                CategoryName = (string)reader["CategoryName"],
                                JobDate = (DateTime)reader["JobDate"]
                            });
                        }
                    }
                }
                connection.Close();
            }
            return jobsWithDetails;
        }
        public List<EmployeeWithCategory> GetAllEmployeesWithCategory()
        {
            List<EmployeeWithCategory> employeesWithCategory = new List<EmployeeWithCategory>();
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "SELECT E.EmployeeID, E.FullName, E.PassportNumber, E.DateOfBirth, E.PhoneNumber, " +
                             "EC.CategoryName, EC.HourlyRate " +
                             "FROM Employees E " +
                             "INNER JOIN EmployeeCategories EC ON E.CategoryID = EC.CategoryID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employeesWithCategory.Add(new EmployeeWithCategory
                            {
                                EmployeeID = (int)reader["EmployeeID"],
                                FullName = (string)reader["FullName"],
                                PassportNumber = (string)reader["PassportNumber"],
                                DateOfBirth = (DateTime)reader["DateOfBirth"],
                                PhoneNumber = (string)reader["PhoneNumber"],
                                CategoryName = (string)reader["CategoryName"],
                                HourlyRate = (decimal)reader["HourlyRate"]
                            });
                        }
                    }
                }
                connection.Close();
            }
            return employeesWithCategory;
        }
        public List<JobStatus> GetAllJobStatuses()
        {
            List<JobStatus> jobStatuses = new List<JobStatus>();
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM JobStatuses";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            jobStatuses.Add(new JobStatus
                            {
                                JobStatusID = (int)reader["JobStatusID"],
                                JobStatusName = (string)reader["JobStatusName"]
                            });
                        }
                    }
                }
                connection.Close();
            }
            return jobStatuses;
        }
        public List<JobType> GetAllJobTypes()
        {
            List<JobType> jobTypes = new List<JobType>();
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM JobTypes";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            jobTypes.Add(new JobType
                            {
                                JobTypeID = (int)reader["JobTypeID"],
                                JobTypeName = (string)reader["JobTypeName"]
                            });
                        }
                    }
                }
                connection.Close();
            }
            return jobTypes;
        }
        public List<EmployeeCategory> GetAllEmployeeCategories()
        {
            List<EmployeeCategory> employeeCategories = new List<EmployeeCategory>();
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM EmployeeCategories";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employeeCategories.Add(new EmployeeCategory
                            {
                                CategoryID = (int)reader["CategoryID"],
                                CategoryName = (string)reader["CategoryName"],
                                HourlyRate = (decimal)reader["HourlyRate"]
                            });
                        }
                    }
                }
                connection.Close();
            }
            return employeeCategories;
        }
        public int AddEmployee(Employee employee)
        {
            int col;
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Employees (FullName, PassportNumber, DateOfBirth, PhoneNumber, CategoryID) " +
                             "VALUES (@FullName, @PassportNumber, @DateOfBirth, @PhoneNumber, @CategoryID); " +
                             "SELECT SCOPE_IDENTITY()";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@FullName", employee.FullName);
                    command.Parameters.AddWithValue("@PassportNumber", employee.PassportNumber);
                    command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                    command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                    command.Parameters.AddWithValue("@CategoryID", employee.CategoryID);

                    col = Convert.ToInt32(command.ExecuteScalar());
                }
                connection.Close();
            }
            return col;
        }

        public int AddEmployeeCategory(EmployeeCategory category)
        {
            int col;
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "INSERT INTO EmployeeCategories (CategoryName, HourlyRate) " +
                             "VALUES (@CategoryName, @HourlyRate); " +
                             "SELECT SCOPE_IDENTITY()";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    command.Parameters.AddWithValue("@HourlyRate", category.HourlyRate);

                    col = Convert.ToInt32(command.ExecuteScalar());
                }
                connection.Close();
            }
            return col;
        }

        public int AddJob(Job job)
        {
            int col;
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Jobs (EmployeeID, JobName, EnterpriseID, JobDate, JobTypeID, JobStatusID, HoursWorked) " +
                             "VALUES (@EmployeeID, @JobName, @EnterpriseID, @JobDate, @JobTypeID, @JobStatusID, @HoursWorked); " +
                             "SELECT SCOPE_IDENTITY()";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", job.EmployeeID);
                    command.Parameters.AddWithValue("@JobName", job.JobName);
                    command.Parameters.AddWithValue("@EnterpriseID", job.EnterpriseID);
                    command.Parameters.AddWithValue("@JobDate", job.JobDate);
                    command.Parameters.AddWithValue("@JobTypeID", job.JobTypeID);
                    command.Parameters.AddWithValue("@JobStatusID", job.JobStatusID);
                    command.Parameters.AddWithValue("@HoursWorked", job.HoursWorked);

                    col = Convert.ToInt32(command.ExecuteScalar());
                }
                connection.Close();
            }
            return col;
        }

        public int AddJobStatus(JobStatus jobStatus)
        {
            int col;
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "INSERT INTO JobStatuses (JobStatusName) VALUES (@JobStatusName); " +
                             "SELECT SCOPE_IDENTITY()";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@JobStatusName", jobStatus.JobStatusName);

                    col = Convert.ToInt32(command.ExecuteScalar());
                }
                connection.Close();
            }
            return col;
        }

        public int AddJobType(JobType jobType)
        {
            int col;
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "INSERT INTO JobTypes (JobTypeName) VALUES (@JobTypeName); " +
                             "SELECT SCOPE_IDENTITY()";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@JobTypeName", jobType.JobTypeName);

                    col = Convert.ToInt32(command.ExecuteScalar());
                }
                connection.Close();
            }
            return col;
        }

        public int AddEnterprise(Enterprise enterprise)
        {
            int col;
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Enterprises (CompanyName) VALUES (@CompanyName); " +
                             "SELECT SCOPE_IDENTITY()";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CompanyName", enterprise.CompanyName);
                    
                    col =  Convert.ToInt32(command.ExecuteScalar());
                }
                connection.Close();
            }
            return col;
        }

        public bool UpdateEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "UPDATE Employees SET FullName = @FullName, PassportNumber = @PassportNumber, " +
                             "DateOfBirth = @DateOfBirth, PhoneNumber = @PhoneNumber, CategoryID = @CategoryID " +
                             "WHERE EmployeeID = @EmployeeID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                    command.Parameters.AddWithValue("@FullName", employee.FullName);
                    command.Parameters.AddWithValue("@PassportNumber", employee.PassportNumber);
                    command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                    command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                    command.Parameters.AddWithValue("@CategoryID", employee.CategoryID);

                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0;
                }
            }
        }

        public bool DeleteEmployee(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0;
                }
            }
        }

        public bool UpdateEmployeeCategory(EmployeeCategory category)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "UPDATE EmployeeCategories SET CategoryName = @CategoryName, HourlyRate = @HourlyRate " +
                             "WHERE CategoryID = @CategoryID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                    command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    command.Parameters.AddWithValue("@HourlyRate", category.HourlyRate);

                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0;
                }
            }
        }

        public bool DeleteEmployeeCategory(int categoryId)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "DELETE FROM EmployeeCategories WHERE CategoryID = @CategoryID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CategoryID", categoryId);
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0;
                }
            }
        }

        public bool UpdateJob(Job job)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "UPDATE Jobs SET EmployeeID = @EmployeeID, JobName = @JobName, EnterpriseID = @EnterpriseID, " +
                             "JobDate = @JobDate, JobTypeID = @JobTypeID, JobStatusID = @JobStatusID, HoursWorked = @HoursWorked " +
                             "WHERE JobID = @JobID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@JobID", job.JobID);
                    command.Parameters.AddWithValue("@EmployeeID", job.EmployeeID);
                    command.Parameters.AddWithValue("@JobName", job.JobName);
                    command.Parameters.AddWithValue("@EnterpriseID", job.EnterpriseID);
                    command.Parameters.AddWithValue("@JobDate", job.JobDate);
                    command.Parameters.AddWithValue("@JobTypeID", job.JobTypeID);
                    command.Parameters.AddWithValue("@JobStatusID", job.JobStatusID);
                    command.Parameters.AddWithValue("@HoursWorked", job.HoursWorked);

                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0;
                }
            }
        }

        public bool DeleteJob(int jobId)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Jobs WHERE JobID = @JobID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@JobID", jobId);
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0;
                }
            }
        }

        public bool UpdateJobStatus(JobStatus jobStatus)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "UPDATE JobStatuses SET JobStatusName = @JobStatusName " +
                             "WHERE JobStatusID = @JobStatusID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@JobStatusID", jobStatus.JobStatusID);
                    command.Parameters.AddWithValue("@JobStatusName", jobStatus.JobStatusName);

                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0;
                }
            }
        }

        public bool DeleteJobStatus(int jobStatusId)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "DELETE FROM JobStatuses WHERE JobStatusID = @JobStatusID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@JobStatusID", jobStatusId);
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0;
                }
            }
        }

        public bool UpdateJobType(JobType jobType)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "UPDATE JobTypes SET JobTypeName = @JobTypeName " +
                             "WHERE JobTypeID = @JobTypeID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@JobTypeID", jobType.JobTypeID);
                    command.Parameters.AddWithValue("@JobTypeName", jobType.JobTypeName);

                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0;
                }
            }
        }

        public bool DeleteJobType(int jobTypeId)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "DELETE FROM JobTypes WHERE JobTypeID = @JobTypeID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@JobTypeID", jobTypeId);
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0;
                }
            }
        }

        public bool UpdateEnterprise(Enterprise enterprise)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "UPDATE Enterprises SET CompanyName = @CompanyName " +
                             "WHERE EnterpriseID = @EnterpriseID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@EnterpriseID", enterprise.EnterpriseID);
                    command.Parameters.AddWithValue("@CompanyName", enterprise.CompanyName);

                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0;
                }
            }
        }

        public bool DeleteEnterprise(int enterpriseId)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Enterprises WHERE EnterpriseID = @EnterpriseID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@EnterpriseID", enterpriseId);
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0;
                }
            }
        }
        public List<Enterprise> GetAllEnterprises()
        {
            List<Enterprise> enterprises = new List<Enterprise>();
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Enterprises";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            enterprises.Add(new Enterprise
                            {
                                EnterpriseID = (int)reader["EnterpriseID"],
                                CompanyName = (string)reader["CompanyName"]
                            });
                        }
                    }
                }
                connection.Close();
            }
            return enterprises;
        }

        public Enterprise GetEnterpriseById(int enterpriseId)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Enterprises WHERE EnterpriseID = @EnterpriseID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@EnterpriseID", enterpriseId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Enterprise
                            {
                                EnterpriseID = (int)reader["EnterpriseID"],
                                CompanyName = (string)reader["CompanyName"]
                            };
                        }
                    }
                }
                connection.Close();
            }
            return null;
        }

        public EmployeeWithCategory GetEmployeeWithCategory(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "SELECT E.EmployeeID, E.FullName, E.PassportNumber, E.DateOfBirth, E.PhoneNumber, " +
                             "EC.CategoryName, EC.HourlyRate " +
                             "FROM Employees E " +
                             "INNER JOIN EmployeeCategories EC ON E.CategoryID = EC.CategoryID WHERE  E.EmployeeID = @EmployeeID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new EmployeeWithCategory
                            {
                                EmployeeID = (int)reader["EmployeeID"],
                                FullName = (string)reader["FullName"],
                                PassportNumber = (string)reader["PassportNumber"],
                                DateOfBirth = (DateTime)reader["DateOfBirth"],
                                PhoneNumber = (string)reader["PhoneNumber"],
                                CategoryName = (string)reader["CategoryName"],
                                HourlyRate = (decimal)reader["HourlyRate"]
                            };
                        }
                    }
                }
                connection.Close();
            }
            return null;
        }

        public Report GenerateEmployeeReport(int employeeId)
        {
            EmployeeWithCategory employee = GetEmployeeWithCategory(employeeId);

            if (employee != null)
            {
                List<Job> finishedJobs = GetFinishedJobsForEmployee(employeeId);
                Report report = new Report(employee.FullName);
                report.Jobs = finishedJobs;
                report.employeeCategory_HourlyRate = employee.HourlyRate;
                report.CalculateTotalPayment();
                return report;
            }

            return null;
        }

        public List<LogEntry> GetLogEntries()
        {
            List<LogEntry> logEntries = new List<LogEntry>();

            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                string sql = "SELECT LogID, LoginUsername, LoginTime, SuccessfulLogin FROM AdminLoginLogs";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logEntries.Add(new LogEntry
                            {
                                LogID = (int)reader["LogID"],
                                Username = (string)reader["LoginUsername"],
                                LoginTime = (DateTime)reader["LoginTime"],
                                IsSuccessful = (bool)reader["SuccessfulLogin"]
                            });
                        }
                    }
                }
                connection.Close();
            }
            return logEntries;
        }

    }
}