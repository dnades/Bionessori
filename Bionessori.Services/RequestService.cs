using Bionessori.Core;
using Bionessori.Core.Constants;
using Bionessori.Core.Data;
using Bionessori.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bionessori.Services {
    /// <summary>
    /// Сервис реализует методы по работы с заявками.
    /// </summary>
    public class RequestService : BaseRequest {
        ApplicationDbContext _db;
        public RequestService(ApplicationDbContext db) {
            _db = db;
        }

        /// <summary>
        /// Метод получает список заявок на потребности.
        /// </summary>
        /// <returns></returns>
        public async override Task<IEnumerable> GetRequests() {
            return await _db.Requests.ToListAsync();
        }

        /// <summary>
        /// Метод реализует удаление заявки на потребности в закупках.
        /// </summary>
        /// <returns></returns>
        public async Task Delete(int number) {
            //using (var db = new SqlConnection(_connectionString)) {
            //    try {
            //        if (number == 0) {
            //            throw new ArgumentNullException();
            //        }

            //        await db.QueryAsync($"DELETE dbo.Requests WHERE number = {number}");
            //    }
            //    catch (ArgumentNullException ex) {
            //        throw new ArgumentNullException("Не указан номер заявки", ex.Message.ToString());
            //    }
            //    catch(Exception ex) {
            //        throw new Exception(ex.Message);
            //    }
            //}            
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод реализует изменение существующей заявки на потребности в закупках.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task Edit(Request request) {
            //try {
            //    var materialJson = JsonSerializer.Serialize<Request>(request);

            //    using (var db = new SqlConnection(_connectionString)) {                    
            //        // Сохраняет изменения заявки.
            //        await db.QueryAsync($"UPDATE dbo.Requests SET " +
            //            $"count = {request.Count}," +
            //            $"measure = '{request.Measure}'," +
            //            $"status = '{request.Status}'," +
            //            $"material_group = '{request.MaterialGroup}'," +
            //            $"material = '{materialJson}'");
            //    }
            //}
            //catch(Exception ex) {
            //    throw new Exception(ex.Message);
            //}
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод проверяет существование заявки.
        /// </summary>
        /// <param name="typeParam"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<string> CheckingRequest(string typeParam, int param) {           
            try {
                object checkRequest = null;
                if (typeParam == "request") {
                    checkRequest = await _db.Requests.Where(r => r.Number == param).FirstOrDefaultAsync();
                }                
                return (checkRequest == null ? false : true).ToString();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод создает новую заявку.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async override Task CreateRequest(object request) {
            try {
                string typeParam = "request";
                int generateNumber = 0;
                var objParse = JsonSerializer.Serialize(request);
                JObject jsonObject = JObject.Parse(objParse);

                // Выбирает материалы.
                var aMaterials = (JArray)jsonObject["Material"];
                var aMaterialValues = aMaterials.Values().ToList();

                // Выбирает группы.
                var aGroups = (JArray)jsonObject["MaterialGroup"];
                var aMaterialGroups = aGroups.Values().ToList();

                // Выбирает кол-во.
                var count = (JArray)jsonObject["Count"];
                var aCount = count.Values().ToList();

                // Выбирает ед.изм.
                var sMeasures = (JArray)jsonObject["Measure"];
                var aMeasures = sMeasures.Values().ToList();
                //var sMeasure = jsonObject["Measure"].ToString();

                // Генерит рандомный номер заявки.
                int RandomGenerate() {
                    return RandomDataService.GenerateRandomNumber();
                }

                generateNumber = RandomGenerate();
                
                // Проверяет, существует ли уже такая заявка.
                var resultCheck = await CheckingRequest(typeParam, generateNumber);

                // Если такая заявка уже существует, то повторно пойдет генерить номер заявки.
                if (Convert.ToBoolean(resultCheck)) {
                    generateNumber = RandomGenerate();
                }

                // Перебор материалов.
                int i = 0;
                foreach (var material in aMaterialValues) {
                    Request reqObject = new Request() {
                        Number = generateNumber,
                        Status = RequestStatus.REQ_STATUS_NEW
                    };

                    // Итеративно дополняет объект заявки.
                    reqObject.Material = material.ToString();
                    reqObject.MaterialGroup = aMaterialGroups[i].ToString();
                    reqObject.Count = Convert.ToInt32(aCount[i]);
                    reqObject.Measure = aMeasures[i].ToString();

                    // Итеративно сохраняет объект.
                    await _db.Requests.AddRangeAsync(reqObject);
                    i++;
                }
                // В итоге все добавленные объекты сохраняться разом. В БД каждая такая итерация сохранится новой строкой.
                await _db.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }
    }    
}
