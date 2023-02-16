using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Models
{
    public class Research
    {
        public int Research_Id { get; set; }
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
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Progress_Bar { get; set; }
        public bool IsPaid { get; set; }
        public bool IsNonpaid { get; set; }
        public bool IsCredit { get; set; }
        public string[] ResearchDepts { get; set; }
        public float SearchScore { get; set; }
        public float MatchScore { get; set; }
        public string GUID { get; set; }

        public string RequiredSkillLevel { get; set; }
        public string EncouragedSkillLevel { get; set; }
    }
}
