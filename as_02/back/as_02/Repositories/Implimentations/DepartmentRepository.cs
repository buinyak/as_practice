using as_02.Interfaces;
using as_02.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace as_02.Repositories
{
    public class DepartmentRepository:IDepartmentRepository
    {
        readonly IConfiguration _configuration;
        readonly string connectionString;
        public DepartmentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration["ConnectionString"];
        }
        public List<Department> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Department>("SELECT * FROM Departments").ToList();
            }
        }

        public Department Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Department>("SELECT * FROM Departments WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }
        public Department Create(Department department)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Departments (Name) VALUES(@Name)";
                db.Execute(sqlQuery, department);
                return department;

            }
        }

        public Department Update(Department department)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Departments SET Name = @Name WHERE Id = @Id";
                db.Execute(sqlQuery, department);
                return department;
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Departments WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
        public List<dynamic> GetMidSalaries()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<dynamic>("SELECT Department_id, AVG(Salary) AS MidSalary FROM Staffs GROUP BY Department_id").ToList();
            }

        }
    }
}
