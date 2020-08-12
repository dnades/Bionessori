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
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
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
        public async Task<object> GetRequests() {
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
        public async Task Create(Request request) {
            using (var db = new SqlConnection(_connectionString)) {
                string typeParam = "request";
                int generateNumber = 0;

                // Генерит рандомный номер заявки.
                int RandomGenerate() {
                    return RandomDataService.GenerateRandomNumber();
                }                
                
                generateNumber = RandomGenerate();

                // Проверяет существует ли уже такая заявка.
                var resultCheck = await CheckingRequest(typeParam, request.Number);

                // Если такая заявка уже существует, то повторно пойдет генерить номер заявки.
                if (Convert.ToBoolean(resultCheck)) {
                    generateNumber = RandomGenerate();
                }

                request.Number = generateNumber;
                                     
                string materialjson = JsonSerializer.Serialize<Request>(request);

                // Создает новую заявку всегда в статусе "Новая".
                await db.QueryAsync($"INSERT INTO dbo.Requests (number, status, count, measure, material_group, material) " +
                    $"VALUES ({request.Number}, 'Новая', {request.Count}, '{request.Measure}', '{request.MaterialGroup}', '{materialjson}')");
            }
        }

        /// <summary>
        /// Метод реализует удаление заявки на потребности в закупках.
        /// </summary>
        /// <returns></returns>
        public async Task Delete(int number) {
            using (var db = new SqlConnection(_connectionString)) {
                try {
                    if (number == 0) {
                        throw new ArgumentNullException();
                    }

                    await db.QueryAsync($"DELETE dbo.Requests WHERE number = {number}");
                }
                catch (ArgumentNullException ex) {
                    throw new ArgumentNullException("Не указан номер заявки", ex.Message.ToString());
                }
                catch(Exception ex) {
                    throw new Exception(ex.Message);
                }
            }                
        }

        /// <summary>
        /// Метод реализует изменение существующей заявки на потребности в закупках.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task Edit(Request request) {            
            try {
                var materialJson = JsonSerializer.Serialize<Request>(request);

                using (var db = new SqlConnection(_connectionString)) {                    
                    // Сохраняет изменения заявки.
                    await db.QueryAsync($"UPDATE dbo.Requests SET " +
                        $"count = {request.Count}," +
                        $"measure = '{request.Measure}'," +
                        $"status = '{request.Status}'," +
                        $"material_group = '{request.MaterialGroup}'," +
                        $"material = '{materialJson}'");
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
        public async Task<string> CheckingRequest(string typeParam, int param) {
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
