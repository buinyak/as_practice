using as_02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace as_02.Interfaces
{
    public interface IStaffSkillsRepository
    {
        StaffSkills Create(StaffSkills staffSkills);
        void Delete(int id);
        StaffSkills Get(int id);
        List<StaffSkills> GetAll();
        StaffSkills Update(StaffSkills staffSkills);
    }
}
