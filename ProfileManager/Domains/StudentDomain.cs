using ProfileManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileManager.Domains
{
    public class StudentDomain
    {
        public Student GetStudent()
        {
            Student s = new Student();
            s.FirstName = "Bobby";
            s.LastName = "Jones";

            return s;
        }
    }
}
