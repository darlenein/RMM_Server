using RMM_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Contracts
{
    public interface IFacultyDomain
    {
        public Faculty GetFaculty(string id); //[x]
        public List<Faculty> GetAllFaculty(); //[x]
        public Faculty CreateFaculty(Faculty f); //[x]
        public void EditFaculty(Faculty f);

        // new 
        public List<Faculty> GetFilteredAndSearchedFaculty(FacultyFilter ff);
       // public List<Faculty> GetFilteredFaculty(FacultyFilter ff);
        public List<Faculty> GetSearchedFacultyByKeyword(string keyword, List<Faculty> faculty);
        public void UpdateFacultyProfileImage(string faculty_id, string profile_url);
       // public List<Faculty> GetSortedFacultyByFacultyID(string f);
    }
}
