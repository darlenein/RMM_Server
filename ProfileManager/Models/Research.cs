using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileManager.Models
{
    public class Research
    {
        public int Id { get; set; }
        public string Faculty_Id { get; set; }
        public int SubDept_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Required_skills { get; set; }
        public string Encouraged_Skills { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public bool Active { get; set; }
        public string Student_Id { get; set; }
        public string Incentive_type { get; set; }
        public string Address { get; set; }
        public string Faculty_FirstName{ get; set; }
        public string Faculty_LastName { get; set; }
        public int Research_id { get; set; }
        public int Progression { get; set; }
    }
}
