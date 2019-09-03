using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_cipher
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Caesar cipher");

            //ask phrase
            Console.WriteLine("Enter phrase");
            string phrase = Console.ReadLine();
            //ask key
            Console.WriteLine("Enter key");
            int key = Convert.ToInt32(Console.ReadLine());
            //write English alphabet
            const string alfabet = "abcdefghijklmnopqrstuvwxyz";
            int countLetterInAlphabet = alfabet.Length;
            string result = "";


            //foreach on entered phrase
            foreach (var letterFromPhrase in phrase)
            {
                //foreach and simile letter with alphabet
                for (int i = 0; i < alfabet.Length; i++)
                {
                    if (Char.ToUpper(letterFromPhrase) == Char.ToUpper(alfabet[i]))
                    {
                        int shift = i + key;
                        while (shift>=countLetterInAlphabet)
                        {
                            shift -= countLetterInAlphabet;
                        }

                        result = result + (Char.IsLower(letterFromPhrase) ? alfabet[shift] : Char.ToUpper(alfabet[shift]));
                    }
                }
            }

            Console.WriteLine(result);
            Console.ReadKey();
        }

    }
}
