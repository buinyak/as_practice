using as_02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace as_02.Interfaces
{
    public interface IDepartmentRepository
    {
        void Create(Department department);
        void DeleteById(int id);
        Department GetById(int id);
        List<Department> GetAllDepartments();
        void Update(Department department);
    }
}
