using System;

namespace XORCipher
{
    internal class XORCipher
    {
        //key generator
        private string GetRandomKey(int k, int len)
        {
            var gamma = string.Empty;
            var rnd = new Random(k);
            for (var i = 0; i < len; i++) gamma += ((char) rnd.Next(35, 126)).ToString();
            return gamma;
        }

        //pseudorandom number encryption/decryption method
        private string EncryptDecrypt(string text, int key)
        {
            var secretKey = GetRandomKey(key, text.Length);
            var result = string.Empty;
            for (var i = 0; i < text.Length; i++) result += ((char) (text[i] ^ secretKey[i])).ToString();
            return result;
        }

        //text encryption
        public string EncryptRandomKey(string plainText, int key)
        {
            return EncryptDecrypt(plainText, key);
        }

        //text decryption
        public string DecryptRandomKey(string encryptedText, int key)
        {
            return EncryptDecrypt(encryptedText, key);
        }

        //key replay generator
        private string GetRepeatKey(string p, int len)
        {
            var r = p;
            while (r.Length < len) r += r;
            return r.Substring(0, len);
        }

        //encryption/decryption method
        private string EncryptDecrypt(string text, string password)
        {
            var secretPassword = GetRepeatKey(password, text.Length);
            var result = string.Empty;
            for (var i = 0; i < text.Length; i++) result += ((char) (text[i] ^ secretPassword[i])).ToString();
            return result;
        }

        //text encryption
        public string Encrypt(string plainText, string password)
        {
            return EncryptDecrypt(plainText, password);
        }

        //text decryption
        public string Decrypt(string encryptedText, string password)
        {
            return EncryptDecrypt(encryptedText, password);
        }
    }
}