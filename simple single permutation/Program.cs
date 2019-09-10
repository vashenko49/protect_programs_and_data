using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_single_permutation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("simple single permutation простой одинарной перестановки");
            Console.WriteLine("Enter phrase");
            string phrase = Console.ReadLine();

            int[,] table = new int[2,phrase.Length];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < phrase.Length; j++)
                {
                    table[i, j] = j;
                }
            }
            Random rand = new Random();

            for (int i = phrase.Length - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);

                int tmp = table[1,j];
                table[1,j] = table[1,i];
                table[1,i] = tmp;
            }

            string result = "";

            for (int i = 0; i < phrase.Length; i++)
            {
                int temp = table[0, i];

                for (int j = 0; j < phrase.Length; j++)
                {
                    if (temp == table[1, j])
                    {
                        temp = table[0, j];
                        break;
                    }
                }

                result += phrase[temp];
            }

            Console.WriteLine("encrypted phrase");
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
