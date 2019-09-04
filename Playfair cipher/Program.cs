using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playfair_cipher
{
    class Program
    {
        static List<string> GetNextChars(string str, int iterateCount)
        {
            List<string> words = new List<string>();

            for (int i = 0; i < str.Length; i += iterateCount)
            {
                if (str.Length - i >= iterateCount) {
                    words.Add(str.Substring(i, iterateCount));
                }
                else
                {
                    words.Add(str.Substring(i, str.Length - i));
                }
            }

            for (int i = 0; i < words.Count; i++)
            {

                if (words[i].Length != 2)
                {
                    words[i] += "x";
                }
                string bigramm = words[i];
                if (bigramm[0] == bigramm[1])
                {
                    words[i] = bigramm[0]+"x";
                }
                Console.WriteLine(words[i]);
            }

            return words;
        }

        static int[] findInAlphabet(char[,] alph, char letter, int h)
        {
            int[] result = new int[2];

                for (int x = 0; x < h; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        if (letter == alph[x, y])
                        {
                            result[0] = x;
                            result[1] = y;
                        }
                    }
                }

                return result;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Playfair cipher");

            Console.WriteLine("Enter phrase for cipher");
            var phrase = Console.ReadLine().ToLower();

            var bigramm = GetNextChars(phrase, 2);


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

            foreach (var letterBigramm in bigramm)
            {
                var firstLeter = findInAlphabet(quard, letterBigramm[0], h);
                var secondLeter = findInAlphabet(quard, letterBigramm[1], h);
                if (firstLeter[0] == secondLeter[0])
                {
                    firstLeter[0] = firstLeter[0] + 1;
                    if ((firstLeter[0] + 1) == h || quard[firstLeter[0] + 1, (firstLeter[1])] == Convert.ToChar("-"))
                    {
                        firstLeter[0] = 0;
                    }
                    else
                    {
                        firstLeter[0] = firstLeter[0] + 1;
                    }
                    secondLeter[0] = secondLeter[0] + 1;
                    if ((secondLeter[0] + 1) == h || quard[secondLeter[0]+1, (secondLeter[1])] == Convert.ToChar("-"))
                    {
                        secondLeter[0] = 0;
                    }
                    else
                    {
                        secondLeter[0] = secondLeter[0] + 1;
                    }


                }
                else if (firstLeter[1] == secondLeter[1])
                {
                    firstLeter[1] = firstLeter[1] + 1;
                    if ((firstLeter[1] + 1) == h || quard[firstLeter[0], (firstLeter[1] + 1)] == Convert.ToChar("-"))
                    {
                        firstLeter[1] = 0;
                    }
                    else
                    {
                        firstLeter[1] = firstLeter[1] + 1;
                    }
                    secondLeter[1] = secondLeter[1] + 1;
                    if ((secondLeter[1] + 1) == h||quard[secondLeter[0], (secondLeter[1]+1)]==Convert.ToChar("-"))
                    {
                        secondLeter[1] = 0;
                    }
                    else
                    {
                        secondLeter[1] = secondLeter[1] + 1;
                    }
                }
                else
                {
                    int temp = firstLeter[0];
                    firstLeter[0] = secondLeter[0];
                    secondLeter[0] = temp;
                }
                result += quard[firstLeter[0], firstLeter[1]] + ":" +
                          quard[secondLeter[0], secondLeter[1]] + " ";
            }


            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
