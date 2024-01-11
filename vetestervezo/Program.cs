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
        /*// usage: WriteColor("This is my [message] with inline [color] changes.", ConsoleColor.Yellow);
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
        }*/

        int Height = 15;
        int Width = 20;

        public void WriteBorad()
        {
            Console.Clear();
            for (int i = 1; i <=(Width+2) ; i++)
            {
                Console.SetCursorPosition(i, 1);
                //Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.White; Console.Write("#");
                Console.Write("▓");
            }
            for (int i = 1; i <= (Width + 2); i++)
            {
                Console.SetCursorPosition(i, (Height+2));
                //Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.White; Console.Write("#");
                Console.Write("▓");
            }
            for (int i = 1; i <= (Height + 1); i++)
            {
                Console.SetCursorPosition(1, i);
                //Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.White; Console.Write("#");
                Console.Write("▓");
            }
            for (int i = 1; i <= (Height + 1); i++)
            {
                Console.SetCursorPosition((Width+2), i);
                //Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.White; Console.Write("#");
                Console.Write("▓");
            }

            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            
            Program tabla = new Program();
            tabla.WriteBorad();
            
            
            
            
            //Console.BackgroundColor = ConsoleColor.Blue; Console.Write(" ");
            //Console.ForegroundColor = ConsoleColor.White;

            Console.ResetColor(); Console.BackgroundColor = ConsoleColor.Red; Console.Write(" ");


            //WriteColor("This is my [message] with inline [color] changes.", ConsoleColor.Yellow);

            Console.ReadKey();
        }
    }


}
