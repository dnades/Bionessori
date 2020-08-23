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
            var oNamesWerehouse = await _db.Werehouses.Select(w => w.WerehouseNumber).Distinct().ToListAsync();

            return oNamesWerehouse;
        }

        /// <summary>
        /// Метод получает список групп материалов.
        /// </summary>
        /// <returns></returns>
        public async override Task<IEnumerable> GetGroupsWerehouses() {
            // Выбирает только уникальные.
            var oGroups = await _db.Werehouses.Select(w => w.MaterialGroup).Distinct().ToListAsync();

            return oGroups;
        }

        /// <summary>
        /// Метод получает список ед.изм.
        /// </summary>
        /// <returns></returns>
        public async override Task<IEnumerable> GetMeasuresWerehouses() {
            var oMeasures = await _db.Werehouses.Select(m => m.Measure).Distinct().ToListAsync();

            return oMeasures;
        }

        /// <summary>
        /// Метод получает список материалов без дубликатов.
        /// </summary>
        /// <returns></returns>
        public async override Task<IEnumerable> GetDistinctMaterials() {
            var oDistinctMaterials = await _db.Werehouses.Select(m => m.Material).Distinct().ToListAsync();

            return oDistinctMaterials;
        }

        /// <summary>
        /// Метод реализует выбор материалов группы.
        /// </summary>
        /// <param name="group"></param>
        /// <returns>Материалы группы.</returns>
        public async override Task<IEnumerable> GetMaterialsGroup(string group) {
            var oMaterialGroups = await _db.Werehouses.Where(m => m.MaterialGroup == group).Select(m => m.Material).ToListAsync();

            return oMaterialGroups;
        }

        /// <summary>
        /// Метод получает кол-во заявок со статусом "Новая".
        /// </summary>
        /// <returns>Кол-во заявок.</returns>
        public async override Task<int> GetCountNewRequests() {
            try {
                int countNewRequests = await _db.Requests.Where(c => c.Status == RequestStatus.REQ_STATUS_NEW).CountAsync();

                return countNewRequests;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает кол-во заявок со статусом "В работе".
        /// </summary>
        /// <returns>Кол-во заявок.</returns>
        public async override Task<int> GetCountRequestInWork() {
            try {
                int countInWorkReq = await _db.Requests.Where(r => r.Status == RequestStatus.REQ_STATUS_IN_WORK).CountAsync();

                return countInWorkReq;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает кол-во материалов, которые требуют пополнения.
        /// </summary>
        /// <returns>Кол-во материалов.</returns>
        public async override Task<int> GetCountRefillMaterials() {
            try {
                int countRefillMaterials = await _db.Requests.Where(m => m.Status == RequestStatus.REQ_STATUS_NEED_REFILL).CountAsync();

                return countRefillMaterials;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает кол-во материалов, которые требуют сопоставления.
        /// </summary>
        /// <returns>Кол-во материалов.</returns>
        public async override Task<int> GetCountMappingMaterials() {
            try {
                int countMappMaterials = await _db.Requests.Where(m => m.Status == RequestStatus.REQ_STATUS_NEED_MAPPING).CountAsync();

                return countMappMaterials;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает кол-во заявок, требующих подтверждения удаления.
        /// </summary>
        /// <returns>Кол-во заявок.</returns>
        public async override Task<int> GetCountAcceptDeleteRequests() {
            try {
                int countAcceptDeleteReq = await _db.Requests.Where(r => r.Status == RequestStatus.REQ_STATUS_NEED_ACCEPT_DELETE).CountAsync();

                return countAcceptDeleteReq;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
