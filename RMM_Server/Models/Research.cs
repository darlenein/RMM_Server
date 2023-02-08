using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Models
{
    public class Research
    {
        public int Id { get; set; }
        public string Faculty_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Required_Skills { get; set; }
        public string Encouraged_Skills { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public bool Active { get; set; }
        public string Address { get; set; }
        public string Faculty_FirstName { get; set; }
        public string Faculty_LastName { get; set; }
        public int Research_id { get; set; }
        public int Progression { get; set; }
        public bool IsPaid { get; set; }
        public bool IsNonpaid { get; set; }
        public bool IsCredit { get; set; }
        public string[] ResearchDepts { get; set; }
        public float SearchScore { get; set; }
        public string GUID { get; set; }

        public int Counter { get; set; }

    }
}
