using Bionessori.Core.Data;
using Bionessori.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bionessori.Tests {
    /// <summary>
    /// Класс тестирования получения данных.
    /// </summary>
    public class GetTestDataQuery {
        ApplicationDbContext _db;
        public GetTestDataQuery(ApplicationDbContext db) {
            _db = db;
        }

        /// <summary>
        /// Получает список заявок.
        /// </summary>
        /// <returns>Список заявок.</returns>
        public IList<Request> GetRequests() {
            return _db.Requests.ToList();
        }

        /// <summary>
        /// Получает список коммерческих предложений.
        /// </summary>
        /// <returns>Список заявок.</returns>
        public IList<CommerceOffer> GetOffers() {
            return _db.CommerceOffers.ToList();
        }
    }
}