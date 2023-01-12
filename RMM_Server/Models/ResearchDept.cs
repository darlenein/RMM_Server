using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Models
{
    // This is the model for the ResearchDept table in dB, which is to match associated dept(s) to a research
    public class ResearchDept
    {
        public int research_id { get; set; }
        public int subdeptartment_id { get; set; }
    }
}
