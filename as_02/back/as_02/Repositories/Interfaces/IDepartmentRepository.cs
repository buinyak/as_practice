using as_02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace as_02.Interfaces
{
    public interface IDepartmentRepository
    {
        Department Create(Department department);
        void Delete(int id);
        Department Get(int id);
        Department Update(Department department);
        List<Department> GetAll();
        public List<dynamic> GetMidSalaries();
    }
}
