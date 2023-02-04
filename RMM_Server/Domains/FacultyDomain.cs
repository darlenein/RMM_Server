using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using RMM_Server.Models;
using RMM_Server.Services;

namespace RMM_Server.Domains
{
    public class FacultyDomain
    {
        public Faculty GetFaculty(string id)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"SELECT * from faculty WHERE faculty_id = '{id}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            Faculty f = new Faculty();
            while (reader.Read())
            {
                f.Id = ConvertFromDBVal<string>(reader[0]);
                f.FirstName = ConvertFromDBVal<string>(reader[1]);
                f.LastName = ConvertFromDBVal<string>(reader[2]);
                f.Title = ConvertFromDBVal<string>(reader[3]);
                f.Email = ConvertFromDBVal<string>(reader[4]);
                f.Office = ConvertFromDBVal<string>(reader[5]);
                f.Phone = ConvertFromDBVal<string>(reader[6]);
                f.Link1 = ConvertFromDBVal<string>(reader[7]);
                f.Link2 = ConvertFromDBVal<string>(reader[8]);
                f.Link3 = ConvertFromDBVal<string>(reader[9]);
                f.AboutMe = ConvertFromDBVal<string>(reader[10]);
                f.ResearchInterest = ConvertFromDBVal<string>(reader[11]);
                f.ProfileUrl = ConvertFromDBVal<string>(reader[12]);
            }
            reader.Close();
            return f;
        }

        public List<Faculty> GetAllFaculty()
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"SELECT * FROM faculty";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            List<Faculty> fl = new List<Faculty>();
            while (reader.Read())
            {
                Faculty f = new Faculty();
                f.Id = ConvertFromDBVal<string>(reader[0]);
                f.FirstName = ConvertFromDBVal<string>(reader[1]);
                f.LastName = ConvertFromDBVal<string>(reader[2]);
                f.Title = ConvertFromDBVal<string>(reader[3]);
                f.Email = ConvertFromDBVal<string>(reader[4]);
                f.Office = ConvertFromDBVal<string>(reader[5]);
                f.Phone = ConvertFromDBVal<string>(reader[6]);
                f.Link1 = ConvertFromDBVal<string>(reader[7]);
                f.Link2 = ConvertFromDBVal<string>(reader[8]);
                f.Link3 = ConvertFromDBVal<string>(reader[9]);
                f.AboutMe = ConvertFromDBVal<string>(reader[10]);
                f.ResearchInterest = ConvertFromDBVal<string>(reader[11]);
                f.ProfileUrl = ConvertFromDBVal<string>(reader[12]);

                fl.Add(f);
            }
            reader.Close();

            return fl;
        }

        public Faculty CreateFaculty(Faculty f)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"INSERT into faculty VALUES (" +
                $" '{f.Id}', '{f.FirstName}', '{f.LastName}', '{f.Title}', '{f.Email}', '{f.Office}'," +
                $" '{f.Phone}', '{f.Link1}', '{f.Link2}', '{f.Link3}', '{f.AboutMe}'," +
                $" '{f.ResearchInterest}', '{f.ProfileUrl}')";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            reader.Close();

            return f;
        }

        public Faculty EditFaculty(Faculty f)
        { 
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"UPDATE faculty " +
                $"SET first_name = {f.FirstName}, SET last_name = {f.LastName}, " +
                $"SET title = {f.Title}, SET email = {f.Email}, SET office = {f.Office}, " +
                $"SET phone = {f.Phone}, SET link1 = {f.Link1}, SET link2 = {f.Link2}, " +
                $"SET link3 = {f.Link3}, SET about_me = {f.AboutMe}, SET research_interest = {f.ResearchInterest}" +
                $"WHERE faculty_id = '{f.Id}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            reader.Close();

            return f;
        }

        public void DeleteFacultyByID(string id)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"DELETE from faculty WHERE faculty_id = '{id}'";
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

    }
}
