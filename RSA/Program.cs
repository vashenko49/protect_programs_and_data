using System;
using System.Numerics;
using System.Security.Cryptography;

namespace RSA
{
    class Program
    {
        static string alph = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя ";
        public static int[] RSACipher(int p, int q, int e, string message)
        {
            int[] result = new int[message.Length];
            message = message.ToLower();
            int[] posArray = new int[message.Length];
            //int[] cipherArray = new int[message.Length];
            for (int i = 0; i < posArray.Length; i++)
            {
                posArray[i] = alph.IndexOf(message[i]) + 1;
            }
            for (int i = 0; i < posArray.Length; i++)
            {
                result[i] = Convert.ToInt32(Math.Pow(posArray[i], e) % (p * q));
                Console.Write(result[i] + " ");
            }
            return result;
        }
        public static string RSADecipher(int n, int f, int e, int[] message)
        {
            int d = 0;
            int counter = 1;
            string result = "";
            while (d == 0)
            {
                if (Math.Truncate(Convert.ToDouble((1 + counter * f) / e)) * e % f == 1)
                {
                    d = Convert.ToInt32(Math.Truncate(Convert.ToDouble((1 + counter * f) / e)));
                }
                counter++;
            }
            for (int i = 0; i < message.Length; i++)
            {
                BigInteger t1 = BigInteger.Pow(message[i], d);
                result += alph[(int)(t1 % n) - 1];
            }
            return result;
        }
        public static int NOD(int a, int b)
        {
            if (a == b)
                return a;
            else
            if (a > b)
                return NOD(a - b, b);
            else
                return NOD(b - a, a);
        }
        static void Main(string[] args)
        {
            //var myrsa = new RSACryptoServiceProvider();
            //var encoding = new System.Text.ASCIIEncoding();
            //Console.Write("Enter text to encrypt\t");
            //var data = Console.ReadLine();

            //var newdata = encoding.GetBytes(data ?? throw new NullReferenceException());
            //var encrypted = myrsa.Encrypt(newdata, false);

            //Console.Write("Encrypted Data:\t");
            //foreach (var t in encrypted)
            //    Console.Write("{0} ", t);

            //var decrypted = myrsa.Decrypt(encrypted, false); 
            //Console.Write("\n\nDecrypted Data:\t");
            //var dData = encoding.GetString(decrypted);
            //for (var i = 0; i < decrypted.Length; i++)
            //    Console.Write("{0}", dData[i]);

            int p = 23;
            int q = 41;
            int modulus = p * q;
            int f = (p - 1) * (q - 1);
            int e = 0;
            for (int i = 2; i < f; i++)
            {
                if (NOD(i, f) == 1)
                {
                    e = i;
                    break;
                }
            }
            string word = "Абрамов";
            Console.WriteLine(RSADecipher(p * q, f, e,RSACipher(p, q, e, word)));
            Console.ReadKey(true);
        }
    }
}
