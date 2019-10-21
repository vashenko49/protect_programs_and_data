using System;
using System.Security.Cryptography;
using System.Text;

namespace md5
{
    internal class Program
    {
        private static string key { get; } = "A!9HHhi%XjjYY4YP2@Nob009X";

        public static string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        var textBytes = Encoding.UTF8.GetBytes(text);
                        var bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public static string Decrypt(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        var cipherBytes = Convert.FromBase64String(cipher);
                        var bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Enter phase");
            var phase = Console.ReadLine();
            var cipher = Encrypt(phase);
            Console.WriteLine("Encrypt");
            Console.WriteLine();
            Console.WriteLine(cipher);

            phase = Decrypt(cipher);
            Console.WriteLine("Decrypt");
            Console.WriteLine();
            Console.WriteLine(phase);
            Console.ReadKey(true);
        }
    }
}