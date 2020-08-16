using Bionessori.Core.Constants;
using Bionessori.Core.Data;
using Bionessori.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override Task<IEnumerable<Werehouse>> GetDynamicDataRefillMaterials() {
            throw new NotImplementedException();
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

        public override Task<IEnumerable> GetDynamicDataAcceptDeleteRequests() {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<Werehouse>> GetDynamicDataMappingMaterials() {
            throw new NotImplementedException();
        }
    }
}
