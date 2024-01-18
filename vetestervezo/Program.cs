using System;

namespace kerstboom
{
    class Program
    {
        static void Main(string[] args)
        {
            // Enter Height //
            Console.WriteLine("Enter desired height:");
            // Read number entered //
            int n = int.Parse(Console.ReadLine());

            String stars = "";
            String spaces = "";

            for (int i = 0; i <= n; i++)
            {
                stars = new string('*', i);
                spaces = new string(' ', n - i);
                Console.Write(spaces);
                Console.Write(stars);
                Console.Write("*");
                Console.Write(stars);
                Console.WriteLine(spaces);
            }

            spaces = new string(' ', stars.Length + spaces.Length);
            Console.Write(spaces);
            Console.Write('*');

            Console.ReadKey();
        }
    }
}