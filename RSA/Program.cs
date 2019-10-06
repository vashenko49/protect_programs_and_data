using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            var myrsa = new RSACryptoServiceProvider();
            var encoding = new System.Text.ASCIIEncoding();//Encode String to Convert to Bytes
            Console.Write("Enter text to encrypt\t");
            var data = Console.ReadLine();//whatever you want to encrypt

            var newdata = encoding.GetBytes(data ?? throw new NullReferenceException());//convert to Bytes
            var encrypted = myrsa.Encrypt(newdata, false);

            Console.Write("Encrypted Data:\t");
            foreach (var t in encrypted)
                Console.Write("{0} ", t);

            var decrypted = myrsa.Decrypt(encrypted, false);//decrypt 
            Console.Write("\n\nDecrypted Data:\t");
            var dData = encoding.GetString(decrypted); //encode bytes back to string 
            for (var i = 0; i < decrypted.Length; i++)
                Console.Write("{0}", dData[i]);

            Console.ReadKey(true);
        }
    }
}
