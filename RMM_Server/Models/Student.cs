using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Models
{
    public class Student
    { 
        public string Student_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public double GPA { get; set; }
        public string Graduation_Month { get; set; }
        public string Graduation_Year { get; set; }
        public string Major { get; set; }
        public string Skills { get; set; }
        public string Link1 { get; set; }
        public string Link2 { get; set; }
        public string Link3 { get; set; }
        public string Research_Interest { get; set; }
        public string Research_Project { get; set; }
        public string Email { get; set; }
        public bool PreferPaid { get; set; }
        public bool PreferNonpaid { get; set; }
        public bool PreferCredit { get; set; }
        public int Progress_Bar { get; set; }
        public string Research_name { get; set; }
        public string PreferLocation { get; set; }
        public float SearchScore { get; set; }
        public string GUID { get; set; }
        public string[] StudentIDs { get; set; }

        public string Minor { get; set; }

        public string SkillLevel { get; set; }
        public string Major2 { get; set; }
        public float MatchScore { get; set; }
        public string Name { get; set; } // research name
        public string Profile_url { get; set; }

    }
}
