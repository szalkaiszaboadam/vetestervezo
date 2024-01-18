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
			int h, w;
			Console.Write("magasság");
			h=Convert.ToInt32(Console.ReadLine());
            Console.Write("szélesség:");
			w=Convert.ToInt32(Console.ReadLine());


			for (int i = 0; i <= h; i++)
			{
				for (int j = 1; j < w; j++)
				{
                    Console.Write("*");
                }
                Console.WriteLine();
            }


			Console.ReadKey();
        }
	}
}