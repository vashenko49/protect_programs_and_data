using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertical_permutation_cipher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Vertical permutation cipher вертикальной перестановки");
            Console.WriteLine("Enter phrase");
            string phrase = Console.ReadLine();
            Console.WriteLine("Enter key");
            string key = Console.ReadLine();

            string aphabet = "abcdefghijklmnopqrstuvwxyz";

            int height = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(phrase.Length) / Convert.ToDecimal(key.Length)));

            string[,] table = new string[key.Length,height+2];

            for (int i = 2, k = 0; i < height+2; i++)
            {
                for (int j = 0; j <key.Length ; j++)
                {
                    table[j, i] = k >= phrase.Length ? "-" : Convert.ToString(phrase[k++]);
                }
            }


            for (int j = 0; j < key.Length; j++)
            {

                table[j, 0] = Convert.ToString(key[j]);
                
            }

            int position = 0;
            foreach (var element in aphabet)
            {
                for (int i = 0; i < key.Length; i++)
                {
                    if (element == key[i])
                    {
                        table[i, 1] = Convert.ToString(position++);
                    }
                }
            }

            string result = "";

            position = 0;

            while (position<key.Length)
            {
                for (int i = 0; i < key.Length; i++)
                {
                    if (position == Convert.ToInt32(table[i, 1]))
                    {
                        for (int j = 2; j < height+2; j++)
                        {
                            result += table[i, j];
                        }
                        position++;
                        break;
                    }
                }

            }

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
