using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Esf;
using Spire.Pdf.Exporting.XPS.Schema;

namespace vetestervezo
{

    class forrasAdat
    {
        public string novenyNeve;
        public string novenyBetu;
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


    class Program
	{

            static void Main(string[] args)
		    {
                /*#region Adatbázis
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
          
                
                #endregion*/

                string betuk = "BbBOboKkCc";
                string valaszottNoveny = "";
                int agyasokSzama;
                int hossz, szel; //ágyások mélysége és szélessége
                string kezdoValasztas = "";
                List<forrasAdat> tesztLista = new List<forrasAdat>();
                List<string> valasztott = new List<string>();


                StreamReader olvas = new StreamReader("adatTeszt.txt");



                while (!olvas.EndOfStream)
                {

                    string egysor = olvas.ReadLine();
                    forrasAdat adatok = new forrasAdat(egysor);
                    tesztLista.Add(adatok);
                }

                olvas.Close();



    

            
                //Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("\n Üdvözöllek a Vetéstervezőben! \n"); Console.ResetColor();

    

            Console.Write("\t("); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("T"); Console.ResetColor(); Console.Write(") Tervező\n");
                Console.Write("\t("); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Ú"); Console.ResetColor(); Console.Write(") Új növények feltöltése\n");
                Console.Write("\t("); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("K"); Console.ResetColor(); Console.ResetColor(); Console.Write(") Kilépés\n\n");

                while (true)
                {

                    Console.Write("\t Választásod: "); Console.ForegroundColor = ConsoleColor.Yellow;
                    kezdoValasztas = Console.ReadLine(); Console.ResetColor();

                    if (kezdoValasztas == "T" || kezdoValasztas == "t")
                    {
                        break;
                    }
                    if (kezdoValasztas == "Ú" || kezdoValasztas == "ú")
                    {
                        break;
                    }
                    if (kezdoValasztas == "K" || kezdoValasztas == "k")
                    {
                        break;
                    }
                    else
                    {
                        Console.ResetColor(); Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("\t  A rendelkezése álló lehetőségek közül válassz!"); Console.ResetColor();
                    }

                }

            if (kezdoValasztas == "T" || kezdoValasztas == "t")
            {
                Console.Clear();

                do
                {
                    Console.ResetColor(); Console.Write("\tElőször kérlek add, hogy hány ágyást szeretnél beültetni (szám): "); Console.ResetColor(); Console.ForegroundColor = ConsoleColor.DarkGreen;
                } while (!int.TryParse(Console.ReadLine(), out agyasokSzama));


                Console.ResetColor(); Console.WriteLine("\n\tIlletve kérlek add meg, hogy megkkorák az ágyások");
                Console.WriteLine("\t Egy ilyen csillag: *, 20 négyzetcentit és egy növényt jelent.\n\t Kérlek ennek fényében add meg a pontos adatokat!\n");

                do
                {
                    Console.ResetColor(); Console.Write("\t- Az ágyások hosszúsága (szám, cm): "); Console.ForegroundColor = ConsoleColor.DarkGreen;
                } while (!int.TryParse(Console.ReadLine(), out hossz));
                do
                {
                    Console.ResetColor(); Console.Write("\t- Az ágyások szélessége (szám, cm): "); Console.ForegroundColor = ConsoleColor.DarkGreen;
                } while (!int.TryParse(Console.ReadLine(), out szel));

                Console.ResetColor(); Console.WriteLine("\n");


                Console.Write("\n\tMost pedig válaszd ki, hogy milyen növényeket szeretnél elülteni:\n\t - Kilépés ("); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("0"); Console.ResetColor(); Console.Write(")\n\n");

                for (int i = 0; i < tesztLista.Count; i++)
                {
                    Console.Write("\t - {0} (", tesztLista[i].novenyNeve); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("{0}", tesztLista[i].novenybetuNagy); Console.ResetColor(); Console.Write(")\n");
                }

                while (true)
                {
                    Console.ResetColor();
                    Console.Write("\n\t - ");
                    Console.ResetColor(); Console.ForegroundColor = ConsoleColor.DarkGreen; valaszottNoveny = Console.ReadLine();
                    Console.ResetColor();


                    if (betuk.Contains(valaszottNoveny))
                    {
                        //break;
                        string tempNoveny = "";
                        for (int i = 0; i < tesztLista.Count; i++)
                        {
                            if (valaszottNoveny == tesztLista[i].novenybetuNagy || valaszottNoveny == tesztLista[i].novenybetuKicsi)
                            {
                                tempNoveny = tesztLista[i].novenyNeve;
                            }
                        }

                        int tempnovenyekSzama = 0;

                        do
                        {
                            Console.ResetColor(); Console.Write("\tAdd meg mennyi {0}-t szeretnél elültetni: ", tempNoveny); Console.ForegroundColor = ConsoleColor.DarkGreen;
                        } while (!int.TryParse(Console.ReadLine(), out tempnovenyekSzama));


                        for (int i = 0; i < tempnovenyekSzama; i++)
                        {
                            valasztott.Add(tempNoveny);
                        }


                    }
                    if (valaszottNoveny == "0" || valaszottNoveny == "0")
                    {
                        break;
                    }
                    /*else
                    {
                        Console.ResetColor(); Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("\t  A rendelkezése álló lehetőségek közül válassz!"); Console.ResetColor();
                    }*/

                }

                //Console.WriteLine(valasztott);


                //Console.WriteLine("asd");
                //Console.WriteLine("\x1b[1masd\x1b[0m");

                Console.Clear();
                Console.Write("\nÁgyások száma: "); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("{0}\n", agyasokSzama);
                Console.ResetColor(); Console.Write("Ágyások hosszúsága: "); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("{0}\n", hossz);
                Console.ResetColor(); Console.Write("Ágyások szélessége: "); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("{0}\n\n", szel); Console.ResetColor();

                int tempSzam = agyasokSzama * (hossz * szel);
        

                if (valasztott.Count <= (agyasokSzama * (hossz * szel)))
                {
                   if (valasztott.Count != 0)
                   {

                    for (int ii = 0; ii < valasztott.Count; ii++)
                    {
                        for (int i = 1; i <= hossz; i++)
                        {
                            for (int j = 1; j < szel+1; j++)
                            {

                                if (valasztott.Count != 0)
                                {

                                    if (valasztott[0] == "kakukkfű")
                                    {
                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("*"); Console.ResetColor();
                                        valasztott.Remove("kakukkfű");

                                    }
                                    else if (valasztott[0] == "bazsalikom")
                                    {
                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkRed; Console.ForegroundColor = ConsoleColor.Red; Console.Write("*"); Console.ResetColor();
                                        valasztott.Remove("bazsalikom");
                                    } 

                                }
                                else
                                {
                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGray; Console.ForegroundColor = ConsoleColor.Gray; Console.Write("*"); Console.ResetColor();
                                }                                

                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine("\n");
                    }
                   }
                } else
                {
                    if (valasztott.Count != 0)
                    {

                        List<string> tempSzamok = new List<string>();

                        for (int i = 0; i < tempSzam+1; i++)
                        {
                            tempSzamok.Add("o");
                        }

                        for (int ii = 0; ii < tempSzamok.Count; ii++)
                        {
                            for (int i = 1; i <= hossz; i++)
                            {
                                for (int j = 1; j < szel + 1; j++)
                                {

                                    if (valasztott.Count != 0)
                                    {

                                        if (valasztott[0] == "kakukkfű")
                                        {
                                            Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("*"); Console.ResetColor();
                                            valasztott.Remove("kakukkfű");
                                            tempSzamok.Remove("o");

                                        }
                                        else if (valasztott[0] == "bazsalikom")
                                        {
                                            Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkRed; Console.ForegroundColor = ConsoleColor.Red; Console.Write("*"); Console.ResetColor();
                                            valasztott.Remove("bazsalikom");
                                            tempSzamok.Remove("o");
                                        }

                                    }
                                    else
                                    {
                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGray; Console.ForegroundColor = ConsoleColor.Gray; Console.Write("*"); Console.ResetColor();
                                    }

                                }
                                Console.WriteLine();
                            }
                            Console.WriteLine("\n");
                        }
                    }



                    HashSet<string> maradek = new HashSet<string>();

                    for (int i = 0; i < valasztott.Count; i++)
                    {
                        maradek.Add(valasztott[i]);
                    }
                    

                    Console.Write("Az ágyások nem elegek az összes növény beültetéséhez.\nÖsszesen {0} maradt ki, ezekből a növényekből:\n - ", valasztott.Count);
                    Console.Write(string.Join("\n - ", maradek));
                    //Console.WriteLine("\nösszesen {0} maradt ki.", valasztott.Count);

                }
                
                






                }


                if (kezdoValasztas == "Ú" && kezdoValasztas == "ú")
                {

                }


                if (kezdoValasztas == "K" || kezdoValasztas == "k")
                {
                   Environment.Exit(0);
                }

                Console.ResetColor(); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("\n\nKilépsz? "); Console.ResetColor(); Console.Write("(Nyomj meg egy billentyűt)");
                Console.ReadKey();
        }
	}
}