using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RMM_Server.Contracts;
using RMM_Server.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.DataAccess
{
    public class FacultyRepository : IFacultyRepository
    {
        private readonly string connectionString;

        public FacultyRepository(IConfiguration config)
        {
            // find param ConnectionString in appsettings.json file 
            connectionString = config.GetConnectionString("ConnectionString");
        }

        public Faculty GetFaculty(string id)
        {
            Faculty f;

            // Connect to database 
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"SELECT * from faculty WHERE faculty_id = '{id}'";
                f = connection.Query<Faculty>(query, null).FirstOrDefault();
            };

            return f;
        }

        public List<Faculty> GetAllFaculty()
        {
            List<Faculty> fl = new List<Faculty>();

            // Connect to database 
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"SELECT * FROM faculty";
                fl = connection.Query<Faculty>(query, null).ToList();
            };

            return fl;
        }

        public Faculty CreateFaculty(Faculty f)
        {
            Faculty fl;
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"INSERT into faculty VALUES (" +
                    $" '{f.Faculty_Id}', '{f.First_Name}', '{f.Last_Name}', '{f.Title}', '{f.Email}', '{f.Office}'," +
                    $" '{f.Phone}', '{f.Link1}', '{f.Link2}', '{f.Link3}', '{f.About_Me}'," +
                    $" '{f.Research_Interest}', '{f.Profile_Url}')";
                fl = connection.Query<Faculty>(query, null).FirstOrDefault();
            };
            return fl;
        }

        public void EditFaculty(Faculty f)
        {
           
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"UPDATE faculty " +
                    $"SET first_name = {f.First_Name}, SET last_name = {f.Last_Name}, " +
                    $"SET title = {f.Title}, SET email = {f.Email}, SET office = {f.Office}, " +
                    $"SET phone = {f.Phone}, SET link1 = {f.Link1}, SET link2 = {f.Link2}, " +
                    $"SET link3 = {f.Link3}, SET about_me = {f.About_Me}, SET research_interest = {f.Research_Interest}" +
                    $"WHERE faculty_id = '{f.Faculty_Id}'";
               
            };
            
        }
    }
}
