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
        {
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
                
            }
            



            string novenyneve = "körte";
            string kisbetu = "ki";
            string nagybetu = "KI";
            string szereti = "alma";
            string nemszereti = "kukorica";



            
            string Query = "INSERT INTO novenyek(novenyNeve,novenybetuNagy,novenybetuKicsi,szereti,nemszereti) VALUES('" + novenyneve + "','" + nagybetu + "','" + kisbetu + "','" + szereti + "','" + nemszereti + "');";
            MySqlConnection MyConn2 = new MySqlConnection(constring);
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataReader MyReader2;
            MyConn2.Open();
            MyReader2 = MyCommand2.ExecuteReader();    









            /*StreamWriter ir = new StreamWriter("adatTeszt.txt");
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

            ir.Close();*/





            Console.ReadKey();  
        }
    }
}
