using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace as_02.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public int Department_id { get; set; }
        public string Fio { get; set; }
        public float Salary { get; set; }

    }
}
