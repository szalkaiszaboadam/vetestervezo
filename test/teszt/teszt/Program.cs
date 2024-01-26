using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Mysqlx.Connection;

namespace teszt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string server = "localhost";
            string database = "balaton";
            string username = "root";
            string password = "";
            string constring = "Server="+server+";"+"Database="+database+";"+"UID="+username+";"+"Password="+password+";";
            MySqlConnection conn = new MySqlConnection(constring); 
            conn.Open();    
            string query = "SELECT * FROM hajok";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();   
            while (reader.Read()) 
            {
                Console.WriteLine(reader["hajoNev"]);
                Console.WriteLine(reader["hajoOszt"]);
            }
            Console.ReadKey();  
        }
    }
}
