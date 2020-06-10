using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Bionessori.Services {
    /// <summary>
    /// Сервис описывает реализации методов работы с пользователем.
    /// </summary>
    public class UserService : IUserRepository {
        string connectionString = null;

        public UserService(string conn) {
            connectionString = conn;
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
            using (var db = new SqlConnection(connectionString)) {
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
            using (var db = new SqlConnection(connectionString)) {
                var isUser = await db.QueryFirstOrDefaultAsync($"SELECT * FROM Users WHERE email = '{email}'");

                if (isUser != null) {
                    throw new ArgumentException("Такой email уже существует.");
                }
            }

            return "Email свободен.";
        }

        /// <summary>
        /// Метод получает список всех пользователей.
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUsers() {
            using (IDbConnection db = new SqlConnection(connectionString)) {
                var users = await db.QueryAsync<User>("SELECT * FROM Users");

                return users.ToList();
            }
        }

        /// <summary>
        /// Метод добавляет в БД нового пользователя.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> Create(User user) {
            using (var db = new SqlConnection(connectionString)) {
                string sQuery = $"INSERT INTO Users VALUES ('{user.Login}', '{user.Email}', '{user.Number}', '{user.Password}')";
                await db.ExecuteAsync(sQuery);
            }

            return "Пользователь успешно добавлен.";
        }
    }
}
