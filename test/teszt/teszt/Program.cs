using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Mysqlx.Connection;

namespace teszt
{
    class forrasAdat
    {
        public string novenyNeve;
        public string novenybetuNagy;
        public string novenybetuKicsi;
        public string szereti;
        public string nemszereti;

        public forrasAdat(string sor)
        {
            string[] m = sor.Split(';');
            novenyNeve = m[0];
            novenybetuNagy = m[1];
            novenybetuKicsi = m[2];
            szereti = m[3];
            nemszereti = m[4];

        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {/*
            string server = "localhost";
            string database = "vetestervezo";
            string username = "root";
            string password = "";
            string constring = "Server="+server+";"+"Database="+database+";"+"UID="+username+";"+"Password="+password+";";
            MySqlConnection conn = new MySqlConnection(constring); 
            conn.Open();    
            string query = "SELECT * FROM novenyek";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();   
            while (reader.Read()) 
            {
                Console.WriteLine("Növény neve: " + reader["novenyNeve"]);
                ;
            }*/


            List<forrasAdat> tesztLista = new List<forrasAdat>();
            StreamWriter ir = new StreamWriter("adatTeszt.txt");
            #region Adatbázis
            string server = "localhost";
            string database = "vetestervezo";
            string username = "root";
            string password = "";
            string constring = "Server=" + server + ";" + "Database=" + database + ";" + "UID=" + username + ";" + "Password=" + password + ";";
            MySqlConnection conn = new MySqlConnection(constring);
            conn.Open();
            string query = "SELECT * FROM novenyek";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ir.WriteLine(Convert.ToString(reader["novenyNeve"]) + ";" + reader["novenybetuNagy"] + ";" + reader["novenybetuKicsi"] + ";" + reader["szereti"] + ";" + reader["nemszereti"]);
            }

            ir.Close();


            
            #endregion
           
            Console.ReadKey();  
        }
    }
}
