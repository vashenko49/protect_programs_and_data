using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cipher_Crossroads
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("cipher Crossroads");
            Console.WriteLine("Enter phrase");
            string phrase = Console.ReadLine();

            int amountCross = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(phrase.Length) / 4));

            char[,,] table = new Char[amountCross,2,2];

            int pos = 0;

            for (int i = 0; i < amountCross; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (j == 0)
                    {
                        table[i, j, 0] = pos<phrase.Length?phrase[pos++]:'-';
                        table[i, j, 1] = pos < phrase.Length ? phrase[pos++] : '-';
                    }else if (j==1)
                    {
                        table[i, j, 1] = pos < phrase.Length ? phrase[pos++]:'-';
                        table[i, j, 0] = pos < phrase.Length ? phrase[pos++] : '-';
                    }
                }
            }


            string result = "";

            for (int i = 0; i < amountCross; i++)
            {
                result += table[i, 0, 0];
            }

            for (int i = 0; i < amountCross; i++)
            {
                result += Convert.ToString(table[i, 0, 1]) + Convert.ToString(table[i, 1, 0]);
            }

            for (int i = 0; i < amountCross; i++)
            {
                result += table[i, 1, 1];
            }

            Console.WriteLine(result);

            Console.ReadKey(true);
        }
    }
}
