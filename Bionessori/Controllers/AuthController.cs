using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Bionessori.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
