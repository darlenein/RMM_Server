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
    public class ResearchController : ControllerBase
    {
        // gets
        [HttpGet("getResearchByFaculty/{faculty_id}")]
        public List<Research> GetResearchByFaculty(string faculty_id)
        {
            ResearchDomain rd = new ResearchDomain();
            List<Research> result = rd.GetResearchByFacultyId(faculty_id);
            return result;
        }

        [HttpGet("getAllResearch")]
        public List<Research> GetAllResearch()
        {
            ResearchDomain rd = new ResearchDomain();
            List<Research> result = rd.GetAllResearch();
            return result;
        }

        [HttpGet("getAllResearchSorted/{student_id}")]
        public List<Research> GetAllResearchSorted(string student_id)
        {
            ResearchDomain rd = new ResearchDomain();
            List<Research> result = rd.GetSortedResearchesByStudentId(student_id);
            return result;
        }

        [HttpGet("getAllResearchByStudent/{student_id}")]
        public List<Research> GetAllResearchByStudentId(string student_id)
        {
            ResearchDomain rd = new ResearchDomain();
            List<Research> result = rd.GetAllResearchByStudentId(student_id);
            return result;
        }

        [HttpGet("getSearchedResearchList/{keyword}")]
        public List<Research> GetSearchedResearchList(string keyword)
        {
            ResearchDomain rd = new ResearchDomain();
            List<Research> result = rd.GetSearchedResearchByKeyword(keyword);
            return result;
        }

        // post
        [HttpPost("createResearch")]
        public Research CreateResearch(Research r)
        {
            ResearchDomain rd = new ResearchDomain();
            rd.AddResearch(r);
            return r;
        }

        [HttpPost("addResearchApplicant")]
        public Progress CreateResearchApplicant(Progress p)
        {
            ResearchDomain rd = new ResearchDomain();
            rd.AddResearchApplicant(p);
            return p;
        }

        // PUT api/<ResearchController>/5
        [HttpPut("updateAppProgress")]
        public void Put(Progress p)
        {
            ResearchDomain r = new ResearchDomain();
            r.UpdateApplicantProgress(p.progress, p.research_id, p.student_id);
        }


        // DELETE api/<ResearchController>/5
        [HttpDelete("deleteResearchApplicant")]
        public void DeleteResearchApp(Progress p)
        {
            ResearchDomain r = new ResearchDomain();
            r.DeleteResearchApplicant(p.research_id, p.student_id);
        }
    }
}
