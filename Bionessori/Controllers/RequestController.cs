using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Bionessori.Core;
using Bionessori.Core.Data;
using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Bionessori.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Bionessori.Controllers {
    [ApiController, Route("api/werehouse/request")]
    public class RequestController : ControllerBase {
        ApplicationDbContext _db;

        public RequestController(ApplicationDbContext db) {
            _db = db;
        }
        /// <summary>
        /// Метод получает список заявок на потребности.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("get-requests")]
        public async Task<IActionResult> GetRequests() {
            BaseRequest baseRequest = new RequestService(_db);
            return Ok(await baseRequest.GetRequests());
        }

        /// <summary>
        /// Метод создает новую заявку на потребность в материалах.
        /// </summary>
        /// <param name="werehouse"></param>
        /// <returns></returns>
        [HttpPost, Route("create-request")]
        public async Task<IActionResult> Create([FromBody] object request) {
            BaseRequest baseRequest = new RequestService(_db);
            await baseRequest.CreateRequest(request);

            return Ok("Заявка на потребность успешно создана");
        }      

        /// <summary>
        /// Метод сохраняет изменения в заявке.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("save-change-request")]
        public async Task<IActionResult> SaveChangeRequest([FromBody] Request request) {
            //await _request.Edit(request);

            return Ok("Заявка успешно изменена");
        }

        /// <summary>
        /// Метод удаляет заявку.
        /// </summary>
        [HttpPut, Route("delete-request")]
        public async Task<IActionResult> DeleteRequest([FromQuery] int number) {
            //await _request.Delete(number);

            return Ok("Заявка успешно удалена");
        }

        /// <summary>
        /// Метод получает список новых заявок.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("get-new-requests")]
        public async Task<IActionResult> GetDataNewRequests() {
            BaseRequest template = new RequestService(_db);
            var oRequests = await template.GetDynamicDataNewRequests();

            return Ok(oRequests);
        }

        /// <summary>
        /// Метод получает список заявок в работе.
        /// </summary>
        [HttpGet, Route("get-inwork-requests")]
        public async Task<IActionResult> GetDataInWorkRequests() {
            BaseRequest template = new RequestService(_db);
            var oRequests = await template.GetDynamicDataWorkRequests();

            return Ok(oRequests);
        }

        /// <summary>
        /// Метод получает заявки, которые ожидают подтверждения удаления.
        /// </summary>
        [HttpGet, Route("get-accept-delete-requests")]
        public async Task<IActionResult> GetAcceptDeleteRequests() {
            BaseRequest template = new RequestService(_db);
            var oRequests = await template.GetDynamicDataAcceptDeleteRequests();

            return Ok(oRequests);
        }

        /// <summary>
        /// Метод получает материалы, которые нужно пополнить.
        /// </summary>
        [HttpPost, Route("get-refill-materials")]
        public async Task<IActionResult> GetRefillMaterials() {
            BaseRequest template = new RequestService(_db);
            var oMaterials = await template.GetDynamicDataRefillMaterials();

            return Ok(oMaterials);
        }
    }      
}
