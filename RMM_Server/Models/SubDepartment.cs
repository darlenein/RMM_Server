using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Models
{
    public class SubDepartment
    {
        public int Subdepartment_id { get; set; }
        public int Department_id { get; set; }
        public string Name { get; set; }
        public int Research_id { get; set; }
    }
}
