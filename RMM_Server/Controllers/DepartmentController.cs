using Microsoft.AspNetCore.Mvc;
using RMM_Server.Contracts;
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
        // dependecy injection
        private readonly IDepartmentDomain idd;
        public DepartmentController(IDepartmentDomain idd)
        {
            this.idd = idd;
        }

        // GET: api/<DepartmentController>
        [HttpGet("getAllDepartments")]
        public List<Department> GetAllDepartments()
        {
            List<Department> result = idd.GetAllDepartments();
            return result;
        }

        [HttpGet("getAllSubDeptByDeptId/{id}")]
        public List<SubDepartment> GetAllSubDeptByDeptId(int id)
        {
            List<SubDepartment> result = idd.GetSubDeptByDeptId(id);
            return result;
        }

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
