using as_02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace as_02.Interfaces
{
    public interface ISkillRepository
    {
        void Create(Skill skill);
        void DeleteById(int id);
        Skill GetById(int id);
        List<Skill> GetAllSkills();
        void Update(Skill skill);
    }
}
