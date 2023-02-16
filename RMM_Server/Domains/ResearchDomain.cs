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
        private readonly IStudentDomain isd;
        private readonly IDepartmentDomain idd;

        public ResearchDomain(IResearchRepository irr, IStudentDomain isd, IDepartmentDomain idd)
        {
            this.irr = irr;
            this.isd = isd;
            this.idd = idd;
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
            Student student = isd.GetStudent(s);

            //Have to get each researches list
            foreach (Research r in result)
            {
                r.ResearchDepts = idd.GetSubDeptByResearchId(r.Research_Id);
            }

            var sortedByMinor = result.OrderByDescending(x => x.Research_Id).ThenByDescending(x => x.Location == student.PreferLocation).ThenBy(x => x.ResearchDepts[0] == student.Minor).ThenBy(x => x.ResearchDepts[1] == student.Minor).ThenBy(x => x.ResearchDepts[2] == student.Minor).ToList();
            var sortedByMajor = sortedByMinor.OrderByDescending(x => x.ResearchDepts[0] == student.Major).ThenBy(x => x.ResearchDepts[1] == student.Major && x.Active == true).ThenBy(x => x.ResearchDepts[2] == student.Major && x.Active == true).ToList();
            var sortedByStatus = sortedByMajor.OrderByDescending(x => x.Active == true).ToList();

            return sortedByStatus;
        }
        

        public List<Research> GetFilteredAndSearchedResearch(Filter f)
        {
            List<Research> result;
            if (f.Keyword == "" && f.FilterValue.Count > 0)
            {
                result = GetFilteredResearch(f);
            }
            else if (f.Keyword == "" && f.FilterValue.Count == 0)
            {
                if (f.PsuID == "") result = irr.GetAllResearch();
                else result = MatchResearchToStudent(f.PsuID);
            }
            else
            {
                result = GetSearchedResearchByKeyword(f.Keyword, f.Research);
                f.Research = result;
                if (f.FilterValue.Count > 0) result = GetFilteredResearch(f);
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
            
            foreach(FilterValue fv in f.FilterValue)
            {
                if(fv.CategoryValue == "Department")
                {
                    List<Research> departmentFilterList = new List<Research>();                   
                    foreach(Research r in f.Research)
                    {
                        foreach(string s in r.ResearchDepts)
                        {
                            if(s == fv.CheckedValue)
                            {
                                departmentFilterList.Add(r);
                                break;
                            }
                        }
                    }
                    temp = departmentFilterList;
                }
                if(fv.CategoryValue == "Status")
                {
                    bool value = Convert.ToBoolean(fv.CheckedValue);
                    temp = f.Research.Where(x => x.Active == value).ToList();
                }
                if (fv.CategoryValue == "Location")
                {
                    temp = f.Research.Where(x => x.Location == fv.CheckedValue).ToList();
                }
                if (fv.CategoryValue == "Incentive")
                {
                    if (fv.CheckedValue == "Paid") temp = f.Research.Where(x => x.IsPaid == true).ToList();
                    if (fv.CheckedValue == "Nonpaid") temp = f.Research.Where(x => x.IsNonpaid == true).ToList();
                    if (fv.CheckedValue == "Credit") temp = f.Research.Where(x => x.IsCredit == true).ToList();
                }
                filteredResults.AddRange(temp);
            }

                return filteredResults;
        }

        public List<Research> MatchResearchToStudent(string student_id)
        {
            MatchService ms = new MatchService();
            List<Research> research = GetAllResearch();
            List<Research> result = new List<Research>();
            Student s = isd.GetStudent(student_id);
            string[] tokenized_StudentSkills = s.Skills.Split(';');
            s.Skills = s.Skills.Trim();
            float match_score = 0;

            // get all research by student's major, major2, and minor
            foreach (Research r in research)
            {
                foreach(string dept in r.ResearchDepts)
                {
                    if(dept == s.Major || (dept == s.Major2 && s.Major2 != null) || (dept == s.Minor && s.Minor != null))
                    {
                        result.Add(r);
                        break;
                    }
                }
            }

            foreach(Research r in result)
            {
                match_score = 0;
                // if research_location == student's preferred location, match_score +0.1
                if(r.Location == s.PreferLocation)
                {
                    match_score += (float)0.1;
                }
                // if research isCredit, isPaid, isNonpaid == student's, foreach match_score +0.1
                if (r.IsPaid == s.PreferPaid && s.PreferPaid == true)
                {
                    match_score += (float)0.05;
                }
                if (r.IsNonpaid == s.PreferNonpaid && s.PreferNonpaid == true)
                {
                    match_score += (float)0.05;
                }
                if (r.IsCredit == s.PreferCredit && s.PreferCredit == true)
                {
                    match_score += (float)0.05;
                }

                // get list of required skills, if matches student +0.2 and remove from student skill list 
                r.Required_Skills = r.Required_Skills.Trim();
                if (r.Required_Skills != ";" && s.Skills != ";")
                {
                    // if skills not empty, get list of student skill, noramlize, and tokenize 
                    tokenized_StudentSkills = ms.TokenizeString(s.Skills);
                    string[] required_skills = ms.TokenizeString(r.Required_Skills);
                    foreach (string rs in required_skills)
                    {
                        int counter = 0;
                        foreach (string ss in tokenized_StudentSkills)
                        {
                            if (rs == ss)
                            {
                                match_score += (float)0.2;
                                tokenized_StudentSkills = tokenized_StudentSkills.Where(x => x != tokenized_StudentSkills[counter]).ToArray();
                            }
                            counter++;
                        }
                    }
                }

                // get list of encouraged skills, if matches student +0.1, and remove from list 
                r.Encouraged_Skills = r.Encouraged_Skills.Trim();
                if (r.Encouraged_Skills != ";" && s.Skills != ";")
                {
                    // if skills not empty, get list of student skill, noramlize, and tokenize 
                    tokenized_StudentSkills = ms.TokenizeString(s.Skills);
                    string[] encouraged_skills = ms.TokenizeString(r.Encouraged_Skills);
                    foreach (string es in encouraged_skills)
                    {
                        int counter = 0;
                        foreach (string ss in tokenized_StudentSkills)
                        {
                            if (es == ss)
                            {
                                match_score += (float)0.1;
                                tokenized_StudentSkills = tokenized_StudentSkills.Where(x => x != tokenized_StudentSkills[counter]).ToArray();
                            }
                            counter++;
                        }
                    }
                }
                

                r.MatchScore = match_score;
            }

            // sort list by match_score
            result = result.OrderByDescending(x => x.MatchScore).ToList();

            return result;
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
