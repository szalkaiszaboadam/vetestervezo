using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace vetestervezo
{

    //https://www.youtube.com/watch?app=desktop&v=_GmjXi1kcHA

    internal class Program
    {
        // usage: WriteColor("This is my [message] with inline [color] changes.", ConsoleColor.Yellow);
        static void WriteColor(string message, ConsoleColor color)
        {
            var pieces = Regex.Split(message, @"(\[[^\]]*\])");

            for (int i = 0; i < pieces.Length; i++)
            {
                string piece = pieces[i];

                if (piece.StartsWith("[") && piece.EndsWith("]"))
                {
                    Console.ForegroundColor = color;
                    piece = piece.Substring(1, piece.Length - 2);
                }

                Console.Write(piece);
                Console.ResetColor();
            }

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Blue; Console.Write(" ");
            //Console.ForegroundColor = ConsoleColor.White;

            Console.ResetColor(); Console.BackgroundColor = ConsoleColor.Red; Console.Write(" ");


            WriteColor("This is my [message] with inline [color] changes.", ConsoleColor.Yellow);

            Console.ReadKey();
        }
    }


}
