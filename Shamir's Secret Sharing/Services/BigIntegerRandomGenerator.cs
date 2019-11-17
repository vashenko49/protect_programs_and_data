using System.Numerics;
using System.Security.Cryptography;

namespace ShamirsSecretSharingScheme.Services
{
    public static class BigIntegerRandomGenerator
    {
        public static BigInteger Get(BigInteger maxValue, uint bytes = 32)
        {
            var data = new byte[bytes];
            var cryptoProvider = new RNGCryptoServiceProvider();
            cryptoProvider.GetBytes(data);

            // Assert that value is greater than 0
            data[data.Length - 1] &= 0x7F;
            return new BigInteger(data) % maxValue;
        }

        public static BigInteger[] GetMultiple(BigInteger maxValue, uint count, uint bytes = 32)
        {
            var result = new BigInteger[count];

            for (var i = 0; i < count; i++) result[i] = Get(maxValue, bytes);

            return result;
        }
    }
}