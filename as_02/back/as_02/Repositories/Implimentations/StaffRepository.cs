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
    public class StaffRepository : IStaffRepository
    {
        readonly IConfiguration _configuration;
        readonly string connectionString;

        public StaffRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration["ConnectionString"];
        }
        public List<Staff> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Staff>("SELECT * FROM Staffs").ToList();
            }
        }

        public Staff Get(int id)
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

        public Staff Update(Staff staff)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Staffs SET Department_id = @Department_id, Fio = @Fio, Salary = @Salary WHERE Id = @Id";
                db.Execute(sqlQuery, staff);
                return staff;
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Staffs WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
        public ICollection<Department> GetAllByDepartmentsWithSkills()
        {
            string sql = "SELECT DEP.id, DEP.Name, S.id, S.Department_id, S.Fio, S.Salary, SK.Id, SK.Name " +
                "FROM Departments DEP " +
                "LEFT JOIN Staffs S ON DEP.Id = S.Department_id " +
                "LEFT JOIN Staffs_skills SS ON S.id = SS.Staff_id " +
                "LEFT JOIN Skills SK ON SS.Skill_id = SK.Id";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var deps = new Dictionary<int, Department>();
                var staffs = new Dictionary<int, Staff>();
                return db.Query<Department, Staff, Skill, Department>(sql,
                    (d, s, sk) =>
                    {


                        if (!deps.TryGetValue(d.Id, out Department department))
                        {
                            department = d;
                            department.Staffs = new List<Staff>();
                            deps.Add(d.Id, department);
                            staffs.Clear();
                        }

                        if (!staffs.TryGetValue(s.Id, out Staff staff))
                        {
                            staff = s;
                            staff.Skills = new List<Skill>();
                            staffs.Add(s.Id, staff);
                            department.Staffs.Add(staff);
                        }
                        if (sk != null)
                        {
                            staff.Skills.Add(sk);
                        }

                        return department;
                    }).Distinct().ToList();
            }
        }
    }
}
