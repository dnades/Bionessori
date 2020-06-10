using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bionessori.Core {
    /// <summary>
    /// Класс токена пользователя.
    /// </summary>
    public class AuthOptions {
        public const string ISSUER = "MyAuthServer"; // Издатель токена
        public const string AUDIENCE = "MyAuthClient"; // Потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // Ключ для шифрования
        public const int LIFETIME = 1; // Время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey() {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
