using Bionessori.Core;
using Bionessori.Core.Constants;
using Bionessori.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Services {
    /// <summary>
    /// Сервис реализует методы работы отдела закупок.
    /// </summary>
    public class PurchasesService : BasePurchases {
        ApplicationDbContext _db;
        public PurchasesService(ApplicationDbContext db) {
            _db = db;
        }

        /// <summary>
        /// Метод получает список заявок.
        /// </summary>
        /// <returns>Список заявок.</returns>
        public async override Task<IEnumerable> GetRequests() {
            try {
                var oRequests = await _db.Requests
                    .Where(r => r.Status.Equals(RequestStatus.REQ_STATUS_IN_WORK) || 
                    r.Status.Equals(RequestStatus.REQ_STATUS_ACCEPT) ||
                    r.Status.Equals(RequestStatus.REQ_STATUS_COMPLETE)).ToListAsync();

                return oRequests;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
