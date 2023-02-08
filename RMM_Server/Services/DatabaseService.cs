using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server.Services
{
    public class DatabaseService
    {
        public MySqlConnection Connect()
        {
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;" + "pwd=hello;database=sys";

            try
            {
                MySqlConnection conn = new MySqlConnection();
                conn = new MySqlConnection(myConnectionString);
                conn.Open();
                
                return conn;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


    }
}
