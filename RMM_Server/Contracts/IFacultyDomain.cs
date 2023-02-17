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
        public Faculty EditFaculty(Faculty f);

        // new 
        public List<Faculty> GetFilteredAndSearchedFaculty(FacultyFilter ff);
       // public List<Faculty> GetFilteredFaculty(FacultyFilter ff);
        public List<Faculty> GetSearchedFacultyByKeyword(string keyword, List<Faculty> faculty);
       // public List<Faculty> GetSortedFacultyByFacultyID(string f);
    }
}
