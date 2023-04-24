using MySql.Data.MySqlClient;
using RMM_Server.Contracts;
using RMM_Server.Models;
using RMM_Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Domains
{
    public class DepartmentDomain : IDepartmentDomain
    {
        private readonly IDepartmentRepository idr;

        public DepartmentDomain(IDepartmentRepository idr)
        {
            this.idr = idr;
        }

        public List<Department> GetAllDepartments()
        {
            List<Department> result = idr.GetAllDepartments();
            return result;
        }

        public List<SubDepartment> GetSubDeptByDeptId(int dID)
        {
            List<SubDepartment> result = idr.GetSubDeptByDeptId(dID);
            return result;
        }

        public string[] GetSubDeptByResearchId(int rID)
        {
            string[] result = idr.GetSubDeptByResearchId(rID);
            return result;
        }

        public string[] GetAllSubDeptByResearchId(int rID)
        {
            string[] result = idr.GetAllSubDeptByResearchId(rID);
            return result;
        }

    }
}
