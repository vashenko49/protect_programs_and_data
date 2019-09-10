using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magic_square
{
    class Program
    {
        static void Swap(int a, int b)
        {
            var T = a;
            a = b;
            b = T;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("magic square магический квадрат");
            int i, j, k;

            Console.WriteLine("Enter phrase");
            string phrase = Console.ReadLine();
            phrase = phrase.Replace(" ", "-");
            int n =Math.Ceiling(Math.Sqrt(phrase.Length)) % 2 != 0 ? Convert.ToInt32(Math.Ceiling(Math.Sqrt(phrase.Length))) : Convert.ToInt32(Math.Ceiling(Math.Sqrt(phrase.Length))) + 1;

            var a = new int[n + 1, n + 1];
            string[,] table = new string[n + 1, n + 1];


            i = 1;
            j = (n / 2) + 1; 

            for (k = 1; k <= n * n; k++)
            {
                a[i, j] = k;
                if ((k % n) == 0) i++;
                else
                {
                    i--;
                    j++;
                    if (i == 0) i = n;
                    if (j > n) j = 1;
                }
            }


            int position = 1;

            foreach (var element in phrase)
            {
                for (i = 1; i <= n; i++)
                {
                    for (j = 1; j <= n; j++)
                    {
                        if (a[i, j] == position)
                        {
                            table[i, j] = Convert.ToString(element);
                        }
                        else
                        {
                            if (table[i, j] == null)
                            {
                                table[i, j] = "-";
                            }

                        }
                    }
                }

                position++;
            }


            for (i = 1; i <= n; i++)
            {
                for (j = 1; j <= n; j++)
                {
                    Console.Write(a[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }

            for (i = 1; i <= n; i++)
            {
                for (j = 1; j <= n; j++)
                {
                    Console.Write(table[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }

            string result = "";

            for (i = 1; i <= n; i++)
            {
                for (j = 1; j <= n; j++)
                {
                    result += table[i, j];
                }
            }

            Console.WriteLine(result);

            Console.ReadKey(true);
        }
    }
}
