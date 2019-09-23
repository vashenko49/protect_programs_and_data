using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamm2
{
    class Program
    {
        private static int exp;
        private static int dotPos;
        private static readonly string alphabet = "abcdefghijklmnopqrstuvwxyz";
        private static string IntBitConvert(double input)
        {
            var floor = Math.Floor(input);
            var frac = input - floor;

            var e = 0;
            while (Math.Pow(2, e) <= floor)
                e++;
            e = e - 1;
            var bits = "";
            double temp;
            if (input > 1 || input < 0)
            {
                bits += "1";

                temp = Math.Pow(2, e);
                for (var i = e - 1; i >= 0; i--)
                    if (temp + Math.Pow(2, i) <= floor)
                    {
                        temp += Math.Pow(2, i);
                        bits += "1";
                    }
                    else
                    {
                        bits += "0";
                    }

                exp = bits.Length - 1;
            }

            if (frac == 0)
                return bits;
            dotPos = bits.Length;
            temp = 0;
            e = -1;
            while (temp <= frac && e > -80)
            {
                if (temp + Math.Pow(2, e) <= frac)
                {
                    temp += Math.Pow(2, e);
                    bits += "1";
                }

                else
                {
                    bits += "0";
                }

                e--;
            }

            temp = 1;
            if (input < 1 && input > 0)
            {
                for (var i = dotPos; i < bits.Length; i++)
                {
                    if (bits[i] == '1')
                    {
                        exp = (int)temp * -1;
                        break;
                    }

                    temp++;
                }

                if (input < .5)
                    bits = bits.Remove(0, (int)temp - 1);
            }

            return bits;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Enter gamma module 2");
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
            var table = new int[4, phrase.Length];


            for (var i = 0; i < phrase.Length; i++)
            for (var j = 0; j < alphabet.Length; j++)
            {
                if (Convert.ToString(key[i]).ToLower() == Convert.ToString(alphabet[j]).ToLower()) table[1, i] = j;
                if (Convert.ToString(phrase[i]).ToLower() == Convert.ToString(alphabet[j]).ToLower()) table[0, i] = j;
            }



            for (int i = 0; i < phrase.Length; i++)
            {
                string praseIn = IntBitConvert(table[0, i]);
                string keyIn = IntBitConvert(table[1, i]);
                string result = "";

                if (praseIn.Length > keyIn.Length)
                    for (int j = 0, g = keyIn.Length; g < praseIn.Length; g++, j++)
                        keyIn = "0" + keyIn;
                else if (keyIn.Length > praseIn.Length)
                    for (var g = praseIn.Length; g < keyIn.Length; g++)
                        praseIn = "0" + praseIn;

                for (int j = 0; j < praseIn.Length; j++)
                {
                    if (praseIn[j] == keyIn[j])
                    {
                        result += "0";
                    }
                    else
                    {
                        result += "1";
                    }
                }

                if (result == "")
                {
                    result += "0";
                }


                table[2, i] = Convert.ToInt32(result, 2);


            }


            for (int i = 0; i < phrase.Length; i++)
            {
                string praseIn = IntBitConvert(table[1, i]);
                string keyIn = IntBitConvert(table[2, i]);
                string result = "";

                if (praseIn.Length > keyIn.Length)
                    for (int j = 0, g = keyIn.Length; g < praseIn.Length; g++, j++)
                        keyIn = "0" + keyIn;
                else if (keyIn.Length > praseIn.Length)
                    for (var g = praseIn.Length; g < keyIn.Length; g++)
                        praseIn = "0" + praseIn;

                for (int j = 0; j < praseIn.Length; j++)
                {
                    if (praseIn[j] == keyIn[j])
                    {
                        result += "0";
                    }
                    else
                    {
                        result += "1";
                    }
                }

                if (result == "")
                {
                    result += "0";
                }


                table[3, i] = Convert.ToInt32(result, 2);


            }


            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < phrase.Length; j++)
                {
                    if (i != 2)
                    {
                        Console.Write(alphabet[table[i, j]] + " " + table[i, j] + " ");
                    }
                    else
                    {
                        Console.Write("  " + table[i, j] + " ");
                    }
                }

                Console.WriteLine();
            }


            Console.WriteLine();
            Console.WriteLine("Encrypted phrase");
            for (int i = 0; i < phrase.Length; i++)
            {
                Console.Write(table[2, i] + " ");
            }
            Console.ReadLine();
        }
    }
}
