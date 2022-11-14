using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class StudentController : ControllerBase
    {
        private readonly ILogger _logger;
        // GET: api/<StudentController>
        [HttpGet("{id}")]
        public Student GetStudentById(string id)
        {
            StudentDomain fd = new StudentDomain();
            Student result = fd.getStudent(id);
            return result;
        }

        // POST api/<StudentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
