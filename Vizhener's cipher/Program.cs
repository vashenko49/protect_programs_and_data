using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizhener_s_cipher
{

    class Program
    {
        private static readonly char[] Characters = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',
            'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
            'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

        private static readonly int N = Characters.Length;

        private static string Encode(string input, string keyword)
        {
            input = input.ToUpper();
            keyword = keyword.ToUpper();

            var result = "";
            var keywordIndex = 0;

            foreach (var symbol in input)
            {
                var c = (Array.IndexOf(Characters, symbol) +
                         Array.IndexOf(Characters, keyword[keywordIndex])) % N;

                result += Characters[c];

                keywordIndex++;

                if ((keywordIndex + 1) == keyword.Length)
                    keywordIndex = 0;
            }
            return result;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Vizhener's cipher");

            Console.WriteLine("Enter phrase for cipher");
            var inputEncode = Console.ReadLine();

            Console.WriteLine("Enter key word");
            var key = Console.ReadLine();


            Console.WriteLine(Encode(inputEncode, key));
            Console.ReadKey(true);

        }
    }
}
