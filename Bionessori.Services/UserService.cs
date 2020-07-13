using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bionessori.Services {
    /// <summary>
    /// Сервис реализует методы работы с пользователем.
    /// </summary>
    public class UserService : IUserRepository {
        string _connectionString = null;
        
        public UserService(string conn) {
            _connectionString = conn;
        }

        /// <summary>
        /// Метод проверяет, существует ли пользователь с таким логином.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<string> GetIdentityLogin(string login) {
            if (string.IsNullOrEmpty(login)) {
                throw new ArgumentNullException("Логин не передан.");
            }

            // Проверяет, есть ли пользователь с таким логином.
            using (var db = new SqlConnection(_connectionString)) {
                var isUser = await db.QueryFirstOrDefaultAsync($"SELECT * FROM Users WHERE login = '{login}'");

                if (isUser != null) {
                    throw new ArgumentException("Пользователь с таким логином уже существует.");
                }
            }

            return "Логин свободен.";
        }

        /// <summary>
        /// Метод проверяет, существует ли пользователь с таким email.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetIdentityEmail(string email) {
            if (string.IsNullOrEmpty(email)) {
                throw new ArgumentNullException("Логин не передан.");
            }

            // Проверяет, есть ли пользователь с таким email.
            using (var db = new SqlConnection(_connectionString)) {
                var isUser = await db.QueryFirstOrDefaultAsync($"SELECT * FROM Users WHERE email = '{email}'");

                if (isUser != null) {
                    throw new ArgumentException("Такой email уже существует.");
                }
            }

            return "Email свободен.";
        }

        /// <summary>
        /// Метод добавляет в БД нового пользователя.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> Create(User user) {
            using (var db = new SqlConnection(_connectionString)) {
                var parameters = new DynamicParameters();
                parameters.Add("@login", user.Login, DbType.String);
                parameters.Add("@email", user.Email, DbType.String);
                parameters.Add("@number", user.Number, DbType.String);
                parameters.Add("@password", user.Password, DbType.String);

                // Вызывает процедуру добавление нового пользователя в БД.
                var oCards = await db.QueryAsync<User>("sp_CreateUser",
                    commandType: CommandType.StoredProcedure,
                    param: parameters);
            }

            return "Пользователь успешно добавлен.";
        }

        /// <summary>
        /// Метод выбирает пароль пользователя из БД.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<bool> GetUserPassword(string password) {
            using (var db = new SqlConnection(_connectionString)) {
                var oUser = await db.QueryFirstOrDefaultAsync($"SELECT * FROM Users WHERE password = '{password}'");

                if (oUser == null) {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Метод выбирает пользователя из БД
        /// </summary>
        /// <param name = "input" ></ param >
        /// < param name="password"></param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GetIdentity(string input) {
            bool isEmail = input.Contains("@"); // Проверяет логин передан или email.

            if (isEmail) {
                using (var db = new SqlConnection(_connectionString)) {
                    var isUser = await db.QueryFirstOrDefaultAsync($"SELECT * FROM Users WHERE email = '{input}'");
                }

                var claims = new List<Claim> {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, input)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }
            else {
                using (var db = new SqlConnection(_connectionString)) {
                    var isUser = await db.QueryFirstOrDefaultAsync($"SELECT * FROM Users WHERE login = '{input}'");
                }

                var claims = new List<Claim> {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, input)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }
        }

        /// <summary>
        /// Метод получает список ролей пользователя.
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> TakeUserRole(string login) {
            using (var db = new SqlConnection(_connectionString)) {
                var param = new DynamicParameters();
                param.Add("@login", login, DbType.String);

                // Вызывает процедуру, которая возвращает список ролей пользователя.
                var isMemberRole = await db.QueryAsync<string>("sp_IsMemberUserRole",
                    commandType: CommandType.StoredProcedure,
                    param: param);

                return isMemberRole.ToList();
            }
        }

        /// <summary>
        /// Метод реализует оповещение о регистрации нового пользователя в системе.
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public async Task NotificationCheckIn(Notification notification) {
            var parameters = new DynamicParameters();
            parameters.Add("@message", notification.Message, DbType.String);
            parameters.Add("@category", notification.Category, DbType.String);
            parameters.Add("@module", notification.Module, DbType.String);

            try {
                using (var db = new SqlConnection(_connectionString)) {
                    // Вызывает процедуру добавления оповещения о регистрации нового пользователя.
                    await db.QueryAsync("dbo.sp_SendNotification",
                        commandType: CommandType.StoredProcedure,
                        param: parameters);
                }
            }
            catch(ArgumentException ex) {
                throw new ArgumentException("Ошибка оповещения о регистрации пользователя.", ex.Message.ToString());
            }
        }
    }
}
