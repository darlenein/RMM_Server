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
        public List<Research> getResearchByFacultyId(string f_id)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.connect();
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

        public List<Research> getAllResearch()
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.connect();
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

        public List<Research> getAllResearchByStudentId(string s_id)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.connect();
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
