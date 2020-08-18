using Bionessori.Core.Constants;
using Bionessori.Core.Data;
using Bionessori.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Bionessori.Core.Interfaces;

namespace Bionessori.Core {
    /// <summary>
    /// Сервис реализует методы различных шаблонов.
    /// </summary>
    public class DynamicDataTemplateService : BaseTemplate {
        ApplicationDbContext _db;

        public DynamicDataTemplateService(ApplicationDbContext db) { 
            _db = db;
        }
       
        /// <summary>
        /// Метод получает список новых заявок.
        /// </summary>
        /// <returns></returns>
        public async override Task<IEnumerable> GetDynamicDataNewRequests() {
            try {
                return await _db.Requests.Where(r => r.Status == RequestStatus.REQ_STATUS_NEW).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает материалы, которые нужно пополнить.
        /// </summary>
        /// <returns>Список материалов.</returns>
        public async override Task<IEnumerable> GetDynamicDataRefillMaterials() {
            // Получает материалы заявки.
            var oMaterials = await _db.Requests.Where(m => m.Status == RequestStatus.REQ_STATUS_NEED_REFILL).Select(s => s.Material).ToListAsync();
            List<string> aMaterials = new List<string>();   // Коллекция со строками материалов.
            List<Werehouse> aFindResult = new List<Werehouse>(); // Коллекция c результатами найденных материалов на складах.

            for (int i = 0; i < oMaterials.Count; i++) {
                // Приводит к нужному виду.
                JObject jsonObject = JObject.Parse(oMaterials[i]);
                var jArray = (JArray)jsonObject["Material"];
                aMaterials.Add(jArray.Values().ToList()[i].ToString());

                // Находит объекты материалов по их наименованию.
                var oFindMaterials = await (from m in _db.Werehouses
                                           where m.Material == aMaterials[i]
                                           select m).ToListAsync();
                aFindResult.Add(oFindMaterials[i]);
            }
            return aFindResult;
        }

        /// <summary>
        /// Метод получает список заявок в работе.
        /// </summary>
        /// <returns></returns>
        public async override Task<IEnumerable> GetDynamicDataWorkRequests() {
            try {
                return await _db.Requests.Where(r => r.Status == RequestStatus.REQ_STATUS_IN_WORK).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает заявки ожидающие подтверждения удаления.
        /// </summary>
        /// <returns></returns>
        public async override Task<IEnumerable> GetDynamicDataAcceptDeleteRequests() {
            return await _db.Requests.Where(r => r.Status == RequestStatus.REQ_STATUS_NEED_ACCEPT_DELETE).ToListAsync();
        }

        public override Task<IEnumerable<Werehouse>> GetDynamicDataMappingMaterials() {
            throw new NotImplementedException();
        }
    }
}
