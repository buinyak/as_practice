using as_02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace as_02.Services.Interfaces
{
    public interface IStaffService
    {
        public Staff CreateWithSkills(Staff staff);
        public Staff UpdateWithSkills(Staff staff);
    }
}
