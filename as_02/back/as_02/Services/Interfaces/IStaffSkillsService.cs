using as_02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace as_02.Services.Interfaces
{
    interface IStaffSkillsService
    {
        public void AddStaffSkills(List<Skill> skills);
    }
}
