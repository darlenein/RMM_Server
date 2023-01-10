using MySql.Data.MySqlClient;
using RMM_Server.Models;
using RMM_Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Domains
{
    public class StudentDomain
    {
        public Student GetStudent(string id)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"SELECT * from student WHERE student_id = '{id}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            Student s = new Student();
            while (reader.Read())
            {
                s.Id = ConvertFromDBVal<string>(reader[0]);
                s.FirstName = ConvertFromDBVal<string>(reader[1]);
                s.LastName = ConvertFromDBVal<string>(reader[2]);
                s.GPA = ConvertFromDBVal<double>(reader[3]);
                s.GraduationMonth = ConvertFromDBVal<string>(reader[4]);
                s.GraduationYear = ConvertFromDBVal<string>(reader[5]);
                s.Major = ConvertFromDBVal<string>(reader[6]);
                s.Skills = ConvertFromDBVal<string>(reader[7]);
                s.Link1 = ConvertFromDBVal<string>(reader[8]);
                s.Link2 = ConvertFromDBVal<string>(reader[9]);
                s.Link3 = ConvertFromDBVal<string>(reader[10]);
                s.ResearchInterest = ConvertFromDBVal<string>(reader[11]);
                s.ResearchProject = ConvertFromDBVal<string>(reader[12]);
                s.Email = ConvertFromDBVal<string>(reader[13]);
                if (ConvertFromDBVal<int>(reader[14]) == 1)
                {
                    s.PreferPaid = true;
                }
                else
                {
                    s.PreferPaid = false;
                }
                if (ConvertFromDBVal<sbyte>(reader[15]) == 1)
                {
                    s.PreferNonpaid = true;
                }
                else
                {
                    s.PreferNonpaid = false;
                }
                if (ConvertFromDBVal<sbyte>(reader[16]) == 1)
                {
                    s.PreferCredit = true;
                }
                else
                {
                    s.PreferCredit = false;
                }
            }
            reader.Close();

            return s;
        }

        public List<Student> GetAllStudent()
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"SELECT * FROM student";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            List<Student> sl = new List<Student>();
            while (reader.Read())
            {
                Student s = new Student();
                s.Id = ConvertFromDBVal<string>(reader[0]);
                s.FirstName = ConvertFromDBVal<string>(reader[1]);
                s.LastName = ConvertFromDBVal<string>(reader[2]);
                s.GPA = ConvertFromDBVal<double>(reader[3]);
                s.GraduationMonth = ConvertFromDBVal<string>(reader[4]);
                s.GraduationYear = ConvertFromDBVal<string>(reader[5]);
                s.Major = ConvertFromDBVal<string>(reader[6]);
                s.Skills = ConvertFromDBVal<string>(reader[7]);
                s.Link1 = ConvertFromDBVal<string>(reader[8]);
                s.Link2 = ConvertFromDBVal<string>(reader[9]);
                s.Link3 = ConvertFromDBVal<string>(reader[10]);
                s.ResearchInterest = ConvertFromDBVal<string>(reader[11]);
                s.ResearchProject = ConvertFromDBVal<string>(reader[12]);
                s.Email = ConvertFromDBVal<string>(reader[13]);
                if (ConvertFromDBVal<int>(reader[14]) == 1)
                {
                    s.PreferPaid = true;
                }
                else
                {
                    s.PreferPaid = false;
                }
                if (ConvertFromDBVal<int>(reader[15]) == 1)
                {
                    s.PreferNonpaid = true;
                }
                else
                {
                    s.PreferNonpaid = false;
                }
                if (ConvertFromDBVal<int>(reader[16]) == 1)
                {
                    s.PreferCredit = true;
                }
                else
                {
                    s.PreferCredit = false;
                }
                s.PreferLocation = ConvertFromDBVal<int>(reader[17]);

                sl.Add(s);
            }
            reader.Close();

            return sl;
        }

        public List<Student> GetAllStudentsByResearch(int research_id)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"SELECT a.*, b.progress_bar, c.name FROM student AS a " +
                $"JOIN participant AS b ON a.student_id = b.student_id " +
                $"JOIN research AS c ON b.research_id = c.research_id " +
                $"WHERE b.research_id = '{research_id}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            List<Student> sl = new List<Student>();
            while (reader.Read())
            {
                Student s = new Student();
                s.Id = ConvertFromDBVal<string>(reader[0]);
                s.FirstName = ConvertFromDBVal<string>(reader[1]);
                s.LastName = ConvertFromDBVal<string>(reader[2]);
                s.GPA = ConvertFromDBVal<double>(reader[3]);
                s.GraduationMonth = ConvertFromDBVal<string>(reader[4]);
                s.GraduationYear = ConvertFromDBVal<string>(reader[5]);
                s.Major = ConvertFromDBVal<string>(reader[6]);
                s.Skills = ConvertFromDBVal<string>(reader[7]);
                s.Link1 = ConvertFromDBVal<string>(reader[8]);
                s.Link2 = ConvertFromDBVal<string>(reader[9]);
                s.Link3 = ConvertFromDBVal<string>(reader[10]);
                s.ResearchInterest = ConvertFromDBVal<string>(reader[11]);
                s.ResearchProject = ConvertFromDBVal<string>(reader[12]);
                s.Email = ConvertFromDBVal<string>(reader[13]);
                if (ConvertFromDBVal<int>(reader[14]) == 1)
                {
                    s.PreferPaid = true;
                }
                else
                {
                    s.PreferPaid = false;
                }
                if (ConvertFromDBVal<int>(reader[15]) == 1)
                {
                    s.PreferNonpaid = true;
                }
                else
                {
                    s.PreferNonpaid = false;
                }
                if (ConvertFromDBVal<int>(reader[16]) == 1)
                {
                    s.PreferCredit = true;
                }
                else
                {
                    s.PreferCredit = false;
                }
                s.PreferLocation = ConvertFromDBVal<int>(reader[17]);
                s.Progression = ConvertFromDBVal<int>(reader[18]);
                s.Research_name = ConvertFromDBVal<string>(reader[19]);

                sl.Add(s);
            }
            reader.Close();

            return sl;
        }

        public Student CreateStudent(Student s)
        {
            int paid = ConvertBoolToInt(s.PreferPaid);
            int nonpaid = ConvertBoolToInt(s.PreferNonpaid);
            int credit = ConvertBoolToInt(s.PreferCredit);

            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"INSERT into student VALUES (" +
                $" '{s.Id}', '{s.FirstName}', '{s.LastName}', '{s.GPA}', '{s.GraduationMonth}', '{s.GraduationYear}'," +
                $" '{s.Major}', '{s.Skills}', '{s.Link1}', '{s.Link2}', '{s.Link3}', '{s.ResearchInterest}'," +
                $" '{s.ResearchProject}', '{s.Email}', '{paid}', '{nonpaid}', '{credit}', '{s.PreferLocation}')";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            reader.Close();

            return s;
        }

        public void DeleteStudentByID(string id)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"DELETE from student WHERE student_id = '{id}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            reader.Close();
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
