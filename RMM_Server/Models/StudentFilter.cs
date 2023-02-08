using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Models
{
    public class StudentFilter
    {
        public List<Student> student { get; set; }
        public List<StudentFilterValue> studentFilterValue { get; set; }
        public string psuID { get; set; }
        public string keyword { get; set; }
    }

    public class StudentFilterValue
    {
        public string checkedValue { get; set; }
        public string categoryValue { get; set; }
    }
}
