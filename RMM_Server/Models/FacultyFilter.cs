using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Models
{
    public class FacultyFilter
    {
        public List<Faculty> faculty { get; set; }
        public List<FacultyFilterValue> facultyFilterValue { get; set; }
        public string psuID { get; set; }
        public string keyword { get; set; }
    }

    public class FacultyFilterValue
    {
        public string checkedValue { get; set; }
        public string categoryValue { get; set; }
    }
}
