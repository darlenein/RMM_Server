using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Models
{
    public class Faculty
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Office { get; set; }
        public string Phone { get; set; }
        public string Link1 { get; set; }
        public string Link2 { get; set; }
        public string Link3 { get; set; }
        public int ResearchId { get; set; }
        public string StudentId { get; set; }
        public string AboutMe { get; set; }
        public string ResearchInterest { get; set; }
        public string ProfileUrl { get; set; }
    }
}
