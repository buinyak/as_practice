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
    public class StaffSkillRepository:IStaffSkillRepository
    {
        readonly IConfiguration _configuration;
        readonly string connectionString;
        public StaffSkillRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration["ConnectionString"];
        }
        public List<StaffSkill> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<StaffSkill>("SELECT * FROM Staffs_Skills").ToList();
            }
        }

        public StaffSkill Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<StaffSkill>("SELECT * FROM Staffs_Skills WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }
        public StaffSkill Create(StaffSkill staffSkill)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Staffs_Skills (Staff_id,Skill_id) VALUES (@Staff_id,@Skill_id); SELECT CAST(SCOPE_IDENTITY() as int)";
                int id = db.Query<int>(sqlQuery, staffSkill).FirstOrDefault();
                staffSkill.Id = id;
                return staffSkill;

            }
        }

        public StaffSkill Update(StaffSkill staffSkill)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Staffs_Skills SET Staff_id = @Staff_Id, Skill_id = @Skill_id WHERE Id = @Id";
                db.Execute(sqlQuery, staffSkill);
                return staffSkill;
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Staffs_Skills WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
        public void DeleteByStaffId(int staff_id )
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Staffs_Skills WHERE Staff_id = @staff_id";
                db.Execute(sqlQuery, new { staff_id});
            }
        }
        public StaffSkill FindBySkillIdAndStaffid(StaffSkill staffSkill)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<StaffSkill>("SELECT * FROM Staffs_Skills WHERE Staff_id = @Staff_id AND Skill_id = @Skill_id", new { staffSkill.Staff_id, staffSkill.Skill_id }).FirstOrDefault();
            }
        }
    }
}
