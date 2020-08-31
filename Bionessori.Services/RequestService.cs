using Bionessori.Core;
using Bionessori.Core.Constants;
using Bionessori.Core.Data;
using Bionessori.Core.Extensions;
using Bionessori.Models;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
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
            var oRequests = await _db.Requests.Select(r => new { r.Id, r.Status, r.Number }).Distinct().ToListAsync();
            return oRequests;
        }
        
        /// <summary>
        /// Метод реализует изменение существующей заявки на потребности в закупках.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async override Task Edit(object request) {
            try {
                var objParse = JsonSerializer.Serialize(request);
                JObject jsonObject = JObject.Parse(objParse);
                int reqNumber = int.Parse(jsonObject["Number"].ToString());   // Номер заявки.
                string typeParam = "request";

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

                // Проверяет, существует ли уже такая заявка.
                var resultCheck = await CheckingRequest(typeParam, reqNumber);

                // Если такой заявки нет, то ругается.
                if (!Convert.ToBoolean(resultCheck)) {
                    throw new ArgumentException();
                }

                // Удаляет заявку перед ее пересозданием.
                var oSelect = await _db.Requests.Where(r => r.Number == reqNumber).ToListAsync();
                _db.RemoveRange(oSelect);
                _db.SaveChanges();                

                // Перебор материалов.
                int i = 0;
                aMaterialValues.ForEach(el => {
                    Request reqObject = new Request() {
                        Number = reqNumber,
                        Status = RequestStatus.REQ_STATUS_NEW
                    };

                    // Итеративно дополняет объект заявки.
                    reqObject.Material = el.ToString();
                    reqObject.MaterialGroup = aMaterialGroups[i].ToString();
                    reqObject.Count = Convert.ToInt32(aCount[i]);
                    reqObject.Measure = aMeasures[i].ToString();

                    // Итеративно сохраняет объект.
                    _db.Requests.AddRange(reqObject);
                    i++;
                });                
                await _db.SaveChangesAsync();
            }
            catch (ArgumentException ex) {
                throw new ArgumentException("Такой заявки не существует", ex.Message.ToString());
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
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

        /// <summary>
        /// Метод получает заявки со статусом требует пополнения.
        /// </summary>
        /// <returns>Список материалов.</returns>
        public async override Task<IEnumerable> GetDynamicDataRefillMaterials() {
            try {
                var oRefillMaterials = await _db.Requests.Where(r => r.Status == RequestStatus.REQ_STATUS_NEED_REFILL).ToListAsync();

                return oRefillMaterials;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
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

        /// <summary>
        /// Метод получает заявки ожидающие подтверждения удаления.
        /// </summary>
        /// <returns></returns>
        public async override Task<IEnumerable> GetDynamicDataAcceptDeleteRequests() {
            try {
                return await _db.Requests.Where(r => r.Status == RequestStatus.REQ_STATUS_NEED_ACCEPT_DELETE).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод полуает заявки со статусом требует сопоставления.
        /// </summary>
        /// <returns></returns>
        public async override Task<IEnumerable> GetDynamicDataMappingMaterials() {
            try {
                return await _db.Requests.Where(r => r.Status == RequestStatus.REQ_STATUS_NEED_MAPPING).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод получает заявку для редактирования.
        /// </summary>
        /// <returns>Данные заявки.</returns>
        public async override Task<object> GetRequestForEdit(int number) {
            try {
                if (number == 0) {
                    throw new ArgumentNullException();
                }
                var oSelect = await _db.Requests.Where(r => r.Number == number).ToListAsync();
                List<string> aMaterials = new List<string>();
                List<string> aGroups = new List<string>();
                List<int> aCounts = new List<int>();
                List<string> aMeasures = new List<string>();

                foreach (var el in oSelect) {
                    aMaterials.Add(el.Material);
                    aGroups.Add(el.MaterialGroup);
                    aCounts.Add(el.Count);
                    aMeasures.Add(el.Measure);
                }

                // Записывает данные заявки в анонимный объект.
                var oRequestData = new { 
                    id = oSelect[0].Id,
                    numberRequest = oSelect[0].Number,  
                    aMaterials, 
                    aGroups, 
                    aCounts, 
                    aMeasures 
                };                

                return oRequestData;
            }
            catch (ArgumentNullException ex) {
                throw new ArgumentNullException("№ заявки не передан", ex.Message.ToString());
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод переводит заявку в статус "Ожидает подтверждения удаления".
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public async override Task PostDeleteRequest(int number) {
            try {
                string typeParam = "request";
                if (number == 0) {
                    throw new ArgumentNullException();
                }

                // Проверяет, существует ли уже такая заявка.
                var resultCheck = await CheckingRequest(typeParam, number);

                // Если такой заявки нет, то ругается.
                if (!Convert.ToBoolean(resultCheck)) {
                    throw new ArgumentOutOfRangeException();
                }

                List<Request> oRequest = await _db.Requests.Where(r => r.Number == number).ToListAsync();
                oRequest.ForEach(el => {
                    el.Status = RequestStatus.REQ_STATUS_NEED_ACCEPT_DELETE;
                });
                _db.Requests.UpdateRange(oRequest);
                _db.SaveChanges();
            }
            catch (ArgumentNullException ex) {
                throw new ArgumentNullException("№ заявки не передан", ex.Message.ToString());
            }
            catch (ArgumentOutOfRangeException ex) {
                throw new ArgumentOutOfRangeException("Такой заявки не существует", ex.Message.ToString());
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }
    }    
}
