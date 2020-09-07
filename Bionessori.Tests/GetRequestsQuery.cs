using Bionessori.Core.Data;
using Bionessori.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bionessori.Tests {
    /// <summary>
    /// Класс тестирования заявок.
    /// </summary>
    public class GetRequestsQuery {
        ApplicationDbContext _db;
        public GetRequestsQuery(ApplicationDbContext db) {
            _db = db;
        }

        /// <summary>
        /// Получет список заявок.
        /// </summary>
        /// <returns>Список заявок.</returns>
        public IList<Request> GetRequests() {
            return _db.Requests.ToList();
        }
    }
}