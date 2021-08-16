using as_02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace as_02.Interfaces
{
    public interface ISkillRepository
    {
        Skill Create(Skill skill);
        void Delete(int id);
        Skill Get(int id);
        List<Skill> GetAll();
        Skill Update(Skill skill);
    }
}
