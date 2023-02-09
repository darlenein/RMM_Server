using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using RMM_Server.Contracts;
using RMM_Server.Models;
using RMM_Server.Services;

namespace RMM_Server.Domains
{
    public class FacultyDomain : IFacultyDomain
    {
        private readonly IFacultyRepository ifr;

        public FacultyDomain(IFacultyRepository ifr)
        {
            this.ifr = ifr;
        }
        public Faculty GetFaculty(string id)
        {
            Faculty result = ifr.GetFaculty(id);
            return result;
        }

        public List<Faculty> GetAllFaculty()
        {
            List<Faculty> result = ifr.GetAllFaculty();
            return result;
        }

        public Faculty CreateFaculty(Faculty f)
        {
            Faculty result = ifr.CreateFaculty(f);
            return result;
        }

        public Faculty EditFaculty(Faculty f)
        {
            Faculty result = ifr.EditFaculty(f);
            return result;
        }

        public void DeleteFacultyByID(string id)
        {
            ifr.DeleteFacultyByID(id);
        }
    }
}
