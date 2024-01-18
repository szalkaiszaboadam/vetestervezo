using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vetestervezo
{
	class Program
	{
		static void Main(string[] args)
		{
            int agyasokSzama;
            int mely, szel; //ágyások mélysége és szélessége
            string kezdoValasztas = "";

            
            //Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.DarkGreen; Console.WriteLine("\n Üdvözöllek a Vetéstervezőben! \n"); Console.ResetColor();

            Console.WriteLine("\t(T) Tervező");
            Console.WriteLine("\t(Ú) Új növények feltöltése");
            Console.WriteLine("\t(K) Kilépés");
           
            while (true)
            {

                Console.Write("kérlek: ");
                kezdoValasztas = Console.ReadLine();

                if (kezdoValasztas == "T")
                {
                    break;
                }
                if (kezdoValasztas == "Ú")
                {
                    break;
                }
                if (kezdoValasztas == "K")
                {
                    break;
                }
                else
                {
                    Console.ResetColor(); Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("A rendelkezése álló lehetőségek közül válassz"); Console.ResetColor();
                }

            }

            if (kezdoValasztas == "T")
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
            if (kezdoValasztas == "K")
            {
                Environment.Exit(0);
            }

 









			Console.ReadKey();
        }
	}
}