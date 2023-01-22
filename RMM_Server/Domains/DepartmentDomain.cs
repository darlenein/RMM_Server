using MySql.Data.MySqlClient;
using RMM_Server.Models;
using RMM_Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Domains
{
    public class DepartmentDomain
    {
        public List<Department> GetAllDepartments()
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"SELECT * FROM department";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            List<Department> dl = new List<Department>();
            while (reader.Read())
            {
                Department d = new Department();
                d.Id = ConvertFromDBVal<int>(reader[0]);
                d.Name = ConvertFromDBVal<string>(reader[1]);
                dl.Add(d);
            }
            reader.Close();

            return dl;
        }

        public List<SubDepartment> GetSubDeptByDeptId(int dID)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"SELECT * FROM subdepartment WHERE department_id = '{dID}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            List<SubDepartment> sdl = new List<SubDepartment>();
            while (reader.Read())
            {
                SubDepartment sd = new SubDepartment();
                sd.Id = ConvertFromDBVal<int>(reader[0]);
                sd.Dept_Id = ConvertFromDBVal<int>(reader[1]);
                sd.Name = ConvertFromDBVal<string>(reader[2]);
                sdl.Add(sd);
            }
            reader.Close();

            return sdl;
        }

        public string[] GetSubDeptByResearchId(int rID)
        {
            DatabaseService ds = new DatabaseService();
            MySqlConnection conn = ds.Connect();
            string query = $"SELECT a.*, b.name FROM researchdept AS a JOIN subdepartment as b " +
                $"ON a.subdepartment_id = b.subdepartment_id WHERE research_id = '{rID}'";
            MySqlCommand com = new MySqlCommand(query, conn);
            MySqlDataReader reader = com.ExecuteReader();
            string[] sdl = new string[3];
            int counter = 0;
            while (reader.Read())
            {
                //SubDepartment sd = new SubDepartment();
                //sd.research_id = ConvertFromDBVal<int>(reader[0]);
                //sd.Id = ConvertFromDBVal<int>(reader[1]);
                //sd.Name = ConvertFromDBVal<string>(reader[2]);
                sdl[counter] = (ConvertFromDBVal<string>(reader[2]));
                counter++;
            }
            reader.Close();

            return sdl;
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
