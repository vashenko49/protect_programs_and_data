using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bag
{
    internal class Program
    {
        private static int[] securKey = new[] {2, 3, 6, 13, 27, 52, 105, 210};
        private static int[] openKey = new[] {62, 93, 186, 403, 417, 352, 315, 210};


        private static int exp;
        private static int dotPos;

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
                        exp = (int) temp * -1;
                        break;
                    }

                    temp++;
                }

                if (input < .5)
                    bits = bits.Remove(0, (int) temp - 1);
            }

            return bits;
        }

        private static string GetCodePoint(char ch)
        {
            var retVal = "u+";
            var bytes = Encoding.Unicode.GetBytes(ch.ToString());
            for (var ctr = bytes.Length - 1; ctr >= 0; ctr--)
                retVal += bytes[ctr].ToString("X2");

            return retVal;
        }

        private static string[] EncodeToWindow1251BinaryCode(string phase)
        {
            var enc = Encoding.GetEncoding(1251);
            var bytes = enc.GetBytes(phase);
            var window1251 = new string[bytes.Length];
            for (var i = 0; i < bytes.Length; i++) window1251[i] = IntBitConvert(bytes[i]);
            return window1251;
        }

        private static int[] Cipher( ref string[] binaryCode)
        {
            int[] cipherWeight = new int[binaryCode.Length];
            for (int i = 0; i < binaryCode.Length; i++)
            {
                int temp = 0;
                for (int j = 0; j < binaryCode[i].Length; j++)
                {
                    if (binaryCode[i][j] == '1')
                    {
                        temp += openKey[j];
                    }
                }

                cipherWeight[i] = temp;
            }

            return cipherWeight;
        }
        public static IEnumerable<string> GetPermutations(string s)
        {
            if (s.Length > 1)
                return from ch in s
                    from permutation in GetPermutations(s.Remove(s.IndexOf(ch), 1))
                    select string.Format("{0}{1}", ch, permutation);
            else
                return new string[] { s };
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Enter phase");
            var phrase = "Проверка текста на уникальность онлайн, точная и быстрая проверка покажет процент уникальности статьи.";
            var windows1251Code = EncodeToWindow1251BinaryCode(phrase);

            var ciperWeight = Cipher(ref windows1251Code);

            for (int i = 0; i < ciperWeight.Length; i++)
            {
                Console.Write(ciperWeight[i]+" ");
            }

            Console.WriteLine();

            var DecriptValue = new int[ciperWeight.Length];

            for (int i = 0; i < DecriptValue.Length; i++)
            {
                DecriptValue[i] = (ciperWeight[i] * 271) % 420;
            }



            int[] decoderValue = new int[DecriptValue.Length];


            for (int i = 0; i < DecriptValue.Length; i++)
            {
                foreach (var s in GetPermutations("10000000").ToArray())
                {

                    int temp = 0;
                    for (int j = 0; j < s.Length; j++)
                    {
                        if (s[j] == '1')
                        {
                            temp += securKey[j];
                        }
                    }

                    if (temp == DecriptValue[i])
                    {
                        int codeSymbol = Convert.ToInt32(s, 2);
                        decoderValue[i] = codeSymbol;

                        break;
                    }
                }

                foreach (var s in GetPermutations("11000000").ToArray())
                {

                    int temp = 0;
                    for (int j = 0; j < s.Length; j++)
                    {
                        if (s[j] == '1')
                        {
                            temp += securKey[j];
                        }
                    }

                    if (temp == DecriptValue[i])
                    {
                        int codeSymbol = Convert.ToInt32(s, 2);
                        decoderValue[i] = codeSymbol;

                        break;
                    }
                }
                foreach (var s in GetPermutations("11100000").ToArray())
                {

                    int temp = 0;
                    for (int j = 0; j < s.Length; j++)
                    {
                        if (s[j] == '1')
                        {
                            temp += securKey[j];
                        }
                    }

                    if (temp == DecriptValue[i])
                    {
                        int codeSymbol = Convert.ToInt32(s, 2);
                        decoderValue[i] = codeSymbol;

                        break;
                    }
                }
                foreach (var s in GetPermutations("11110000").ToArray())
                {

                    int temp = 0;
                    for (int j = 0; j < s.Length; j++)
                    {
                        if (s[j] == '1')
                        {
                            temp += securKey[j];
                        }
                    }

                    if (temp == DecriptValue[i])
                    {
                        int codeSymbol = Convert.ToInt32(s, 2);
                        decoderValue[i] = codeSymbol;

                        break;
                    }
                }
                foreach (var s in GetPermutations("11111000").ToArray())
                {

                    int temp = 0;
                    for (int j = 0; j < s.Length; j++)
                    {
                        if (s[j] == '1')
                        {
                            temp += securKey[j];
                        }
                    }

                    if (temp == DecriptValue[i])
                    {
                        int codeSymbol = Convert.ToInt32(s, 2);
                        decoderValue[i] = codeSymbol;

                        break;
                    }
                }
                foreach (var s in GetPermutations("11111100").ToArray())
                {

                    int temp = 0;
                    for (int j = 0; j < s.Length; j++)
                    {
                        if (s[j] == '1')
                        {
                            temp += securKey[j];
                        }
                    }

                    if (temp == DecriptValue[i])
                    {
                        int codeSymbol = Convert.ToInt32(s, 2);
                        decoderValue[i] = codeSymbol;

                        break;
                    }
                }
                foreach(var s in GetPermutations("11111110").ToArray())
                {

                    int temp = 0;
                    for (int j = 0; j < s.Length; j++)
                    {
                        if (s[j] == '1')
                        {
                            temp += securKey[j];
                        }
                    }

                    if (temp == DecriptValue[i])
                    {
                        int codeSymbol = Convert.ToInt32(s, 2);
                        decoderValue[i] = codeSymbol;

                        break;
                    }
                }
                foreach (var s in GetPermutations("11111111").ToArray())
                {

                    int temp = 0;
                    for (int j = 0; j < s.Length; j++)
                    {
                        if (s[j] == '1')
                        {
                            temp += securKey[j];
                        }
                    }

                    if (temp == DecriptValue[i])
                    {
                        int codeSymbol = Convert.ToInt32(s, 2);
                        decoderValue[i] = codeSymbol;

                        break;
                    }
                }
            }


            byte[] bytes = decoderValue.Select(x => (byte)x).ToArray();

            var enc = Encoding.GetEncoding(1251);



            string decoderPhrase = enc.GetString(bytes);



            Console.WriteLine(decoderPhrase);

            Console.ReadKey();
        }
    }
}