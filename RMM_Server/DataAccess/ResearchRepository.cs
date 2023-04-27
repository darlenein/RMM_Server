using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RMM_Server.Contracts;
using RMM_Server.Models;
using RMM_Server.Services;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RMM_Server.DataAccess
{
    public class ResearchRepository : IResearchRepository
    {
        private readonly string connectionString;
        private readonly IDepartmentDomain idd;

        public ResearchRepository(IConfiguration config, IDepartmentDomain idd)
        {
            // find param ConnectionString in appsettings.json file 
            connectionString = config.GetConnectionString("ConnectionString");

            // injecting dependency to use method from department domain
            this.idd = idd;
        }

        public List<Research> GetResearchByFacultyId(string f_id)
        {
            List<Research> rl = new List<Research>();

            // Connect to database 
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"SELECT a.*, b.first_name, b.last_name " +
                    $"FROM research AS a JOIN faculty as B ON a.faculty_id = b.faculty_id " +
                    $"WHERE a.faculty_id = '{f_id}'";
                rl = connection.Query<Research>(query, null).ToList();
            };
            return rl;
        }

        public Research GetResearchByID(int id)
        {
            Research r;
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"SELECT a.*, b.first_name, b.last_name FROM research AS a " +
                    $"JOIN faculty as B ON a.faculty_id = b.faculty_id WHERE research_id = '{id}'";
                r = connection.Query<Research>(query, null).FirstOrDefault();

                r.ResearchDepts = idd.GetSubDeptByResearchId(r.Research_Id);
            };
            return r;
        }

        public List<Research> GetAllResearch()
        {
            List<Research> rl = new List<Research>();

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"SELECT a.*, b.first_name, b.last_name, b.faculty_id FROM research AS a " +
                    $"JOIN faculty as B ON a.faculty_id = b.faculty_id;";
                rl = connection.Query<Research>(query, null).ToList();
            };

            foreach (Research r in rl)
            {
                r.ResearchDepts = idd.GetSubDeptByResearchId(r.Research_Id);
            }

            rl = rl.OrderByDescending(x => x.Research_Id).ToList();

            return rl;
        }

        public List<Research> GetAllResearchByStudentId(string s_id)
        {
            List<Research> rl = new List<Research>();

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"SELECT a.*, c.first_name, c.last_name, b.progress_bar FROM research AS a " +
                    $"JOIN participant AS b ON a.research_id = b.research_id " +
                    $"JOIN faculty AS c ON a.faculty_id = c.faculty_id " +
                    $"WHERE b.student_id = '{s_id}'";
                rl = connection.Query<Research>(query, null).ToList();
            };
            return rl;
        }

        public Participant AddResearchApplicant(Participant p)
        {
            Participant progress;
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"INSERT into participant VALUES ({p.Research_id}, '{p.Student_id}', 0)";
                progress = connection.Query<Participant>(query, null).FirstOrDefault();
            };
            return progress;
        }

        public void DeleteResearchApplicant(int rID, string sID)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"DELETE from participant " +
                    $"WHERE research_id = {rID} AND student_id = '{sID}'";
                connection.Execute(query, null);
            };

        }

        public void UpdateApplicantProgress(int p, int rID, string sID)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"UPDATE participant " +
                    $"SET progress_bar = {p} " +
                    $"WHERE research_id = {rID} AND student_id = '{sID}'";
                connection.Execute(query, null);
            };
        }

        public Participant GetAppProgression(int rID, string sID)
        {
            Participant p = new Participant();
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"SELECT * FROM participant " +
                    $"WHERE research_id = {rID} AND student_id = '{sID}'";
                p = connection.Query<Participant>(query, null).FirstOrDefault();
            };
            return p;
        }

        public Research AddResearch(Research r)
        {
            Research rl;
            int paid = ConvertBoolToInt(r.IsPaid);
            int nonpaid = ConvertBoolToInt(r.IsNonpaid);
            int credit = ConvertBoolToInt(r.IsCredit);
            int active = ConvertBoolToInt(r.Active);

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                // add research to db
                string query = $"INSERT into research VALUES (" +
                    $" null, '{r.Faculty_Id}', '{r.Name}', '{r.Description}', '{r.Location}', '{r.Required_Skills}'," +
                    $" '{r.Encouraged_Skills}', '{r.Start_Date}', '{r.End_Date}', '{active}', '{r.Address}', '{paid}'," +
                    $" '{nonpaid}', '{credit}', '{r.RequiredSkillLevel}', '{r.EncouragedSkillLevel}')";
                rl = connection.Query<Research>(query, null).FirstOrDefault();
            };

            int r_Id = GetLastIDFromResearch();

            // add associated depts to research
            foreach (string dept in r.ResearchDepts)
            {
                int d_id = 0;

                using (IDbConnection connection = new MySqlConnection(connectionString))
                {
                    string query = $"SELECT subdepartment_id from subdepartment WHERE name = '{dept}'";
                    d_id = connection.Query<int>(query, null).FirstOrDefault();
                    query = $"INSERT into researchdept VALUES ('{r_Id}', '{d_id}')";
                    connection.Execute(query, null);
                };
            }

            return rl;
        }

        public void EditResearch(Research r)
        {
           
            int paid = ConvertBoolToInt(r.IsPaid);
            int nonpaid = ConvertBoolToInt(r.IsNonpaid);
            int credit = ConvertBoolToInt(r.IsCredit);
            int active = ConvertBoolToInt(r.Active);

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                // edit research
                string query = $"UPDATE research " +
                    $"SET name = '{r.Name}', description = '{r.Description}', location = '{r.Location}', required_skills = '{r.Required_Skills}', " +
                    $"encouraged_skills = '{r.Encouraged_Skills}', start_date = '{r.Start_Date}', end_date = '{r.End_Date}', " +
                    $"active = {active}, address = '{r.Address}', isPaid = {paid}, isNonpaid = {nonpaid}, isCredit = {credit}, requiredSkillLevel = '{r.RequiredSkillLevel}', encouragedSkillLevel = '{r.EncouragedSkillLevel}' " +
                    $"WHERE research_id = '{r.Research_Id}'";
                connection.Execute(query, null);
            };

            // delete all depts associated to research ID
            DeleteResearchDeptByResearchID(r.Research_Id);

            // add associated depts to research
            foreach (string dept in r.ResearchDepts)
            {
                int d_id = 0;

                using (IDbConnection connection = new MySqlConnection(connectionString))
                {
                    // edit research
                    string query = $"SELECT subdepartment_id from subdepartment WHERE name = '{dept}'";
                    d_id = connection.Query<int>(query, null).FirstOrDefault();
                    query = $"INSERT into researchdept VALUES ('{r.Research_Id}', '{d_id}')";
                    connection.Execute(query, null);
                };
            }
            
        }

        public void DeleteResearchDeptByResearchID(int ID)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"DELETE from researchdept WHERE research_id = '{ID}'";
                connection.Execute(query, null);
            };
        }

        public int GetLastIDFromResearch()
        {
            int ID = 0;
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"SELECT * FROM research ORDER BY research_id desc LIMIT 1";
                ID = connection.Query<int>(query, null).FirstOrDefault();
            };
            return ID;
        }

        public List<int> GetHiddenResearchesId(string student_id)
        {
            List<int> exclusions = new List<int>();
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"SELECT research_id from hidden_research WHERE student_id = '{student_id}'";
                exclusions = connection.Query<int>(query, null).ToList();
            };

            return exclusions;
        }

        public List<Research> GetHiddenResearchesByStudentId(string student_id)
        {
            var hiddenResearches = new List<Research>();
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"SELECT a.*, c.first_name, c.last_name FROM research a " +
                    $"JOIN hidden_research b ON a.research_id = b.research_id " +
                    $"JOIN faculty c ON a.faculty_id = c.faculty_id where b.student_id = '{student_id}'";
                hiddenResearches = connection.Query<Research>(query, null).ToList();
            };

            return hiddenResearches;
        }

        public void DeleteHiddenResearch(int research_id, string student_id)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"DELETE from hidden_research WHERE research_id = {research_id} AND student_id = '{student_id}'";
                connection.Execute(query, null);
            };
        }

        public int ConvertBoolToInt(bool b)
        {
            if (b == true) return 1;
            else return 0;
        }

        public List<int> GetAppliedResearchesByStudentId(string student_id)
        {
            var appliedResearches = new List<int>();
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"SELECT research_id from participant WHERE student_id = '{student_id}'";
                appliedResearches = connection.Query<int>(query, null).ToList();
            };
            return appliedResearches;
        }
    }

}
