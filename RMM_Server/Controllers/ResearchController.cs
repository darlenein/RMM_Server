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
    public class ResearchController : ControllerBase
    {
        private readonly IResearchDomain ird;

        public ResearchController(IResearchDomain ird)
        {
            this.ird = ird;
        }

        // gets
        [HttpGet("getResearchByFaculty/{faculty_id}")]
        public List<Research> GetResearchByFaculty(string faculty_id)
        {
            List<Research> result = ird.GetResearchByFacultyId(faculty_id);
            return result;
        }

        [HttpGet("getResearchByID/{id}")]
        public Research GetResearchById(int id)
        {
            Research result = ird.GetResearchByID(id);
            return result;
        }

        [HttpGet("getAllResearch")]
        public List<Research> GetAllResearch()
        {
            List<Research> result = ird.GetAllResearch();
            return result;
        }

        [HttpGet("getAllResearchSorted/{student_id}")]
        public List<Research> GetAllResearchSorted(string student_id)
        {
            List<Research> result = ird.GetSortedResearchesByStudentId(student_id);
            return result;
        }

        [HttpGet("getMatchedResearches/{student_id}")]
        public List<Research> GetMatchedResearchesByStudent(string student_id)
        {
            List<Research> result = ird.MatchResearchToStudent(student_id);
            return result;
        }

        [HttpGet("getAllResearchByStudent/{student_id}")]
        public List<Research> GetAllResearchByStudentId(string student_id)
        {
            List<Research> result = ird.GetAllResearchByStudentId(student_id);
            return result;
        }

        // post
        [HttpPost("getSearchedResearchList/{keyword}")]
        public List<Research> GetSearchedResearchList(string keyword, List<Research> research)
        {
            List<Research> result = ird.GetSearchedResearchByKeyword(keyword, research);
            return result;
        }

        [HttpPost("getFilteredAndSearchedResearchList")]
        public List<Research> GetFilteredAndSearchedResearchList(Filter f)
        {
            List<Research> result = ird.GetFilteredAndSearchedResearch(f);
            return result;
        }

        [HttpPost("getFilteredResearchList")]
        public List<Research> GetFilteredResearchList(Filter f)
        {
            List<Research> result = ird.GetFilteredResearch(f);
            return result;
        }

        [HttpPost("createResearch")]
        public Research CreateResearch(Research r)
        {
            Research result = ird.AddResearch(r);
            return result;
        }

        [HttpPost("addResearchApplicant")]
        public Participant CreateResearchApplicant(Participant p)
        {
            Participant result = ird.AddResearchApplicant(p);
            return result;
        }

        // PUT api/<ResearchController>/5
        [HttpPut("updateAppProgress")]
        public void Put(Participant p)
        {
            ird.UpdateApplicantProgress(p.Progress_Bar, p.Research_id, p.Student_id);
        }

        [HttpPut("editResearch")]
        public void EditResearchPage(Research r)
        {
            ird.EditResearch(r);
            
        }

        // DELETE api/<ResearchController>/5
        [HttpDelete("deleteResearchApplicant")]
        public void DeleteResearchApp(Participant p)
        {
            ird.DeleteResearchApplicant(p.Research_id, p.Student_id);
        }

        [HttpGet("getHiddenResearchByStudentId/{student_id}")]
        public List<Research> GetHiddenResearchByStudentId(string student_id)
        {
            var result = ird.GetHiddenResearchesByStudentId(student_id);
            return result;
        }

        [HttpDelete("deleteHiddenResearch")]
        public IActionResult DeleteHiddenResearches(int research_id, string student_id)
        {
            ird.DeleteHiddenResearch(research_id, student_id);
            return Ok();
        }
    }
}
