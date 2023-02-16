using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RMM_Server.Contracts;
using RMM_Server.Models;
using RMM_Server.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.DataAccess
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string connectionString;

        public StudentRepository(IConfiguration config)
        {
            // find param ConnectionString in appsettings.json file 
            connectionString = config.GetConnectionString("ConnectionString");
        }

        public Student GetStudent(string id)
        {
            Student s;

            // Connect to database 
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"SELECT * from student WHERE student_id = '{id}'";
                s = connection.Query<Student>(query, null).FirstOrDefault();
            };

            return s;
        }

        public List<Student> GetAllStudent()
        {
            List<Student> sl = new List<Student>();

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"SELECT * FROM student";
                sl = connection.Query<Student>(query, null).ToList();
            };

            return sl;
        }

        public List<Student> GetAllStudentsByResearch(int research_id)
        {
            List<Student> sl = new List<Student>();

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"SELECT a.*, b.progress_bar, c.name FROM student AS a " +
                    $"JOIN participant AS b ON a.student_id = b.student_id " +
                    $"JOIN research AS c ON b.research_id = c.research_id " +
                    $"WHERE b.research_id = '{research_id}'";
                sl = connection.Query<Student>(query, null).ToList();
            };

            return sl;
        }

        public Student CreateStudent(Student s)
        {
            Student sl;
            int paid = ConvertBoolToInt(s.PreferPaid);
            int nonpaid = ConvertBoolToInt(s.PreferNonpaid);
            int credit = ConvertBoolToInt(s.PreferCredit);

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"INSERT into student VALUES (" +
                    $" '{s.Student_Id}', '{s.First_Name}', '{s.Last_Name}', '{s.GPA}', '{s.Graduation_Month}', '{s.Graduation_Year}'," +
                    $" '{s.Major}', '{s.Skills}', '{s.Link1}', '{s.Link2}', '{s.Link3}', '{s.Research_Interest}'," +
                    $" '{s.Research_Project}', '{s.Email}', '{paid}', '{nonpaid}', '{credit}', '{s.PreferLocation}'," +
                    $" '{s.Minor}', '{s.SkillLevel}', '{s.Major2}')";
                sl = connection.Query<Student>(query, null).FirstOrDefault();
            };

            return sl;
        }

        public Student EditStudent(Student s)
        {
            Student sl;
            int paid = ConvertBoolToInt(s.PreferPaid);
            int nonpaid = ConvertBoolToInt(s.PreferNonpaid);
            int credit = ConvertBoolToInt(s.PreferCredit);

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"UPDATE student " +
                    $"SET first_name = '{s.First_Name}',  last_name = '{s.Last_Name}'," +
                    $" GPA = {s.GPA}, graduation_month = '{s.Graduation_Month}'," +
                    $" graduation_year = '{s.Graduation_Year}', major = '{s.Major}', skills = '{s.Skills}'," +
                    $" link1 = '{s.Link1}', link2 = '{s.Link2}', link3 = '{s.Link3}', " +
                    $"research_interest = '{s.Research_Interest}', research_project = '{s.Research_Project}', " +
                    $"email = '{s.Email}', preferPaid = {paid}, preferNonPaid = {nonpaid}, " +
                    $"preferCredit = {credit}, minor = '{s.Minor}', skillLevel = '{s.SkillLevel}', major2 = '{s.Major2}' " +
                    $"WHERE student_id = '{s.Student_Id}'";

                sl = connection.Query<Student>(query, null).FirstOrDefault();
            };

            return sl;  
        }

        public void getParsedResume()
        {
            string resumePath = "path_to_file.pdf";
            var service = new ParseService("REPLACE_TOKEN");
            var resume = service.CreateResume(resumePath);
        }

        public int ConvertBoolToInt(bool b)
        {
            if (b == true) return 1;
            else return 0;
        }
    }
}
