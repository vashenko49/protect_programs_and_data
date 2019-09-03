using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trisemus_cipher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Trisemus cipher");



            Console.WriteLine("Enter phrase for cipher");
            var phrase = Console.ReadLine().ToLower();

            Console.WriteLine("Enter key word");
            var key = Console.ReadLine();


            const string alphabet = "abcdefghijklmnopqrstuvwxyz";
            var tableTemplate = (key.ToLower() + alphabet).ToCharArray().Distinct().ToArray();


            int h = Convert.ToInt32(Math.Ceiling(Math.Sqrt(alphabet.Length)));
            var quard = new Char[h, h];


            for (int x = 0, letter = 0; x < h; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    quard[x, y] = letter < tableTemplate.Length ? (tableTemplate[letter++]) : '-';
                }
            }


            string result = "";

            //шифруем
            foreach (var letter in phrase)
            {
                for (int x = 0; x < h; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        if (letter == quard[x, y])
                        {
                            result += Convert.ToString(x) + ":" + Convert.ToString(y) + " ";
                        }
                    }
                }
            }

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
