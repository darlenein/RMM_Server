using MySql.Data.MySqlClient;
using ProfileManager.Models;
using ProfileManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileManager.Domains
{
    public class StudentDomain
    {
        public Student getStudent(string id)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.connect();
            string query = $"SELECT * from student WHERE student_id = '{id}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            Student s = new Student();
            while (reader.Read())
            {
                s.Id = ConvertFromDBVal<string>(reader[0]);
                s.FacultyId = ConvertFromDBVal<string>(reader[1]);
                s.ResearchId = ConvertFromDBVal<int>(reader[2]);
                s.FirstName = ConvertFromDBVal<string>(reader[3]);
                s.LastName = ConvertFromDBVal<string>(reader[4]);
                s.GPA = ConvertFromDBVal<double>(reader[5]);
                s.GraduationMonth = ConvertFromDBVal<string>(reader[6]);
                s.GraduationYear = ConvertFromDBVal<string>(reader[7]);
                s.Major = ConvertFromDBVal<string>(reader[8]);
                s.Skills = ConvertFromDBVal<string>(reader[9]);
                s.Link1 = ConvertFromDBVal<string>(reader[10]);
                s.Link2 = ConvertFromDBVal<string>(reader[11]);
                s.Link3 = ConvertFromDBVal<string>(reader[12]);
                s.ResearchInterest = ConvertFromDBVal<string>(reader[13]);
                s.ResearchProject = ConvertFromDBVal<string>(reader[14]);
                s.Email = ConvertFromDBVal<string>(reader[15]);
            }
            reader.Close();

            return s;
        }

        public List<Student> getAllStudent()
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.connect();
            string query = $"SELECT * FROM student";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            List<Student> sl = new List<Student>();
            while (reader.Read())
            {
                Student s = new Student();
                s.Id = ConvertFromDBVal<string>(reader[0]);
                s.FacultyId = ConvertFromDBVal<string>(reader[1]);
                s.ResearchId = ConvertFromDBVal<int>(reader[2]);
                s.FirstName = ConvertFromDBVal<string>(reader[3]);
                s.LastName = ConvertFromDBVal<string>(reader[4]);
                s.GPA = ConvertFromDBVal<double>(reader[5]);
                s.GraduationMonth = ConvertFromDBVal<string>(reader[6]);
                s.GraduationYear = ConvertFromDBVal<string>(reader[7]);
                s.Major = ConvertFromDBVal<string>(reader[8]);
                s.Skills = ConvertFromDBVal<string>(reader[9]);
                s.Link1 = ConvertFromDBVal<string>(reader[10]);
                s.Link2 = ConvertFromDBVal<string>(reader[11]);
                s.Link3 = ConvertFromDBVal<string>(reader[12]);
                s.ResearchInterest = ConvertFromDBVal<string>(reader[13]);
                s.ResearchProject = ConvertFromDBVal<string>(reader[14]);
                s.Email = ConvertFromDBVal<string>(reader[15]);

                sl.Add(s);
            }
            reader.Close();

            return sl;
        }

        public List<Student> getAllStudentsByResearch(int research_id)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.connect();
            string query = $"SELECT a.*, b.progress_bar, c.name FROM sys.student AS a " +
                $"JOIN sys.participant AS b ON a.student_id = b.student_id " +
                $"JOIN sys.research AS c ON b.research_id = c.research_id " +
                $"WHERE b.research_id = '{research_id}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            List<Student> sl = new List<Student>();
            while (reader.Read())
            {
                Student s = new Student();
                s.Id = ConvertFromDBVal<string>(reader[0]);
                s.FacultyId = ConvertFromDBVal<string>(reader[1]);
                s.ResearchId = ConvertFromDBVal<int>(reader[2]);
                s.FirstName = ConvertFromDBVal<string>(reader[3]);
                s.LastName = ConvertFromDBVal<string>(reader[4]);
                s.GPA = ConvertFromDBVal<double>(reader[5]);
                s.GraduationMonth = ConvertFromDBVal<string>(reader[6]);
                s.GraduationYear = ConvertFromDBVal<string>(reader[7]);
                s.Major = ConvertFromDBVal<string>(reader[8]);
                s.Skills = ConvertFromDBVal<string>(reader[9]);
                s.Link1 = ConvertFromDBVal<string>(reader[10]);
                s.Link2 = ConvertFromDBVal<string>(reader[11]);
                s.Link3 = ConvertFromDBVal<string>(reader[12]);
                s.ResearchInterest = ConvertFromDBVal<string>(reader[13]);
                s.ResearchProject = ConvertFromDBVal<string>(reader[14]);
                s.Email = ConvertFromDBVal<string>(reader[15]);
                s.Progression = ConvertFromDBVal<int>(reader[16]);
                s.Research_name = ConvertFromDBVal<string>(reader[17]);

                sl.Add(s);
            }
            reader.Close();

            return sl;
        }

        public int ReturnOne()
        {
            return 1;
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
