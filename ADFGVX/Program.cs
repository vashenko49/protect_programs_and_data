using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ADFGVX
{
    class Program
    {
        private static readonly string alphabet = "abcdefghijklmnopqrstuvwxyz ,-+_0123456789.";
        private static readonly string ADFGVX = "ADFGVX";

        static void Main(string[] args)
        {
            Console.WriteLine("ADFGVX");
            Console.WriteLine("Enter phrase:");
            string phrase = Console.ReadLine();

            char[,] table = new char[7,7];



            for (int i = 1; i < table.GetLength(0); i++)
            {
                table[0, i] = ADFGVX[i-1];
                table[i, 0] = ADFGVX[i-1];
            }

            for (int i = 1,g=alphabet.Length-1; i < table.GetLength(0); i++)
            {
                for (int j = 1; j < table.GetLength(1); j++, g--)
                {
                    table[i, j] = g >= 0 ? alphabet[g] : '-';
                }
            }

            List<char> preResult = new List<char>();

            foreach (var element in phrase)
            {
                for (int i = 1; i < table.GetLength(0); i++)
                {
                    for (int j = 1; j < table.GetLength(1); j++)
                    {
                        if (table[i, j] == element)
                        {
                            preResult.Add(table[i, 0]);
                            preResult.Add(table[0, j]);
                        }
                    }
                }
            }




            char[,] preTable = new char[ADFGVX.Length, 2+Convert.ToInt32(Math.Ceiling(Convert.ToDouble(preResult.Count) / Convert.ToDouble(ADFGVX.Length)))];


            for (int i = 0, j = ADFGVX.Length-1; i < ADFGVX.Length; i++, j--)
            {
                preTable[i, 0] = ADFGVX[j];

            }

            for (int i = 0,b=0; i < alphabet.Length; i++)
            {
                for (int j = 0; j< preTable.GetLength(0); j++)
                {
                    if (alphabet[i] == Convert.ToChar(preTable[j, 0].ToString().ToLower()))
                    {
                        preTable[j, 1] = Convert.ToChar(b++);
                    }
                }
            }

            for (int i = 2, g =0; i < preTable.GetLength(1); i++)
            {
                for (int j = 0; j < preTable.GetLength(0); j++, g++)
                {
                    preTable[j, i] = g < preResult.Count ? preResult[g] : '-';
                }
            }


            for (int i = 0; i < preTable.GetLength(1); i++)
            {
                for (int j = 0; j < preTable.GetLength(0); j++)
                {
                    if (i == 1)
                    {
                        Console.Write(Convert.ToInt32(preTable[j, i]));

                    }
                    else
                    {
                        Console.Write(Char.ToString(preTable[j, i]));

                    }
                }

                Console.WriteLine();
            }


            string result = "";

            int temp = 0;
            while (temp< ADFGVX.Length)
            {
                for (int i = 0; i < ADFGVX.Length; i++)
                {
                    if (temp == Convert.ToInt32(preTable[i, 1]))
                    {
                        temp++;
                        for (int j = 2; j < preTable.GetLength(1); j++)
                        {
                            result += preTable[i, j];
                        }

                        result += " ";
                    }
                }
            }

            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
