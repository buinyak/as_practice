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
    public class StaffSkillsRepository:IStaffSkillsRepository
    {
        readonly IConfiguration _configuration;
        readonly string connectionString;
        public StaffSkillsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration["ConnectionString"];
        }
        public List<StaffSkills> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<StaffSkills>("SELECT * FROM Skills").ToList();
            }
        }

        public StaffSkills Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<StaffSkills>("SELECT * FROM Skills WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }
        public StaffSkills Create(StaffSkills staffSkills)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Skills (Name) VALUES(@Name)";
                db.Execute(sqlQuery, staffSkills);
                return staffSkills;

            }
        }

        public StaffSkills Update(StaffSkills staffSkills)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Skills SET Name = @Name WHERE Id = @Id";
                db.Execute(sqlQuery, staffSkills);
                return staffSkills;
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

    }
}
