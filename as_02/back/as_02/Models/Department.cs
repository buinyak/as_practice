using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace as_02.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Staff> Staffs { get; set; }
    }
}
