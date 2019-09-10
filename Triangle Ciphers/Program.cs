using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle_Ciphers
{
    class Program
    {
        static void writeArray(ref int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + " ");
                }

                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Triangle Ciphers");
            Console.WriteLine("Enter phrase");
            string phrase = Console.ReadLine();
            Console.WriteLine("Enter key");
            string key = Console.ReadLine();

            int[,] gogo = new int[5,5];

            Random rdm = new Random();

            for (int i = 0; i < gogo.GetLength(0); i++)
            {
                for (int j = 0; j < gogo.GetLength(1); j++)
                {
                    gogo[i, j] = rdm.Next(0, 150);
                }
            }



            writeArray(ref gogo);



            

            for (int i = 0; i < gogo.GetLength(0)-1; ++i)
            {
                for (int j = 0; i + j < gogo.GetLength(0); ++j)
                {
                    if (i+j != i)
                    {
                        Console.Write(gogo[i + j, j] + " ");
                    }
                }

                Console.WriteLine();
            }



            ///
            for (int i = 1; i < gogo.GetLength(0); ++i)
            {
                for (int j = 0; i + j < gogo.GetLength(0); ++j)
                {
                    Console.Write(gogo[j, j+i] + " ");

                }
                Console.WriteLine();
            }



            Console.ReadKey();


        }
    }
}
