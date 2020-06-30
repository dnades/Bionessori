using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Services {
    /// <summary>
    /// Сервис реализует методы складов.
    /// </summary>
    public class WerehouseService : IWerehouse {
        string _connectionString = null;

        public WerehouseService(string conStr) {
            _connectionString = conStr;
        }

        /// <summary>
        /// Метод получает список продуктов со склада.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Werehouse>> GetMaterials() {
            using (var db = new SqlConnection(_connectionString)) {
                // Вызывает процедуру для выбора списка материалов.
                var oMaterials = await db.QueryAsync<Werehouse>("sp_GetMaterials");

                return oMaterials.ToList();
            }
        }

        /// <summary>
        /// Метод получает список названий и кодов складов.
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetNameWerehouses() {
            using (var db = new SqlConnection(_connectionString)) {
                var oWerehouses = await db.QueryAsync("sp_GetNameWerehouses");

                return oWerehouses.ToList();
            }
        }
    }
}
