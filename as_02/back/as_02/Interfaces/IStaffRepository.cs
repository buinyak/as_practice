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
        void DeleteById(int id);
        Staff GetById(int id);
        List<Staff> GetAllStaffs();
        void Update(Staff staff);
        dynamic GetByDepartmentId(int id);

    }
}
