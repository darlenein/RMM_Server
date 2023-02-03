using MySql.Data.MySqlClient;
using RMM_Server.Models;
using RMM_Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Domains
{
    public class ResearchDomain
    {
        public List<Research> GetResearchByFacultyId(string f_id)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"SELECT a.*, b.first_name, b.last_name " +
                $"FROM research AS a JOIN faculty as B ON a.faculty_id = b.faculty_id " +
                $"WHERE a.faculty_id = '{f_id}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            List<Research> research_list = new List<Research>();
            while (reader.Read())
            {
                Research r = new Research();
                r.Id = ConvertFromDBVal<int>(reader[0]);
                r.Faculty_Id = ConvertFromDBVal<string>(reader[1]);
                r.Name = ConvertFromDBVal<string>(reader[2]);
                r.Description = ConvertFromDBVal<string>(reader[3]);
                r.Location = ConvertFromDBVal<string>(reader[4]);
                r.Required_Skills = ConvertFromDBVal<string>(reader[5]);
                r.Encouraged_Skills = ConvertFromDBVal<string>(reader[6]);
                r.Start_Date = ConvertFromDBVal<DateTime>(reader[7]).ToShortDateString();
                r.End_Date = ConvertFromDBVal<DateTime>(reader[8]).ToShortDateString();
                if (ConvertFromDBVal<sbyte>(reader[9]) == 1)
                {
                    r.Active = true;
                } 
                else
                {
                    r.Active = false;
                }
                r.Address = ConvertFromDBVal<string>(reader[10]);
                if (ConvertFromDBVal<sbyte>(reader[11]) == Convert.ToSByte(1))
                {
                    r.IsPaid = true;
                }
                else
                {
                    r.IsPaid = false;
                }
                if (ConvertFromDBVal<sbyte>(reader[12]) == Convert.ToSByte(1))
                {
                    r.IsNonpaid = true;
                }
                else
                {
                    r.IsNonpaid = false;
                }
                if (ConvertFromDBVal<sbyte>(reader[13]) == Convert.ToSByte(1))
                {
                    r.IsCredit = true;
                }
                else
                {
                    r.IsCredit = false;
                }
                r.Faculty_FirstName = ConvertFromDBVal<string>(reader[14]);
                r.Faculty_LastName = ConvertFromDBVal<string>(reader[15]);

                research_list.Add(r);
            }
            reader.Close();

            return research_list;
        }

        public Research GetResearchByID(int id)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"SELECT a.*, b.first_name, b.last_name FROM research AS a " +
                $"JOIN faculty as B ON a.faculty_id = b.faculty_id WHERE research_id = '{id}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            Research r = new Research();
            while (reader.Read())
            {
                r.Id = ConvertFromDBVal<int>(reader[0]);
                r.Faculty_Id = ConvertFromDBVal<string>(reader[1]);
                r.Name = ConvertFromDBVal<string>(reader[2]);
                r.Description = ConvertFromDBVal<string>(reader[3]);
                r.Location = ConvertFromDBVal<string>(reader[4]);
                r.Required_Skills = ConvertFromDBVal<string>(reader[5]);
                r.Encouraged_Skills = ConvertFromDBVal<string>(reader[6]);
                r.Start_Date = ConvertFromDBVal<DateTime>(reader[7]).ToShortDateString();
                r.End_Date = ConvertFromDBVal<DateTime>(reader[8]).ToShortDateString();
                if (ConvertFromDBVal<sbyte>(reader[9]) == 1)
                {
                    r.Active = true;
                }
                else
                {
                    r.Active = false;
                }
                r.Address = ConvertFromDBVal<string>(reader[10]);
                if (ConvertFromDBVal<sbyte>(reader[11]) == Convert.ToSByte(1))
                {
                    r.IsPaid = true;
                }
                else
                {
                    r.IsPaid = false;
                }
                if (ConvertFromDBVal<sbyte>(reader[12]) == Convert.ToSByte(1))
                {
                    r.IsNonpaid = true;
                }
                else
                {
                    r.IsNonpaid = false;
                }
                if (ConvertFromDBVal<sbyte>(reader[13]) == Convert.ToSByte(1))
                {
                    r.IsCredit = true;
                }
                else
                {
                    r.IsCredit = false;
                }
                r.Faculty_FirstName = ConvertFromDBVal<string>(reader[14]);
                r.Faculty_LastName = ConvertFromDBVal<string>(reader[15]);
            }
            reader.Close();

            return r;
        }

        public List<Research> GetAllResearch()
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"SELECT a.*, b.first_name, b.last_name, b.faculty_id FROM research AS a " +
                $"JOIN faculty as B ON a.faculty_id = b.faculty_id;";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            List<Research> research_list = new List<Research>();
            while (reader.Read())
            {
                Research r = new Research();
                r.Id = ConvertFromDBVal<int>(reader[0]);
                r.Faculty_Id = ConvertFromDBVal<string>(reader[1]);
                r.Name = ConvertFromDBVal<string>(reader[2]);
                r.Description = ConvertFromDBVal<string>(reader[3]);
                r.Location = ConvertFromDBVal<string>(reader[4]);
                r.Required_Skills = ConvertFromDBVal<string>(reader[5]);
                r.Encouraged_Skills = ConvertFromDBVal<string>(reader[6]);
                r.Start_Date = ConvertFromDBVal<DateTime>(reader[7]).ToShortDateString();
                r.End_Date = ConvertFromDBVal<DateTime>(reader[8]).ToShortDateString();
                if (ConvertFromDBVal<sbyte>(reader[9]) == 1)
                {
                    r.Active = true;
                }
                else
                {
                    r.Active = false;
                }
                r.Address = ConvertFromDBVal<string>(reader[10]);
                if (ConvertFromDBVal<sbyte>(reader[11]) == Convert.ToSByte(1))
                {
                    r.IsPaid = true;
                }
                else
                {
                    r.IsPaid = false;
                }
                if (ConvertFromDBVal<sbyte>(reader[12]) == Convert.ToSByte(1))
                {
                    r.IsNonpaid = true;
                }
                else
                {
                    r.IsNonpaid = false;
                }
                if (ConvertFromDBVal<sbyte>(reader[13]) == Convert.ToSByte(1))
                {
                    r.IsCredit = true;
                }
                else
                {
                    r.IsCredit = false;
                }
                r.Faculty_FirstName = ConvertFromDBVal<string>(reader[14]);
                r.Faculty_LastName = ConvertFromDBVal<string>(reader[15]);
                r.Faculty_Id = ConvertFromDBVal<string>(reader[16]);

                research_list.Add(r);
            }
            reader.Close();

            DepartmentDomain dd = new DepartmentDomain();
            foreach (Research r in research_list)
            {
                r.ResearchDepts = dd.GetSubDeptByResearchId(r.Id);
            }

            research_list = research_list.OrderByDescending(x => x.Id).ToList();

            return research_list;
        }

        public List<Research> GetAllResearchByStudentId(string s_id)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"SELECT a.*, c.first_name, c.last_name, b.progress_bar FROM research AS a " +
                $"JOIN participant AS b ON a.research_id = b.research_id " +
                $"JOIN faculty AS c ON a.faculty_id = c.faculty_id " +
                $"WHERE b.student_id = '{s_id}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            List<Research> research_list = new List<Research>();
            while (reader.Read())
            {
                Research r = new Research();
                r.Id = ConvertFromDBVal<int>(reader[0]);
                r.Faculty_Id = ConvertFromDBVal<string>(reader[1]);
                r.Name = ConvertFromDBVal<string>(reader[2]);
                r.Description = ConvertFromDBVal<string>(reader[3]);
                r.Location = ConvertFromDBVal<string>(reader[4]);
                r.Required_Skills = ConvertFromDBVal<string>(reader[5]);
                r.Encouraged_Skills = ConvertFromDBVal<string>(reader[6]);
                r.Start_Date = ConvertFromDBVal<DateTime>(reader[7]).ToShortDateString();
                r.End_Date = ConvertFromDBVal<DateTime>(reader[8]).ToShortDateString();
                if (ConvertFromDBVal<sbyte>(reader[9]) == 1)
                {
                    r.Active = true;
                }
                else
                {
                    r.Active = false;
                }
                r.Address = ConvertFromDBVal<string>(reader[10]);
                if (ConvertFromDBVal<sbyte>(reader[11]) == Convert.ToSByte(1))
                {
                    r.IsPaid = true;
                }
                else
                {
                    r.IsPaid = false;
                }
                if (ConvertFromDBVal<sbyte>(reader[12]) == Convert.ToSByte(1))
                {
                    r.IsNonpaid = true;
                }
                else
                {
                    r.IsNonpaid = false;
                }
                if (ConvertFromDBVal<sbyte>(reader[13]) == Convert.ToSByte(1))
                {
                    r.IsCredit = true;
                }
                else
                {
                    r.IsCredit = false;
                }
                r.Faculty_FirstName = ConvertFromDBVal<string>(reader[14]);
                r.Faculty_LastName = ConvertFromDBVal<string>(reader[15]);
                r.Progression = ConvertFromDBVal<int>(reader[16]);

                research_list.Add(r);
            }
            reader.Close();

            return research_list;
        }

        public Progress AddResearchApplicant(Progress p)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"INSERT into participant VALUES ({p.research_id}, '{p.student_id}', 0)";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            reader.Close();

            return p;
        }
        public void DeleteResearchApplicant(int rID, string sID)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"DELETE from participant " +
                $"WHERE research_id = {rID} AND student_id = '{sID}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            reader.Close();
        }
        public void UpdateApplicantProgress(int p, int rID, string sID)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"UPDATE participant " +
                $"SET progress_bar = {p} " +
                $"WHERE research_id = {rID} AND student_id = '{sID}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            reader.Close();
        }

        public Progress GetAppProgression(int rID, string sID)
        {
            Progress p = new Progress();
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"SELECT * FROM participant " +
                $"WHERE research_id = {rID} AND student_id = '{sID}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                p.research_id = ConvertFromDBVal<int>(reader[0]);
                p.student_id = ConvertFromDBVal<string>(reader[1]);
                p.progress = ConvertFromDBVal<int>(reader[2]);
            }
            reader.Close();

            return p;
        }

        public Research AddResearch(Research r)
        {
            int active = ConvertBoolToInt(r.Active);

            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();

            // add research to db
            string query = $"INSERT into research VALUES (" +
                $" null, '{r.Faculty_Id}', '{r.Name}', '{r.Description}', '{r.Location}', '{r.Required_Skills}'," +
                $" '{r.Encouraged_Skills}', '{r.Start_Date}', '{r.End_Date}', '{active}', '{r.Address}', '{r.IsPaid}'," +
                $" '{r.IsNonpaid}', '{r.IsCredit}')";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            reader.Close();

            int count = GetCountOfResearchTable();

            // add associated depts to research
            foreach (string dept in r.ResearchDepts)
            {
                int d_id = 0;
                query = $"SELECT subdepartment_id from subdepartment WHERE name = '{dept}'";
                com = new MySqlCommand(query, conn);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    d_id = ConvertFromDBVal<int>(reader[0]);
                }
                reader.Close();

                query = $"INSERT into researchdept VALUES ('{count}', '{d_id}')";
                com = new MySqlCommand(query, conn);
                reader = com.ExecuteReader();
                reader.Close();
            }

            return r;
        }

        public int GetCountOfResearchTable()
        {
            int count = 0; 
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"SELECT count(*) FROM research";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                count = ConvertFromDBVal<int>(Convert.ToInt32(reader[0]));
            }
            reader.Close();

            return count;
        }

        public void DeleteResearchByID(int id)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"DELETE from research WHERE research_id = '{id}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            reader.Close();
        }

        public List<Research> GetSortedResearchesByStudentId(string s)
        {
            StudentDomain sd = new StudentDomain();
            DepartmentDomain dd = new DepartmentDomain();
            List<Research> result = GetAllResearch();
            //List<Research> activeResearch = result.Where(x => x.Active == true).ToList();
            Student student = sd.GetStudent(s);

            //Have to get each researches list
            foreach (Research r in result)
            {
                r.ResearchDepts = dd.GetSubDeptByResearchId(r.Id);
            }
            var sortedByMinor = result.OrderByDescending(x => x.Id).ThenByDescending(x => x.Location == student.PreferLocation).ThenBy(x => x.ResearchDepts[0] == student.Minor).ThenBy(x => x.ResearchDepts[1] == student.Minor).ThenBy(x => x.ResearchDepts[2] == student.Minor).ToList();
            var sortedByMajor = sortedByMinor.OrderByDescending(x => x.ResearchDepts[0] == student.Major).ThenBy(x => x.ResearchDepts[1] == student.Major && x.Active == true).ThenBy(x => x.ResearchDepts[2] == student.Major && x.Active == true).ToList();
            var sortedByStatus = sortedByMajor.OrderByDescending(x => x.Active == true).ToList();

            return sortedByStatus;
        }

        public List<Research> GetFilteredAndSearchedResearch(Filter f)
        {
            List<Research> result;
            if(f.keyword == "" && f.filterValue.Count > 0)
            {
                result = GetFilteredResearch(f);
            }
            else if(f.keyword == "" && f.filterValue.Count == 0)
            {
                ResearchDomain rd = new ResearchDomain();
                if (f.psuID == "") result = rd.GetAllResearch();
                else result = rd.GetSortedResearchesByStudentId(f.psuID);
            }
            else
            {
                result = GetSearchedResearchByKeyword(f.keyword, f.research);
                f.research = result;
                if (f.filterValue.Count > 0) result = GetFilteredResearch(f);
            }
            
            result = result.GroupBy(x => x.Id).OrderByDescending(c => c.Count()).SelectMany(c => c.Select(x => x)).Distinct().ToList();
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

        public static T ConvertFromDBVal<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T); // returns the default value for the type
            }
            else
            {
                return (T)obj;
            }
        }

        public int ConvertBoolToInt(bool b)
        {
            if (b == true) return 1;
            else return 0;
        }
    }
}
