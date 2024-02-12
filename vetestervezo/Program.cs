using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Asn1.Esf;
using Spire.Pdf.Exporting.XPS.Schema;

namespace vetestervezo
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

    /*class valasztottAdat
    {
        public string nev;
        public string szeret;
        public string nemszeret;
    }*/


    class Program
    {

        static void Main(string[] args)
        {

            #region Adatbázis

            StreamWriter ir = new StreamWriter("adatTeszt.txt");
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

            #region Változoók

            //valasztottAdat valasztottAdatok = new valasztottAdat();

            string betuk = ""; //BOboCc
            string valaszottNoveny = "";
    
            //int /*hossz,*//* szel*/; //ágyások mélysége és szélessége
            string kezdoValasztas = "";
            List<forrasAdat> tesztLista = new List<forrasAdat>();
            List<string> valasztott = new List<string>();
            List<string> valasztottAllando = new List<string>();


            List<string> maradok = new List<string>();



            StreamReader olvas = new StreamReader("adatTeszt.txt");



            while (!olvas.EndOfStream)
            {

                string egysor = olvas.ReadLine();
                forrasAdat adatok = new forrasAdat(egysor);
                tesztLista.Add(adatok);
            }

            olvas.Close();


            for (int i = 0; i < tesztLista.Count; i++)
            {
                betuk += tesztLista[i].novenybetuNagy + tesztLista[i].novenybetuKicsi;
            }


            #endregion

            Console.CursorVisible = false;

            Console.WriteLine("\n Üdvözöllek a Vetéstervezőben! \n"); Console.ResetColor();

            Console.Write(" ["); Console.ForegroundColor = ConsoleColor.Green; Console.Write("T"); Console.ResetColor(); Console.Write("] Tervező\n");
            Console.Write(" ["); Console.ForegroundColor = ConsoleColor.Green; Console.Write("U"); Console.ResetColor(); Console.Write("] Új növények feltöltése\n");
            Console.Write(" ["); Console.ForegroundColor = ConsoleColor.Green; Console.Write("ESC"); Console.ResetColor(); Console.ResetColor(); Console.Write("] Kilépés\n");

            Console.WriteLine("\n *---------------------------* \n"); Console.ResetColor();

            ConsoleKeyInfo cki;
            //Console.TreatControlCAsInput = true;

            do
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.U)
                {
                    Console.Clear();

                    Console.Write("Növény neve: ");
                    string novenyneve = Console.ReadLine();
                    Console.Write("Kisbetű rövidítés: ");
                    string kisbetu = Console.ReadLine();
                    Console.Write("Nagybetű rövidítés: ");
                    string nagybetu = Console.ReadLine();
                    Console.Write("Szereti(zöldségek): ");
                    string szereti = Console.ReadLine();
                    Console.Write("Nem szereti(zöldségek): ");
                    string nemszereti = Console.ReadLine();




                    string Query = "INSERT INTO novenyek(novenyNeve,novenybetuNagy,novenybetuKicsi,szereti,nemszereti) VALUES('" + novenyneve + "','" + nagybetu + "','" + kisbetu + "','" + szereti + "','" + nemszereti + "');";
                    MySqlConnection MyConn2 = new MySqlConnection(constring);
                    MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                    MySqlDataReader MyReader2;
                    MyConn2.Open();
                    MyReader2 = MyCommand2.ExecuteReader();
                }
                if (cki.Key == ConsoleKey.T)
                {
                    Console.Clear();


                    int hossz = 0;
                    int szel = 0;
                    int agyasokSzama = 0;

                    Console.WriteLine("\n Kérjük add meg az ágyások pontos adatait\n");
                    Console.Write(" * Az ágyások számának megadásához nyomj egy ["); Console.ForegroundColor = ConsoleColor.Green; Console.Write("ENTER"); Console.ResetColor(); Console.Write("]-t\n");
                    Console.WriteLine(" * Az ágyások hosszúsága: 0");
                    Console.WriteLine(" * Az ágyások szélessége: 0");

                    Console.Write("\n Egy ilyen négyzet: "); Console.ForegroundColor = ConsoleColor.DarkGray; Console.BackgroundColor = ConsoleColor.Gray; Console.Write("[NN]"); Console.ResetColor(); Console.Write(",\n 20 négyzetcentit és egy növényt mutat.\n");

                    Console.WriteLine("\n *--------------------------------------* \n"); Console.ResetColor();

                    ConsoleKeyInfo cky;
                    do
                    {
                        cky = Console.ReadKey();
                        if (cky.Key == ConsoleKey.Enter)
                        {
                            Console.Clear();

                            Console.WriteLine("\n\n");
                            do
                            {

                                Console.ResetColor(); Console.Write(" * Az ágyások száma ["); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("potitív egész szám"); Console.ResetColor(); Console.Write("]: ");
                            } while (!int.TryParse(Console.ReadLine(), out agyasokSzama));

                            Console.Write("\n A véglegesítéshez nyomj egy ["); Console.ForegroundColor = ConsoleColor.Green; Console.Write("ESC"); Console.ResetColor(); Console.Write("]-t\n");

                        }

                    } while (cky.Key != ConsoleKey.Escape);


                    Console.Clear();
                    Console.WriteLine("\n Kérjük add meg az ágyások pontos adatait\n");
                    Console.WriteLine(" * Az ágyások száma: {0}", agyasokSzama);
                    Console.Write(" * Az ágyások hosszúságának megadásához nyomj egy ["); Console.ForegroundColor = ConsoleColor.Green; Console.Write("ENTER"); Console.ResetColor(); Console.Write("]-t\n");
                    Console.WriteLine(" * Az ágyások szélessége: 0");

                    Console.Write("\n Egy ilyen négyzet: "); Console.ForegroundColor = ConsoleColor.DarkGray; Console.BackgroundColor = ConsoleColor.Gray; Console.Write("[NN]"); Console.ResetColor(); Console.Write(",\n 20 négyzetcentit és egy növényt mutat.\n");

                    Console.WriteLine("\n *-------------------------------------* \n"); Console.ResetColor();

                    ConsoleKeyInfo ckyy;
                    do
                    {
                        ckyy = Console.ReadKey();
                        if (ckyy.Key == ConsoleKey.Enter)
                        {
                            Console.Clear();

                            Console.WriteLine("\n\n\n");
                            do
                            {

                                Console.ResetColor(); Console.Write(" * Az ágyások hosszúsága ["); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("potitív egész szám, 1-28 között, cm-ben"); Console.ResetColor(); Console.Write("]: ");
                            } while (!int.TryParse(Console.ReadLine(), out hossz));

                            Console.Write("\n A véglegesítéshez nyomj egy ["); Console.ForegroundColor = ConsoleColor.Green; Console.Write("ESC"); Console.ResetColor(); Console.Write("]-t\n");

                        }

                    } while (ckyy.Key != ConsoleKey.Escape);

                    Console.Clear();
                    Console.WriteLine("\n Kérjük add meg az ágyások pontos adatait\n");
                    Console.WriteLine(" * Az ágyások száma: {0}", agyasokSzama);
                    Console.WriteLine(" * Az ágyások hosszúsága: {0}",hossz);
                    Console.Write(" * Az ágyások szélességének megadásához nyomj egy ["); Console.ForegroundColor = ConsoleColor.Green; Console.Write("ENTER"); Console.ResetColor(); Console.Write("]-t\n");

                    Console.Write("\n Egy ilyen négyzet: "); Console.ForegroundColor = ConsoleColor.DarkGray; Console.BackgroundColor = ConsoleColor.Gray; Console.Write("[NN]"); Console.ResetColor(); Console.Write(",\n 20 négyzetcentit és egy növényt mutat.\n");

                    Console.WriteLine("\n *-------------------------------------* \n"); Console.ResetColor();

                    ConsoleKeyInfo ckyyy;
                    do
                    {
                        ckyyy = Console.ReadKey();
                        if (ckyyy.Key == ConsoleKey.Enter)
                        {
                            Console.Clear();

                            Console.WriteLine("\n\n\n\n");
                            do
                            {

                                Console.ResetColor(); Console.Write(" * Az ágyások szélessége ["); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("potitív egész szám, 1-28 között, cm-ben"); Console.ResetColor(); Console.Write("]: ");
                            } while (!int.TryParse(Console.ReadLine(), out szel));

                            Console.Write("\n A véglegesítéshez nyomj egy ["); Console.ForegroundColor = ConsoleColor.Green; Console.Write("ESC"); Console.ResetColor(); Console.Write("]-t\n");

                        }

                    } while (ckyyy.Key != ConsoleKey.Escape);

                    Console.Clear();
                    Console.WriteLine("\n Kérjük add meg az ágyások pontos adatait\n");
                    Console.WriteLine(" * Az ágyások száma: {0}", agyasokSzama);
                    Console.WriteLine(" * Az ágyások hosszúsága: {0}", hossz);
                    Console.WriteLine(" * Az ágyások szélessége: {0}", szel);

                    Console.Write("\n A növények kiválasztásához nyomj egy ["); Console.ForegroundColor = ConsoleColor.Green; Console.Write("ENTER"); Console.ResetColor(); Console.Write("]-t\n");

                    Console.WriteLine("\n *-------------------------------------* \n"); Console.ResetColor();

                    // Console.WriteLine("\n\tIlletve kérlek add meg, hogy megkkorák az ágyások");


                    /*Console.WriteLine("sdfsdf");
                    int hossz = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("dfsdf");
                    int szel = Convert.ToInt32(Console.ReadLine());*/



                    /*do
                    {
                        Console.ResetColor(); Console.Write("\t* Az ágyások hosszúsága ["); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("cm"); Console.ResetColor(); Console.Write("]: ");
                        hossz = int.Parse(Console.ReadLine());


                    } while (hossz > 28);
                     do
                     {
                         Console.ResetColor(); Console.Write("\t* Az ágyások szélessége (cm): "); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("cm"); Console.ResetColor(); Console.Write("]: ");
                        szel = int.Parse(Console.ReadLine());

                     } while (szel > 28);*/
                    /*!int.TryParse(Console.ReadLine(), out szel) && */

                    //Console.ResetColor(); Console.WriteLine("\n");

                    //Console.Clear();

                    Console.Write("\nMost pedig válaszd ki, hogy milyen növényeket szeretnél elülteni:\n\t - Kilépés ("); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("0"); Console.ResetColor(); Console.Write(")\n\n");

                    for (int i = 0; i < tesztLista.Count; i++)
                    {
                        Console.Write(" ["); Console.ForegroundColor = ConsoleColor.Green; Console.Write("{0}", tesztLista[i].novenybetuNagy); Console.ResetColor(); Console.Write("] {0}\n", tesztLista[i].novenyNeve);
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
                            string tempSzzereti = "";
                            for (int i = 0; i < tesztLista.Count; i++)
                            {
                                if (valaszottNoveny == tesztLista[i].novenybetuNagy || valaszottNoveny == tesztLista[i].novenybetuKicsi)
                                {
                                    tempNoveny = tesztLista[i].novenyNeve;
                                    tempSzzereti = tesztLista[i].szereti;
                                }
                            }

                            int tempnovenyekSzama = 0;

                            do
                            {
                                Console.ResetColor(); Console.Write("\tAdd meg mennyi {0}-t szeretnél elültetni: ", tempNoveny); Console.ForegroundColor = ConsoleColor.DarkGreen;
                            } while (!int.TryParse(Console.ReadLine(), out tempnovenyekSzama));

                            tempNoveny = tempNoveny.ToUpper();

                            for (int i = 0; i < tempnovenyekSzama; i++)
                            {
                                valasztott.Add(tempNoveny + ";" + tempSzzereti);
                                valasztottAllando.Add(tempNoveny + ";" + tempSzzereti);
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


                    Console.Clear();
                    //Console.Write("\nÁgyások száma: "); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("{0}\n", agyasokSzama);
                    //Console.ResetColor(); Console.Write("Ágyások hosszúsága: "); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("{0}\n", hossz);
                    //Console.ResetColor(); Console.Write("Ágyások szélessége: "); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("{0}\n\n", szel); Console.ResetColor();


                     Console.WriteLine("\n*-------------* Jelmagyarázat *-------------*\n");
                     Console.ResetColor();

                    Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[BA]"); Console.ResetColor(); Console.Write(" Bab \t"); Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkYellow; Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("[CÉ]"); Console.ResetColor(); Console.Write(" Cékla\t"); Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkMagenta; Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("[KÁ]"); Console.ResetColor(); Console.Write(" Káposzták\n"); Console.ResetColor();
                    Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[BA]"); Console.ResetColor(); Console.Write(" Bazsalikom\t"); Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[CU]"); Console.ResetColor(); Console.Write(" Cukkini\t"); Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkYellow; Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("[KA]"); Console.ResetColor(); Console.Write(" Karalábé\n"); Console.ResetColor();
                    Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[BO]");  Console.ResetColor(); Console.Write(" Borsikafű\t"); Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[FO]"); Console.ResetColor(); Console.Write(" Fokhagyma\t"); Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[KA]"); Console.ResetColor(); Console.Write(" Karfiol\n"); Console.ResetColor();
                    Console.BackgroundColor = ConsoleColor.DarkRed; Console.ForegroundColor = ConsoleColor.Red; Console.Write("[BO]"); Console.ResetColor(); Console.Write(" Borsó\t"); Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[KA]"); Console.ResetColor(); Console.Write(" Kakukkfű\t"); Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[KE]"); Console.ResetColor(); Console.Write(" Kelbimbó\n"); Console.ResetColor();
                    Console.BackgroundColor = ConsoleColor.DarkMagenta; Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("[BU]"); Console.ResetColor(); Console.Write(" Burgonya\t"); Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkRed; Console.ForegroundColor = ConsoleColor.Red; Console.Write("[KA]"); Console.ResetColor(); Console.Write(" Kapor\t"); Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[KU]"); Console.ResetColor(); Console.Write(" Kukorica\n"); Console.ResetColor();









                    





                    Console.WriteLine("\n");

                    int szamolo = 1;
                    int szamoloFelul = 0;
                    int tempSzam = agyasokSzama * (hossz * szel);


                    List<string> kuki = new List<string>();
                    for (int i = 0; i < tempSzam; i++)
                    {
                        kuki.Add("x");
                    }






                    if (valasztott.Count <= (agyasokSzama * (hossz * szel)))
                    {


                        if (valasztott.Count != 0)
                        {




                            for (int ii = 0; ii < valasztott.Count; ii++)
                            {
                                Console.Write("*");
                                for (int i = 1; i < szel + 1; i++)
                                {
                                    Console.Write("----");
                                }
                                Console.Write("*");
                                Console.WriteLine();


                                for (int i = 1; i <= hossz; i++)
                                {
                                    Console.Write("|");

                                    for (int j = 1; j < szel + 1; j++)
                                    {

                                        if (valasztott.Count != 0)
                                        {
                                            szamolo = szamolo - 1;



                                            if (szamoloFelul < szel)
                                            {
                                                if (valasztott[0].Contains("BAB") && valasztottAllando[szamolo].Contains("bab")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);

                                                }

                                                else if (valasztott[0].Contains("BAZSALIKOM") && valasztottAllando[szamolo].Contains("bazsalikom"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);

                                                }
                                                else if (valasztott[0].Contains("BORSIKAFŰ") && valasztottAllando[szamolo].Contains("borsikafű"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);

                                                }
                                                else if (valasztott[0].Contains("BORÓ") && valasztottAllando[szamolo].Contains("borsó"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkRed; Console.ForegroundColor = ConsoleColor.Red; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);

                                                }
                                                else if (valasztott[0].Contains("BURGONYA") && valasztottAllando[szamolo].Contains("burgonya"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkMagenta; Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);

                                                }
                                                else if (valasztott[0].Contains("CÉKLA") && valasztottAllando[szamolo].Contains("cékla"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkYellow; Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);

                                                }


                                                else if (valasztott[0].Contains("CUKKINI") && valasztottAllando[szamolo].Contains("cukkini")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);

                                                }

                                                else if (valasztott[0].Contains("FOKHAGYMA") && valasztottAllando[szamolo].Contains("fokhagyma"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);

                                                }
                                                else if (valasztott[0].Contains("KAKUKKFŰ") && valasztottAllando[szamolo].Contains("kakukkfű"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);

                                                }
                                                else if (valasztott[0].Contains("KAPOR") && valasztottAllando[szamolo].Contains("kapor"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkRed; Console.ForegroundColor = ConsoleColor.Red; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);

                                                }
                                                else if (valasztott[0].Contains("KÁPOSZTÁK") && valasztottAllando[szamolo].Contains("káposzták"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkMagenta; Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);

                                                }
                                                else if (valasztott[0].Contains("KARALÁBÉ") && valasztottAllando[szamolo].Contains("karalábé"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkYellow; Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);

                                                }

                                                else if (valasztott[0].Contains("KARFIOL") && valasztottAllando[szamolo].Contains("karfiol")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);

                                                }

                                                else if (valasztott[0].Contains("KELBIMBÓ") && valasztottAllando[szamolo].Contains("kelbimbó"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);

                                                }
                                                else if (valasztott[0].Contains("KUKORICA") && valasztottAllando[szamolo].Contains("kukorica"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);

                                                }



                                                else
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.White; Console.Write("éééé"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);
                                                    if (valasztott[0] != null)
                                                    {
                                                        maradok.Add(valasztott[0]);
                                                    }
                                                }



                                            }
                                            else
                                            {
                                                if (j == 1)
                                                {

                                                    //Console.ResetColor(); Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.White; Console.Write("óó"); Console.ResetColor();

                                                    if (valasztott[0].Contains("BAB") && valasztottAllando[szamoloFelul - szel].Contains("bab")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }
                                                    else if (valasztott[0].Contains("BAZSALIKOM") && valasztottAllando[szamoloFelul - szel].Contains("bazsalikom")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }
                                                    else if (valasztott[0].Contains("BORSIKAFŰ") && valasztottAllando[szamoloFelul - szel].Contains("borsikafű")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }
                                                    else if (valasztott[0].Contains("BORSÓ") && valasztottAllando[szamoloFelul - szel].Contains("borsó")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkRed; Console.ForegroundColor = ConsoleColor.Red; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }
                                                    else if (valasztott[0].Contains("BURGONYA") && valasztottAllando[szamoloFelul - szel].Contains("burgonya")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkMagenta; Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }
                                                    else if (valasztott[0].Contains("CÉKLA") && valasztottAllando[szamoloFelul - szel].Contains("cékla")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkYellow; Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }


                                                    else if (valasztott[0].Contains("CUKKINI") && valasztottAllando[szamoloFelul - szel].Contains("cukkini")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }
                                                    else if (valasztott[0].Contains("FOKHAGYMA") && valasztottAllando[szamoloFelul - szel].Contains("fokhagyma")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }
                                                    else if (valasztott[0].Contains("KAKUKKFŰ") && valasztottAllando[szamoloFelul - szel].Contains("kakukkfű")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }
                                                    else if (valasztott[0].Contains("KAPOR") && valasztottAllando[szamoloFelul - szel].Contains("kapor")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkRed; Console.ForegroundColor = ConsoleColor.Red; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }
                                                    else if (valasztott[0].Contains("KÁPOSZTÁK") && valasztottAllando[szamoloFelul - szel].Contains("káposzták")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkMagenta; Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }
                                                    else if (valasztott[0].Contains("KARALÁBÉ") && valasztottAllando[szamoloFelul - szel].Contains("karalábé")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkYellow; Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }


                                                    else if (valasztott[0].Contains("KARFIOL") && valasztottAllando[szamoloFelul - szel].Contains("karfiol")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }
                                                    else if (valasztott[0].Contains("KELBIMBÓ") && valasztottAllando[szamoloFelul - szel].Contains("kelbimbó")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }
                                                    else if (valasztott[0].Contains("KUKORICA") && valasztottAllando[szamoloFelul - szel].Contains("kukorica")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }




                                                    else
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.White; Console.Write("űűűű"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        if (valasztott[0] != null)
                                                        {
                                                            maradok.Add(valasztott[0]);
                                                        }
                                                    }

                                                }
                                                else
                                                {

                                                    if (valasztott[0].Contains("BAB") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("bab") && valasztottAllando[szamoloFelul - 1].Contains("bab")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }

                                                    else if (valasztott[0].Contains("BAZSALIKOM") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("bazsalikom") && valasztottAllando[szamoloFelul - 1].Contains("bazsalikom"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }

                                                    else if (valasztott[0].Contains("BORSIKAFŰ") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("borsikafű") && valasztottAllando[szamoloFelul - 1].Contains("borsikafű"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }
                                                    else if (valasztott[0].Contains("BORSÓ") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("borsó") && valasztottAllando[szamoloFelul - 1].Contains("borsó"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkRed; Console.ForegroundColor = ConsoleColor.Red; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }
                                                    else if (valasztott[0].Contains("BURGONYA") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("burgonya") && valasztottAllando[szamoloFelul - 1].Contains("burgonya"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkMagenta; Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }
                                                    else if (valasztott[0].Contains("CÉKLA") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("cékla") && valasztottAllando[szamoloFelul - 1].Contains("cékla"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkYellow; Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }



                                                    else if (valasztott[0].Contains("CUKKINI") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("bab") && valasztottAllando[szamoloFelul - 1].Contains("bab")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }

                                                    else if (valasztott[0].Contains("FOKHAGYMA") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("fokhagyma") && valasztottAllando[szamoloFelul - 1].Contains("fokhagyma"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }

                                                    else if (valasztott[0].Contains("KAKUKKFŰ") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("kakukkfű") && valasztottAllando[szamoloFelul - 1].Contains("kakukkfű"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }
                                                    else if (valasztott[0].Contains("KAPOR") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("kapor") && valasztottAllando[szamoloFelul - 1].Contains("kapor"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkRed; Console.ForegroundColor = ConsoleColor.Red; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }
                                                    else if (valasztott[0].Contains("KÁPOSZTÁK") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("káposzták") && valasztottAllando[szamoloFelul - 1].Contains("káposzták"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkMagenta; Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }
                                                    else if (valasztott[0].Contains("KARALÁBÉ") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("karalábé") && valasztottAllando[szamoloFelul - 1].Contains("karalábé"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkYellow; Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }



                                                    else if (valasztott[0].Contains("KARFIOL") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("karfiol") && valasztottAllando[szamoloFelul - 1].Contains("karfiol")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }

                                                    else if (valasztott[0].Contains("KELBIMBÓ") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("kelbimbó") && valasztottAllando[szamoloFelul - 1].Contains("kelbimbó"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }

                                                    else if (valasztott[0].Contains("KUKORICA") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("kukorica") && valasztottAllando[szamoloFelul - 1].Contains("kukorica"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);

                                                    }


                                                    else
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.White; Console.Write("áááá"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        if (valasztott[0] != null)
                                                        {
                                                            maradok.Add(valasztott[0]);
                                                        }

                                                    }

                                                }





                                            }

                                            szamolo++;
                                            szamoloFelul++;



                                        }
                                        else
                                        {
                                            Console.ResetColor(); Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.White; Console.Write("áááá"); Console.ResetColor();
                                        }

                                    }
                                    Console.Write("|");
                                    Console.WriteLine();
                                }

                                Console.Write("*");
                                for (int i = 1; i < szel + 1; i++)
                                {
                                    Console.Write("----");
                                }
                                Console.Write("*");
                                Console.WriteLine();

                                Console.WriteLine("\n");
                            }
                        }
                    }
                    else if (valasztott.Count > (agyasokSzama * (hossz * szel)))
                    {



                        if (valasztott.Count != 0)
                        {

                            for (int ii = 0; ii < kuki.Count; ii++)
                            {


                                Console.Write("*");
                                for (int i = 1; i < szel + 1; i++)
                                {
                                    Console.Write("----");
                                }
                                Console.Write("*");
                                Console.WriteLine();

                                for (int i = 1; i <= hossz; i++)
                                {
                                    Console.Write("|");

                                    for (int j = 1; j < szel + 1; j++)
                                    {

                                        if (valasztott.Count != 0)
                                        {
                                            szamolo = szamolo - 1;



                                            if (szamoloFelul < szel)
                                            {

                                                if (valasztott[0].Contains("BAB") && valasztottAllando[szamolo].Contains("bab")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);
                                                    kuki.Remove(kuki[0]);
                                                }

                                                else if (valasztott[0].Contains("BAZSALIKOM") && valasztottAllando[szamolo].Contains("bazsalikom"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);
                                                    kuki.Remove(kuki[0]);
                                                }
                                                else if (valasztott[0].Contains("BORSIKAFŰ") && valasztottAllando[szamolo].Contains("borsikafű"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);
                                                    kuki.Remove(kuki[0]);
                                                }
                                                else if (valasztott[0].Contains("BORSÓ") && valasztottAllando[szamolo].Contains("borsó"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkRed; Console.ForegroundColor = ConsoleColor.Red; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);
                                                    kuki.Remove(kuki[0]);
                                                }
                                                else if (valasztott[0].Contains("BURGONYA") && valasztottAllando[szamolo].Contains("burgonya"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkMagenta; Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);
                                                    kuki.Remove(kuki[0]);
                                                }
                                                else if (valasztott[0].Contains("CÉKLA") && valasztottAllando[szamolo].Contains("cékla"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkYellow; Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);
                                                    kuki.Remove(kuki[0]);
                                                }


                                                else if (valasztott[0].Contains("CUKKINI") && valasztottAllando[szamolo].Contains("cukkini")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);
                                                    kuki.Remove(kuki[0]);
                                                }

                                                else if (valasztott[0].Contains("FOKHAGYMA") && valasztottAllando[szamolo].Contains("fokhagyma"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);
                                                    kuki.Remove(kuki[0]);
                                                }
                                                else if (valasztott[0].Contains("KAKUKKFŰ") && valasztottAllando[szamolo].Contains("kakukkfű"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);
                                                    kuki.Remove(kuki[0]);
                                                }
                                                else if (valasztott[0].Contains("KAPOR") && valasztottAllando[szamolo].Contains("kapor"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkRed; Console.ForegroundColor = ConsoleColor.Red; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);
                                                    kuki.Remove(kuki[0]);
                                                }
                                                else if (valasztott[0].Contains("KÁPOSZTÁK") && valasztottAllando[szamolo].Contains("káposzták"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkMagenta; Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);
                                                    kuki.Remove(kuki[0]);
                                                }
                                                else if (valasztott[0].Contains("KARALÁBÉ") && valasztottAllando[szamolo].Contains("karalábé"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkYellow; Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);
                                                    kuki.Remove(kuki[0]);
                                                }

                                                else if (valasztott[0].Contains("KARFIOL") && valasztottAllando[szamolo].Contains("karfiol")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);
                                                    kuki.Remove(kuki[0]);
                                                }

                                                else if (valasztott[0].Contains("KELBIMBÓ") && valasztottAllando[szamolo].Contains("kelbimbó"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);
                                                    kuki.Remove(kuki[0]);
                                                }
                                                else if (valasztott[0].Contains("KUKORICA") && valasztottAllando[szamolo].Contains("kukorica"))
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);
                                                    kuki.Remove(kuki[0]);
                                                }

                                                else
                                                {
                                                    Console.ResetColor(); Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.White; Console.Write("éééé"); Console.ResetColor();
                                                    valasztott.Remove(valasztott[0]);
                                                    if (valasztott[0] != null)
                                                    {
                                                        maradok.Add(valasztott[0]);
                                                    }
                                                    kuki.Remove(kuki[0]);
                                                }


                                            }
                                            else
                                            {
                                                if (j == 1)
                                                {



                                                    //Console.ResetColor(); Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.White; Console.Write("óó"); Console.ResetColor();




                                                    if (valasztott[0].Contains("BAB") && valasztottAllando[szamoloFelul - szel].Contains("bab")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }
                                                    else if (valasztott[0].Contains("BAZSALIKOM") && valasztottAllando[szamoloFelul - szel].Contains("bazsalikom")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }
                                                    else if (valasztott[0].Contains("BORSIKAFŰ") && valasztottAllando[szamoloFelul - szel].Contains("borsikafű")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }
                                                    else if (valasztott[0].Contains("BORSÓ") && valasztottAllando[szamoloFelul - szel].Contains("borsó")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkRed; Console.ForegroundColor = ConsoleColor.Red; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }
                                                    else if (valasztott[0].Contains("BURGONYA") && valasztottAllando[szamoloFelul - szel].Contains("burgonya")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkMagenta; Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }
                                                    else if (valasztott[0].Contains("CÉKLA") && valasztottAllando[szamoloFelul - szel].Contains("cékla")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkYellow; Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }


                                                    else if (valasztott[0].Contains("CUKKINI") && valasztottAllando[szamoloFelul - szel].Contains("cukkini")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }
                                                    else if (valasztott[0].Contains("FOKHAGYMA") && valasztottAllando[szamoloFelul - szel].Contains("fokhagyma")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }
                                                    else if (valasztott[0].Contains("KAKUKKFŰ") && valasztottAllando[szamoloFelul - szel].Contains("kakukkfű")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }
                                                    else if (valasztott[0].Contains("KAPOR") && valasztottAllando[szamoloFelul - szel].Contains("kapor")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkRed; Console.ForegroundColor = ConsoleColor.Red; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }
                                                    else if (valasztott[0].Contains("KÁPOSZTÁK") && valasztottAllando[szamoloFelul - szel].Contains("káposzták")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkMagenta; Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }
                                                    else if (valasztott[0].Contains("KARALÁBÉ") && valasztottAllando[szamoloFelul - szel].Contains("karalábé")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkYellow; Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }


                                                    else if (valasztott[0].Contains("KARFIOL") && valasztottAllando[szamoloFelul - szel].Contains("karfiol")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }
                                                    else if (valasztott[0].Contains("KELBIMBÓ") && valasztottAllando[szamoloFelul - szel].Contains("kelbimbó")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }
                                                    else if (valasztott[0].Contains("KUKORICA") && valasztottAllando[szamoloFelul - szel].Contains("kukorica")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }







                                                    else
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.White; Console.Write("űűűű"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        if (valasztott[0] != null)
                                                        {
                                                            maradok.Add(valasztott[0]);
                                                        }
                                                        kuki.Remove(kuki[0]);
                                                    }
                                                }
                                                else
                                                {










                                                    if (valasztott[0].Contains("BAB") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("bab") && valasztottAllando[szamoloFelul - 1].Contains("bab")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }

                                                    else if (valasztott[0].Contains("BAZSALIKOM") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("bazsalikom") && valasztottAllando[szamoloFelul - 1].Contains("bazsalikom"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }

                                                    else if (valasztott[0].Contains("BORSIKAFŰ") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("borsikafű") && valasztottAllando[szamoloFelul - 1].Contains("borsikafű"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }
                                                    else if (valasztott[0].Contains("BORSÓ") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("borsó") && valasztottAllando[szamoloFelul - 1].Contains("borsó"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkRed; Console.ForegroundColor = ConsoleColor.Red; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }
                                                    else if (valasztott[0].Contains("BURGONYA") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("burgonya") && valasztottAllando[szamoloFelul - 1].Contains("burgonya"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkMagenta; Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }
                                                    else if (valasztott[0].Contains("CÉKLA") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("cékla") && valasztottAllando[szamoloFelul - 1].Contains("cékla"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkYellow; Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }



                                                    else if (valasztott[0].Contains("CUKKINI") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("bab") && valasztottAllando[szamoloFelul - 1].Contains("bab")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }

                                                    else if (valasztott[0].Contains("FOKHAGYMA") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("fokhagyma") && valasztottAllando[szamoloFelul - 1].Contains("fokhagyma"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }

                                                    else if (valasztott[0].Contains("KAKUKKFŰ") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("kakukkfű") && valasztottAllando[szamoloFelul - 1].Contains("kakukkfű"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }
                                                    else if (valasztott[0].Contains("KAPOR") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("kapor") && valasztottAllando[szamoloFelul - 1].Contains("kapor"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkRed; Console.ForegroundColor = ConsoleColor.Red; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }
                                                    else if (valasztott[0].Contains("KÁPOSZTÁK") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("káposzták") && valasztottAllando[szamoloFelul - 1].Contains("káposzták"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkMagenta; Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }
                                                    else if (valasztott[0].Contains("KARALÁBÉ") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("karalábé") && valasztottAllando[szamoloFelul - 1].Contains("karalábé"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkYellow; Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }



                                                    else if (valasztott[0].Contains("KARFIOL") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("karfiol") && valasztottAllando[szamoloFelul - 1].Contains("karfiol")) //valasztott[0].ToLower() //valasztott[0] !!!
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkBlue; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }

                                                    else if (valasztott[0].Contains("KELBIMBÓ") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("kelbimbó") && valasztottAllando[szamoloFelul - 1].Contains("kelbimbó"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.Green; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }

                                                    else if (valasztott[0].Contains("KUKORICA") && valasztottAllando[((szamoloFelul - j) - szel) + j].Contains("kukorica") && valasztottAllando[szamoloFelul - 1].Contains("kukorica"))
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.DarkCyan; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("[" + valasztott[0].Substring(0, 2) + "]"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                    }











                                                    else
                                                    {
                                                        Console.ResetColor(); Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.White; Console.Write("áááá"); Console.ResetColor();
                                                        valasztott.Remove(valasztott[0]);
                                                        kuki.Remove(kuki[0]);
                                                        if (valasztott[0] != null)
                                                        {
                                                            maradok.Add(valasztott[0]);
                                                        }
                                                    }
                                                }





                                            }

                                            szamolo++;
                                            szamoloFelul++;



                                        }
                                        else
                                        {
                                            Console.ResetColor(); Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.White; Console.Write("óóóó"); Console.ResetColor();
                                        }

                                    }
                                    Console.Write("|"); 
                                    Console.WriteLine();
                                }


                                Console.Write("*");
                                for (int i = 1; i < szel + 1; i++)
                                {
                                    Console.Write("----");
                                }
                                Console.Write("*");
                                Console.WriteLine();

                                Console.WriteLine("\n");
                            }
                        }



                        HashSet<string> maradek = new HashSet<string>();

                        for (int i = 0; i < valasztott.Count; i++)
                        {
                            maradek.Add(valasztott[i].Substring(0, 2));
                        }

                         
                        Console.Write("["); Console.ResetColor(); Console.ForegroundColor = ConsoleColor.Red; Console.Write("Az megadott ágyások és növények ezen formájában {0} növény\nnem ültethező be. A következő növényekből maradtak ki: ", valasztott.Count); 
                        Console.Write(string.Join(", ", maradek)); Console.ResetColor(); Console.Write("]\n ");
                        Console.ResetColor();
                        //Console.WriteLine("\nösszesen {0} maradt ki.", valasztott.Count);


                    }



                    Console.WriteLine();



                    HashSet<string> maradek2 = new HashSet<string>();

                    if (maradok.Count != 0)
                    {
                        for (int i = 0; i < maradok.Count; i++)
                        {
                            maradek2.Add(maradok[i].Substring(0, 2));
                        }

                        Console.Write("["); Console.ResetColor(); Console.ForegroundColor = ConsoleColor.Red;  Console.Write("Az ágyások nem elegek az összes növény beültetéséhez.\n Összesen {0} maradt ki, a következő növényfajtákból: ", maradok.Count); 
                        Console.Write(string.Join(", ", maradek2)); Console.ResetColor(); Console.Write("]\n ");
                        Console.ResetColor();
                        //Console.WriteLine("\nösszesen {0} maradt ki.", valasztott.Count);
                    }
                }

                    //Console.WriteLine(cki.Key.ToString());
            } while (cki.Key != ConsoleKey.Escape);




            /*while (true)
            {

                Console.Write("\t Választásod: "); Console.ForegroundColor = ConsoleColor.Yellow;
                kezdoValasztas = Console.ReadLine(); Console.ResetColor();

                if (kezdoValasztas == "T" || kezdoValasztas == "t")
                {
                    break;
                }
                if (kezdoValasztas == "U" || kezdoValasztas == "u")
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

            }*/



            /*if (kezdoValasztas == "T" || kezdoValasztas == "t")
            {
     

            }*/


            /*if (kezdoValasztas == "U" || kezdoValasztas == "u")
            {
                //Environment.Exit(0);
                Console.Write("Növény neve: ");
                string novenyneve = Console.ReadLine();
                Console.Write("Kisbetű rövidítés: ");
                string kisbetu = Console.ReadLine();
                Console.Write("Nagybetű rövidítés: ");
                string nagybetu = Console.ReadLine();
                Console.Write("Szereti(zöldségek): ");
                string szereti = Console.ReadLine();
                Console.Write("Nem szereti(zöldségek): ");
                string nemszereti = Console.ReadLine();




                string Query = "INSERT INTO novenyek(novenyNeve,novenybetuNagy,novenybetuKicsi,szereti,nemszereti) VALUES('" + novenyneve + "','" + nagybetu + "','" + kisbetu + "','" + szereti + "','" + nemszereti + "');";
                MySqlConnection MyConn2 = new MySqlConnection(constring);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
            }*/
            
            /*if (kezdoValasztas == "K" && kezdoValasztas == "k")
            {
                Environment.Exit(0);
            }*/

            //Console.ResetColor(); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("\n\nKilépsz? \n"); Console.ResetColor(); Console.Write("(Nyomj meg egy billentyűt)");
            //Console.ReadKey();

        }
    }
}