using System;
using SecretSharing.SecretEncryption;

namespace SecretSharing
{
    internal class EntryPoint
    {
        public static void Main()
        {
            Console.WriteLine("1: Split\n2: Combine");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("Players: ");
                var playerStr = Console.ReadLine();

                Console.WriteLine("Required: ");
                var requiredStr = Console.ReadLine();

                Console.WriteLine("Message:");
                var message = Console.ReadLine();

                var players = int.Parse(playerStr);
                var required = int.Parse(requiredStr);

                var polynomsCount = 3;

                var byteKey = KeyGenerator.GenerateKey(polynomsCount * 16);
                var key = KeyGenerator.GenerateDoubleBytesKey(byteKey);
                var hexKey = KeyGenerator.GetHexKey(key);

                var encrypted = Encryption.Encrypt(message, hexKey);

                Console.WriteLine("\nEncrypted message:\n{0}", encrypted);

                Console.WriteLine("\nShares: ");

                var splitted = SharesManager.SplitKey(key, players, required);
                for (var i = 0; i < splitted.Length; i++) Console.WriteLine(splitted[i]);
                Console.WriteLine();
            }
            else if (choice == "2")
            {
                Console.WriteLine("Encrypted message: ");
                var message = Console.ReadLine();

                Console.WriteLine("Number of shares: ");
                var sharesCountStr = Console.ReadLine();
                var sharesCount = int.Parse(sharesCountStr);


                var shares = new string[sharesCount];

                for (var i = 0; i < sharesCount; i++)
                {
                    Console.WriteLine("Share {0}:", i + 1);
                    shares[i] = Console.ReadLine();
                }

                var generatedKey = SharesManager.CombineKey(shares);
                var hexKey = KeyGenerator.GetHexKey(generatedKey);

                var decrypted = Encryption.Decrypt(message, hexKey);
                Console.WriteLine("\nDecrypted message:");
                Console.WriteLine(decrypted);
                Console.WriteLine();
            }

            var a = Console.ReadLine();
        }
    }
}