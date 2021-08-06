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
                var sqlQuery = "DELETE FROM Departments WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
