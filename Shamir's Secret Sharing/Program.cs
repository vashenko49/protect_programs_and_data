using System;
using System.Linq;
using Shamir_s_Secret_Sharing.Models;

namespace Shamir_s_Secret_Sharing
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const int minimum = 3;
            const int maximum = 5;

            var secret = SecretSharing.Create(minimum, maximum);

            Console.WriteLine($"Secret: {secret.Secret}");

            Console.WriteLine();
            Console.WriteLine("Shares:");
            foreach (var share in secret.Shares) Console.WriteLine($"\t{share}");

            var random = new Random();
            var randomShares = secret.Shares.OrderBy(x => random.Next()).Take(minimum).OrderBy(x => x.X).ToArray();

            Console.WriteLine();
            Console.WriteLine("Used random secrets:");
            foreach (var share in randomShares) Console.WriteLine($"\t{share}");

            var reconstructed = Secret.ReconstructFrom(randomShares);
            Console.WriteLine();
            Console.WriteLine($"Reconstructed secret: {reconstructed.Value}");

            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}