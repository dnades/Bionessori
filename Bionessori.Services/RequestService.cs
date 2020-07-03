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
        /// <summary>
        /// Метод реализует создание новой заявки на потребности в закупках.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> Create(Request request) {
            using (var db = new SqlConnection(_connectionString)) {
                // Генерит номер заявки.
                var reqNumber = Guid.NewGuid().ToString();

                // Добавляет новую заявку в список заявок со статусом "Новая".
                await db.QueryAsync($"INSERT INTO Requests VALUES ('{reqNumber}', {request.Count}, '{request.Measure}', " +
                    $"'Новая', '{request.Material}', '{request.MaterialGroup}')");

                return "Заявка успешно создана.";
            }
        }

        /// <summary>
        /// Метод реализует удаление заявки на потребности в закупках.
        /// </summary>
        /// <returns></returns>
        public async Task<string> Delete() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод реализует изменение существующей заявки на потребности в закупках.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Request> Edit(Request request) {
            throw new NotImplementedException();
        }
    }
}
