using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Dapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

        /// <summary>
        /// Метод реализует выбору материалов группы.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public async Task<List<string>> GetMaterialsGroup(string group) {
            using (var db = new SqlConnection(_connectionString)) {
                var parameters = new DynamicParameters();
                parameters.Add("@group", group, DbType.String);

                // Процедура выбирает все материалы группы.
                var oMaterialsGroup = await db.QueryAsync<string>("dbo.sp_GetMaterialsGroup",
                    commandType: CommandType.StoredProcedure,
                    param: parameters);

                return oMaterialsGroup.ToList();
            }
        }

        /// <summary>
        /// Метод получает кол-во заявок.
        /// </summary>
        /// <returns>Кол-во заявок.</returns>
        public async Task<int> GetCountNewRequests() {
            try {
                using (var db = new SqlConnection(_connectionString)) {
                    var iRequests = await db.QueryAsync<int>("SELECT COUNT(*) FROM dbo.Requests " +
                        "WHERE status = 'Новая'");

                    return iRequests.FirstOrDefault();
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает кол-во заявок со статусом "В работе".
        /// </summary>
        /// <returns>Кол-во заявок.</returns>
        public async Task<int> GetCountRequestInWork() {
            try {
                using (var db = new SqlConnection(_connectionString)) {
                    var iRequests = await db.QueryAsync<int>("SELECT COUNT(*) FROM dbo.Requests " +
                        $"WHERE status = 'В работе'");

                    return iRequests.FirstOrDefault();
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает кол-во материалов, которые требуют пополнения.
        /// </summary>
        /// <returns>Кол-во материалов.</returns>
        public async Task<int> GetCountRefillMaterials() {
            try {
                int iMaterials = 0; // Кол-во материалов.

                using (var db = new SqlConnection(_connectionString)) {
                    IEnumerable<dynamic> aMaterials = await db.QueryAsync("SELECT * FROM dbo.Requests " +
                        $"WHERE status = 'Требует пополнения'");                    

                    // Обрабатывает результат выборки и десериализует в объект.
                    foreach (var el in aMaterials) {
                        var materials = el as IDictionary<string, dynamic>;
                        var oMaterials = materials["material"];
                        Request parseMaterial = JsonSerializer.Deserialize<Request>(oMaterials);
                        iMaterials = parseMaterial.Material.Count();
                    }

                    return iMaterials;
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает кол-во материалов, которые требуют сопоставления.
        /// </summary>
        /// <returns>Кол-во материалов.</returns>
        public async Task<int> GetCountMappingMaterials() {
            int iMaterials = 0; // Кол-во материалов.

            using (var db = new SqlConnection(_connectionString)) {
                IEnumerable<dynamic> aMaterials = await db.QueryAsync("SELECT * FROM dbo.Requests " +
                    $"WHERE status = 'Требует сопоставления'");

                // Обрабатывает результат выборки и десериализует в объект.
                foreach (var el in aMaterials) {
                    var materials = el as IDictionary<string, dynamic>;
                    var oMaterials = materials["material"];
                    Request parseMaterial = JsonSerializer.Deserialize<Request>(oMaterials);
                    iMaterials = parseMaterial.Material.Count();
                }

                return iMaterials;
            }
        }

        /// <summary>
        /// Метод получает кол-во заявок, требующих подтверждения удаления.
        /// </summary>
        /// <returns>Кол-во заявок.</returns>
        public async Task<int> GetCountAcceptDeleteRequests() {
            try {
                using (var db = new SqlConnection(_connectionString)) {
                    var iRequests = await db.QueryAsync<int>("SELECT COUNT(*) FROM dbo.Requests " +
                        $"WHERE status = 'Требует подтверждения удаления'");

                    return iRequests.FirstOrDefault();
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
