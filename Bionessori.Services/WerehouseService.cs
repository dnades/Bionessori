using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections;
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
        /// Метод получает список материалов со складов.
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
        /// Метод получает список названий складов.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable> GetNameWerehouses() {
            using (var db = new SqlConnection(_connectionString)) {
                var oNames = await db.QueryAsync("sp_GetNamesWerehouses");

                return oNames.ToList(); 
            }
        }

        /// <summary>
        /// Метод получает список групп материалов.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable> GetGroupsWerehouses() { 
            using (var db = new SqlConnection(_connectionString)) {
                var oGroups = await db.QueryAsync("sp_GetGroupNames");

                return oGroups.ToList();
            }
        }

        /// <summary>
        /// Метод получает список ед.изм.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable> GetMeasuresWerehouses() {
            using (var db = new SqlConnection(_connectionString)) {
                var oMeasures = await db.QueryAsync("sp_GetMeasures");

                return oMeasures.ToList();
            }
        }

        /// <summary>
        /// Метод получает список материалов без дубликатов.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable> GetDistinctMaterials() {
            using (var db = new SqlConnection(_connectionString)) {
                var oDistinctMaterials = await db.QueryAsync("sp_GetDistinctMaterials");

                return oDistinctMaterials.ToList();
            }
        }
    }
}
