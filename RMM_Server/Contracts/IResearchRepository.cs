using RMM_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Contracts
{
    public interface IResearchRepository
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
        public void EditResearch(Research r);
        public void DeleteResearchDeptByResearchID(int ID);
        public int GetLastIDFromResearch();
    }
}
