using RMM_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Contracts
{
    public interface IResearchDomain
    {
        public List<Research> GetResearchByFacultyId(string f_id);
        public Research GetResearchByID(int id);
        public List<Research> GetAllResearch();
        public List<Research> GetAllResearchByStudentId(string s_id);
        public Participant AddResearchApplicant(Participant p);
        public void DeleteResearchApplicant(int rID, string sID);
        public void UpdateApplicantProgress(int p, int rID, string sID);
        public Participant GetAppProgression(int rID, string sID);
        public Research AddResearch(Research r);
        public Research EditResearch(Research r);
        public void DeleteResearchDeptByResearchID(int ID);
        public int GetLastIDFromResearch();
        public List<Research> GetSortedResearchesByStudentId(string s);
        public List<Research> GetFilteredAndSearchedResearch(Filter f);
        public List<Research> GetSearchedResearchByKeyword(string keyword, List<Research> research);
        public List<Research> GetFilteredResearch(Filter f);

        public List<Research> MatchResearchToStudent(string student_id);
        public List<Research> ConvertDateTimeToDate(List<Research> research);
        public int ConvertBoolToInt(bool b);
    }
}
