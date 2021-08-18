using as_02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace as_02.Interfaces
{
    public interface IStaffSkillRepository
    {
        StaffSkill Create(StaffSkill StaffSkill);
        void Delete(int id);
        StaffSkill Get(int id);
        List<StaffSkill> GetAll();
        StaffSkill Update(StaffSkill StaffSkill);

        public StaffSkill FindBySkillIdAndStaffid(StaffSkill staffSkill);
        public void DeleteByStaffId(int staff_id);
    }
}
