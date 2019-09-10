using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace block_single_permutation
{
    class Program
    {
        static Random random = new Random((int)DateTime.Now.Ticks);
        static string RandomString()
        {
            StringBuilder builder = new StringBuilder();
            char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
            return builder.ToString();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("block single permutation");
            Console.WriteLine("Enter phrase");
            string phrase = Console.ReadLine();
            Console.WriteLine("Enter key");
            string key = Console.ReadLine();

            int[,] table = new int[2, key.Length];


            for (int i = 0; i < key.Length; i++)
            {
                table[0, i] = i;
            }

            for (int i = 0; i < key.Length; i++)
            {
                table[1, i] = Convert.ToInt32(Convert.ToString(key[i]));
            }

            string result = "";


            for (int i = 0;
                i < key.Length*Math.Ceiling(Convert.ToDouble(phrase.Length) / Convert.ToDouble(key.Length));
                i+= key.Length)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    for (int k = 0; k < key.Length; k++)
                    {
                        if (table[1, j] == table[0, k])
                        {
                            result += k + i >= phrase.Length ? "-" : Convert.ToString(phrase[k + i]);
                        }
                    }
                }
            }

            Console.WriteLine(result);
            Console.ReadKey();
        }   
    }
}
