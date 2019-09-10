using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace double_permutation
{
    class Program
    {
        static void writeArray(ref string[,] array)
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
        static void replaseColom(ref string[,] array, int first, int second)
        {
            string[] temp =new string[array.GetLength(0)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                temp[i] = array[i, first];
                array[i, first] = array[i, second];
                array[i, second] = temp[i];

            }
        }

        static void replaseLine(ref string[,] array, int first, int second)
        {
            string[] temp = new string[array.GetLength(1)];
            for (int i = 0; i < array.GetLength(1); i++)
            {
                temp[i] = array[first,i];
                array[first,i] = array[second,i];
                array[second,i] = temp[i];

            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("double permutation");
            Console.WriteLine("Enter phrase");
            string phrase = Console.ReadLine();
            phrase = phrase.Replace(" ", "-");
            Console.WriteLine("Enter first phrase-key");
            string keyPhrase = Console.ReadLine();
            Console.WriteLine("Enter second number-key");
            string keyNumber = Console.ReadLine();

            string aphabet = "abcdefghijklmnopqrstuvwxyz";

            if (phrase.Length < keyNumber.Length * keyPhrase.Length)
            {
                int height = keyNumber.Length + 2;
                int weight = keyPhrase.Length + 1;

                string[,] table = new string[height,weight];

                for (int i = 2, j =0; i < height; i++,j++)
                {
                    table[i, 0] = Convert.ToString(keyNumber[j]);
                }

                for (int i = 1, j =0; i < weight; i++,j++)
                {
                    table[0, i] = Convert.ToString(keyPhrase[j]);
                }


                int postion = 0;
                foreach (var element in aphabet)
                {
                    for (int i = 1, j = 0; i < weight; i++, j++)
                    {
                        if (Convert.ToChar(table[0, i]) == element)
                        {
                            table[1, i] = Convert.ToString(postion++);
                        }
                    }
                }

                Random random = new Random();




                //заполняем
                for (int i = 2, p =0; i < height; i++)
                {
                    for (int j = 1; j < weight; j++)
                    {
                        table[i, j] = p >= phrase.Length ? "-" : Convert.ToString(phrase[p]);
                        ++p;
                    }
                }


                //вывод до сортировки
                writeArray(ref table);

                //сортировка столбцов
                for (int i = 1; i < weight; i++)
                {
                    for (int j = i + 1; j < weight; j++)
                    {
                        if (Convert.ToInt32(table[1, i]) > Convert.ToInt32(table[1, j]))
                        {
                            replaseColom(ref table, i, j);
                        }
                    }
                }

                //сортировка рядков

                for (int i = 2; i < height; i++)
                {
                    for (int j = i + 1; j < height; j++)
                    {
                        if (Convert.ToInt32(table[i,0]) > Convert.ToInt32(table[j,0]))
                        {
                            replaseLine(ref table, i, j);
                        }
                    }
                }

                writeArray(ref table);

                string result = "";

                for (int i = 1; i < weight; i++)
                {
                    for (int j = 2; j < height; j++)
                    {
                        result += table[j, i];
                    }
                }

                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("keys is small");
            }
            Console.ReadKey();
        }
    }
}
