using RMM_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Contracts
{
    public interface IResearchDomain
    {
        public List<Research> GetResearchByFacultyId(string f_id); //[x], means documented test
        public Research GetResearchByID(int id); //[x]
        public List<Research> GetAllResearch(); //[x]
        public List<Research> GetAllResearchByStudentId(string s_id); //[x]
        public Participant AddResearchApplicant(Participant p); //[x]
        public void DeleteResearchApplicant(int rID, string sID);
        public void UpdateApplicantProgress(int p, int rID, string sID); //[x]
        public Participant GetAppProgression(int rID, string sID);
        public Research AddResearch(Research r); //[x]
        public void EditResearch(Research r);
        public void DeleteResearchDeptByResearchID(int ID); //[x]
        public int GetLastIDFromResearch(); //[x]
        public List<Research> GetSortedResearchesByStudentId(string s); // incomplete method
        public List<Research> GetFilteredAndSearchedResearch(Filter f); //[x][x][x]
        public List<Research> GetSearchedResearchByKeyword(string keyword, List<Research> research); //[x]
        public List<Research> GetFilteredResearch(Filter f); //[x]

        public List<Research> MatchResearchToStudent(string student_id); //[x][x]
        public List<Research> ConvertDateTimeToDate(List<Research> research); //[x][x]
        public int ConvertBoolToInt(bool b); //[x][x]
    }
}
