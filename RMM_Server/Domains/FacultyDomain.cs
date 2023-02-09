using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using RMM_Server.Contracts;
using RMM_Server.Models;
using RMM_Server.Services;

namespace RMM_Server.Domains
{
    public class FacultyDomain : IFacultyDomain
    {
        private readonly IFacultyRepository ifr;

        public FacultyDomain(IFacultyRepository ifr)
        {
            this.ifr = ifr;
        }
        public Faculty GetFaculty(string id)
        {
            Faculty result = ifr.GetFaculty(id);
            return result;
        }

        public List<Faculty> GetAllFaculty()
        {
            List<Faculty> result = ifr.GetAllFaculty();
            return result;
        }

        public Faculty CreateFaculty(Faculty f)
        {
            Faculty result = ifr.CreateFaculty(f);
            return result;
        }

        public Faculty EditFaculty(Faculty f)
        {
            Faculty result = ifr.EditFaculty(f);
            return result;
        }

        public void DeleteFacultyByID(string id)
        {
            ifr.DeleteFacultyByID(id);
        }

        public List<Faculty> GetFilteredAndSearchedFaculty(FacultyFilter ff)
        {
            List<Faculty> result;
            if (ff.keyword == "")
            {
                result = GetAllFaculty();
            }

            else
            {
                result = GetSearchedFacultyByKeyword(ff.keyword, ff.faculty);
                ff.faculty = result;
              //  if (ff.facultyFilterValue.Count > 0) result = GetFilteredFaculty(ff);
            }

            result = result.GroupBy(x => x.Faculty_Id).OrderByDescending(c => c.Count()).SelectMany(c => c.Select(x => x)).Distinct().ToList();
            return result;
        }

        //Filtered Students Method
      /*  public List<Faculty> GetFilteredFaculty(FacultyFilter ff)
        {
            List<Faculty> filteredResults = new List<Faculty>();
            List<Faculty> temp = new List<Faculty>();

            foreach (FacultyFilterValue ffv in ff.facultyFilterValue)
            {
                if (ffv.categoryValue == "Major")
                {
                    temp = ff.student.Where(x => x.Major == sfv.checkedValue).ToList();

                }
                if (sfv.categoryValue == "Location")
                {
                    temp = sf.student.Where(x => x.PreferLocation == sfv.checkedValue).ToList();
                }
                if (sfv.categoryValue == "Incentive")
                {
                    if (sfv.checkedValue == "Paid") temp = sf.student.Where(x => x.PreferPaid == true).ToList();
                    if (sfv.checkedValue == "Nonpaid") temp = sf.student.Where(x => x.PreferNonpaid == true).ToList();
                    if (sfv.checkedValue == "Credit") temp = sf.student.Where(x => x.PreferCredit == true).ToList();
                }
                filteredResults.AddRange(temp);
            }

            return filteredResults;
        } 
      */

        //keyword student = DONE
        public List<Faculty> GetSearchedFacultyByKeyword(string keyword, List<Faculty> faculty)
        {
            FacultySearchService fs = new FacultySearchService();
            List<Faculty> temp = fs.Search(keyword, faculty);
            var searchedResults = temp.Where(x => x.SearchScore > 0).OrderByDescending(x => x.SearchScore).ToList();
            return searchedResults;
        }
    }
}
