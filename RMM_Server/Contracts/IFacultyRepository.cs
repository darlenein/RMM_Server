using RMM_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Contracts
{
    public interface IFacultyRepository
    {
        public Faculty GetFaculty(string id);
        public List<Faculty> GetAllFaculty();
        public Faculty CreateFaculty(Faculty f);
        public void EditFaculty(Faculty f);
        public void UpdateFacultyProfileImage(string faculty_id, string profile_url);
    }
}
