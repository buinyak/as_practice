﻿using as_02.Interfaces;
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
        public List<Department> GetAllDepartments()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Department>("SELECT * FROM Departments").ToList();
            }
        }

        public Department GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Department>("SELECT * FROM Departments WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }
        public dynamic GetAllStaffsWithDepartments()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var deps = db.Query<Object>(
                    "SELECT DEP.id AS department_id,DEP.Name AS department_name,STAFF.id AS id,STAFF.Fio AS fio,STAFF.Salary AS salary " +
                    "FROM Departments DEP INNER JOIN Staffs STAFF ON DEP.Id = STAFF.Department_id");
                return deps;
            }
        }
        public void Create(Department Department)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Departments (Name) VALUES(@Name)";
                db.Execute(sqlQuery, Department);

            }
        }

        public void Update(Department Department)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Departments SET Name = @Name WHERE Id = @Id";
                db.Execute(sqlQuery, Department);
            }
        }

        public void DeleteById(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Staffs WHERE Department_id = @id;DELETE FROM Departments WHERE Id = @id";
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
