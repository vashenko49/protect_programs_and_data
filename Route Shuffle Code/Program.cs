using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Route_Shuffle_Code
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Route Shuffle Code");
            Console.WriteLine("Enter phrase");
            string phrase = Console.ReadLine();
            phrase = phrase.Replace(' ', '-');

            int h = Convert.ToInt32(Math.Ceiling(Math.Sqrt(phrase.Length)));
            
            string[,] table = new string[h,h];

            for (int i = 0, k=0; i < h; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    table[i, j] = k >= phrase.Length ? "-" : Convert.ToString(phrase[k++]);
                }
            }

            string result = "";

            for (int i = 0, k = 0; i < h; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result += table[j, i];
                }
            }

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
