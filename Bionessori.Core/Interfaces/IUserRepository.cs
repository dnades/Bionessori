using Bionessori.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Core.Interfaces {
    /// <summary>
    /// Интерфейс описывает методы работы с пользователем.
    /// </summary>
    public interface IUserRepository {
        /// <summary>
        /// Метод получает список пользователей.
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetUsers();

        /// <summary>
        /// Метод проверяет, существует ли уже в БД такой логин.
        /// </summary>
        /// <param name="login"></param>
        Task<string> GetIdentityLogin(string login);

        /// <summary>
        /// Метод проверяет, существует ли уже в БД такой email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<string> GetIdentityEmail(string email);

        /// <summary>
        /// Метод регистрирует нового пользователя.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<string> Create(User user);
    }
}
