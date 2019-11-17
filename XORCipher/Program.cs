using System;

namespace XORCipher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("XOR Cipher\n");
            var x = new XORCipher();
            Console.Write("Enter text: ");
            var message = Console.ReadLine();
            while (true)
            {
                Console.Write(
                    "Choose the encryption method:\n1) Repeat the keyword until the gamma is equal to the length of the message;\n2) Generate a sequence of pseudorandom numbers equal in length to the message body.\n(\"1\" or \"2\"): ");
                var answer = Console.ReadLine();
                if (answer == "1")
                {
                    Console.Write("Enter password: ");
                    var pass = Console.ReadLine();
                    var encryptedMessageByPass = x.Encrypt(message, pass);
                    Console.WriteLine("Encrypted message: {0}", encryptedMessageByPass);
                    Console.WriteLine("Decrypted message: {0}", x.Decrypt(encryptedMessageByPass, pass));
                    break;
                }

                if (answer == "2")
                {
                    Console.Write("Enter key: ");
                    var key = Convert.ToInt32(Console.ReadLine());
                    var encryptedMessageByKey = x.EncryptRandomKey(message, key);
                    Console.WriteLine("Encrypted message: {0}", encryptedMessageByKey);
                    Console.WriteLine("Decrypted message: {0}", x.DecryptRandomKey(encryptedMessageByKey, key));
                    break;
                }
            }

            Console.ReadLine();
        }
    }
}