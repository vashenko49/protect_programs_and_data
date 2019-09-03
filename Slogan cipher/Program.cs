using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slogan_cipher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Slogan cipher");
            Console.WriteLine("Enter phrase for cipher");
            var phrase = Console.ReadLine();

            Console.WriteLine("Enter key word");
            var key = Console.ReadLine();

            const string alphabet = "abcdefghijklmnopqrstuvwxyz";
            var tableTemplate = (key + alphabet).ToCharArray().Distinct().ToArray();

            var result = "";

            foreach (var letter in phrase)
            {
                for (int i = 0; i < alphabet.Length; i++)
                {
                    if (Char.ToUpper(letter) == Char.ToUpper(alphabet[i]))
                    {
                        result += Char.IsLower(letter) ? tableTemplate[i] : Char.ToUpper(tableTemplate[i]);
                    }
                }
            }

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
