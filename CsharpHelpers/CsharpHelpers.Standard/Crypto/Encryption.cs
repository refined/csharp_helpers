using System;
using System.Security.Cryptography;
using System.Text;

namespace CsharpHelpers.Standard.Crypto
{
    public static class Encryption
    {
        public static string Encrypt(this string text, byte[] key, byte[] iv)
        {
            var algorithm = DES.Create();
            var transform = algorithm.CreateEncryptor(key, iv);
            var inputbuffer = Encoding.Unicode.GetBytes(text);
            var outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        public static string Decrypt(this string text, byte[] key, byte[] iv)
        {
            var algorithm = DES.Create();
            var transform = algorithm.CreateDecryptor(key, iv);
            var inputbuffer = Convert.FromBase64String(text);
            var outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Encoding.Unicode.GetString(outputBuffer);
        }
    }
}
