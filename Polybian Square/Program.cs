using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polybian_Square
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Polybian Square");
            const string alphabet = "abcdefghijklmnopqrstuvwxyz";
            Console.WriteLine("Enter phrase");
            string phras = Console.ReadLine();

            if (phras != null)
            {
                int h = Convert.ToInt32(Math.Ceiling(Math.Sqrt(alphabet.Length)));
                var quard = new Char[h,h];

                //заполнили таблицу
                for (int x = 0, letter =0; x < h; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        quard[x, y] = letter < alphabet.Length ? (alphabet[letter++]) : '-';
                    }
                }

                string result = "";

                //шифруем
                foreach (var letter in phras)
                {
                    for (int x = 0; x < h; x++)
                    {
                        for (int y = 0; y < h; y++)
                        {
                            if (letter == quard[x, y])
                            {
                                result+=Convert.ToString(x)+":"+Convert.ToString(y) + " ";
                            }
                        }
                    }
                }

                Console.WriteLine(result);
                Console.ReadKey();
            }


        }
    }
}
