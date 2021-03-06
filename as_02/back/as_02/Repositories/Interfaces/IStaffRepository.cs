using as_02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace as_02.Interfaces
{
    public interface IStaffRepository
    {
        Staff Create(Staff staff);
        void Delete(int id);
        Staff Get(int id);
        List<Staff> GetAll();
        Staff Update(Staff staff);
        public ICollection<Department> GetAllByDepartmentsWithSkills();
    }
}
