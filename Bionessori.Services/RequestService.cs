using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
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
                string typeParam = "request";
                string generateNumber = "";

                // Генерит рандомный номер заявки.
                Task<string> taskGenerate = new Task<string>(() => RandomDataService.GenerateRandomNumber());
                taskGenerate.Start();

                // Ждет результат задачи.
                generateNumber = taskGenerate.Result;

                // Проверяет существует ли уже такая заявка.
                var resultCheck = await CheckingRequest(typeParam, request.Number);

                // Если такая заявка уже существует, то повторно пойдет генерить номер заявки.
                if (Convert.ToBoolean(resultCheck)) {
                    Task<string> repeatTask = new Task<string>(() => RandomDataService.GenerateRandomNumber());
                    repeatTask.Start();
                    generateNumber = repeatTask.Result;
                }

                request.Number = generateNumber;

                // Добавляет новую заявку в список заявок со статусом "Новая".
                await db.QueryAsync($"INSERT INTO Requests VALUES ('{request.Number}', {request.Count}, '{request.Measure}', " +
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

        /// <summary>
        /// Метод проверяет существование заявки.
        /// </summary>
        /// <param name="typeParam"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<string> CheckingRequest(string typeParam, string param) {
            using (var db = new SqlConnection(_connectionString)) {
                var parameters = new DynamicParameters();
                parameters.Add("@type_param", typeParam, DbType.String);
                parameters.Add("@param", param, DbType.String);

                var checkCard = await db.QueryAsync<string>("dbo.sp_Checking",
                    commandType: CommandType.StoredProcedure,
                    param: parameters);

                return checkCard.ToArray()[0];
            }
        }
    }
}
