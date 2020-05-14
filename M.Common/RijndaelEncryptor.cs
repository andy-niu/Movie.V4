using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace M.Common
{
    public class RijndaelEncryptor
    {
        /// <summary>
        /// 使用对称算法加密
        /// </summary>
        public static string Encrypt(string plainText, string key, string IV)
        {
            try
            {
                string result = string.Empty;
                using (RijndaelManaged rijAlg = new RijndaelManaged())
                {
                    rijAlg.Mode = CipherMode.CBC;
                    byte[] buffer = Encoding.UTF8.GetBytes(plainText);
                    byte[] bytes = Encoding.ASCII.GetBytes(key);
                    byte[] buffer3 = Encoding.ASCII.GetBytes(IV);
                    rijAlg.Key = bytes;
                    rijAlg.IV = buffer3;
                    ICryptoTransform transform = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        stream.Write(buffer, 0, buffer.Length);
                        new CryptoStream(stream, transform, CryptoStreamMode.Write);
                        byte[] buffer4 = stream.ToArray();
                        result = Convert.ToBase64String(stream.ToArray());
                    }
                }
                return result;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public static string Decrypt(string cipherText, string key, string IV)
        {
            try
            {
                string result = string.Empty;
                using (RijndaelManaged rijAlg = new RijndaelManaged())
                {
                    rijAlg.Mode = CipherMode.CBC;
                    byte[] buffer = Convert.FromBase64String(cipherText);
                    byte[] bytes = Encoding.ASCII.GetBytes(key);
                    byte[] buffer3 = Encoding.ASCII.GetBytes(IV);
                    rijAlg.Key = bytes;
                    rijAlg.IV = buffer3;
                    ICryptoTransform transform = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        stream.Write(buffer, 0, buffer.Length);
                        new CryptoStream(stream, transform, CryptoStreamMode.Write);
                        byte[] buffer4 = stream.ToArray();
                        result = Encoding.UTF8.GetString(buffer4);
                    }
                }
                return result;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
