using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Emil
{
    class Program
    {
        static void Main(string[] args)
        {
            //  El-Gamal encryption

            int p = 0, g = 0, x = 0, k = 0;
            Random random = new Random();

            BigInteger y = 0, a = 0, p0 = 0, m1 = 0;
            BigInteger[] b = { };
            BigInteger[] r = { };

            string encData;
            string decData;

            char[] array;

        //Encryption

        Start:;

            encData = "";
            decData = "";

            Console.WriteLine("\nEnter prime number between 120 and 1000 to be used in encryption");
            p = Convert.ToInt32(Console.ReadLine());

            if (IsPrime(p))
            {
                g = 50;
                x = 15;
                y = BigInteger.ModPow(g, x, p);

                Console.WriteLine("\nEnter data to be encrypted");
                string data = Console.ReadLine();

                array = data.ToCharArray();

                k = 20;
                a = BigInteger.ModPow(g, k, p);


                b = new BigInteger[array.Length];
                for (int i = 0; i < array.Length; i++)
                {

                    b[i] = BigInteger.Remainder(BigInteger.Multiply(BigInteger.Pow(y, k), array[i]), p);

                    encData = encData + b[i].ToString();
                }

                Console.WriteLine("\nEncrypted data: " + encData);


                //Decryption

                r = new BigInteger[b.Length];

                for (int i = 0; i < b.Length; i++)
                {
                    p0 = BigInteger.Subtract(BigInteger.Subtract(p, new BigInteger(1)), x);
                    m1 = BigInteger.ModPow(a, p0, p);
                    r[i] = BigInteger.Remainder(BigInteger.Multiply(m1, b[i]), p);

                    decData = decData + ((char)r[i]).ToString();

                }

                Console.WriteLine("\nDecrypted data: " + decData);
                goto Start;

            }
            else
            {
                Console.WriteLine("\nThis Number is Not a Prime Number , Enter Another One");
                goto Start;
            }
        }


        public static bool IsPrime(int Number)
        {

            if ((Number & 1) == 0)
            {
                if (Number == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            for (int i = 3; (i * i) <= Number; i += 2)
            {
                if ((Number % i) == 0)
                {
                    return false;
                }
            }
            return Number != 1;
        }
    }
}
