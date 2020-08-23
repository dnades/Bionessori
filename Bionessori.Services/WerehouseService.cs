using Bionessori.Core;
using Bionessori.Core.Constants;
using Bionessori.Core.Data;
using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
    public class WerehouseService : BaseWerehouse {
        ApplicationDbContext _db;

        public WerehouseService(ApplicationDbContext db) {
            _db = db;
        }

        /// <summary>
        /// Метод получает список материалов со складов.
        /// </summary>
        /// <returns>Список материалов.</returns>
        public async override Task<IEnumerable> GetMaterials() {
            // Получает список материалов.
            var oMaterials = await _db.Werehouses.Join(_db.Providers,
                w => w.Id,
                p => p.Id,
                (w, p) => new {
                    id = w.Id,
                    material = w.Material,
                    materialGroup = w.MaterialGroup,
                    measure = w.Measure,
                    count = w.Count,
                    vendorCode = w.VendorCode,
                    werehouseNumber = w.WerehouseNumber,
                    price = w.Price,
                    totalSum = w.TotalSum,
                    reserve = w.Reserve,
                    percentage = w.Percentage,
                    vat = w.VAT,
                    providerName = p.ProviderName
                }).ToListAsync();   

            return oMaterials;
        }

        /// <summary>
        /// Метод получает список названий складов.
        /// </summary>
        /// <returns></returns>
        public async override Task<IEnumerable> GetNameWerehouses() {
            // Выбирает только уникальные.
            return await _db.Werehouses.Select(w => w.WerehouseNumber).Distinct().ToListAsync();
        }

        /// <summary>
        /// Метод получает список групп материалов.
        /// </summary>
        /// <returns></returns>
        public async override Task<IEnumerable> GetGroupsWerehouses() {
            // Выбирает только уникальные.
            return await _db.Werehouses.Select(w => w.MaterialGroup).Distinct().ToListAsync();
        }

        /// <summary>
        /// Метод получает список ед.изм.
        /// </summary>
        /// <returns></returns>
        public async override Task<IEnumerable> GetMeasuresWerehouses() {
            return await _db.Werehouses.Select(m => m.Measure).Distinct().ToListAsync();
        }

        /// <summary>
        /// Метод получает список материалов без дубликатов.
        /// </summary>
        /// <returns></returns>
        public async override Task<IEnumerable> GetDistinctMaterials() {
            return await _db.Werehouses.Select(m => m.Material).Distinct().ToListAsync();
        }

        /// <summary>
        /// Метод реализует выбору материалов группы.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public async override Task<IEnumerable> GetMaterialsGroup(string group) {
            return await _db.Werehouses.Where(m => m.MaterialGroup == group).Select(m => m.Material).ToListAsync();
        }

        /// <summary>
        /// Метод получает кол-во заявок со статусом "Новая".
        /// </summary>
        /// <returns>Кол-во заявок.</returns>
        public async Task<int> GetCountNewRequests() {
            try {
                //using (var db = new SqlConnection(_connectionString)) {
                //    var iRequests = await db.QueryAsync<int>($"SELECT COUNT(*) FROM dbo.Requests " +
                //        $"WHERE status = '{RequestStatus.REQ_STATUS_NEW}'");

                //    return iRequests.FirstOrDefault();
                //}
                throw new NotImplementedException();
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
                //using (var db = new SqlConnection(_connectionString)) {
                //    var iRequests = await db.QueryAsync<int>($"SELECT COUNT(*) FROM dbo.Requests " +
                //        $"WHERE status = '{RequestStatus.REQ_STATUS_IN_WORK}'");

                //    return iRequests.FirstOrDefault();
                //}
                throw new NotImplementedException();
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
            //try {
            //    int iMaterials = 0; // Кол-во материалов.

            //    using (var db = new SqlConnection(_connectionString)) {
            //        IEnumerable<dynamic> aMaterials = await db.QueryAsync($"SELECT * FROM dbo.Requests " +
            //            $"WHERE status = '{RequestStatus.REQ_STATUS_NEED_REFILL}'");                    

            //        // Обрабатывает результат выборки и десериализует объект.
            //        foreach (var el in aMaterials) {
            //            var materials = el as IDictionary<string, dynamic>;
            //            var oMaterials = materials["material"];
            //            Request parseMaterial = JsonSerializer.Deserialize<Request>(oMaterials);
            //            iMaterials = parseMaterial.Material.Count();
            //        }

            //        return iMaterials;
            //    }
            //}
            //catch (Exception ex) {
            //    throw new Exception(ex.Message.ToString());
            //}
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод получает кол-во материалов, которые требуют сопоставления.
        /// </summary>
        /// <returns>Кол-во материалов.</returns>
        public async Task<int> GetCountMappingMaterials() {
            int iMaterials = 0; // Кол-во материалов.

            //using (var db = new SqlConnection(_connectionString)) {
            //    IEnumerable<dynamic> aMaterials = await db.QueryAsync($"SELECT * FROM dbo.Requests " +
            //        $"WHERE status = '{RequestStatus.REQ_STATUS_NEED_MAPPING}'");

            //    // Обрабатывает результат выборки и десериализует в объект.
            //    foreach (var el in aMaterials) {
            //        var materials = el as IDictionary<string, dynamic>;
            //        var oMaterials = materials["material"];
            //        Request parseMaterial = JsonSerializer.Deserialize<Request>(oMaterials);
            //        iMaterials = parseMaterial.Material.Count();
            //    }

            //    return iMaterials;
            //}
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод получает кол-во заявок, требующих подтверждения удаления.
        /// </summary>
        /// <returns>Кол-во заявок.</returns>
        public async Task<int> GetCountAcceptDeleteRequests() {
            try {
                //using (var db = new SqlConnection(_connectionString)) {
                //    var iRequests = await db.QueryAsync<int>($"SELECT COUNT(*) FROM dbo.Requests " +
                //        $"WHERE status = '{RequestStatus.REQ_STATUS_NEED_ACCEPT_DELETE}'");

                //    return iRequests.FirstOrDefault();
                //}
                throw new NotImplementedException();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
