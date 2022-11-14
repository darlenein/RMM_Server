using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileManager.Services
{
    public class DatabaseService
    {
        public MySqlConnection connect()
        {
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;" + "pwd=hello;database=sys";

            try
            {
                MySqlConnection conn = new MySqlConnection();
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();
                
                return conn;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
