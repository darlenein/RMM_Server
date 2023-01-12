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
    public class FacultyController : ControllerBase
    {

        // GET api/<FacultyController>/5
        [HttpGet("{id}")]
        public Faculty GetFacultyById(string id)
        {
            FacultyDomain fd = new FacultyDomain();
            Faculty result = fd.GetFaculty(id);
            return result;
        }

        [HttpGet("getAllFaculty")]
        public List<Faculty> GetAllFaculty()
        {
            FacultyDomain fd = new FacultyDomain();
            List<Faculty> result = fd.GetAllFaculty();
            return result;
        }

        [HttpPost("createFaculty")]
        public Faculty CreateFacultyProfile(Faculty f)
        {
            FacultyDomain fd = new FacultyDomain();
            fd.CreateFaculty(f);
            return f;
        }

        // PUT api/<FacultyController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FacultyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
