using MySql.Data.MySqlClient;
using RMM_Server.Contracts;
using RMM_Server.Models;
using RMM_Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Domains
{
    public class ResearchDomain : IResearchDomain
    {
        private readonly IResearchRepository irr;
        private readonly IStudentRepository isr;
        private readonly IDepartmentRepository idr;

        public ResearchDomain(IResearchRepository irr, IStudentRepository isr, IDepartmentRepository idr)
        {
            this.irr = irr;
            this.isr = isr;
            this.idr = idr;
        }

        public List<Research> GetResearchByFacultyId(string f_id)
        {
            List<Research> result = irr.GetResearchByFacultyId(f_id);
            return result;
        }

        public Research GetResearchByID(int id)
        {
            Research result = irr.GetResearchByID(id);
            if (result.Start_Date != null) result.Start_Date = DateTime.Parse(result.Start_Date).ToString("MM/dd/yyyy");
            if (result.End_Date != null) result.End_Date = DateTime.Parse(result.End_Date).ToString("MM/dd/yyyy");
            return result;
        }

        public List<Research> GetAllResearch()
        {
            List<Research> result = irr.GetAllResearch();
            result = ConvertDateTimeToDate(result);
            return result;
        }

        public List<Research> GetAllResearchByStudentId(string s_id)
        {
            List<Research> result = irr.GetAllResearchByStudentId(s_id);
            result = ConvertDateTimeToDate(result);
            return result;
        }

        public Participant AddResearchApplicant(Participant p)
        {
            Participant result = irr.AddResearchApplicant(p);
            return result;
        }

        public void DeleteResearchApplicant(int rID, string sID)
        {
            irr.DeleteResearchApplicant(rID, sID);
        }

        public void UpdateApplicantProgress(int p, int rID, string sID)
        {
            irr.UpdateApplicantProgress(p, rID, sID);
        }

        public Participant GetAppProgression(int rID, string sID)
        {
            Participant result = irr.GetAppProgression(rID, sID);
            return result;
        }

        public Research AddResearch(Research r)
        {
            Research result = irr.AddResearch(r);
            return result;
        }

        public Research EditResearch(Research r)
        {
            Research result = irr.EditResearch(r);
            return result;
        }

        public void DeleteResearchDeptByResearchID(int ID)
        {
            irr.DeleteResearchDeptByResearchID(ID);
        }

        public int GetLastIDFromResearch()
        {
            int result = irr.GetLastIDFromResearch();
            return result;
        }

        public void DeleteResearchByID(int id)
        {
            irr.DeleteResearchByID(id);
        }

        public List<Research> GetSortedResearchesByStudentId(string s)
        {
            List<Research> result = GetAllResearch();
            Student student = isr.GetStudent(s);

            //Have to get each researches list
            foreach (Research r in result)
            {
                r.ResearchDepts = idr.GetSubDeptByResearchId(r.Research_Id);
            }

            var sortedByMinor = result.OrderByDescending(x => x.Research_Id).ThenByDescending(x => x.Location == student.PreferLocation).ThenBy(x => x.ResearchDepts[0] == student.Minor).ThenBy(x => x.ResearchDepts[1] == student.Minor).ThenBy(x => x.ResearchDepts[2] == student.Minor).ToList();
            var sortedByMajor = sortedByMinor.OrderByDescending(x => x.ResearchDepts[0] == student.Major).ThenBy(x => x.ResearchDepts[1] == student.Major && x.Active == true).ThenBy(x => x.ResearchDepts[2] == student.Major && x.Active == true).ToList();
            var sortedByStatus = sortedByMajor.OrderByDescending(x => x.Active == true).ToList();

            return sortedByStatus;
        }

        public List<Research> GetFilteredAndSearchedResearch(Filter f)
        {
            List<Research> result;
            if (f.keyword == "" && f.filterValue.Count > 0)
            {
                result = GetFilteredResearch(f);
            }
            else if (f.keyword == "" && f.filterValue.Count == 0)
            {
                if (f.psuID == "") result = irr.GetAllResearch();
                else result = GetSortedResearchesByStudentId(f.psuID);
            }
            else
            {
                result = GetSearchedResearchByKeyword(f.keyword, f.research);
                f.research = result;
                if (f.filterValue.Count > 0) result = GetFilteredResearch(f);
            }

            result = result.GroupBy(x => x.Research_Id).OrderByDescending(c => c.Count()).SelectMany(c => c.Select(x => x)).Distinct().ToList();
            return result;
        }

        public List<Research> GetSearchedResearchByKeyword(string keyword, List<Research> research)
        {
            SearchService ss = new SearchService();
            List<Research> temp = ss.Search(keyword, research);
            var searchedResults = temp.Where(x => x.SearchScore > 0).OrderByDescending(x => x.SearchScore).ToList();
            return searchedResults;
        }

        public List<Research> GetFilteredResearch(Filter f)
        {
            List<Research> filteredResults = new List<Research>();
            List<Research> temp = new List<Research>();
            
            foreach(FilterValue fv in f.filterValue)
            {
                if(fv.categoryValue == "Department")
                {
                    temp = f.research.Where(x => x.ResearchDepts[0] == fv.checkedValue || x.ResearchDepts[1] == fv.checkedValue || x.ResearchDepts[2] == fv.checkedValue).ToList();
                }
                if(fv.categoryValue == "Status")
                {
                    bool value = Convert.ToBoolean(fv.checkedValue);
                    temp = f.research.Where(x => x.Active == value).ToList();
                }
                if (fv.categoryValue == "Location")
                {
                    temp = f.research.Where(x => x.Location == fv.checkedValue).ToList();
                }
                if (fv.categoryValue == "Incentive")
                {
                    if (fv.checkedValue == "Paid") temp = f.research.Where(x => x.IsPaid == true).ToList();
                    if (fv.checkedValue == "Nonpaid") temp = f.research.Where(x => x.IsNonpaid == true).ToList();
                    if (fv.checkedValue == "Credit") temp = f.research.Where(x => x.IsCredit == true).ToList();
                }
                filteredResults.AddRange(temp);
            }

                return filteredResults;
        }

        public List<Research> ConvertDateTimeToDate(List<Research> research)
        {
            foreach(Research r in research)
            {
                if(r.Start_Date != null) r.Start_Date = DateTime.Parse(r.Start_Date).ToString("MM/dd/yyyy");
                if (r.End_Date != null) r.End_Date = DateTime.Parse(r.End_Date).ToString("MM/dd/yyyy");
            }
            return research;
        }

        public int ConvertBoolToInt(bool b)
        {
            if (b == true) return 1;
            else return 0;
        }
    }
}
