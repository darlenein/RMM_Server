using MySql.Data.MySqlClient;
using ProfileManager.Models;
using ProfileManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileManager.Domains
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
                r.SubDept_Id = ConvertFromDBVal<int>(reader[2]);
                r.Name = ConvertFromDBVal<string>(reader[3]);
                r.Description = ConvertFromDBVal<string>(reader[4]);
                r.Location = ConvertFromDBVal<string>(reader[5]);
                r.Required_skills = ConvertFromDBVal<string>(reader[6]);
                r.Encouraged_Skills = ConvertFromDBVal<string>(reader[7]);
                r.Start_Date = ConvertFromDBVal<DateTime>(reader[8]).ToShortDateString();
                r.End_Date = ConvertFromDBVal<DateTime>(reader[9]).ToShortDateString();
                if (ConvertFromDBVal<sbyte>(reader[10]) == 1)
                {
                    r.Active = true;
                } 
                else
                {
                    r.Active = false;
                }
                r.Student_Id = ConvertFromDBVal<string>(reader[11]);
                r.Incentive_type = ConvertFromDBVal<string>(reader[12]);
                r.Address = ConvertFromDBVal<string>(reader[13]);
                r.Faculty_FirstName = ConvertFromDBVal<string>(reader[14]);
                r.Faculty_LastName = ConvertFromDBVal<string>(reader[15]);

                research_list.Add(r);
            }
            reader.Close();

            return research_list;
        }

        public List<Research> GetAllResearch()
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"SELECT a.*, b.first_name, b.last_name FROM research AS a " +
                $"JOIN faculty as B ON a.faculty_id = b.faculty_id;";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            List<Research> research_list = new List<Research>();
            while (reader.Read())
            {
                Research r = new Research();
                r.Id = ConvertFromDBVal<int>(reader[0]);
                r.Faculty_Id = ConvertFromDBVal<string>(reader[1]);
                r.SubDept_Id = ConvertFromDBVal<int>(reader[2]);
                r.Name = ConvertFromDBVal<string>(reader[3]);
                r.Description = ConvertFromDBVal<string>(reader[4]);
                r.Location = ConvertFromDBVal<string>(reader[5]);
                r.Required_skills = ConvertFromDBVal<string>(reader[6]);
                r.Encouraged_Skills = ConvertFromDBVal<string>(reader[7]);
                r.Start_Date = ConvertFromDBVal<DateTime>(reader[8]).ToShortDateString();
                r.End_Date = ConvertFromDBVal<DateTime>(reader[9]).ToShortDateString();
                if (ConvertFromDBVal<sbyte>(reader[10]) == 1)
                {
                    r.Active = true;
                }
                else
                {
                    r.Active = false;
                }
                r.Student_Id = ConvertFromDBVal<string>(reader[11]);
                r.Incentive_type = ConvertFromDBVal<string>(reader[12]);
                r.Address = ConvertFromDBVal<string>(reader[13]);
                r.Faculty_FirstName = ConvertFromDBVal<string>(reader[14]);
                r.Faculty_LastName = ConvertFromDBVal<string>(reader[15]);

                research_list.Add(r);
            }
            reader.Close();

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
                r.SubDept_Id = ConvertFromDBVal<int>(reader[2]);
                r.Name = ConvertFromDBVal<string>(reader[3]);
                r.Description = ConvertFromDBVal<string>(reader[4]);
                r.Location = ConvertFromDBVal<string>(reader[5]);
                r.Required_skills = ConvertFromDBVal<string>(reader[6]);
                r.Encouraged_Skills = ConvertFromDBVal<string>(reader[7]);
                r.Start_Date = ConvertFromDBVal<DateTime>(reader[8]).ToShortDateString();
                r.End_Date = ConvertFromDBVal<DateTime>(reader[9]).ToShortDateString();
                if (ConvertFromDBVal<sbyte>(reader[10]) == 1)
                {
                    r.Active = true;
                }
                else
                {
                    r.Active = false;
                }
                r.Student_Id = ConvertFromDBVal<string>(reader[11]);
                r.Incentive_type = ConvertFromDBVal<string>(reader[12]);
                r.Address = ConvertFromDBVal<string>(reader[13]);
                r.Faculty_FirstName = ConvertFromDBVal<string>(reader[14]);
                r.Faculty_LastName = ConvertFromDBVal<string>(reader[15]);
                r.Progression = ConvertFromDBVal<int>(reader[16]);

                research_list.Add(r);
            }
            reader.Close();

            return research_list;
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

        public void CreateResearch()
        {

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
    }
}
