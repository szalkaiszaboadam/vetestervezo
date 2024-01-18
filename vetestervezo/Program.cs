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

            #region Adatok bekérése
            //Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nÜdvözöllek a Vetéstervezőben!\n");

            do
            {
                Console.Write("\tElőször kérlek add, hogy hány ágyást szeretnél beültetni (szám): ");
            } while (!int.TryParse(Console.ReadLine(), out agyasokSzama));

            Console.WriteLine("\n\tIlletve kérlek add meg, hogy megkkorák az ágyások");

            do
            {
                Console.Write("\t  Az ágyások mélysége (szám): ");
            } while (!int.TryParse(Console.ReadLine(), out mely));
            do
            {
                Console.Write("\t  Az ágyások szélessége (szám): ");
            } while (!int.TryParse(Console.ReadLine(), out szel));

            Console.WriteLine("\n");

            /*Console.Write("magasság");
			mag=Convert.ToInt32(Console.ReadLine());
            Console.Write("szélesség:");
			szel=Convert.ToInt32(Console.ReadLine());*/
            #endregion

            Console.WriteLine("asd");
            Console.WriteLine("\x1b[1masd\x1b[0m");

          


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



			Console.ReadKey();
        }
	}
}