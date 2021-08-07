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
    public class StaffRepository : IStaffRepository
    {
        readonly string connectionString;
        public StaffRepository(string conn)
        {
            connectionString = conn;
        }
        public List<Staff> GetAllStaffs()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Staff>("SELECT * FROM Staffs").ToList();
            }
        }

        public Staff GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Staff>("SELECT * FROM Staffs WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public Staff Create(Staff staff)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Staffs (Department_id, Fio, Salary) VALUES(@Department_id, @Fio, @Salary); SELECT CAST(SCOPE_IDENTITY() as int)";
                int id = db.Query<int>(sqlQuery, staff).FirstOrDefault();
                staff.Id = id;
                return staff;
            }
        }

        public void Update(Staff Staff)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Staffs SET Department_id = @Department_id, Fio = @Fio, Salary = @Salary WHERE Id = @Id";
                db.Execute(sqlQuery, Staff);
            }
        }

        public void DeleteById(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Staffs WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public List<Staff> GetByDepartment(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Staff>("SELECT * FROM Staffs WHERE Id = @Id", new { id }).ToList();
            }
        }
    }
}
