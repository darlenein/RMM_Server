using Microsoft.AspNetCore.Mvc;
using RMM_Server.Domains;
using RMM_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RMM_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        // GET: api/<DepartmentController>
        [HttpGet("getAllDepartments")]
        public List<Department> GetAllDepartments()
        {
            DepartmentDomain dd = new DepartmentDomain();
            List<Department> result = dd.GetAllDepartments();
            return result;
        }

        [HttpGet("getAllSubDeptByDeptId/{id}")]
        public List<SubDepartment> GetAllSubDeptByDeptId(int id)
        {
            DepartmentDomain sd = new DepartmentDomain();
            List<SubDepartment> result = sd.GetSubDeptByDeptId(id);
            return result;
        }

        //[HttpGet("getAllSubDeptByResearchId/{id}")]
        //public List<SubDepartment> GetAllSubDeptByResearchId(int id)
        //{
        //    DepartmentDomain sd = new DepartmentDomain();
        //    List<SubDepartment> result = sd.GetSubDeptByResearchId(id);
        //    return result;
        //}

        // POST api/<DepartmentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DepartmentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
