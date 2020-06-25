using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Services {
    /// <summary>
    /// Сервис реализует методы по работы с заявками.
    /// </summary>
    public class RequestService : IRequest {
        string _connectionString = null;

        public RequestService(string strConn) {
            _connectionString = strConn;
        }

        /// <summary>
        /// Метод получает список заявок на потребности.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Request>> GetRequests(Request request) {
            // Вызывает процедуру для выборки списка заявок.
            using (var db = new SqlConnection(_connectionString)) {
                var oRequests = await db.QueryAsync<Request>("sp_GetRequests");

                return oRequests.ToList();
            }
        }
    }
}
