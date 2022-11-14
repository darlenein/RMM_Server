using Microsoft.AspNetCore.Mvc;
using ProfileManager.Domains;
using ProfileManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProfileManager.Controllers
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
            Faculty result = fd.getFaculty(id);
            return result;
        }

        // POST api/<FacultyController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
