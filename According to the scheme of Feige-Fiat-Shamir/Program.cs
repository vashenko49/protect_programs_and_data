using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace According_to_the_scheme_of_Feige_Fiat_Shamir
{

    class Program
    {

        static Random rand = new Random();
        public static int FeigeFiatShamirAuthentication()
        {
            Console.WriteLine("\nFeige-Fiat-Shamir authentication");
            // 1. Key generation
            int[] V = { 15, 16, 21, 25, 29, 30 };
            int p = 5,
                q = 7,
                n = p * q,
                v = V[rand.Next(0, V.Length)],
                s = 9; // (s) => A
            Console.WriteLine("Secret key: (s) => {0}", s);
            Console.WriteLine("Public key: (v, n) => ({0}, {1})", v, n);
            // 2. FeigeFiatShamirAuthentication authentication
            int r = rand.Next(1, n - 1),
                z = (int)Math.Pow(r, 2) % n, // A (z) => Б
                b = rand.Next(0, 2), // Б (0 or 1) => A
                y = 0;
            Console.WriteLine("b => {0}", b);
            if (b == 0) // А (r) => Б
            {
                if (z == (int)Math.Pow(r, 2) % n) Console.WriteLine("(z == (({0} ^ 2) % {1}))", r, n); // Б (z == ((r ^ 2) % n))
                else if (z == (((int)Math.Pow(y, 2) * v) % n)) Console.WriteLine("(z == ((({0} ^ 2) * {1}) % {2}))", y, v, n); // Б (z == (((y ^ 2) * v) % n))
                Console.WriteLine("z => {0}", z);
                return z;
            }
            else
            {
                y = (r * s) % n;
                Console.WriteLine("(y = ({0} * {1}) % {2})", r, s, n);
                Console.WriteLine("y => {0}", y);
                return y;
            }
        }
        static void Main(string[] args)
        {
            var feige_fiat_shamir_signature = FeigeFiatShamirAuthentication();

            Console.ReadLine();
        }
    }
}
