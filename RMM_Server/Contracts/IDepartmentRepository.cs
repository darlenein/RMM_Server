﻿using RMM_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Contracts
{
    public interface IDepartmentRepository
    {
        public List<Department> GetAllDepartments();
        public List<SubDepartment> GetSubDeptByDeptId(int dID);
        public string[] GetSubDeptByResearchId(int rID);
    }
}
