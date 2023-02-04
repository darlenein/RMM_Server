using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RMM_Server.Domains;
using RMM_Server.Models;
using RMM_Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RMM_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // GET: api/<StudentController>
        [HttpGet("getStudentById/{id}")]
        public Student GetStudentById(string id)
        {
            StudentDomain sd = new StudentDomain();
            Student result = sd.GetStudent(id);
            return result;
        }

        [HttpGet("getAllStudents")]
        public List<Student> GetAllStudents()
        {
            StudentDomain sd = new StudentDomain();
            List<Student> result = sd.GetAllStudent();
            return result;
        }

        [HttpGet("getAllStudentsByResearch/{research_id}")]
        public List<Student> GetAllStudentsByResearch(int research_id)
        {
            StudentDomain sd = new StudentDomain();
            List<Student> result = sd.GetAllStudentsByResearch(research_id);
            return result;
        }

        [HttpPost("createStudent")]
        public Student CreateStudentProfile(Student s)
        {
            StudentDomain sd = new StudentDomain();
            sd.CreateStudent(s);
            return s;
        }

        [HttpPut("editStudent")]
        public Student EditStudentProfile(Student s)
        {
            StudentDomain sd = new StudentDomain();
            sd.EditStudent(s);
            return s;
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
