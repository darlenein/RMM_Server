using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Models
{
    public class Filter
    {
        public List<Research> research { get; set; }
        public List<FilterValue> filterValue { get; set; }
        public string psuID { get; set; }
        public string keyword { get; set; }
    }
    public class FilterValue
    {
        public string checkedValue { get; set; }
        public string categoryValue { get; set; }
    }
}