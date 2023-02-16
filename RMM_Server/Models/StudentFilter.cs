using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Models
{
    public class StudentFilter
    {
        public List<Student> Student { get; set; }
        public List<StudentFilterValue> StudentFilterValue { get; set; }
        public string PsuID { get; set; }
        public string Keyword { get; set; }
    }

    public class StudentFilterValue
    {
        public string CheckedValue { get; set; }
        public string CategoryValue { get; set; }
    }
}
