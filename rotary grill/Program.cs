using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rotary_grill
{
    class Program
    {
        static int[] randomCoord(int max, int min)
        {
            int[] result = new int[2];
            
            result[0] = new Random().Next(min, max);
            result[1] = new Random().Next(min, max);

            return result;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("rotary grill");
            Console.WriteLine("Enter phrase");
            string phrase = Console.ReadLine();

            int h = Math.Ceiling(Math.Sqrt(phrase.Length)) % 2 == 0 ? Convert.ToInt32(Math.Ceiling(Math.Sqrt(phrase.Length))) : Convert.ToInt32(Math.Ceiling(Math.Sqrt(phrase.Length))) + 1;

            string[,] template = new string[h,h];
            string[,] table = new string[h,h];





            Console.WriteLine(h);
            Console.ReadKey();
        }
    }
}
