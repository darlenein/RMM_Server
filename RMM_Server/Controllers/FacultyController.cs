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
    public class FacultyController : ControllerBase
    {
        // dependecy injection
        private readonly IFacultyDomain ifd;
        public FacultyController(IFacultyDomain ifd)
        {
            this.ifd = ifd;
        }

        // GET api/<FacultyController>/5
        [HttpGet("{id}")]
        public Faculty GetFacultyById(string id)
        {
            Faculty result = ifd.GetFaculty(id);
            return result;
        }

        [HttpGet("getAllFaculty")]
        public List<Faculty> GetAllFaculty()
        {
            List<Faculty> result = ifd.GetAllFaculty();
            return result;
        }

        [HttpPost("createFaculty")]
        public Faculty CreateFacultyProfile(Faculty f)
        {
            Faculty result = ifd.CreateFaculty(f);
            return result;
        }

        [HttpPut("editFaculty")]
        public void EditFacultyProfile(Faculty f)
        {
           ifd.EditFaculty(f);
            
        }

        [HttpPost("GetFilteredAndSearchFacultyList")]
        public List<Faculty> GetFilteredAndSearchFacultyList(FacultyFilter ff)
        {
            List<Faculty> result = ifd.GetFilteredAndSearchedFaculty(ff);
            return result;
        }

    /*    [HttpPost("getFilteredFacultyList")]
        public List<Faculty> getFilteredFacultyList(FacultyFilter ff)
        {
            List<Faculty> result = isd.GetFilteredFaculty(ff);
            return result;
        }
    */

        [HttpPost("getSearchedFacultyList/{keyword}")]
        public List<Faculty> GetSearchedFacultyList(string keyword, List<Faculty> faculty)
        {
            List<Faculty> result = ifd.GetSearchedFacultyByKeyword(keyword, faculty);
            return result;
        }

    }
}
