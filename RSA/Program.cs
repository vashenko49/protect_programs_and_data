using System;
using System.Security.Cryptography;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            var myrsa = new RSACryptoServiceProvider();
            var encoding = new System.Text.ASCIIEncoding();
            Console.Write("Enter text to encrypt\t");
            var data = Console.ReadLine();

            var newdata = encoding.GetBytes(data ?? throw new NullReferenceException());
            var encrypted = myrsa.Encrypt(newdata, false);

            Console.Write("Encrypted Data:\t");
            foreach (var t in encrypted)
                Console.Write("{0} ", t);

            var decrypted = myrsa.Decrypt(encrypted, false); 
            Console.Write("\n\nDecrypted Data:\t");
            var dData = encoding.GetString(decrypted);
            for (var i = 0; i < decrypted.Length; i++)
                Console.Write("{0}", dData[i]);

            Console.ReadKey(true);
        }
    }
}
