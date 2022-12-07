using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileManager.Models
{
    public class Student
    { 
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double GPA { get; set; }
        public string GraduationMonth { get; set; }
        public string GraduationYear { get; set; }
        public string Major { get; set; }
        public string Skills { get; set; }
        public string Link1 { get; set; }
        public string Link2 { get; set; }
        public string Link3 { get; set; }
        public string ResearchInterest { get; set; }
        public string ResearchProject { get; set; }
        public string Email { get; set; }
        public bool PreferPaid { get; set; }
        public bool PreferNonpaid { get; set; }
        public bool PreferCredit { get; set; }
        public int Progression { get; set; }
        public string Research_name { get; set; }
        public int PreferLocation { get; set; }

    }
}
