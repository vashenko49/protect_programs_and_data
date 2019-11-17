using System;
using System.Numerics;

namespace ShamirsSecretSharingScheme.Services
{
    public static class MersennePrimesGenerator
    {
        private static readonly int[] Powers =
        {
            2, 3, 5, 7, 13, 17, 19, 31, 61, 89, 107, 127, 521, 607, 1279, 2203, 2281, 3217, 4256, 4423
        };

        public static BigInteger Get(int index)
        {
            var powersCount = Powers.Length;

            if (index <= powersCount) return BigInteger.Pow(2, Powers[index - 1]) - 1;

            var msg = $"Index must be between 1 and {powersCount} inclusive";
            throw new IndexOutOfRangeException(msg);
        }
    }
}