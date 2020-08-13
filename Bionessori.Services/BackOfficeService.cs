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
        public async Task GiveRole(UserRole role) { 
            try {
                if (string.IsNullOrEmpty(role.UserName)) {
                    throw new ArgumentNullException();
                }

                int userId = await GetUserIds(role.UserName);

                using (var db = new SqlConnection(_connectionString)) {
                    await db.ExecuteAsync($"INSERT INTO u0772479_admin.UserRoles (user_id, role) VALUES ({userId}, '{role.Role}')");
                }
            }
            catch (ArgumentNullException ex) {
                throw new ArgumentNullException("Входные параметры не заполнены", ex);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Получает id юзера по его имени.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<int> GetUserIds(string login) {
            using (var db = new SqlConnection(_connectionString)) {
                var result = await db.QueryAsync<string>($"SELECT id FROM u0772479_admin.Users WHERE login = '{login}'");
                int userId = Convert.ToInt32(result.FirstOrDefault());

                return userId;
            }
        }
    }
}
