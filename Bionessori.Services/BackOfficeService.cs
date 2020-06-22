using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Services {
    /// <summary>
    /// Класс описывает реализации методов back-офиса.
    /// </summary>
    public class BackOfficeService : IBackOffice {
        string _connectionString = null;

        public BackOfficeService(string conn) {
            _connectionString = conn;
        }

        /// <summary>
        /// Метод получает список пользователей.
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUsers() {
            using (var db = new SqlConnection(_connectionString)) {
                List<User> oUsers = (List<User>)await db.QueryAsync<User>("sp_GetUsers");

                return oUsers;
            }
        }

        /// <summary>
        /// Метод назначает роли пользователю.
        /// </summary>
        ///// <returns></returns>
        public async Task<string> GiveRole(UserRole role) { 
            using (var db = new SqlConnection(_connectionString)) {
                await db.ExecuteAsync($"INSERT INTO UserRoles VALUES " +
                    $"({role.UserId}, '{role.UserName}', '{role.Role}')");
                
                return "Роль успешно назначена.";
            }
        }
    }
}
