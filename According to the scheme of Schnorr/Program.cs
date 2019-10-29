using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace According_to_the_scheme_of_Schnorr
{
    class Program
    {
        static string alf = "abcdefghijklmnopqrstuvwxyz";
        static string str = "karandas";
        static Random rand = new Random();

        private static int findG(int q, int p)
        {
            int g = 3;
            while (true)
            {
                if ((Math.Pow(g, q) % p) == 1)
                {
                    return g;
                }
                g++;
            }
        }
        private static int findY(int g, int x, int p)
        {
            int y = 1;
            while (true)
            {
                if (((Math.Pow(g, x) * y) % p) == 1)
                {
                    return y;
                }
                y++;
            }
        }

        public static int SchnorrAuthentication()
        {
            Console.WriteLine("\nKlaus Schnorr's authentication");
            // 1. Key generation
            int p = 23, // (p - 1) mod q = 0
                q = 11,
                x = rand.Next(1, q - 1), // x є {1, ... , q - 1}
                g = findG(q, p),
                y = findY(g, x, p);
            Console.WriteLine("Secret key: (p, q, x) => ({0}, {1}, {2})", p, q, x);
            Console.WriteLine("Public key: (p, q, y) => ({0}, {1}, {2})", p, q, y);
            // 2. Klaus Schnorr's authentication
            int k = alf.IndexOf(Convert.ToString(str[rand.Next(0, 3)])), // k => 10, a => 0, r => 17
                r = (int)Math.Pow(g, k) % p, // A (r) => Б
                t = rand.Next(1, 10), // Random value 1 ... 10
                e = rand.Next(1, (int)Math.Pow(2, t) - 1), // Б (e) => A
                s = (k + x * e) % q, // A (s) => Б
                new_r = (int)(Math.Pow(g, s) * Math.Pow(y, e)) % p; // Б (new_r == r)
            Console.WriteLine("t => {0}", t);
            if (new_r == r) Console.WriteLine("Correct authentication!");
            else Console.WriteLine("Incorrect authentication!");
            return r;
        }
        static void Main(string[] args)
        {
            int schnorr_signature = SchnorrAuthentication();
            Console.ReadKey();
        }
    }
}
