using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RMM_Server.Contracts;
using RMM_Server.Domains;
using RMM_Server.Models;
using RMM_Server.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RMM_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentDomain isd;

        public StudentController(IStudentDomain isd)
        {
            this.isd = isd;
        }

        // GET: api/<StudentController>
        [HttpGet("getStudentById/{id}")]
        public Student GetStudentById(string id)
        {
            Student result = isd.GetStudent(id);
            return result;
        }

        [HttpGet("getAllStudents")]
        public List<Student> GetAllStudents()
        {
            List<Student> result = isd.GetAllStudent();
            return result;
        }

        [HttpGet("getAllStudentsByResearch/{research_id}")]
        public List<Student> GetAllStudentsByResearch(int research_id)
        {
            List<Student> result = isd.GetAllStudentsByResearch(research_id);
            return result;
        }

        [HttpPost("createStudent")]
        public Student CreateStudentProfile(Student s)
        {
            Student result = isd.CreateStudent(s);
            return result;
        }

        [HttpPut("editStudent")]
        public void EditStudentProfile(Student s)
        {
            isd.EditStudent(s);
            
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

        //Added by Jiawen

        [HttpPost("GetFilteredAndSearchStudentList")]
        public List<Student> GetFilteredAndSearchStudentList(StudentFilter sf)
        {
            List<Student> result = isd.GetFilteredAndSearchedStudents(sf);
            return result;
        }

        [HttpPost("getFilteredStudentList")]
        public List<Student> getFilteredStudentList(StudentFilter sf)
        {
            List<Student> result = isd.GetFilteredStudents(sf);
            return result;
        }

        [HttpPost("getSearchedStudentList/{keyword}")]
        public List<Student> GetSearchedResearchList(string keyword, List<Student> student)
        {
            List<Student> result = isd.GetSearchedStudentByKeyword(keyword, student);
            return result;
        }

        [HttpPost("insertIntoStudentHiddenResearch")]
        public IActionResult InsertIntoStudentHiddenResearch(string student_id, int research_id)
        {
            isd.InsertIntoStudentHiddenResearch(student_id, research_id);
            return Ok();
        }

        [HttpGet("getAllRankedStudentsByResearch/{research_id}")]
        public List<Student> GetAllRankedStudentsByResearch(int research_id)
        {
            List<Student> result = isd.GetAllRankedStudentsByResearch(research_id);
            return result;
        }

        [HttpPost("uploadStudentPicture/{student_id}"), DisableRequestSizeLimit]
        public IActionResult UploadStudentPicture(string student_id)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "StudentImages");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                Directory.CreateDirectory(pathToSave);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    isd.UpdateStudentProfileImage(student_id, fullPath);
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
            return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        /*[HttpGet("getAllStudentSorted/{faculty_id}")]
        public List<Student> GetAllStudentSorted(string faculty_id)
        {
            List<Student> result = isd.GetSortedStudentsByFacultyID(faculty_id);
            return result;
        }*/
    }
}
