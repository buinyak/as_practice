using as_02.Interfaces;
using as_02.Models;
using Dapper;
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
        readonly string connectionString;
        public DepartmentRepository(string conn)
        {
            connectionString = conn;
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
                var sqlQuery = "DELETE FROM Departments WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
