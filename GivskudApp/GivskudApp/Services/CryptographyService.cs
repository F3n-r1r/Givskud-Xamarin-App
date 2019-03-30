using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace GivskudApp.CryptographyService {
    public class EncDecService {
        
        private const string _Key = "gCjK+DZ/GCYbKIGiAt1qCA==";

        public static string Hash(string plain) {

            using(Aes aes = Aes.Create()) {
                aes.GenerateIV();
                byte[] IV = aes.IV;

                SHA256 sha256 = SHA256Managed.Create();
                byte[] bytes = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(IV) + plain);
                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder Result = new StringBuilder();

                for(int i = 0; i < hash.Length; i++) {
                    Result.Append(hash[i].ToString("X2"));
                }

                return Result.ToString();
            }
        }
        public static string Encrypt(string plain) {

            byte[] encrypted;
            byte[] iv;
            
            using(Aes aes = Aes.Create()) {

                aes.Key = Convert.FromBase64String(_Key);
                aes.Mode = CipherMode.CBC;

                aes.GenerateIV();
                iv = aes.IV;

                var Encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using(var ms = new MemoryStream()) {
                    using(var cs = new CryptoStream(ms, Encryptor, CryptoStreamMode.Write)) {
                        using (var sw = new StreamWriter(cs)) {
                            sw.Write(plain);
                        }
                        encrypted = ms.ToArray();
                    }
                }
            }

            byte[] concat = new byte[encrypted.Length + iv.Length];
            Array.Copy(iv, 0, concat, 0, iv.Length);
            Array.Copy(encrypted, 0, concat, iv.Length, encrypted.Length);

            return Convert.ToBase64String(concat);

        }
        public static string Decrypt(string plain) {

            byte[] input = Convert.FromBase64String(plain);
            string decrypted = null;

            using(Aes aes = Aes.Create()) {

                byte[] IV = new byte[aes.BlockSize / 8];
                byte[] encrypted = new byte[input.Length - IV.Length];

                Array.Copy(input, 0, IV, 0, IV.Length);
                Array.Copy(input, IV.Length, encrypted, 0, encrypted.Length);

                aes.IV = IV;
                aes.Key = Convert.FromBase64String(_Key);
                aes.Mode = CipherMode.CBC;

                var Decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using(var ms = new MemoryStream(encrypted)) {
                    using(var cs = new CryptoStream(ms, Decryptor, CryptoStreamMode.Read)) {
                        using(var sr = new StreamReader(cs)) {
                            decrypted = sr.ReadToEnd();
                        }
                    }
                }

            }

            return decrypted;

        }
    }
    
}