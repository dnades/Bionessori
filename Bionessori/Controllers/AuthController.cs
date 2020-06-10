using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Bionessori.Core;
using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Bionessori.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Bionessori.Controllers {
    [ApiController, Route("api/data/auth")]
    public class AuthController : Controller {
        IUserRepository _user;

        public AuthController(IUserRepository repo) {
            _user = repo;
        }

        /// <summary>
        /// Метод регистрирует пользователя, но сначала проверяет существует ли он уже.
        /// </summary>
        /// <param name="user">Объект модели с данными пользователя.</param>
        /// <returns>Статус регистрации.</returns>
        [HttpPost, Route("checkin")]
        public async Task<IActionResult> CheckIn([FromBody] User user) {
            if (string.IsNullOrEmpty(user.Login) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Email)) {
                throw new ArgumentNullException("Параметры не должны быть пустыми.");
            }

            // Проверяет, есть ли пользователь с таким логином.
            await _user.GetIdentityLogin(user.Login);

            // Проверяет, есть ли пользователь с таким email.
            await _user.GetIdentityEmail(user.Email);

            string sPassword = user.Password;

            // Хэширует пароль в MD5.
            var hashPassword = await HashMD5Service.HashPassword(sPassword);
            user.Password = hashPassword;

            // Добавляет нового пользователя в БД.
            await _user.Create(user);

            return Ok("Пользователь успешно зарегистрирован.");
        }

        /// <summary>
        /// Метод авторизует пользователя.
        /// </summary>
        /// <param name="input">Логин или email.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Статус авторизации.</returns>
        [HttpPost, Route("signin")]
        public async Task<IActionResult> SignIn([FromBody] User user) {
            if (string.IsNullOrEmpty(user.Login) || string.IsNullOrEmpty(user.Password)) {
                throw new ArgumentException("Параметры не могут быть пустыми.");
            }

            // Хэширует пароль для сравнения.
            var hashPassword = await HashMD5Service.HashPassword(user.Password);

            // Выбирает пароль пользователя из БД.
            bool getIdentityPassword = await _user.GetUserPassword(hashPassword);

            // Если пароль не совпадает с тем что в БД.
            if (!getIdentityPassword) {
                throw new ArgumentException("Пароль не верен.");
            }

            var isUser = await _user.GetIdentity(user.Login);

            if (isUser != null) {
                var now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: isUser.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var response = new {
                    access_token = encodedJwt,
                    username = isUser.Name
                };

                return Ok(response);
            }

            throw new ArgumentNullException("Пользователь не найден.");
        }

        /// <summary>
        /// Метод получает список всех пользователей.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-users")]
        public async Task<IActionResult> TakeUsers() {
            var oUsers = await _user.GetUsers();

            return Ok(oUsers);
        }
    }
}
