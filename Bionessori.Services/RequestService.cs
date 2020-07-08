using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Dapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
//using System.Text.Json;
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
        public async Task<object> GetRequests(Request request) {
            // Вызывает процедуру для выборки списка заявок.
            using (var db = new SqlConnection(_connectionString)) {
                var oRequests = await db.QueryAsync("sp_GetRequests");
                
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

                var materialJson = JsonSerializer.Serialize(request.Material);
                //var materialJson = JsonSerializer.Deserialize<Request>(json);

                // Добавляет новую заявку в список заявок со статусом "Новая".
                await db.QueryAsync($"INSERT INTO Requests VALUES ('{request.Number}', {request.Count}, '{request.Measure}', " +
                    $"'Новая', '{request.MaterialGroup}', '{materialJson}')");

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
        public async Task<string> Edit(Request request) {
            var materialJson = JsonSerializer.Serialize(request.Material);

            try {
                using (var db = new SqlConnection(_connectionString)) {                    
                    // Сохраняет изменения заявки.
                    await db.QueryAsync($"UPDATE Requests SET " +
                        $"count = {request.Count}," +
                        $"measure = '{request.Measure}'," +
                        $"status = '{request.Status}'," +
                        $"material_group = '{request.MaterialGroup}'," +
                        $"material = '{materialJson}'");

                    return "Заявка изменена.";
                }
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
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
