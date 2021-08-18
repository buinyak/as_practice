using as_02.Interfaces;
using as_02.Models;
using as_02.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace as_02.Services.Implimentations
{
    public class StaffService:IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IStaffSkillRepository _staffSkillRepository;

        public StaffService(IStaffRepository staffRepository, IStaffSkillRepository staffSkillRepository)
        {
            _staffRepository = staffRepository;
            _staffSkillRepository = staffSkillRepository;
        }
        public Staff CreateWithSkills(Staff staff)
        {
            _staffRepository.Create(staff);
            var staffSkill = new StaffSkill();
            foreach(Skill skill in staff.Skills)
            {
                staffSkill.Skill_id = skill.Id;
                staffSkill.Staff_id = staff.Id;
                if (_staffSkillRepository.FindBySkillIdAndStaffid(staffSkill) == null)
                {
                    _staffSkillRepository.Create(staffSkill);
                }else
                {
                    _staffSkillRepository.Update(staffSkill);
                }
                
            }
            return staff;

        }
        public Staff UpdateWithSkills(Staff staff)
        {
            var staffSkill = new StaffSkill();
            _staffSkillRepository.DeleteByStaffId(staff.Id);
            foreach (Skill skill in staff.Skills)
            {
                staffSkill.Skill_id = skill.Id;
                staffSkill.Staff_id = staff.Id;
                    _staffSkillRepository.Create(staffSkill);

            }
            return staff;

        }


    }
}
