using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Based_on_the_RSA_algorithm
{

    class Program
    {
        static string alf = "abcdefghijklmnopqrstuvwxyz";
        static string str = "vashenko";
        static Random rand = new Random();

        public static int RSAauthentication()
        {
            Console.WriteLine("RSA authentication");
            // 1. Key generation
            int e = 5, // A
                n = 91,
                d = 29;
            Console.WriteLine("Secret and public keys: (e, n, d) => ({0}, {1}, {2})", e, n, d);
            // 2. RSA authentication
            int k = alf.IndexOf(Convert.ToString(str[rand.Next(0, 3)])), // k => 10, a => 0, r => 17
                r = (int)Math.Pow(k, e), // Б (r) => A
                new_k = (int)Math.Pow(r, d) % n; // А (new_k) => Б
            if (new_k == k) Console.WriteLine("Correct authentication!"); // A (new_k == k)
            else Console.WriteLine("Incorrect authentication!");
            return k;
        }
        static void Main(string[] args)
        {
            int rsa_signature = RSAauthentication();
            Console.ReadKey();
        }
    }
}
