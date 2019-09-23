using System;

namespace GammN
{
    internal class Program
    {
        private static readonly string alphabet = "abcdefghijklmnopqrstuvwxyz";

        private static void Main(string[] args)
        {
            Console.WriteLine("Gamma modulo N");
            Console.WriteLine("Enter phrase:");
            var phrase = Console.ReadLine();
            Console.WriteLine("Enter gamma(key string):");
            var key = Console.ReadLine();

            var N = alphabet.Length;

            if (phrase.Length > key.Length)
                for (int j = 0, i = key.Length; i < phrase.Length; i++, j++)
                    key += key[j];
            else if (key.Length > phrase.Length)
                for (var i = phrase.Length; i < key.Length; i++)
                    phrase += Convert.ToString(alphabet[alphabet
                                                            .Length - 1]);

            var table = new int[5, phrase.Length];

            for (var i = 0; i < phrase.Length; i++)
            for (var j = 0; j < alphabet.Length; j++)
            {
                if (Convert.ToString(key[i]).ToLower() == Convert.ToString(alphabet[j]).ToLower()) table[1, i] = j;
                if (Convert.ToString(phrase[i]).ToLower() == Convert.ToString(alphabet[j]).ToLower()) table[0, i] = j;
            }


            //По модулю N. N берем как длина алфавита 
            for (var i = 0; i < key.Length; i++) table[2, i] = (table[0, i] + table[1, i]) % N;

            for (var i = 0; i < key.Length; i++)
            {
                table[3, i] = table[2, i] + N - table[1, i];
                table[4, i] = table[3, i] % N;
            }


            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < phrase.Length; j++)
                    if (i != 3)
                        Console.Write(alphabet[table[i, j]] + " " + table[i, j] + " ");
                    else
                        Console.Write("  " + table[i, j] + " ");
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}