using MySql.Data.MySqlClient;
using RMM_Server.Contracts;
using RMM_Server.Models;
using System.Data;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System;

namespace RMM_Server.DataAccess
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly string connectionString;

        public DepartmentRepository(IConfiguration config)
        {
            // find param ConnectionString in appsettings.json file 
            connectionString = config.GetConnectionString("ConnectionString");
        }

        public List<Department> GetAllDepartments()
        {
            List<Department> dl = new List<Department>();

            // Connect to database 
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"SELECT * FROM department";
               dl = connection.Query<Department>(query, null).ToList();
            };
            return dl;
        }

        public List<SubDepartment> GetSubDeptByDeptId(int dID)
        {
            List<SubDepartment> sdl = new List<SubDepartment>();
            
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"SELECT * FROM subdepartment WHERE department_id = '{dID}'";
                sdl = connection.Query<SubDepartment>(query, null).ToList();
            };

            return sdl;
        }

        public string[] GetSubDeptByResearchId(int rID)
        {
            string[] l2, l1 = new string[3];

            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"SELECT b.name FROM researchdept AS a JOIN subdepartment as b " +
                    $"ON a.subdepartment_id = b.subdepartment_id WHERE research_id = '{rID}'";
                l2 = connection.Query<string>(query, null).ToArray();
            };

            int count = 0;
            foreach (string l in l2)
            {
                l1[count] = l;
                count++;
            }

            return l1;
        }

    }
}
