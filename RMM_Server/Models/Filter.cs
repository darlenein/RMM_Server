using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Models
{
    public class Filter
    {
        public List<Research> Research { get; set; }
        public List<FilterValue> FilterValue { get; set; }
        public string PsuID { get; set; }
        public string Keyword { get; set; }
    }
    public class FilterValue
    {
        public string CheckedValue { get; set; }
        public string CategoryValue { get; set; }
    }
}