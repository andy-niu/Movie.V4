using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace M.Common
{
    public class AESEncryptor
    {
        #region Initialization

        static readonly RijndaelManaged rijalg = new RijndaelManaged();

        #endregion

        #region AES 256 CBC Encryptor 

        //set cipher format as AES-256-CBC
        static AESEncryptor()
        {
            rijalg.BlockSize = 128;
            rijalg.KeySize = 256;
            rijalg.FeedbackSize = 128;
            //rijalg.Padding = PaddingMode.Zeros; //PaddingMode.PKCS7;
            rijalg.Mode = CipherMode.CBC;

            rijalg.Key = (new SHA256Managed()).ComputeHash(Encoding.ASCII.GetBytes("IHazSekretKey"));
            rijalg.IV = System.Text.Encoding.ASCII.GetBytes("1234567890123456");
        }

        public static string EncryptionAES256CBC(string plainText)
        {
            ICryptoTransform encryptor = rijalg.CreateEncryptor(rijalg.Key, rijalg.IV);

            byte[] encrypted;
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            return Convert.ToBase64String(encrypted).ToString();
        }

        public static string DecriptionAES256CBC(string cipher)
        {
            if (string.IsNullOrEmpty(cipher)) return cipher;

            byte[] encrypted = Convert.FromBase64String(cipher);
            ICryptoTransform decryptor = rijalg.CreateDecryptor(rijalg.Key, rijalg.IV);
            string plaintext;

            using (MemoryStream msDecrypt = new MemoryStream(encrypted))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

            return plaintext;
        }

        public static string EncryptionAES(string plainText, string key, string iv)
        {
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.Key = Encoding.UTF8.GetBytes(key);
                aesProvider.IV = Encoding.UTF8.GetBytes(iv);
                aesProvider.Mode = CipherMode.CBC;
                aesProvider.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor())
                {
                    byte[] inputBuffers = Encoding.UTF8.GetBytes(plainText);
                    byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    aesProvider.Dispose();
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }
        public static string DecriptionAES(string cipher, string key, string iv)
        {
            string EncryptionResult;
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.Key = Encoding.UTF8.GetBytes(key);
                aesProvider.IV = Encoding.UTF8.GetBytes(iv);
                aesProvider.Mode = CipherMode.CBC;
                aesProvider.Padding = PaddingMode.PKCS7;  //PKCS7填充和PKCS5填充无区别的。
                using (ICryptoTransform crytoTransform = aesProvider.CreateDecryptor())
                {
                    byte[] inputBuffers = Convert.FromBase64String(cipher);
                    byte[] result = crytoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    EncryptionResult = Encoding.UTF8.GetString(result);
                    crytoTransform.Dispose();
                }
                aesProvider.Clear();
                aesProvider.Dispose();
                return EncryptionResult;
            }
        }

        #endregion
    }
}
