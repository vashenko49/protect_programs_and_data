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

            if (phrase.Length == key.Length)
            {



                for (int i = 0; i < key.Length; i++)
                {
                    table[0, i] = i;
                }

                for (int i = 0; i < key.Length; i++)
                {
                    table[1, i] = Convert.ToInt32(Convert.ToString(key[i]));
                }

                string result = "";

                for (int i = 0; i < phrase.Length; i++)
                {
                    for (int j = 0; j < phrase.Length; j++)
                    {
                        if (table[1, i] == table[0, j])
                        {
                            result += phrase[j];
                        }


                    }
                }

                Console.WriteLine(result);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("length aren't equal'");
            }
        }
    }
}
