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
    public class SkillRepository:ISkillRepository
    {
        readonly IConfiguration _configuration;
        readonly string connectionString;
        public SkillRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration["ConnectionString"];
        }
        public List<Skill> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Skill>("SELECT * FROM Skills").ToList();
            }
        }

        public Skill Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Skill>("SELECT * FROM Skills WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }
        public Skill Create(Skill skill)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Skills (Name) VALUES(@Name)";
                db.Execute(sqlQuery, skill);
                return skill;

            }
        }

        public Skill Update(Skill skill)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Skills SET Name = @Name WHERE Id = @Id";
                db.Execute(sqlQuery, skill);
                return skill;
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Skills WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
