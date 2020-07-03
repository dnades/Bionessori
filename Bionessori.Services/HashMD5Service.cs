using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Services {
    /// <summary>
    /// Сервис хэширования паролей в MD5.
    /// </summary>
    public class HashMD5Service {
        public HashMD5Service() { }

        public static async Task<string> HashPassword(string password) {
            string getHashPassword = await GetHash(password);

            return getHashPassword;
        }

        static async Task<string> GetHash(string password) {
            byte[] hash = Encoding.ASCII.GetBytes(password);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            string result = "";

            foreach (var b in hashenc) {
                result += b.ToString("x2");
            }

            return result;
        }
    }
}
