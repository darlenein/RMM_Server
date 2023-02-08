using RMM_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Contracts
{
    public interface IFacultyDomain
    {
        public Faculty GetFaculty(string id);
        public List<Faculty> GetAllFaculty();
        public Faculty CreateFaculty(Faculty f);
        public Faculty EditFaculty(Faculty f);
        public void DeleteFacultyByID(string id);
    }
}
