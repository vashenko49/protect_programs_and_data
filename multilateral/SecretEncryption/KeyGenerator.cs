using System;
using SecretSharing.Helpers;

namespace SecretSharing.SecretEncryption
{
    internal class KeyGenerator
    {
        private static readonly Random rand = new Random();

        public static byte[] GenerateKey(int bitsLength)
        {
            if (bitsLength % 8 != 0) throw new ArgumentException("The bits length must be able to be divided by 8.");

            var bytesLength = bitsLength / 8;
            var key = new byte[bytesLength];
            rand.NextBytes(key);

            return key;
        }

        public static ushort[] GenerateDoubleBytesKey(byte[] arr)
        {
            if (arr.Length % 2 != 0) throw new ArgumentException("The array length must be even.");

            var length = arr.Length;
            var result = new ushort[length / 2];

            ushort el1, el2;
            for (var i = 0; i < length / 2; i++)
            {
                el1 = (ushort) (arr[2 * i] << 8);
                el2 = arr[2 * i + 1];

                result[i] = (ushort) (el1 + el2);
            }

            return result;
        }

        public static string GetHexKey(ushort[] key)
        {
            var newKey = new uint[key.Length];
            for (var i = 0; i < key.Length; i++) newKey[i] = key[i];
            return HexConverter.NumbersArrToHexString(newKey, '-');
        }
    }
}