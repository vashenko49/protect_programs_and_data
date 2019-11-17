using System;
using System.Security.Cryptography;
using System.Text;

namespace SecretSharing.SecretEncryption
{
    public class Encryption_Old
    {
        private static byte[] IV = new byte[8] {1, 2, 3, 4, 5, 6, 7, 8};

        public static string Encrypt(string text, byte[] key)
        {
            var plaintextbytes = Encoding.ASCII.GetBytes(text);
            var aes = new AesCryptoServiceProvider();
            // aes.BlockSize = 128;
            ////aes.KeySize = 256;
            aes.Key = key;
            //aes.IV = IV;
            aes.Padding = PaddingMode.None;
            aes.Mode = CipherMode.CBC;
            var crypto = aes.CreateEncryptor(aes.Key, aes.IV);
            var encrypted = crypto.TransformFinalBlock(plaintextbytes, 0, plaintextbytes.Length);
            crypto.Dispose();

            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string encrypted, byte[] key)
        {
            var encryptedbytes = Convert.FromBase64String(encrypted);
            var aes = new AesCryptoServiceProvider();
            // aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = key;
            //aes.IV = IV;
            aes.Padding = PaddingMode.None;
            aes.Mode = CipherMode.CBC;
            var crypto = aes.CreateDecryptor(aes.Key, aes.IV);
            var secret = crypto.TransformFinalBlock(encryptedbytes, 0, encryptedbytes.Length);
            crypto.Dispose();

            return Encoding.ASCII.GetString(secret);
        }
    }
}