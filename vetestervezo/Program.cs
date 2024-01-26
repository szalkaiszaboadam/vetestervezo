using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vetestervezo
{

    class forrasAdat
    {
        public string vendegAzonosito;
        public string reszlegAzonosito;
        public string kiBe;

        public int osszMasodperc;

        public int ora;  // public byte ora;
        public int perc; // public byte perc;
        public int mp;   // public byte mp;

        public forrasAdat(string sor)
        {
            string[] m = sor.Split(' ');
            vendegAzonosito = m[0];
            reszlegAzonosito = m[1];
            kiBe = m[2];

            osszMasodperc = (Convert.ToInt32(m[3]) * 3600) + (Convert.ToInt32(m[4]) * 60) + (Convert.ToInt32(m[5]));

            ora = Convert.ToInt32(m[3]);
            perc = Convert.ToInt32(m[4]);
            mp = Convert.ToInt32(m[5]);

        }
    }


    class Program
	{

            static void Main(string[] args)
		    {


            
            List<forrasAdat> tesztLista = new List<forrasAdat>();

            // csevegesAdat[] csevegesek = new csevegesAdat[1000];
            StreamReader olvas = new StreamReader("csevegesek.txt");

            //int length = 0;

            //olvas.ReadLine();

            while (!olvas.EndOfStream)
            {
                //csevegesek[length] = new csevegesAdat(olvas.ReadLine());
                //length++;
                string egysor = olvas.ReadLine();
                forrasAdat adatok = new forrasAdat(egysor);
                tesztLista.Add(adatok);
            }

            olvas.Close();
            





            int agyasokSzama;
            int mely, szel; //ágyások mélysége és szélessége
            string kezdoValasztas = "";

            
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

                do
                {
                    Console.ResetColor();  Console.Write("\t- Az ágyások mélysége (szám): "); Console.ForegroundColor = ConsoleColor.DarkGreen;
                } while (!int.TryParse(Console.ReadLine(), out mely));
                do
                {
                    Console.ResetColor(); Console.Write("\t- Az ágyások szélessége (szám): "); Console.ForegroundColor = ConsoleColor.DarkGreen;
                } while (!int.TryParse(Console.ReadLine(), out szel));

                Console.ResetColor(); Console.WriteLine("\n");
                Console.Clear();
                Console.Write("\n- Ágyások száma: "); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("{0}\n", agyasokSzama);
                Console.ResetColor(); Console.Write("- Ágyások mélyésge: "); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("{0}\n", mely);
                Console.ResetColor();  Console.Write("- Ágyások mélysége: "); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("{0}\n\n", szel); Console.ResetColor();

                /*Console.Write("magasság");
                mag=Convert.ToInt32(Console.ReadLine());
                Console.Write("szélesség:");
                szel=Convert.ToInt32(Console.ReadLine());*/

                //Console.WriteLine("asd");
                //Console.WriteLine("\x1b[1masd\x1b[0m");

                for (int y = 0; y < agyasokSzama; y++)
			    {
				    for (int i = 0; i <= mely; i++)
				    {
					    //Console.WriteLine("asd");
					    for (int j = 1; j < szel; j++)
					    {
					
						    Console.Write("*");
					    }
					    Console.WriteLine();
				    }
				    Console.WriteLine("\n");
			    }





            }


            if (kezdoValasztas == "Ú" && kezdoValasztas == "ú")
            {

            }


            if (kezdoValasztas == "K" || kezdoValasztas == "k")
            {
                Environment.Exit(0);
            }

            Console.ResetColor(); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Kilépsz? "); Console.ResetColor(); Console.Write("(Nyomj meg egy billentyűt)");
            Console.ReadKey();
        }
	}
}