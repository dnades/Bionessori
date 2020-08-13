using Bionessori.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Core.Interfaces {
    /// <summary>
    /// Интерфейс описывает работу back-офиса.
    /// </summary>
    public interface IBackOffice {
        /// <summary>
        /// Метод получает список пользователей.
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetUsers();

        /// <summary>
        /// Метод назначает роли пользователю.
        /// </summary>
        /// <returns></returns>
        Task GiveRole(UserRole role);
    }
}
